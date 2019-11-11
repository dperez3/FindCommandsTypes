using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DependenciesFinder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FindCommandTypes.TraditionalWeb.Models;

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
            var res = await _coreCommandsService.GetAllCommandsUsedBySSC(Defaults.CommandsDLLPath,
                                                                         x => x.Name.EndsWith("Command") || x.Name.EndsWith("Message"),
                                                                         Defaults.Repositories);
            var vms =
                    (await Task.WhenAll(res.Select(CoreCommandVM.fromDependencyAsync)))
                    .OrderByDescending(x => x.IsExclusivelyUsedBySSC)
                    .ThenBy(x => x.Dependencies.Count())
                    .ThenBy(x => x.Name);
            
            return View(vms.ToList());
        }

        public IActionResult Privacy() { return View(); }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
