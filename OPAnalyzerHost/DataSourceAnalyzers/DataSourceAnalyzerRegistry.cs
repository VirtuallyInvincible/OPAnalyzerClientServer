using System;
using System.Collections.Generic;

namespace OPAnalyzerHost.DataSourceAnalyzers
{
    public class DataSourceAnalyzerRegistry : IDataSourceAnalyzerRegistry
    {
	    private readonly IDictionary<string, IDataSourceAnalyzer> RegisteredDomainAnalyzers = new Dictionary<string, IDataSourceAnalyzer>();


	    public DataSourceAnalyzerRegistry()
	    {
		    RegisteredDomainAnalyzers.Add("GitHub", new GithubAnalyzer());
		    RegisteredDomainAnalyzers.Add("StackOverflow", new StackOverflowAnalyzer());
	    }


	    public string[] RunDataSourceAnalysis(string dataSourceName)
	    {
		    if (!RegisteredDomainAnalyzers.TryGetValue(dataSourceName, out var domainAnalyzer))
		    {
			    throw new Exception("Unsupported data source name.");
		    }
		    return domainAnalyzer.Analyze();
	    }

        public ICollection<string> GetSupportedDataSources()
        {
            return RegisteredDomainAnalyzers.Keys;
        }
    }
}
