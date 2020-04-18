using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace OPAnalyzerHost.DataSourceAnalyzers
{
    internal class GithubAnalyzer : IDataSourceAnalyzer
    {
	    public string[] Analyze(string dataSource)
        {
	        JArray jsonData = GetData(dataSource);
	        return FilterData(jsonData);
        }

        private string[] FilterData(JArray jsonData)
        {
	        List<string> messages = new List<string>();
	        foreach (var highchartData in jsonData.Children())
	        {
		        var highchartProperties = highchartData.Children<JProperty>();
		        var commitElement = highchartProperties.FirstOrDefault(x => x.Name == "commit");
		        if (commitElement == null)
		        {
			        continue;
		        }
		        var commitData = commitElement.Value;
		        var commitProperties = commitData.Children<JProperty>();
		        var messageElement = commitProperties.FirstOrDefault(x => x.Name == "message");
		        if (messageElement == null)
		        {
			        continue;
		        }
		        messages.Add(messageElement.Value.ToString());
	        }
	        return messages.ToArray();
        }

        private JArray GetData(string dataSource)
        {
	        using (var webClient = new WebClient())
	        {
		        webClient.Headers.Add(HttpRequestHeader.UserAgent, "OPAnalyzer");
		        string responseData = webClient.DownloadString(dataSource);
		        return JArray.Parse(responseData);
	        }
        }
    }
}
