using System.Collections.Generic;

namespace OrionikUA.UrlCreator
{
    public interface IUrlBuildAlgorithm
    {
        string Build(string host, string port, string scheme, List<string> path, Dictionary<string, string> query, string fragment);
    }
}
