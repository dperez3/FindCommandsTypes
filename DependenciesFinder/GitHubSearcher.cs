using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Octokit;

namespace DependenciesFinder
{
    public static class GitHubSearcher
    {
        static GitHubClient _github = null;

        static GitHubSearcher()
        {
            var ghe = new Uri("https://github.mktp.io/");
            _github = new GitHubClient(new ProductHeaderValue("domipe"), ghe);
        }

        /*public static async Task<IEnumerable<Repository>> GetRepositoriesContainingTermsInSameFile(params string[] terms)
        {
            var codeContainingFirstTerm = await GetCodeContainingTerm(terms[0]);
            
            

            return codeContainingFirstTerm
                .Where(x => x.);
        }*/

        public static async Task<IEnumerable<string>> GetConsumersForCommand(string commandName)
        {
            var res = await _github.Search.SearchCode(new SearchCodeRequest(commandName)
            {
                Repos = new RepositoryCollection()
                {
                    "extend-health/service-bus-consumers"
                },
                
            });

            var fileContents = res.Items
                                  .Select(x => getFileContents(x.Repository.Id, x.Path).Result)
                                  .Where(x => x.Contains($"Consume({commandName}"));

            var regex = $"Consume\\({commandName}(.|\\n)*";
            var consumers = fileContents
                .Select(x => Regex.Matches(x, regex))
                .SelectMany(x => x)
                .Select(x => x.Value);

            return consumers;
        }

        public static async Task<IEnumerable<SearchCode>> GetCodeContainingTerm(string term)
        {
            var res = await _github.Search.SearchCode(new SearchCodeRequest(term));
            
            return res.Items;
        }

        private static async Task<bool> repoContainsFileThatContainAllTerms(SearchCode code, string[] terms)
        {
            return await doesFileContainAllTerms(code.Repository.Id, code.Path, terms);
        }

        private static async Task<bool> doesFileContainAllTerms(long repositoryId,
                                                               string path,
                                                               string[] terms)
        {
            var fileContents = await getFileContents(repositoryId, path);
            
            return terms
                .All(x => fileContents.Contains(x));
        }

        private static async Task<string> getFileContents(long repositoryId, string path)
        {
            var res = await _github.Repository.Content.GetAllContentsByRef(repositoryId, path, "master");

            return res.First().Content;
        }
    }
}
