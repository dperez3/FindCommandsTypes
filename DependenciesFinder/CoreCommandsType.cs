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

        public static async Task<CoreCommandsType> Create(Type type)
        {
            var instance = new CoreCommandsType(type);
            await instance.loadDependenciesAsync();

            return instance;
        }
        private CoreCommandsType(Type type) => _type = type;

        public string Name => _type.Name;
        public string FullName => _type.FullName;

        public IEnumerable<SearchCode> SearchCodes { get; private set; }
        public IEnumerable<Repository> DependentRepositories { get; private set; }
        public IEnumerable<Consumer> Consumers { get; private set; }

        public bool IsSSCDependency => DependentRepositories.Any(x => x.FullName == "extend-health/ssc-main");

        private async Task<IEnumerable<SearchCode>> getCodeContainingName() =>
            await GitHubSearcher.GetCodeContainingTerm(Name);

        private async Task loadDependenciesAsync()
        {
            SearchCodes = await GitHubSearcher.GetCodeContainingTerm(Name);

            await Task.WhenAll(
                loadDependentRepositories(),
                loadConsumers()
            );
        }

        private async Task loadDependentRepositories()
        {
            DependentRepositories =
                (await getTrulyDependentCodes(SearchCodes))
                .Select(x => x.Repository)
                .Distinct(new RepositoryComparer());
        }

        private async Task loadConsumers()
        {
            Consumers = await GitHubSearcher.GetConsumersForCommand(Name);
        }
        
        private async Task<IEnumerable<SearchCode>> getTrulyDependentCodes(IEnumerable<SearchCode> searchCodes)
        {
            var res = await Task.WhenAll(searchCodes.Select(async x => new
            {
                x,
                isDependent = await isCodeTrulyDependent(x)
            }));

            return res.Where(x => x.isDependent).Select(x => x.x);
        }

        private async Task<bool> isCodeTrulyDependent(SearchCode searchCode)
        {
            return await GitHubSearcher.DoesFileContainAllTerms(searchCode.Repository.Id, searchCode.Path, Name, "ExtendHealth.Core.Commands");
        }
    }
}
