using System;
using System.Threading.Tasks;
using System.Net.Http;
using Mono.Options;

namespace testcsharp
{
	class MainClass
	{
		public static void Usage () {
			Console.WriteLine ("Usage of healthchecks.io console application:");
			Console.WriteLine ("    hc (runs 'hc list' by default)");
			Console.WriteLine ("    hc list");
		}

		public static void Main (string[] args)
		{
			Console.WriteLine ("healthchecks.io console application");

			// Getting the path of the home directory in C#?
			// http://stackoverflow.com/q/1143706/4126114
			string homePath = (Environment.OSVersion.Platform == PlatformID.Unix ||
			                  Environment.OSVersion.Platform == PlatformID.MacOSX)
				? Environment.GetEnvironmentVariable ("HOME")
				: Environment.ExpandEnvironmentVariables ("%HOMEDRIVE%%HOMEPATH%");
			
			string filename = System.IO.Path.Combine (homePath, ".healthchecksrc");
			if (!System.IO.File.Exists (filename)) {
				Console.WriteLine ("config file {0} not found. Please place your healthchecks.io API key in it. Use chmod 600 for security", filename);
				return;
			}

			string key = System.IO.File.ReadAllText (filename).Trim ();
			Console.WriteLine ("Using API KEY: {0}", key);

			var hc = new HealthChecks (key);

			// check which command
			string command = "list";
			if (args.Length > 0) {
				command = args [0];
			}

			switch (command) {
			case "list":
				Console.WriteLine (hc.List().Result);
				break;
			case "ping":
				// Best way to parse command line arguments in C#? [closed]
				// http://stackoverflow.com/a/2067916/4126114
				/* List<string> names = new List<string> ();
			var configureApiKey = new OptionSet () {
				{ "configure:api-key=", "the {NAME} of someone to greet.",
					v => names.Add (v) },
				{ "r|repeat=", 
					"the number of {TIMES} to repeat the greeting.\n" + 
					"this must be an integer.",
					(int v) => repeat = v },
				{ "v", "increase debug message verbosity",
					v => { if (v != null) ++verbosity; } },
				{ "h|help",  "show this message and exit", 
					v => show_help = v != null },
				};*/
				if (args.Length < 2) {
					Console.WriteLine ("Missing check name to ping");
					return;
				}
				string name = args [1];
				name = name.Trim ();
				Console.WriteLine ("Pinging {0}", name);
				Console.WriteLine (hc.Ping(name).Result);
				break;
			default:
				Console.WriteLine ("Invalid command {0}", command);
				Usage ();
				break;
			}
		}

	}
}