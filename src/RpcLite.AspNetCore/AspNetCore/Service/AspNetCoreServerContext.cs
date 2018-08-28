﻿#if NETCORE

using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using RpcLite.Service;

namespace RpcLite.AspNetCore.Service
{
	/// <inheritdoc />
	/// <summary>
	/// </summary>
	internal class AspNetCoreServerContext : IServerContext
	{
		private const string HeadPrefix = "RpcLite-";
		private readonly HttpContext _httpContext;
		private string _requestPath;

		public AspNetCoreServerContext(HttpContext httpContext)
		{
			_httpContext = httpContext;
			RequestServices = _httpContext.RequestServices;
		}

		public string RequestPath
		{
			get
			{
				if (_requestPath != null) return _requestPath;

				var path = _httpContext.Request.Path.Value.StartsWith("/")
					? _httpContext.Request.Path.Value.Substring(1)
					: _httpContext.Request.Path.Value;

				_requestPath = _httpContext.Request.QueryString.HasValue
					? path + _httpContext.Request.QueryString.Value
					: path;

				return _requestPath;

			}
		}

		/// <inheritdoc />
		public int RequestContentLength => (int)(_httpContext.Request.ContentLength ?? 0);

		/// <inheritdoc />
		public string ResponseContentType
		{
			get => _httpContext.Response.ContentType;
			set => _httpContext.Response.ContentType = value;
		}

		/// <inheritdoc />
		public string RequestContentType => _httpContext.Request.ContentType;

		/// <inheritdoc />
		public Stream RequestStream => _httpContext.Request.Body;

		public Stream ResponseStream => _httpContext.Response.Body;

		public int ResponseStatusCode
		{
			get => _httpContext.Response.StatusCode;
			set => _httpContext.Response.StatusCode = value;
		}

		public string GetRequestHeader(string key)
		{
			return _httpContext.Request.Headers[HeadPrefix + key];
		}

		public void SetResponseHeader(string key, string value)
		{
			_httpContext.Response.Headers[HeadPrefix + key] = value;
		}

		public string GetResponseHeader(string key)
		{
			return _httpContext.Response.Headers[HeadPrefix + key];
		}

		public IServiceProvider RequestServices { get; set; }
	}
}

#endif