using System.Collections.Generic;
using System.ComponentModel;
using System.ServiceModel;

namespace OPAnalyzerHost
{
    [ServiceContract]
    public interface IAnalyzerService
    {
	    [OperationContract]
	    [Description("Retrieves all the supported data sources.")]
	    ICollection<string> GetSupportedDataSources();

        [OperationContract]
	    [Description("Retrieves all the supported analysis flows - their IDs and their descriptions.")]
	    IDictionary<long, string> GetSupportedAnalysisFlows();

	    [OperationContract]
        [Description("Extracts only the titles if {dataSourceName} is 'StackOverflow', or only the commit messages if it is 'GitHub'. In all other cases (unsupported), an exception will be thrown.")]
        string[] Analyze(string dataSourceName);

        [OperationContract]
        [Description("Same as Analyze(dataSourceName), but executes a specific flow after the extraction.")]
        string[] AnalyzeWithFlow(string dataSourceName, long analysisFlowId);

        [OperationContract]
        [Description("Same as Analyze(dataSourceName), but executes specific flows after the extraction.")]
        string[] AnalyzeWithFlows(string dataSourceName, long[] analysisFlowIds);
    }
}
