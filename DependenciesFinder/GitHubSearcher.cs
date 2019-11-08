using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Octokit;

namespace DependenciesFinder
{
    internal static class GitHubSearcher
    {
        static GitHubClient _github = null;

        static GitHubSearcher()
        {
            var ghe = new Uri("https://github.mktp.io/");
            _github = new GitHubClient(new ProductHeaderValue("domipe"), ghe);
        }

        internal static async Task<IEnumerable<Repository>> GetRepositoriesContainingText(string text)
        {
            var res = await _github.Search.SearchCode(new SearchCodeRequest(text));

            return res
                   .Items
                   .Select(x => x.Repository);
        }

        /*internal static async Task<IEnumerable<Repository>> GetRepositoriesContainingText(IEnumerable<string> text)
        {
            
        }*/
    }
}
