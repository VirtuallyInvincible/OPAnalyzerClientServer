using System.Collections.Generic;

namespace OPAnalyzerHost
{
    interface IDataSourceAnalyzerRegistry
    {
        ICollection<string> GetSupportedDataSources();
        string[] RunDataSourceAnalysis(string dataSourceName);
    }
}
