using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace OPAnalyzerHost.DataSourceAnalyzers
{
    internal class GithubAnalyzer : IDataSourceAnalyzer
    {
	    private const string HIGHCHARTS_URL = "https://api.github.com/repos/highcharts/highcharts/commits";


	    public string[] Analyze()
        {
	        JArray jsonData = GetHighchartsData();
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

        private JArray GetHighchartsData()
        {
	        using (var webClient = new WebClient())
	        {
		        webClient.Headers.Add(HttpRequestHeader.UserAgent, "OPAnalyzer");
		        string responseData = webClient.DownloadString(HIGHCHARTS_URL);
		        return JArray.Parse(responseData);
	        }
        }
    }
}
