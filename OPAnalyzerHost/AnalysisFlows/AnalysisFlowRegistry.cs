namespace OPAnalyzerHost.AnalysisFlows
{
	public class AnalysisFlowRegistry : BaseAnalysisFlowRegistry
    {
        public AnalysisFlowRegistry()
	    {
		    long analysisFlowId = 0;
		    RegisteredAnalysisFlows.Add(++analysisFlowId, new RemoveItemsShorterThan25());
		    RegisteredAnalysisFlows.Add(++analysisFlowId, new RemoveSpaces());
        }
    }
}
