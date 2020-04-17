using System.Linq;

namespace OPAnalyzerHost.AnalysisFlows
{
    internal class RemoveSpaces : IAnalysisFlow
    {
	    public string GetDescription()
	    {
		    return "Removes spaces from all items.";
	    }

        public string[] Run(string[] items)
        {
	        return items.Select(i => i.Replace(" ", "")).ToArray();
        }
    }
}
