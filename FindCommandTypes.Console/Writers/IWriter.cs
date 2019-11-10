using System.Collections.Generic;
using System.Threading.Tasks;
using DependenciesFinder;

namespace FindCommandTypes.Console.Writers
{
    public interface IWriter
    {
        Task WriteAsync(IEnumerable<string> repositories, IEnumerable<CoreCommandsType> coreCommandsTypes);

        Task WriteComparisonAsync(IEnumerable<string> repositories,
                                  IEnumerable<CoreCommandsType> seansCommandsNotFoundInDLL,
                                  IEnumerable<CoreCommandsType> dllCommandsNotFoundInSeansList);
    }
}
