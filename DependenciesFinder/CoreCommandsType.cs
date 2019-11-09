using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Octokit;

namespace DependenciesFinder
{
    public class CoreCommandsType
    {
        private readonly Type _type;
        
        public CoreCommandsType(Type type) => _type = type;
        
        internal IEnumerable<SearchCode> DependentCode { get; set; }

        public string Namespace => "ExtendHealth.Core.Commands";
        public string Name => _type.Name;
        public string FullName => _type.FullName;

        public IEnumerable<Repository> DependentRepositories =>
            DependentCode.Select(x => x.Repository).Distinct(new RepositoryComparer());
        public IEnumerable<Consumer> Consumers { get; internal set; }
    }
}
