using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrionikUA.UrlCreator.Tests
{
    [TestClass]
    public class MainTests
    {
        [TestMethod]
        [DataRow("host", "scheme", 1234, new[] { "path1", "path2" }, new[] { "key1", "value1", "key2", "value2" }, "fragment", null, "scheme://host:1234/path1/path2?key1=value1&key2=value2#fragment")]
        [DataRow("host", null, -1, null, null, null, null, "host")]        
        public void BuildTest(string host, string scheme, int port, string[] path, string[] query, string fragment, IUrlBuildAlgorithm algorithm, string expected)
        {
            Build(host, scheme, port, path, query, fragment, algorithm, expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [DataRow(null, "scheme", 1234, new[] { "path1", "path2" }, new[] { "key1", "value1", "key2", "value2" }, "fragment", null, "scheme://host:1234/path1/path2?key1=value1&key2=value2#fragment")]
        public void ExpectedException(string host, string scheme, int port, string[] path, string[] query, string fragment, IUrlBuildAlgorithm algorithm, string expected)
        {
            Build(host, scheme, port, path, query, fragment, algorithm, expected);
        }

        private void Build(string host, string scheme, int port, string[] path, string[] query, string fragment, IUrlBuildAlgorithm algorithm, string expected)
        {
            Dictionary<string, string> queryDict = BuildQueryDictionary(query);
            var builder = new UrlBuilder(host, scheme, port, path?.ToList() ?? null, queryDict, fragment, algorithm);
            var actual = builder.Build();
            Assert.IsTrue(actual.Equals(expected));
        }

        private Dictionary<string, string> BuildQueryDictionary(string[] query)
        {
            Dictionary<string, string> queryDict = new Dictionary<string, string>();
            if (query == null)
                return null;
            for (int index = 0; index < query.Count(); index += 2)
            {
                queryDict.Add(query[index], query[index + 1]);
            }
            return queryDict;
        }
    }
}
