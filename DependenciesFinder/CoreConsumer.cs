using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Octokit;

namespace DependenciesFinder
{
    public class CoreConsumer : Dependency
    {
        public CoreConsumer(string command, SearchCode searchCode, RepositoryContent repositoryContent)
            : base(command, searchCode, repositoryContent)
        {
        }

        public override IEnumerable<string> FoundLines =>
            Regex.Matches(RepositoryContent.Content, $"Consume\\({_command}.*")
            .Select(x => x.Value);
    }
}
