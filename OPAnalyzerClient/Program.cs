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

	        client.Analyze("GitHub").ToList().ForEach(Console.WriteLine);
	        Console.ReadLine();

	        client.AnalyzeWithFlow("StackOverflow", 1).ToList().ForEach(Console.WriteLine);
	        Console.ReadLine();

	        client.AnalyzeWithFlows("GitHub", new long[] { 1, 2 }).ToList().ForEach(Console.WriteLine);
	        Console.ReadLine();

	        //client.Analyze("Rally");
	        //Console.ReadLine();

	        //client.AnalyzeWithFlow("GitHub", 50);
	        //Console.ReadLine();
        }
    }
}
