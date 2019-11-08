using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DependenciesFinder
{
    public class CoreCommands
    {
        private const string DLLLocation =
            "C:\\projects\\ssc-main\\packages\\ExtendHealth.Core.Commands\\lib\\net40\\ExtendHealth.Core.Commands.dll";

        public static IEnumerable<CoreCommandsType> GetAllPublicTypes()
        {
            var commandsAssembly = Assembly.LoadFrom(DLLLocation);

            return commandsAssembly
                   .GetTypes()
                   .Where(x => x.IsPublic)
                   .Select(x => new CoreCommandsType(x));
        }

        public static async Task<IEnumerable<CoreCommandsType>> GetAllPublicTypesThatSSCDependsOn()
        {
            var commandsAssembly = Assembly.LoadFrom(DLLLocation);

            var coreCommands = commandsAssembly
                   .GetTypes()
                   .Where(x => x.IsPublic)
                   .Select(x => new CoreCommandsType(x))
                   .ToList();

            await Task.WhenAll(coreCommands.Select(x => x.GetDependentRepositories()));

            return coreCommands
                .Where(x =>
                {
                    var deps = x.GetDependentRepositories().Result;

                    return deps.Any(x => x.FullName == "extend-health/ssc-main");
                });
        }
    }
}
