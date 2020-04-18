using System;
using System.Collections.Generic;
using System.Linq;
using OPAnalyzerClient.OPAnalyzerHost;

namespace OPAnalyzerClient
{
    class Program
    {
        static void Main()
        {
	        AnalyzerServiceClient client = new AnalyzerServiceClient();

	        client.GetSupportedDataSources().ToList().ForEach(Console.WriteLine);
	        Console.ReadLine();

	        IDictionary<long, string> validAnalysisFlows = client.GetSupportedAnalysisFlows();
	        validAnalysisFlows.Select(kvp => $"{kvp.Key}: {kvp.Value}").ToList().ForEach(Console.WriteLine);
	        Console.ReadLine();

	        //client.Analyze("https://api.github.com/repos/highcharts/highcharts/commits").ToList().ForEach(Console.WriteLine);
	        //Console.ReadLine();

	        client.AnalyzeWithFlow("https://api.stackexchange.com/2.2/tags/singleton/faq?site=stackoverflow", 1).ToList().ForEach(Console.WriteLine);
	        Console.ReadLine();

	        client.AnalyzeWithFlows("https://api.github.com/repos/highcharts/highcharts/commts", new long[] { 1, 2 }).ToList().ForEach(Console.WriteLine);
	        Console.ReadLine();

	        //client.Analyze("Rally");
	        //Console.ReadLine();

	        //client.AnalyzeWithFlow("GitHub", 50);
	        //Console.ReadLine();
        }
    }
}
