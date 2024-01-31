using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace UnknownClient
{
    public class Wrapper
    {
        internal static void killOSCChid(int oscPort = 9000)
        {
            Process process = executeCMDCommand("netstat -aon | findstr " + oscPort.ToString());
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            string childPID = getLastNumber(output);
            bool flag = !string.IsNullOrEmpty(childPID);
            if (flag)
            {
                CLog.L("Found Child PID using OSC port: " + childPID, ConsoleColor.White, ConsoleColor.White, ConsoleColor.Red);
                Process process2 = executeCMDCommand("wmic process get processid,parentprocessid | findstr /i " + childPID);
                string output2 = process2.StandardOutput.ReadToEnd();
                process2.WaitForExit();
                string parentPID = getLastNumber(output2);
                bool flag2 = !string.IsNullOrEmpty(parentPID);
                if (flag2)
                {
                    CLog.L("Found Parent PID using OSC post: " + parentPID, ConsoleColor.White, ConsoleColor.White, ConsoleColor.Red);
                    Process process3 = executeCMDCommand("taskkill /f /pid " + parentPID);
                    process3.WaitForExit();
                    CLog.L("Process Killed, disable & enable OSC in Action Menu!", ConsoleColor.White, ConsoleColor.White, ConsoleColor.Red);
                }
                else
                {
                    CLog.Error("Couldn't find parent PID that is using OSC Port.", ConsoleColor.White, ConsoleColor.Blue, ConsoleColor.Gray);
                }
            }
            else
            {
                CLog.Error("Couldn't find child PID that is using OSC Port.", ConsoleColor.White, ConsoleColor.Blue, ConsoleColor.Gray);
            }
        }

        internal static Process executeCMDCommand(string command)
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = "/C " + command;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            return process;
        }

        internal static string getLastNumber(string input)
        {
            MatchCollection matches = Regex.Matches(input, "\\b\\d+\\b");
            bool flag = matches.Count > 0;
            string result;
            if (flag)
            {
                MatchCollection matchCollection = matches;
                result = matchCollection[matchCollection.Count - 1].Value;
            }
            else
            {
                result = null;
            }
            return result;
        }
    }
}