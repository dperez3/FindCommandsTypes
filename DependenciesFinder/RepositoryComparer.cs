using System.Collections.Generic;
using Octokit;

namespace DependenciesFinder
{
    public class RepositoryComparer : IEqualityComparer<Repository>
    {
        public bool Equals(Repository x, Repository y) => x.Name == y.Name;

        public int GetHashCode(Repository obj) => obj.Name.GetHashCode();
    }
}
