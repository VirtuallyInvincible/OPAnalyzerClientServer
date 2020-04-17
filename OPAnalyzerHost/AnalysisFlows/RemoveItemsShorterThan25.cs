using System.Linq;

namespace OPAnalyzerHost.AnalysisFlows
{
    internal class RemoveItemsShorterThan25 : IAnalysisFlow
    {
        public string GetDescription()
        {
	        return "Removes all items that are shorter than 25 characters.";
        }

        public string[] Run(string[] items)
        {
	        return items.Where(i => i.Length >= 25).ToArray();
        }
    }
}
