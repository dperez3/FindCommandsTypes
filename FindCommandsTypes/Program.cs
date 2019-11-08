using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace FindCommandsTypes
{
    class Program
    {
        private const string _messageRegex = "[a-zA-Z]+Command";

        static void Main(string[] args)
        {
            var content =
                System.IO.File.ReadAllText("C:\\Users\\domipe\\Desktop\\SSC dependencies on Core Commands by IDE.txt");

            var matches = Regex.Matches(content, _messageRegex);

            var types = matches
                        .Select(x => x.Value)
                        .Where(x => char.IsUpper(x[0]))
                        .Distinct()
                        .OrderBy(x => x);

            Debug.WriteLine("-------------------------------------------------");
            Debug.WriteLine($"TOTAL: {types.Count()}");
            //Debug.Indent();
            foreach (var type in types)
            {
                Debug.WriteLine(type);
            }
        }
    }
}
