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
        private readonly string _commandsDllPath;
        
        public CoreCommandsService(string commandsDllPath) { _commandsDllPath = commandsDllPath; }

        public async Task<IEnumerable<CoreCommandsType>> GetAllCommandsUsedBySSC()
        {
            var commandsUsedBySsc = new ConcurrentBag<CoreCommandsType>();
            await Task.WhenAll(getAllPublicTypes(_commandsDllPath).Select(async x =>
            {
                var coreCommandsType = new CoreCommandsType(x);

                var sscCode =
                    await GitHubSearcher.FindCodeContainingAllTerms(
                        new []{ coreCommandsType.Name, coreCommandsType.Namespace }, 
                        "extend-health/ssc-main");

                if (sscCode.Any())
                    commandsUsedBySsc.Add(coreCommandsType);
            }));

            await Task.WhenAll(
                loadDependentCode(commandsUsedBySsc),
                loadConsumers(commandsUsedBySsc)
            );

            return commandsUsedBySsc;
        }

        private async Task loadDependentCode(IEnumerable<CoreCommandsType> commands)
        {
            await Task.WhenAll(commands.Select(loadDependentCode));
        }

        private async Task loadDependentCode(CoreCommandsType command)
        {
            command.DependentCode = await GitHubSearcher.FindCodeContainingAllTerms(new []{ command.Name, command.Namespace });
        }
        
        private async Task loadConsumers(IEnumerable<CoreCommandsType> commands)
        {
            await Task.WhenAll(commands.Select(loadConsumers));
        }

        private async Task loadConsumers(CoreCommandsType command) { command.Consumers = await GitHubSearcher.GetConsumersForCommand(command.Name); }

        private static IEnumerable<Type> getAllPublicTypes(string commandsDllPath)
        {
            return Assembly.LoadFrom(commandsDllPath)
                             .GetTypes()
                             .Where(x => x.IsPublic);
        }
    }
}
