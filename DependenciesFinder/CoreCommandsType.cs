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
        private IEnumerable<SearchCode> _code;

        public CoreCommandsType(Type type) => _type = type;

        public string Name => _type.Name;
        public string FullName => _type.FullName;

        public async Task<IEnumerable<Repository>> GetDependentRepositories()
        {
            _code ??= await getCodeContainingName();

            return _code.Select(x => x.Repository).Where(isRepoTrulyDependent);
        }

        private async Task<IEnumerable<SearchCode>> getCodeContainingName() =>
            await GitHubSearcher.GetCodeContainingTerm(Name);

        private bool isRepoTrulyDependent(Repository repository)
        {
            return true;
        }
    }
}
