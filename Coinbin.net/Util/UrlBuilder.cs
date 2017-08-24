using System;
using System.Collections.Generic;
using System.Linq;

namespace Coinbin.net.Util
{
    class UrlBuilder
    {
        private string _schema;
        private string _host;
        private string _port;
        private List<string> _pathSegments;
        private IDictionary<string, string> _queryParams;

        public UrlBuilder()
        {
            _schema = "";
            _host = "";
            _port = "";
            _pathSegments = new List<string>();
            _queryParams = new Dictionary<string, string>();
        }

        public UrlBuilder SetSchema(string schema)
        {
            _schema = schema;
            return this;
        }

        public UrlBuilder SetHost(string host)
        {
            _host = host;
            return this;
        }

        public UrlBuilder SetPort(string port)
        {
            _port = port;
            return this;
        }

        public UrlBuilder AppendPathSegment(string segment)
        {
            _pathSegments.Add(segment);
            return this;
        }

        public UrlBuilder SetQueryParameter(string key, string value)
        {
            _queryParams.Add(key, value);
            return this;
        }

        public string Build()
        {
            var address = _schema + "://" + _host + (_port.Length == 0 ? "" : ":" + _port);

            var path = string.Join("/", _pathSegments);

            var queryParams = _queryParams
                                .ToList()
                                .Select(kv => $"{kv.Key}={Uri.EscapeDataString(kv.Value)}")
                                .StrCat("&");

            return address
                    + (path.Length == 0 ? "" : "/" + path)
                    + (queryParams.Length == 0 ? "" : "?" + queryParams);
        }
    }
}
