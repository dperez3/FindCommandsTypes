using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using DependenciesFinder;

namespace FindCommandTypes.Console.Writers
{
    public class DebugWriter : IWriter
    {
        public async Task WriteAsync(IEnumerable<CoreCommandsType> commands)
        {
            foreach (var command in commands)
            {
                Debug.WriteLine(command.FullName ?? command.Name);
                Debug.Indent();
                foreach (var dependency in await command.GetDependentRepositoriesAsync())
                {
                    print(dependency);
                }
                Debug.Unindent();
            }
        }

        public async Task WriteComparisonAsync(
            IEnumerable<CoreCommandsType> seansCommandsNotFoundInDLL,
            IEnumerable<CoreCommandsType> dllCommandsNotFoundInSeansList)
        {
            Debug.WriteLine("Seans commands not found in DLL------------------");
            Debug.Indent();
            await WriteAsync(seansCommandsNotFoundInDLL);
            Debug.Unindent();
            
            Debug.WriteLine("DLL commands not found in Seans List------------------");
            Debug.Indent();
            await WriteAsync(dllCommandsNotFoundInSeansList);
            Debug.Unindent();
        }

        private static void print(Dependency dependency)
        {
            Debug.WriteLine(dependency.SearchCode.Repository.FullName);
            Debug.Indent();
            if (dependency.SearchCode.Repository.FullName == "extend-health/service-bus-consumers")
            {
                foreach (var line in dependency.FoundLines)
                {
                    Debug.WriteLine(line.Trim());
                }
            }
            Debug.Unindent();
        }
    }
}
