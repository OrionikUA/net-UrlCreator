using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrionikUA.UrlCreator
{
    public class UrlBuildAlgorithm : IUrlBuildAlgorithm
    {
        public virtual string Build(string host, string port, string scheme, List<string> path, Dictionary<string, string> query, string fragment)
        {            
            var builder = new StringBuilder();
            AppendScheme(builder, scheme);
            AppendHost(builder, host);
            AppendPort(builder, port);
            AppendPath(builder, path);
            AppendQuery(builder, query);
            AppendFragment(builder, fragment);
            return builder.ToString();
        }

        protected virtual void AppendFragment(StringBuilder builder, string fragment)
        {
            if (!string.IsNullOrEmpty(fragment))
            {
                builder.Append($"#{fragment}");
            }
        }

        protected virtual void AppendQuery(StringBuilder builder, Dictionary<string, string> query)
        {
            if (query != null)
            {
                var keys = query.Keys.ToList();
                for (int index = 0; index < query.Count; index++)
                {
                    var key = keys[index];
                    builder.Append(index == 0 ? "?" : "&");
                    builder.Append($"{key}={query[key]}");
                }
            }
        }

        protected virtual void AppendPath(StringBuilder builder, List<string> path)
        {
            if (path != null)
            {
                foreach (var item in path)
                {
                    builder.Append($"/{item}");
                }
            }
        }

        protected virtual void AppendHost(StringBuilder builder, string host)
        {
            if (string.IsNullOrEmpty(host))
            {
                throw new ArgumentNullException("Host cannot be null");
            }
            builder.Append(host);
        }

        protected virtual void AppendScheme(StringBuilder builder, string scheme)
        {
            if (!string.IsNullOrEmpty(scheme))
            {
                builder.Append($"{scheme}://");
            }
        }

        protected virtual void AppendPort(StringBuilder builder, string port)
        {
            if (!string.IsNullOrEmpty(port))
            {
                builder.Append($":{port}");
            }
        }
    }
}
