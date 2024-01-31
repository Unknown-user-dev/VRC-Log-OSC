using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using UnknownClient;
using NAudio.Wave;

namespace UnknownClient
{
    internal static class HandleEvents
    {
        private const string urlPattern = "(https?://[^\\s]+)";
        private static int oscPort = 9000;
        private static readonly Dictionary<string, string> _dictionary = new Dictionary<string, string>();

        internal static void Display(List<string> lines)
        {
            foreach (string line in lines)
            {
                if (line.Contains("Destination set: "))
                {
                    HandleDestinationSet(line);
                }
                else if (line.Contains("OnPlayerJoined "))
                {
                    HandlePlayerJoined(line);
                }
                else if (line.Contains("OnPlayerLeft "))
                {
                    HandlePlayerLeft(line);
                }
                else if (line.Contains("Entering Room: "))
                {
                    HandleEnteringRoom(line);
                }
                else if (line.Contains("Microphone device changing to "))
                {
                    HandleMicrophoneChange(line);
                }
                else if (line.Contains("VRChat Build: "))
                {
                    HandleVRChatBuild(line);
                }
                else if (line.Contains("Using network server version: "))
                {
                    HandleNetworkServerVersion(line);
                }
                else if (line.Contains("User Authenticated: "))
                {
                    HandleUserAuthenticated(line);
                }
                else if (line.Contains("Could not Start OSC: Address already in use"))
                {
                    HandleOSCAddressInUse(line);
                }
                else if (line.Contains("OSC:: "))
                {
                    HandleOSC(line);
                }
                else if (line.Contains("Attempting to load String from URL "))
                {
                    HandleLoadStringFromURL(line);
                }
                else if (line.Contains("[Video Playback] URL "))
                {
                    HandleVideoPlaybackURL(line);
                }
                else if (line.Contains("[VRCX]"))
                {
                    HandleVRCX(line);
                }
            }
        }
        private static WaveOutEvent waveOutEvent = new WaveOutEvent();
        private static AudioFileReader audioFileReader;

        private static void HandleDestinationSet(string line)
        {
            string msg = line.Split(new string[] { "Destination set: " }, StringSplitOptions.None)[1];
            Console.Clear();
            audioFileReader = new AudioFileReader("Sound\\LoadingMusic.wav");
            waveOutEvent.Init(audioFileReader);
            waveOutEvent.Play();
            CLog.L("Instance Link: " + msg, ConsoleColor.White, ConsoleColor.White, ConsoleColor.Red);
        }

        private static void HandlePlayerJoined(string line)
        {
            string msg = line.Split(new string[] { "OnPlayerJoined " }, StringSplitOptions.None)[1];
            CLog.JN(msg, ConsoleColor.White, ConsoleColor.Blue, ConsoleColor.Gray);
            waveOutEvent.Stop();
            audioFileReader = new AudioFileReader("Sound//UserConnect.wav");
            waveOutEvent.Init(audioFileReader);
            waveOutEvent.Play();
        }

private static void HandlePlayerLeft(string line)
{
    string msg = line.Split(new string[] { "OnPlayerLeft " }, StringSplitOptions.None)[1];
    CLog.LT(msg + " Has Left or crashed...", ConsoleColor.White, ConsoleColor.Blue, ConsoleColor.Gray);
}

private static void HandleEnteringRoom(string line)
{
    string msg = line.Split(new string[] { "Entering Room: " }, StringSplitOptions.None)[1];
    CLog.L("Entering Room: " + msg, ConsoleColor.White, ConsoleColor.White, ConsoleColor.Red);
}

private static void HandleMicrophoneChange(string line)
{
    string msg = line.Split(new string[] { "Microphone device changing to " }, StringSplitOptions.None)[1];
    string lastMicChangeMsg;
    if (_dictionary.TryGetValue("lastMicChangeMsg", out lastMicChangeMsg) && lastMicChangeMsg != msg)
    {
        CLog.L("Microphone changed to: " + lastMicChangeMsg, ConsoleColor.White, ConsoleColor.White, ConsoleColor.Red);
    }
    else
    {
        CLog.L("Microphone changed to: " + msg, ConsoleColor.White, ConsoleColor.White, ConsoleColor.Red);
    }
    _dictionary["lastMicChangeMsg"] = msg;
}
public static void HandleVRChatBuild(string line)
{
    string msg = line.Split(new string[] { "VRChat Build: " }, StringSplitOptions.None)[1];
    CLog.L("VRChat Build: " + msg.Trim(), ConsoleColor.White, ConsoleColor.White, ConsoleColor.Red);
    WaveOutEvent waveOutEvent = new WaveOutEvent();
    AudioFileReader audioFileReader = new AudioFileReader("Sound//Notif.wav");
    waveOutEvent.Init(audioFileReader);
    waveOutEvent.Play();
}

public static void HandleNetworkServerVersion(string line)
{
    string msg = line.Split(new string[] { "Using network server version: " }, StringSplitOptions.None)[1];
    CLog.L("Network Ver : " + msg, ConsoleColor.White, ConsoleColor.White, ConsoleColor.Red);
}

public static void HandleUserAuthenticated(string line)
{
    string msg = line.Split(new string[] { "User Authenticated: " }, StringSplitOptions.None)[1];
    CLog.L("Logged in as: " + msg, ConsoleColor.White, ConsoleColor.White, ConsoleColor.Red);
}

public static void HandleOSCAddressInUse(string line)
{
    CLog.Error("OSC Address already in use: " + line, ConsoleColor.White, ConsoleColor.Blue, ConsoleColor.Gray);
    Wrapper.killOSCChid(oscPort);
}

public static void HandleOSC(string line)
{
    string msg = line.Split(new string[] { "OSC:: " }, StringSplitOptions.None)[1];
    Regex regex = new Regex("\\b\\d{4}\\b");
    MatchCollection matches = regex.Matches(msg);
    if (matches.Count > 0)
    {
        oscPort = int.Parse(matches[0].Value);
    }
    CLog.L("OSC: " + msg, ConsoleColor.White, ConsoleColor.White, ConsoleColor.Red);
}

public static void HandleLoadStringFromURL(string line)
{
    string msg = line.Split(new string[] { "Attempting to load String from URL " }, StringSplitOptions.None)[1];
    CLog.L("World is fetching string: " + msg, ConsoleColor.White, ConsoleColor.White, ConsoleColor.Red);
}

public static void HandleVideoPlaybackURL(string line)
{
    Regex regex = new Regex("(https?://[^\\s]+)");
    MatchCollection matches = regex.Matches(line);
    if (matches.Count > 0)
    {
        string url = matches[0].Groups[1].Value;
        CLog.VP("Playing video: " + url, ConsoleColor.White, ConsoleColor.Blue, ConsoleColor.Gray);
    }
}

public static void HandleVRCX(string line)
{
    string msg = line.Split(new string[] { "[VRCX] " }, StringSplitOptions.None)[1];
    string[] msgParts = msg.Split(new string[] { "," }, StringSplitOptions.None);
    string user = msgParts.ElementAt(msgParts.Length - 2);
    string video = msgParts.Last<string>();
    CLog.VP(string.Concat(new string[]
    {
        "VRCX Supported World: User (",
        user,
        ") put on video (",
        video,
        ")"
    }), ConsoleColor.White, ConsoleColor.Blue, ConsoleColor.Gray);
}
    }
}
