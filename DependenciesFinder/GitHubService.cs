using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Octokit;

namespace DependenciesFinder
{
    public class GitHubService
    {
        private const string GitHubEnterpriseURI = "https://github.mktp.io/";
        private const string Username = "domipe";
        private static readonly GitHubClient _github;

        private const string CSharpFile = ".cs";
        
        private static readonly IMemoryCache _cache;

        static GitHubService()
        {
            _github = new GitHubClient(new ProductHeaderValue(Username), new Uri(GitHubEnterpriseURI));
            _cache = new MemoryCache(new MemoryCacheOptions());
        }

        public async Task<IEnumerable<SearchCode>> FindCSharpCodeAsync(IEnumerable<string> repositories, string term)
        {
            var cacheKey = getCacheKey(term, repositories);

            if (_cache.TryGetValue(cacheKey, out var cacheEntry))
                return cacheEntry as IEnumerable<SearchCode>;
            
            var initialResp = await _github.Search.SearchCode(createSearchCodeRequest(term, repositories));
            
            var totalPages = getTotalPages(initialResp.TotalCount, initialResp.Items.Count);
            var subsequentRequests = new List<SearchCodeRequest>();
            for (var i = 1; i < totalPages; i++)
                subsequentRequests.Add(createSearchCodeRequest(term, repositories, i));
            
            var subsequentSearchCodes =
                await Task.WhenAll(
                    getSearchCodesAsync(subsequentRequests)
                );

            var results = initialResp.Items
                                     .Concat(subsequentSearchCodes.SelectMany(x => x));

            _cache.Set(cacheKey, results);
            return results;
        }

        public async Task<IEnumerable<Dependency>> FindDependenciesAsync(
            IEnumerable<string> repositories,
            IEnumerable<string> terms)
        {
            var searchCodesWithFirstTerm = await FindCSharpCodeAsync(repositories, terms.First());

            var contentWithAllTerms = await Task.WhenAll(
                searchCodesWithFirstTerm
                        .Select(async x => new Dependency(terms.First(), x, await getFileContents(x)))
            );

            return contentWithAllTerms;
        }

        private async Task<IEnumerable<SearchCode>> getSearchCodesAsync(
            IEnumerable<SearchCodeRequest> searchCodeRequests)
        {
            return (await Task.WhenAll(searchCodeRequests.Select(getSearchCodesAsync))).SelectMany(x => x);
        }
        
        private async Task<IEnumerable<SearchCode>> getSearchCodesAsync(SearchCodeRequest searchCodeRequest) =>
            (await _github.Search.SearchCode(searchCodeRequest)).Items;

        private async Task<RepositoryContent> getFileContents(SearchCode searchCode)
        {
            var res = await _github.Repository.Content.GetAllContentsByRef(searchCode.Repository.Id, searchCode.Path, "master");

            return res.Single();
        }

        private bool fileContainsAllTerms(RepositoryContent content, IEnumerable<string> terms) => terms.All(x => content.Content.Contains(x));

        private static SearchCodeRequest createSearchCodeRequest(string term, IEnumerable<string> filteredRepos = null, int page = 0)
        {
            var request = new SearchCodeRequest(term) { FileName = CSharpFile };

            var repos = createRepositoryCollection(filteredRepos);
            if (repos != null)
                request.Repos = repos;

            if (page != 0)
                request.Page = page;

            request.Extensions = new[] { "cs" };
            request.Language = Language.CSharp;

            return request;
        }

        private static RepositoryCollection createRepositoryCollection(IEnumerable<string> repos)
        {
            if (repos == null || !repos.Any())
                return null;
            
            var coll = new RepositoryCollection();
            foreach (var repo in repos)
                coll.Add(repo);

            return coll;
        }

        private static decimal getTotalPages(int totalCount, int itemCount) =>
            itemCount == 0 ? 0 : Math.Ceiling(totalCount / (decimal)itemCount);

        private static string getCacheKey(string code, IEnumerable<string> filteredRepos = null) =>
            Tuple.Create(code, filteredRepos).GetHashCode().ToString();
    }
}
