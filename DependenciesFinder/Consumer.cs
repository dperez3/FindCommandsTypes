using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Octokit;

namespace DependenciesFinder
{
    public class Consumer
    {
        private readonly string _commandRegex;
        private readonly RepositoryContent _repositoryContent;

        public Consumer(string consumedCommand, RepositoryContent repositoryContent)
        {
            _commandRegex = $"Consume\\({consumedCommand}.*";
            _repositoryContent = repositoryContent;
        }

        public string FileContents => _repositoryContent.Content;
        public IEnumerable<string> ConsumerLines =>
            Regex.Matches(FileContents, _commandRegex)
                 .Select(x => x.Value);
    }
}
