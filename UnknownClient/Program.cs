using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using BuildSoft.VRChat.Osc.Chatbox;
using UnknownClient;
using NAudio.Wave;
using DiscordRPC;
using Newtonsoft.Json;
using System.Text;

namespace UnknownClient
{
    class VRChat_ConsoleLogger_PC
    {
        private static long readOffset;
        private static bool needClean;
        public const string Name = "UnknownClient";
        public const string Version = "1.0.4";

        private static ManualResetEvent mre = new ManualResetEvent(true);
        private static Queue<string> messageQueue = new Queue<string>();

        static bool vrcrunstatus(Process[] runningProcess)
        {
            for (int i = 0; i < runningProcess.Length; i++)
            {
                if (runningProcess[i].ProcessName == "VRChat")
                {
                    return true;
                }
            }
            return false;
        }

        static void killeac(Process[] runningProcess)
        {
            for (int i = 0; i < runningProcess.Length; i++)
            {
                if (runningProcess[i].ProcessName == "EasyAntiCheat_EOS")
                {
                    runningProcess[i].Kill();
                }
            }
        }


        static void Main(string[] args)
        {

            DiscordRpcClient client = new DiscordRpcClient("1197866493292068897");

            client.Initialize();

            RichPresence presence = new RichPresence()
            {
                Details = "VRChat Private Client | Made by Unknown User",
                State = "Only my friends can got this | Fuck Easy Anti Cheat",
                Assets = new Assets()
                {
                    LargeImageKey = "client",
                    LargeImageText = "VRChat Client",
                    SmallImageKey = "client2"
                }
            };

            client.SetPresence(presence);
            Console.Title = "UnknownClient";
            CLog.L("Welcome Back " + Environment.UserName, ConsoleColor.White, ConsoleColor.White, ConsoleColor.Red);
            Thread.Sleep(5000);
            WaveOutEvent waveOutEvent = new WaveOutEvent();
            AudioFileReader audioFileReader = new AudioFileReader("Sound\\Welcome api.wav");
            waveOutEvent.Init(audioFileReader);
            waveOutEvent.Play();
            CLog.L("Is VRChat Launched ?", ConsoleColor.White, ConsoleColor.White, ConsoleColor.Red);
            CLog.L("[1] Yes (You will use the client Without EAC Bypass)", ConsoleColor.White, ConsoleColor.White, ConsoleColor.Red);
            CLog.L("[2] No (You will use the client With EAC Bypass)", ConsoleColor.White, ConsoleColor.White, ConsoleColor.Red);



            var input = Console.ReadLine();

            if (input == "1")
            {
                Thread.Sleep(5000);
                Thread startLogScanThread = new Thread(new ThreadStart(startLogScan));
                startLogScanThread.Start();
            }
            else if (input == "2")
            {
                bool vrcrunstat = false;
                var uri = "steam://rungameid/438100%22";
                var psi = new System.Diagnostics.ProcessStartInfo();
                psi.UseShellExecute = true;
                psi.FileName = uri;
                System.Diagnostics.Process.Start(psi);

                while (!vrcrunstat)
                {
                    Process[] runingProcess = Process.GetProcesses();
                    vrcrunstat = vrcrunstatus(runingProcess);
                    if (vrcrunstat == true)
                    {
                        killeac(runingProcess);
                    }
                }
                Thread.Sleep(5000);
                Thread startLogScanThread = new Thread(new ThreadStart(startLogScan));
                startLogScanThread.Start();
            }
            else
            {
                Console.WriteLine("Invalid input. Please press 1 or 2.");
                return;
            }
            while (true)
            {
                mre.WaitOne();

                if (messageQueue.Count > 0)
                {
                    string messageToSend = messageQueue.Dequeue();
                    OscChatbox.SendMessage(messageToSend, true, false);
                }

                Process currentProcess = GetActiveProcess();

                if (currentProcess != null)
                {
                    currentProcess.Refresh();

                    string currentTime = DateTime.Now.ToString("HH:mm:ss");

                    string message = "[Unknown Client] Actuellement entrain d'utilisé : " + currentProcess.ProcessName + ", L'heure actuellement : " + currentTime;
                    messageQueue.Enqueue(message);
                    Thread.Sleep(5000);
                }
                Thread.Sleep(2000);
            }
        }

