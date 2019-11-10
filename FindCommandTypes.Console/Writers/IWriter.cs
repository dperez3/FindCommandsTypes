using System.Collections.Generic;
using System.Threading.Tasks;
using DependenciesFinder;

namespace FindCommandTypes.Console.Writers
{
    public interface IWriter
    {
        Task WriteAsync(IEnumerable<CoreCommandsType> coreCommandsTypes);

        Task WriteComparisonAsync(IEnumerable<CoreCommandsType> seansCommandsNotFoundInDLL,
                                  IEnumerable<CoreCommandsType> dllCommandsNotFoundInSeansList);
    }
}
