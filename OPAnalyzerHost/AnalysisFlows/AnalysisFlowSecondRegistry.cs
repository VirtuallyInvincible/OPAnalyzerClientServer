namespace OPAnalyzerHost.AnalysisFlows
{
    public class AnalysisFlowSecondRegistry : BaseAnalysisFlowRegistry
    {
	    public AnalysisFlowSecondRegistry()
	    {
		    long analysisFlowId = 0;
		    RegisteredAnalysisFlows.Add(++analysisFlowId, new RemoveItemsShorterThan50());
	    }
    }
}
