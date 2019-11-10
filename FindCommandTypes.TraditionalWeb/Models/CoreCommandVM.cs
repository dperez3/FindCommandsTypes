using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DependenciesFinder;
using Octokit;

namespace FindCommandTypes.TraditionalWeb.Models
{
    public class CoreCommandVM
    {
        private CoreCommandVM() { }

        public string Namespace { get; private set; }
        public string Name { get; private set; }
        public string FullName { get; private set; }
        public IEnumerable<Dependency> Dependencies { get; private set; }

        public static async Task<CoreCommandVM> fromDependencyAsync(CoreCommandsType command)
        {
            return new CoreCommandVM
            {
                Namespace = command.Namespace,
                Name = command.Name,
                FullName = command.FullName,
                Dependencies = await command.GetDependentRepositoriesAsync()
            };
        }
    }
}
