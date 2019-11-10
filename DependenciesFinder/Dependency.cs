using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Octokit;

namespace DependenciesFinder
{
    public class Dependency
    {
        protected readonly string _command;

        public Dependency(string command, SearchCode searchCode, RepositoryContent repositoryContent)
        {
            _command = command;
            SearchCode = searchCode;
            RepositoryContent = repositoryContent;
        }

        public SearchCode SearchCode { get; }
        public RepositoryContent RepositoryContent { get; }

        public virtual IEnumerable<string> FoundLines =>
            Regex.Matches(RepositoryContent.Content, $".*{_command}.*")
                 .Select(x => x.Value);

        public bool IsSSC => SearchCode.Repository.FullName == Defaults.SSCRepoName;
        public bool IsCoreConsumers => SearchCode.Repository.FullName == Defaults.CCRepoName;

        public override string ToString()
        {
            return $"{SearchCode.Repository.FullName} / {SearchCode.Path}";
        }
    }
}
