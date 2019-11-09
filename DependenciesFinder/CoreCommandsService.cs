using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DependenciesFinder
{
    public class CoreCommandsService
    {
        public static async IAsyncEnumerable<CoreCommandsType> GetAllPublicTypesThatSSCDependsOnStream(string commandsDllPath)
        {
            foreach (var type in getAllPublicTypes(commandsDllPath))
            {
                var coreCommandsType = await CoreCommandsType.Create(type);
                if (coreCommandsType.IsSSCDependency)
                    yield return coreCommandsType;
            }
        }

        private static IEnumerable<Type> getAllPublicTypes(string commandsDllPath)
        {
            return Assembly.LoadFrom(commandsDllPath)
                             .GetTypes()
                             .Where(x => x.IsPublic);
        }
    }
}
