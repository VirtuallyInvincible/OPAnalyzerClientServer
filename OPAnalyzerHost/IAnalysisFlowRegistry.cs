using System.Collections.Generic;

namespace OPAnalyzerHost
{
    public interface IAnalysisFlowRegistry
    {
	    IDictionary<long, string> GetSupportedAnalysisFlows();
	    string[] RunAnalysis(string[] items, params long[] analysisFlowIds);
    }
}
