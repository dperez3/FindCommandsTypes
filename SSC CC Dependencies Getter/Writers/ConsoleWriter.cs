using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DependenciesFinder;

namespace SSC_CC_Dependencies_Getter.Writers
{
    public class ConsoleWriter : IWriter
    {
        public async Task WriteAsync(IEnumerable<string> repositories, IEnumerable<CoreCommandsType> commands)
        {
            foreach (var command in commands)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(command.FullName ?? command.Name);
                foreach (var dependency in await command.GetDependentRepositoriesAsync(repositories))
                {
                    print(dependency);
                }
            }
        }

        public async Task WriteComparisonAsync(
            IEnumerable<string> repositories,
            IEnumerable<CoreCommandsType> seansCommandsNotFoundInDLL,
            IEnumerable<CoreCommandsType> dllCommandsNotFoundInSeansList)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Seans commands not found in DLL------------------");
            await WriteAsync(repositories, seansCommandsNotFoundInDLL);
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("DLL commands not found in Seans List------------------");
            await WriteAsync(repositories, dllCommandsNotFoundInSeansList);
            Console.ResetColor();
        }

        private static void print(Dependency dependency)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(dependency.SearchCode.Repository.FullName);
            if (dependency.SearchCode.Repository.FullName == "extend-health/service-bus-consumers")
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                foreach (var line in dependency.FoundLines)
                {
                    Console.WriteLine(line.Trim());
                }
            }

            Console.ResetColor();
        }
    }
}
