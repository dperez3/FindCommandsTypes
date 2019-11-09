using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DependenciesFinder
{
    public class CoreCommandsService
    {
        private const string DLLLocation =
            "C:\\projects\\ssc-main\\packages\\ExtendHealth.Core.Commands\\lib\\net40\\ExtendHealth.Core.Commands.dll";

        public static async Task<IEnumerable<CoreCommandsType>> GetAllPublicTypesThatSSCDependsOn()
        {
            return await Task.WhenAll(
                                   GetAllPublicTypes()
                                       .Select(async x => await CoreCommandsType.Create(x)));
        }

        public static async IAsyncEnumerable<CoreCommandsType> GetAllPublicTypesThatSSCDependsOnStream()
        {
            foreach (var type in GetAllPublicTypes())
            {
                var coreCommandsType = await CoreCommandsType.Create(type);
                if (coreCommandsType.IsSSCDependency)
                    yield return coreCommandsType;
            }
            
        }

        public static IEnumerable<Type> GetAllPublicTypes()
        {
            var commandsAssembly = Assembly.LoadFrom(DLLLocation);

            return commandsAssembly
                             .GetTypes()
                             .Where(x => x.IsPublic);
        }
    }
}
