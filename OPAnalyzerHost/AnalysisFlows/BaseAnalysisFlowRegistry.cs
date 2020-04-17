using System;
using System.Collections.Generic;

namespace OPAnalyzerHost.AnalysisFlows
{
    public abstract class BaseAnalysisFlowRegistry : IAnalysisFlowRegistry
    {
	    protected readonly IDictionary<long, IAnalysisFlow> RegisteredAnalysisFlows = new Dictionary<long, IAnalysisFlow>();
	    private IDictionary<long, string> ValidAnalysisFlows = new Dictionary<long, string>();


	    public string[] RunAnalysis(string[] items, params long[] analysisFlowIds)
	    {
		    foreach (long analysisFlowId in analysisFlowIds)
		    {
			    if (!RegisteredAnalysisFlows.TryGetValue(analysisFlowId, out var analysisFlow))
			    {
				    throw new Exception("Invalid analysis flow ID");
			    }
			    items = analysisFlow.Run(items);
		    }

		    return items;
	    }

	    public IDictionary<long, string> GetSupportedAnalysisFlows()
	    {
		    if (ValidAnalysisFlows.Count > 0)
		    {
			    return ValidAnalysisFlows;
		    }

		    ValidAnalysisFlows = new Dictionary<long, string>();
		    foreach (var kvp in RegisteredAnalysisFlows)
		    {
			    ValidAnalysisFlows.Add(kvp.Key, kvp.Value.GetDescription());
		    }
		    return ValidAnalysisFlows;
	    }
    }
}
