namespace OPAnalyzerHost.AnalysisFlows
{
    public interface IAnalysisFlow
    {
	    string GetDescription();
	    string[] Run(string[] items);
    }
}
