using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Octokit;

namespace DependenciesFinder
{
    public class CoreCommandsService
    {
        private static readonly IMemoryCache _cache = new MemoryCache(new MemoryCacheOptions());
        private readonly GitHubService _gitHubService;

        public CoreCommandsService(GitHubService gitHubService)
        {
            _gitHubService = gitHubService;
        }

        public async Task<IEnumerable<CoreCommandsType>> GetAllCommandsUsedBySSC(string commandsDllPath, Func<Type, bool> typeFilter, IEnumerable<string> repositories)
        {
            var types = getAllPublicTypes(commandsDllPath)
                .Where(typeFilter);

            var cacheKey = getCacheKey(types, repositories);
            if (_cache.TryGetValue(cacheKey, out var cacheEntry))
                return cacheEntry as IEnumerable<CoreCommandsType>;
            
            var coreCommandsTypes = await Task.WhenAll(
                        types.Select(
                                           async x => new CoreCommandsType(x, await _gitHubService.FindCSharpCodeAsync(repositories, x.Name))));

            var results = coreCommandsTypes.Where(x => x.FoundInSSCCode);
            _cache.Set(cacheKey, results);
            return results;
        }

        public async Task<IEnumerable<CoreCommandsType>> GetAllCommandsUsedBySSC(IEnumerable<string> commandNames,
                                                                                 IEnumerable<string> repositories)
        {
            var cacheKey = getCacheKey(commandNames, repositories);
            if (_cache.TryGetValue(cacheKey, out var cacheEntry))
                return cacheEntry as IEnumerable<CoreCommandsType>;
            
            var coreCommandsTypes = await Task.WhenAll(
                        commandNames.Select(
                                           async x => new CoreCommandsType(x, x, await _gitHubService.FindCSharpCodeAsync(repositories, x))));

            var results = coreCommandsTypes.Where(x => x.FoundInSSCCode);
            _cache.Set(cacheKey, results);
            return results;
        }

        private static IEnumerable<Type> getAllPublicTypes(string commandsDllPath)
        {
            return Assembly.LoadFrom(commandsDllPath)
                           .GetTypes()
                           .Where(x => x.IsPublic);
        }
        
        private static string getCacheKey(IEnumerable<Type> types, IEnumerable<string> repositories) =>
            Tuple.Create(types, repositories).GetHashCode().ToString();
        
        private static string getCacheKey(IEnumerable<string> commandNames, IEnumerable<string> repositories) =>
            Tuple.Create(commandNames, repositories).GetHashCode().ToString();
    }
}
