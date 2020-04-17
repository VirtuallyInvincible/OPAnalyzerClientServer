using System.Collections.Generic;

namespace OPAnalyzerHost
{
    public class AnalyzerService : IAnalyzerService
    {
	    private IAnalysisFlowRegistry AnalysisFlowRegistry;
	    private IDataSourceAnalyzerRegistry DataSourceAnalyzerRegistry;


	    public AnalyzerService()
	    {
		    AnalysisFlowRegistry = DependencyInjector.Retrieve<IAnalysisFlowRegistry>();
		    DataSourceAnalyzerRegistry = DependencyInjector.Retrieve<IDataSourceAnalyzerRegistry>();
	    }


        public string[] Analyze(string dataSourceName)
	    {
			return DataSourceAnalyzerRegistry.RunDataSourceAnalysis(dataSourceName);
	    }

	    public string[] AnalyzeWithFlow(string dataSourceName, long analysisFlowId)
	    {
		    return AnalyzeWithFlows(dataSourceName, new long[] {analysisFlowId});
	    }

	    public string[] AnalyzeWithFlows(string dataSourceName, long[] analysisFlowIds)
	    {
		    string[] items = Analyze(dataSourceName);
			return AnalysisFlowRegistry.RunAnalysis(items, analysisFlowIds);
	    }

        public IDictionary<long, string> GetSupportedAnalysisFlows()
        {
	        return AnalysisFlowRegistry.GetSupportedAnalysisFlows();
        }

        public ICollection<string> GetSupportedDataSources()
        {
	        return DataSourceAnalyzerRegistry.GetSupportedDataSources();
        }
    }
}
