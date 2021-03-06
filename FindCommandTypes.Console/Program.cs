﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DependenciesFinder;
using FindCommandTypes.Console.Writers;

namespace FindCommandTypes.Console
{
    class Program
    {
        private static readonly GitHubService _github;
        private static readonly CoreCommandsService _coreCommandsService;
        private static readonly IWriter _writer;

        static Program()
        {
            _github = new GitHubService();
            _coreCommandsService = new CoreCommandsService(_github);
            
            //_writer = new ConsoleWriter();
            _writer = new DebugWriter();
        }

        static async Task Main(string[] args)
        {
            try
            {
                var seansCommands = await _coreCommandsService.GetAllCommandsUsedBySSCAsync(Defaults.SeansCommands,
                                                                                       Defaults.Repositories);
                var dllCommands = await _coreCommandsService.GetAllCommandsUsedBySSCAsync();

                await _writer.WriteComparisonAsync(seansCommands.Where(x => dllCommands.All(y => y.Name != x.Name)),
                        dllCommands.Where(x => seansCommands.All(y => y.Name != x.Name)));
            
                await _writer.WriteAsync(dllCommands);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.ToString());
                throw;
            }
        }
    }
}
