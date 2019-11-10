using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Octokit;

namespace DependenciesFinder
{
    public class CoreCommandsService
    {
        private readonly GitHubService _gitHubService;
        
        public CoreCommandsService(GitHubService gitHubService)
        {
            _gitHubService = gitHubService;
        }

        public async Task<IEnumerable<CoreCommandsType>> GetAllCommandsUsedBySSC(string commandsDllPath, Func<Type, bool> filter, IEnumerable<string> repositories)
        {
            var types = getAllPublicTypes(commandsDllPath)
                .Where(filter);
            var coreCommandsTypes = await Task.WhenAll(
                        types.Select(
                                           async x => new CoreCommandsType(x, await _gitHubService.FindCSharpCodeAsync(repositories, x.Name))));
            
            return coreCommandsTypes.Where(x => x.FoundInSSCCode);
        }

        public async Task<IEnumerable<CoreCommandsType>> GetAllCommandsUsedBySSC(IEnumerable<string> commandNames,
                                                                                 IEnumerable<string> repositories)
        {
            var coreCommandsTypes = await Task.WhenAll(
                        commandNames.Select(
                                           async x => new CoreCommandsType(x, x, await _gitHubService.FindCSharpCodeAsync(repositories, x))));
            
            return coreCommandsTypes.Where(x => x.FoundInSSCCode);
        }

        private static IEnumerable<Type> getAllPublicTypes(string commandsDllPath)
        {
            return Assembly.LoadFrom(commandsDllPath)
                           .GetTypes()
                           .Where(x => x.IsPublic);
        }
    }
}
