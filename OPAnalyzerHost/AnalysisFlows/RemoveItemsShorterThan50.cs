using System.Linq;

namespace OPAnalyzerHost.AnalysisFlows
{
	internal class RemoveItemsShorterThan50 : IAnalysisFlow
    {
	    public string GetDescription()
	    {
		    return "Removes all items that are shorter than 50 characters.";
	    }

	    public string[] Run(string[] items)
	    {
		    return items.Where(i => i.Length >= 50).ToArray();
	    }
    }
}
