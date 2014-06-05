using System.Diagnostics;
using System.Text;

namespace BztToolbox.Common.Utility
{
	public static class ProcessHelper
	{
		public static int ExecuteCommand(string cmdText, out string trace) {
			Process process = new Process();

			// convig de l'appel du process
			ProcessStartInfo startInfo = new ProcessStartInfo();
			startInfo.UseShellExecute = false;
			startInfo.CreateNoWindow = true;
			startInfo.RedirectStandardOutput = true;
			startInfo.StandardOutputEncoding = Encoding.GetEncoding(850); // default encoding of cmd.exe
			startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
			startInfo.FileName = "btstask.exe";
			startInfo.Arguments = cmdText;
			process.StartInfo = startInfo;

			// appel du process
			process.Start();

			trace = process.StandardOutput.ReadToEnd();
			return process.ExitCode;
		}
	}
}
