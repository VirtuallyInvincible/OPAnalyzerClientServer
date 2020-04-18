using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace OPAnalyzerHost.DataSourceAnalyzers
{
    public class DataSourceAnalyzerRegistry : IDataSourceAnalyzerRegistry
    {
	    private readonly IDictionary<Regex, IDataSourceAnalyzer> RegisteredDataSourceAnalyzers = new Dictionary<Regex, IDataSourceAnalyzer>();


	    public DataSourceAnalyzerRegistry()
	    {
		    RegisteredDataSourceAnalyzers.Add(new Regex("https://api.github.com/repos/.+/.+/commits"), new GithubAnalyzer());
		    RegisteredDataSourceAnalyzers.Add(new Regex($"https://api.stackexchange.com/2.2/tags/.+/faq{Regex.Escape("?")}site=stackoverflow"), new StackOverflowAnalyzer());
	    }


	    public string[] RunDataSourceAnalysis(string dataSource)
	    {
			Regex matchingDataSource = RegisteredDataSourceAnalyzers.Keys.FirstOrDefault(regex => regex.IsMatch(dataSource));
		    if (matchingDataSource == null)
		    {
			    throw new Exception("Unsupported data source.");
		    }

		    try
		    {
			    return RegisteredDataSourceAnalyzers[matchingDataSource].Analyze(dataSource);
		    }
		    catch (Exception e)
		    {
			    throw new Exception($"An exception has occurred: {e}");
		    }
	    }

        public ICollection<string> GetSupportedDataSources()
        {
            return RegisteredDataSourceAnalyzers.Keys.Select(regex => regex.ToString()).ToList();
        }
    }
}
