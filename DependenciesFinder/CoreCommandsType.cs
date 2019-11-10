using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Octokit;

namespace DependenciesFinder
{
    public class CoreCommandsType
    {
        private const string SSCRepoName = "extend-health/ssc-main";
        private const string CCRepoName = "extend-health/service-bus-consumers";
        private static readonly GitHubService _githubService = new GitHubService();
        
        internal CoreCommandsType(Type type, IEnumerable<SearchCode> searchCodes) : this(type.Name, type.FullName, searchCodes) { }
        internal CoreCommandsType(string name, string fullName, IEnumerable<SearchCode> searchCodes)
        {
            Name = name;
            FullName = fullName;
            SearchCodes = searchCodes;
        }
        
        public IEnumerable<SearchCode> SearchCodes { get; }

        public string Namespace => "ExtendHealth.Core.Commands";
        public string Name { get; }
        public string FullName { get; }

        public bool FoundInSSCCode => SearchCodes.Any(x => x.Repository.FullName == SSCRepoName);

        public async Task<bool> CheckSSCDependencyAsync()
        {
            if (!FoundInSSCCode)
                return false;

            var dependencies = await GetDependentRepositoriesAsync(new []{ SSCRepoName });

            return dependencies.Any();
        }

        public async Task<IEnumerable<Dependency>> GetDependentRepositoriesAsync(IEnumerable<string> repositories) =>
            await _githubService.FindDependenciesAsync(repositories, new [] { Name, Namespace });

        public async Task<IEnumerable<CoreConsumer>> GetConsumersAsync()
        {
            var deps = await GetDependentRepositoriesAsync(new[] { CCRepoName });

            //return deps.Cast<CoreConsumer>(); // TODO: Use actual CC instance
            throw new NotImplementedException();
        }
    }
}
