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

        public static async Task<IEnumerable<Consumer>> GetConsumersForCommand(string commandName)
        {
            var res = await _github.Search.SearchCode(new SearchCodeRequest(commandName)
            {
                Repos = new RepositoryCollection
                {
                    "extend-health/service-bus-consumers"
                }
            });

            var fileContents = res.Items
                                  .Select(x => GetFileContents(x.Repository.Id, x.Path).Result)
                                  .Where(x => x.Content.Contains($"Consume({commandName}"));
            
            var consumers = fileContents
                .Select(x => new Consumer(commandName, x));

            return consumers;
        }

        public static async Task<IEnumerable<SearchCode>> GetCodeContainingTerm(string term)
        {
            var res = await _github.Search.SearchCode(new SearchCodeRequest(term));
            
            return res.Items;
        }

        public static async Task<bool> RepoContainsFileThatContainAllTerms(SearchCode code, string[] terms)
        {
            return await DoesFileContainAllTerms(code.Repository.Id, code.Path, terms);
        }

        public static async Task<bool> DoesFileContainAllTerms(long repositoryId,
                                                               string path,
                                                               params string[] terms)
        {
            var repoContent = await GetFileContents(repositoryId, path);
            
            return terms
                .All(x => repoContent.Content.Contains(x));
        }

        public static async Task<RepositoryContent> GetFileContents(long repositoryId, string path)
        {
            var res = await _github.Repository.Content.GetAllContentsByRef(repositoryId, path, "master");
            
            return res.Single();
        }
    }
}
