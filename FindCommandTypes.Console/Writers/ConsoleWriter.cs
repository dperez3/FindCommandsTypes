using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DependenciesFinder;

namespace FindCommandTypes.Console.Writers
{
    public class ConsoleWriter : IWriter
    {
        public async Task WriteAsync(IEnumerable<CoreCommandsType> commands)
        {
            foreach (var command in commands)
            {
                System.Console.ForegroundColor = ConsoleColor.Blue;
                System.Console.WriteLine(command.FullName ?? command.Name);
                foreach (var dependency in await command.GetDependentRepositoriesAsync())
                {
                    print(dependency);
                }
            }
        }

        public async Task WriteComparisonAsync(
            IEnumerable<CoreCommandsType> seansCommandsNotFoundInDLL,
            IEnumerable<CoreCommandsType> dllCommandsNotFoundInSeansList)
        {
            System.Console.ForegroundColor = ConsoleColor.Magenta;
            System.Console.WriteLine("Seans commands not found in DLL------------------");
            await WriteAsync(seansCommandsNotFoundInDLL);
            System.Console.ResetColor();

            System.Console.ForegroundColor = ConsoleColor.DarkMagenta;
            System.Console.WriteLine("DLL commands not found in Seans List------------------");
            await WriteAsync(dllCommandsNotFoundInSeansList);
            System.Console.ResetColor();
        }

        private static void print(Dependency dependency)
        {
            System.Console.ForegroundColor = ConsoleColor.Cyan;
            System.Console.WriteLine(dependency.SearchCode.Repository.FullName);
            if (dependency.SearchCode.Repository.FullName == "extend-health/service-bus-consumers")
            {
                System.Console.ForegroundColor = ConsoleColor.Gray;
                foreach (var line in dependency.FoundLines)
                {
                    System.Console.WriteLine(line.Trim());
                }
            }

            System.Console.ResetColor();
        }
    }
}
