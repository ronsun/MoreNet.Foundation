using MoreNet.Foundation.Extensions;
using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Web;

namespace MoreNet.Foundation
{
    /// <summary>
    /// An adaptor used to adapt to <see cref="UriBuilder"/> in Fluent API style.
    /// </summary>
    public class FluentUriBuilder
    {
        private UriBuilder _uriBuilder;
        private NameValueCollection _query = new NameValueCollection();

        private FluentUriBuilder()
        {
            _uriBuilder = new UriBuilder();
        }

        private FluentUriBuilder(Uri uri)
        {
            _uriBuilder = new UriBuilder(uri);
        }

        /// <summary>
        /// Gets value of <see cref="UriBuilder.Uri"/> of the adaptee.
        /// </summary>
        public Uri Uri
        {
            get
            {
                Commit();
                return _uriBuilder.Uri;
            }
        }

        /// <summary>
        /// Create <see cref="FluentUriBuilder"/>.
        /// </summary>
        /// <returns>Created <see cref="FluentUriBuilder"/>.</returns>
        public static FluentUriBuilder Create()
        {
            return new FluentUriBuilder();
        }

        /// <summary>
        /// Create <see cref="FluentUriBuilder"/>.
        /// </summary>
        /// <param name="uri">Uri.</param>
        /// <returns>Created <see cref="FluentUriBuilder"/>.</returns>
        public static FluentUriBuilder Create(Uri uri)
        {
            return new FluentUriBuilder(uri);
        }

        /// <summary>
        /// Adapt and to set <see cref="UriBuilder.Scheme"/>.
        /// </summary>
        /// <param name="scheme">Scheme.</param>
        /// <returns>Current <see cref="FluentUriBuilder"/>.</returns>
        public FluentUriBuilder SetScheme(string scheme)
        {
            _uriBuilder.Scheme = scheme;
            return this;
        }

        /// <summary>
        /// Set query string.
        /// </summary>
        /// <param name="query">Query.</param>
        /// <returns>Current <see cref="FluentUriBuilder"/>.</returns>
        public FluentUriBuilder SetQuery(string query)
        {
            _query = HttpUtility.ParseQueryString(query);
            return this;
        }

        /// <summary>
        /// Add new item into query.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="values">Values.</param>
        /// <returns>Current <see cref="FluentUriBuilder"/>.</returns>
        /// <remarks>
        /// Throw exception if <paramref name="values"/> is null or empty.
        /// If the value in <paramref name="values"/>, the value will be ignore.
        /// </remarks>
        public FluentUriBuilder AddQuery(string key, params string[] values)
        {
            values.ShouldNotEmpty(nameof(values));

            foreach (var val in values)
            {
                if (!string.IsNullOrEmpty(val))
                {
                    _query.Add(key, val);
                }
            }

            return this;
        }

        /// <summary>
        /// Add query string.
        /// </summary>
        /// <typeparam name="TEnum">Type of enum.</typeparam>
        /// <typeparam name="TUnderlying">Underlying type of enum, ex: int, uint...etc.</typeparam>
        /// <param name="key">Key.</param>
        /// <param name="values">Values.</param>
        /// <returns>Current <see cref="FluentUriBuilder"/>.</returns>
        public FluentUriBuilder AddQuery<TEnum, TUnderlying>(string key, params TEnum[] values)
            where TEnum : Enum
        {
            values.ShouldNotEmpty(nameof(values));

            foreach (var val in values)
            {
                var stringValue = Convert.ChangeType(val, typeof(TUnderlying), CultureInfo.InvariantCulture).ToString();
                _query.Add(key, stringValue);
            }

            return this;
        }

        /// <summary>
        /// Adapt and to set <see cref="UriBuilder.Port"/>.
        /// </summary>
        /// <param name="port">Port number.</param>
        /// <returns>Current <see cref="FluentUriBuilder"/>.</returns>
        public FluentUriBuilder SetPort(int port)
        {
            _uriBuilder.Port = port;
            return this;
        }

        /// <summary>
        /// Adapt and to set <see cref="UriBuilder.Path"/>.
        /// </summary>
        /// <param name="path">Path.</param>
        /// <returns>Current <see cref="FluentUriBuilder"/>.</returns>
        public FluentUriBuilder SetPath(string path)
        {
            _uriBuilder.Path = path;
            return this;
        }

        /// <summary>
        /// Adapt and to set <see cref="UriBuilder.UserName"/>.
        /// </summary>
        /// <param name="userName">User name.</param>
        /// <returns>Current <see cref="FluentUriBuilder"/>.</returns>
        public FluentUriBuilder SetUser(string userName)
        {
            _uriBuilder.UserName = userName;
            return this;
        }

        /// <summary>
        /// Adapt and to set <see cref="UriBuilder.UserName"/> and <see cref="UriBuilder.Password"/>.
        /// </summary>
        /// <param name="userName">User name.</param>
        /// <param name="password">Password.</param>
        /// <returns>Current <see cref="FluentUriBuilder"/>.</returns>
        public FluentUriBuilder SetUser(string userName, string password)
        {
            _uriBuilder.UserName = userName;
            _uriBuilder.Password = password;
            return this;
        }

        /// <summary>
        /// Adapt and to set <see cref="UriBuilder.Fragment"/>.
        /// </summary>
        /// <param name="fragment">Fragment portion.</param>
        /// <returns>Current <see cref="FluentUriBuilder"/>.</returns>
        public FluentUriBuilder SetFragment(string fragment)
        {
            _uriBuilder.Fragment = fragment;
            return this;
        }

        /// <summary>
        /// Adapt and to set <see cref="UriBuilder.Host"/>.
        /// </summary>
        /// <param name="host">Host.</param>
        /// <returns>Current <see cref="FluentUriBuilder"/>.</returns>
        public FluentUriBuilder SetHost(string host)
        {
            _uriBuilder.Host = host;
            return this;
        }

        /// <summary>
        /// Adapt to <see cref="UriBuilder.ToString"/>.
        /// </summary>
        /// <inheritdoc/>
        public override string ToString()
        {
            Commit();
            return _uriBuilder.ToString();
        }

        private void Commit()
        {
            if (_query.HasKeys())
            {
                var q = HttpUtility.ParseQueryString(_uriBuilder.Query);
                q.Add(_query);

                _uriBuilder.Query = q.ToString();

                _query.Clear();
            }
        }
    }
}
