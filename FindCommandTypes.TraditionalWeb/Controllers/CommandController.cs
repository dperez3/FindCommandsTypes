using System.Linq;
using System.Threading.Tasks;
using DependenciesFinder;
using FindCommandTypes.TraditionalWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace FindCommandTypes.TraditionalWeb.Controllers
{
    [Route("command")]
    public class CommandController : Controller
    {
        private readonly CoreCommandsService _coreCommandsService;

        public CommandController() { _coreCommandsService = new CoreCommandsService(new GitHubService()); }

        // GET
        [HttpGet("{commandName}")]
        public async Task<IActionResult> Index([FromRoute] string commandName)
        {
            var res = await _coreCommandsService
                          .GetAllCommandsAsync();

            var found = res.SingleOrDefault(x => x.Name == commandName);

            if (found == null)
                return NotFound();
            
            return View(await CoreCommandVM.fromDependencyAsync(found));
        }
    }
}
