using System;
using System.ServiceModel;
using System.Text.RegularExpressions;
using OPAnalyzerHost.AnalysisFlows;
using OPAnalyzerHost.DataSourceAnalyzers;

namespace OPAnalyzerHost
{
	/*
	 * Notes: The system is designed to be highly flexible and de-coupled. The use of UnityContainer is meant for decoupling the components. You can now not only create as many new analysis flows and
	 * data source analyzers as you wish, but you can create completely different registries for both these hierarchies to test different scenarios at different times.
	 * My code also assumes that the client does not dictate which analysis flows / data sources the server supports (each one requires implementation on host side... the client doesn't perform the operations). I could
	 * have delegated more power to the client side by putting the DependencyInjector in a OPAnalyzerCommon project and setting the registries from the client side, but this would imply that the client has to know
	 * about the analysis flows and data source analyzers and the contents of the host project and I don't want that. The client can call the two added APIs to let him know which data sources and flows are available
	 * for him. The data sources and flows supported by the server at any given time depends on the registries which we in the server side inject.
	 */
    public class Program
    {
		static void Main()
        {
			DependencyInjector.Register<IAnalysisFlowRegistry, AnalysisFlowRegistry>();
			DependencyInjector.Register<IDataSourceAnalyzerRegistry, DataSourceAnalyzerRegistry>();
			
            ServiceHost server = new ServiceHost(typeof(AnalyzerService));
	        server.Open();

	        Console.WriteLine("Listening for requests...");
	        Console.ReadLine();

	        server.Close();
        }
    }
}
