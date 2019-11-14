using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Octokit;

namespace DependenciesFinder
{
    public class CoreCommandsType
    {
        private static readonly GitHubService _githubService = new GitHubService();

        internal CoreCommandsType(Type type, IEnumerable<SearchCode> searchCodes) : this(type.Name, type.FullName, searchCodes) { }
        internal CoreCommandsType(string name, string fullName, IEnumerable<SearchCode> searchCodes)
        {
            Name = name;
            FullName = fullName;
            SearchCodes = searchCodes;
        }
        
        public IEnumerable<SearchCode> SearchCodes { get; }

        public string Namespace => Defaults.CommandsAssemblyName;
        public string Name { get; }
        public string FullName { get; }

        public bool FoundInSSCCode => SearchCodes.Any(x => x.Repository.FullName == Defaults.SSCRepoName);

        public bool IsDeadCode
        {
            get {
                var isOnlySomeRepos = SearchCodes.All(x =>
                                                     x.Repository.FullName == Defaults.CCommandsRepoName
                                                     || x.Repository.FullName == Defaults.CCRepoName
                                                     || x.Repository.FullName == Defaults.SSCRepoName);

                var onlyTwoReposMax = SearchCodes
                                   .Select(x => x.Repository)
                                   .Distinct(new RepositoryComparer())
                                   .Count() <= 2;

                return isOnlySomeRepos && onlyTwoReposMax;
            }
        }

        public async Task<bool> IsExclusivelyUsedBySSCAsync()
        {
            if (!FoundInSSCCode)
                return false;

            var dependencies = await GetDependentRepositoriesAsync();

            return dependencies.All(x => x.IsSSC || x.IsCoreConsumers || x.IsCoreCommands);
        }

        public async Task<IEnumerable<Dependency>> GetDependentRepositoriesAsync() =>
            await _githubService.FindDependenciesAsync(Defaults.Repositories, new [] { Name, Namespace });
    }
}
