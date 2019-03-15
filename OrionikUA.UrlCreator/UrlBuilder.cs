using System.Collections.Generic;

namespace OrionikUA.UrlCreator
{
    public class UrlBuilder
    {
        private string _host;
        private string _scheme;
        private int _port;
        private List<string> _path;
        private Dictionary<string, string> _query;
        private string _fragment;

        private string Port => _port < 0 ? null : _port.ToString();

        private IUrlBuildAlgorithm _algorithm;

        public UrlBuilder(string host = null, string scheme = null, int port = -1, List<string> path = null, Dictionary<string, string> query = null,  string fragment = null, IUrlBuildAlgorithm algorithm = null)
        {
            _host = host;
            _scheme = scheme;
            _port = port;
            _path = path?.Clone() ?? new List<string>();
            _fragment = fragment;
            _query = query?.Clone() ?? new Dictionary<string, string>();
            _algorithm = algorithm;
        }

        public string Build()
        {
            if (_algorithm == null)
            {
                _algorithm = new UrlBuildAlgorithm();
            }

            return _algorithm.Build(_host, Port , _scheme, _path, _query, _fragment);
        }

        public UrlBuilder ChangeAlgorithm(IUrlBuildAlgorithm algorithm)
        {
            var newBuilder = Clone();
            newBuilder._algorithm = algorithm;
            return newBuilder;
        }

        public UrlBuilder AddHost(string host)
        {
            var newBuilder = Clone();
            newBuilder._host = host;
            return newBuilder;
        }

        public UrlBuilder AddScheme(string scheme)
        {
            var newBuilder = Clone();
            newBuilder._scheme = scheme;
            return newBuilder;
        }

        public UrlBuilder AddFragment(string fragment)
        {
            var newBuilder = Clone();
            newBuilder._fragment = fragment;
            return newBuilder;
        }

        public UrlBuilder AddPort(int port)
        {
            var newBuilder = Clone();
            newBuilder._port = port;
            return newBuilder;
        }

        public UrlBuilder AddPath(params string[] path)
        {
            var newBuilder = Clone();
            newBuilder._path.AddRange(path);
            return newBuilder;
        }
        
        public UrlBuilder AddQuery(Dictionary<string, string> query)
        {
            var newBuilder = Clone();

            foreach (var item in query)
            {
                if (newBuilder._query.ContainsKey(item.Key))
                {
                    newBuilder._query[item.Key] = item.Value;
                }
                else
                {
                    newBuilder._query.Add(item.Key, item.Value);
                }
            }                      

            return newBuilder;
        }

        public UrlBuilder Clone()
        {
            return new UrlBuilder(_host, _scheme, _port, _path?.Clone(), _query?.Clone(), _fragment, _algorithm);
        }        
    }
}
