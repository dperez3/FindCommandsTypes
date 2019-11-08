using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Octokit;

namespace DependenciesFinder
{
    public class CoreCommandsType
    {
        private readonly Type _type;
        private IEnumerable<Repository> _repos;

        public CoreCommandsType(Type type) => _type = type;

        public string Name => _type.Name;
        public string FullName => _type.FullName;

        public async Task<IEnumerable<Repository>> GetDependentRepositories()
        {
            _repos ??= await getRepositoriesContainingName();

            return _repos.Where(isRepoTrulyDependent);
        }

        private async Task<IEnumerable<Repository>> getRepositoriesContainingName() =>
            await GitHubSearcher.GetRepositoriesContainingText(Name);

        private bool isRepoTrulyDependent(Repository repository)
        {
            return true;
        }
    }
}
