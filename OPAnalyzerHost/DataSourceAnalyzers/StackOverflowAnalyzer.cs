using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;

namespace OPAnalyzerHost.DataSourceAnalyzers
{
    internal class StackOverflowAnalyzer : IDataSourceAnalyzer
    {
	    public string[] Analyze(string dataSource)
        {
	        JObject jsonData = GetData(dataSource);
	        return FilterData(jsonData);
        }

        private string[] FilterData(JObject jsonData)
        {
	        List<string> titles = new List<string>();
	        var itemsElement = jsonData.Children<JProperty>().FirstOrDefault(x => x.Name == "items");
	        if (itemsElement == null)
	        {
		        return null;
	        }
	        var items = itemsElement.Value;
	        foreach (var highchartData in items.Children())
	        {
		        var highchartProperties = highchartData.Children<JProperty>();
		        var titleElement = highchartProperties.FirstOrDefault(x => x.Name == "title");
		        if (titleElement == null)
		        {
			        continue;
		        }
		        titles.Add(titleElement.Value.ToString());
	        }
	        return titles.ToArray();
        }

        private JObject GetData(string dataSource)
        {
	        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(dataSource);
	        request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
	        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
	        Stream responseStream = response?.GetResponseStream();
	        if (responseStream == null)
	        {
		        return null;
	        }
	        string responseData = new StreamReader(responseStream, Encoding.UTF8).ReadToEnd();
	        return JObject.Parse(responseData);
        }
    }
}
