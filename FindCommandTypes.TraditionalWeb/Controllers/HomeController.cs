using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DependenciesFinder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FindCommandTypes.TraditionalWeb.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace FindCommandTypes.TraditionalWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CoreCommandsService _coreCommandsService;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _coreCommandsService = new CoreCommandsService(new GitHubService());
        }

        public async Task<IActionResult> Index()
        {
            var vms = await getCommandsUsedBySscAsync();
            
            return View(vms.ToList());
        }

        [HttpGet("dead-commands")]
        public async Task<IActionResult> GetDeadCommands()
        {
            var deadCommands = await getDeadCommandsAsync();

            return View(deadCommands.ToList());
        }

        [HttpGet("jira")]
        public async Task<IActionResult> GetJiraSubTasks()
        {
            var sscExclusiveCommands =
                (await getCommandsUsedBySscAsync())
                .Where(x => x.IsExclusivelyUsedBySSC);

            var jiraTasks = sscExclusiveCommands
                .Select(getJiraTask);

            return View(jiraTasks.ToList());
        }

        private static string getJiraTask(CoreCommandVM command)
        {
            return
                $"- Replace {command.Name} / cfield:\"Assigned Team:@inherit\" / description:\"h1. {command.Name}\n# Confirm this command fits the Req/Resp model.\n ## If it *does NOT*, close this task.\n ## If it *does*, continue to move this out of Core Consumers and into the SSC BFF.\"";
        }

        private async Task<IEnumerable<CoreCommandVM>> getCommandsUsedBySscAsync()
        {
            var res = await _coreCommandsService.GetAllCommandsUsedBySSCAsync();

            var vms =
                (await Task.WhenAll(res.Select(CoreCommandVM.fromDependencyAsync)))
                .OrderByDescending(x => x.IsExclusivelyUsedBySSC)
                .ThenBy(x => x.Dependencies.Count())
                .ThenBy(x => x.Name);

            return vms;
        }

        private async Task<IEnumerable<CoreCommandVM>> getDeadCommandsAsync()
        {
            var res = await _coreCommandsService.GetDeadCommandsAsync();

            var vms =
                (await Task.WhenAll(res.Select(CoreCommandVM.fromDependencyAsync)))
                .OrderBy(x => x.Dependencies.Count())
                .ThenBy(x => x.Name);

            return vms;
        }

        public IActionResult Privacy() { return View(); }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
