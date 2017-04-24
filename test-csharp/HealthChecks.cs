using System;
using System.Threading.Tasks;
using System.Net.Http;
using Mono.Options;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace testcsharp
{
	public class HealthChecks
	{
		public string key;
		public HealthChecks (string key)
		{
			this.key = key;
		}
			
		private string HandleResult (HttpResponseMessage result) {

			if (!result.StatusCode.Equals (System.Net.HttpStatusCode.OK)) {
				throw new System.InvalidOperationException(result.ToString());
			}

			// HttpResponseMessage
			// https://msdn.microsoft.com/en-us/library/system.net.http.HttpResponseMessage(v=vs.118).aspx
			// HttpContent.ReadAsStringAsync Method
			// https://msdn.microsoft.com/en-us/library/system.net.http.httpcontent.readasstringasync.aspx
			Task<String> content = result.Content.ReadAsStringAsync ();
			content.Wait ();
			return(content.Result);
		}

		// consuming REST API with C#
		// http://stackoverflow.com/a/19085571/4126114
		// Task<TResult> Class
		// https://msdn.microsoft.com/en-us/library/dd321424(v=vs.110).aspx
		public async Task<string> List() {
			// HttpClient documentation
			// https://msdn.microsoft.com/en-us/library/system.net.http.httpclient(v=vs.118).aspx
			HttpClient client = new HttpClient();
			// Adding Http Headers to HttpClient
			// http://stackoverflow.com/a/12023307/4126114
			var request = new HttpRequestMessage() {
				RequestUri = new Uri("https://healthchecks.io/api/v1/checks/"),
				Method = HttpMethod.Get,
			};
			// HttpRequestHeaders Class
			// https://msdn.microsoft.com/en-us/library/system.net.http.headers.HttpRequestHeaders(v=vs.118).aspx
			request.Headers.Add("X-Api-Key", this.key);
			// request.Headers.Accept.Add(new Media...("text/plain"));
			var result = await client.SendAsync(request);
			return this.HandleResult(result);
		}

		public async Task<string> Ping(string name) {
			// http://stackoverflow.com/a/33514541/4126114
			string jsonString = this.List ().Result;
			Console.WriteLine ("string: {0}", jsonString);
			//JObject jsonObj = JObject.Parse(jsonString);
			//Console.WriteLine ((string)jsonObj["checks"]);
			//byte[] checks = (byte[]) jsonObj ["checks"];
			//Console.WriteLine (checks);

			//throw new System.InvalidOperationException("WIP");

			// HttpClient documentation
			// https://msdn.microsoft.com/en-us/library/system.net.http.httpclient(v=vs.118).aspx
			HttpClient client = new HttpClient();
			// Adding Http Headers to HttpClient
			// http://stackoverflow.com/a/12023307/4126114
			var request = new HttpRequestMessage() {
				RequestUri = new Uri("https://healthchecks.io/api/v1/checks/"),
				Method = HttpMethod.Get,
			};
			// HttpRequestHeaders Class
			// https://msdn.microsoft.com/en-us/library/system.net.http.headers.HttpRequestHeaders(v=vs.118).aspx
			request.Headers.Add("X-Api-Key", this.key);
			// request.Headers.Accept.Add(new Media...("text/plain"));
			var result = await client.SendAsync(request);
			return this.HandleResult(result);
		}

	}
}