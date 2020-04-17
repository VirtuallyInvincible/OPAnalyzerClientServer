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
	    private const string HIGHCHARTS_URL = "https://api.stackexchange.com/2.2/tags/highcharts/faq?site=stackoverflow";


        public string[] Analyze()
        {
	        JObject jsonData = GetHighchartsData();
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

        private JObject GetHighchartsData()
        {
	        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(HIGHCHARTS_URL);
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