        private static Process GetActiveProcess()
        {
            int activeProcessId;
            GetWindowThreadProcessId(GetForegroundWindow(), out activeProcessId);

            // Get the process
            return Process.GetProcessById(activeProcessId);
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);

        private static void startLogScan()
        {
            for (;;)
            {
                while (Process.GetProcessesByName("VRChat").Length == 0)
                {
                    Thread.Sleep(1000);
                }
                Process[] processes = Process.GetProcessesByName("VRChat");
                bool flag = processes != null && processes.Length > 0;
                if (flag)
                {
                    Process VRChatProcess = processes[0];
                    bool hasExited = VRChatProcess.HasExited;
                    if (hasExited)
                    {
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        bool flag2 = VRChat_ConsoleLogger_PC.needClean;
                        if (flag2)
                        {
                            VRChat_ConsoleLogger_PC.needClean = false;
                        }
                        CLog.L("VRChat process " + VRChatProcess.Id.ToString(), ConsoleColor.White, ConsoleColor.White, ConsoleColor.Red);
                        string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "Low\\VRChat\\VRChat";
                        DirectoryInfo directory = new DirectoryInfo(path);
                        bool exists = directory.Exists;
                        if (!exists)
                        {
                            break;
                        }
                        FileInfo selectedLogInfo = null;
                        while (selectedLogInfo == null)
                        {
                            foreach (FileInfo fileInfo in directory.GetFiles("output_log_*.txt", SearchOption.TopDirectoryOnly))
                            {
                                bool flag3 = VRChatProcess.StartTime.CompareTo(fileInfo.LastWriteTime) <= 0;
                                if (flag3)
                                {
                                    selectedLogInfo = fileInfo;
                                }
                            }
                            Thread.Sleep(1000);
                        }
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        VRChat_ConsoleLogger_PC.readLines(selectedLogInfo.FullName, true);
                        while (!VRChatProcess.HasExited)
                        {
                            List<string> lines = VRChat_ConsoleLogger_PC.readLines(selectedLogInfo.FullName, false);
                            HandleEvents.Display(lines);
                            Thread.Sleep(1000);
                        }
                        CLog.L("VRChat Exited or Crashed -_- ", ConsoleColor.White, ConsoleColor.White, ConsoleColor.Red);
                        VRChat_ConsoleLogger_PC.readOffset = 0L;
                        VRChat_ConsoleLogger_PC.needClean = true;
                        CLog.L("Waiting for VRChat process to inject", ConsoleColor.White, ConsoleColor.White, ConsoleColor.Red);
                    }
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }
            CLog.Error("VRChat Folder not found!", ConsoleColor.White, ConsoleColor.Blue, ConsoleColor.Gray);
            for (;;)
            {
                Thread.Sleep(1000);
            }
        }

        private static List<string> readLines(string filePath, bool toEnd = false)
        {
            List<string> lines = new List<string>();
            try
            {
                using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        bool flag = !toEnd;
                        if (flag)
                        {
                            reader.BaseStream.Seek(VRChat_ConsoleLogger_PC.readOffset, SeekOrigin.Begin);
                            for (;;)
                            {
                                string line = reader.ReadLine();
                                bool flag2 = line != null;
                                if (!flag2)
                                {
                                    break;
                                }
                                lines.Add(line);
                            }
                        }
                        else
                        {
                            reader.BaseStream.Seek(0L, SeekOrigin.End);
                        }
                        VRChat_ConsoleLogger_PC.readOffset = reader.BaseStream.Position;
                    }
                }
            }
            catch (IOException ex)
            {
                CLog.L("ys", ConsoleColor.White, ConsoleColor.White, ConsoleColor.Red);
            }
            return lines;
        }
    }
}