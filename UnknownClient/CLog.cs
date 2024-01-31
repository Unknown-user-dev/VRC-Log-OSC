using System;
using System.Media;
using BuildSoft.VRChat.Osc.Chatbox;
using NAudio.Wave;

namespace UnknownClient
{
    internal class CLog
    {
        internal static void L(string MessageToLog, ConsoleColor NameColor = ConsoleColor.White, ConsoleColor TextColor = ConsoleColor.White, ConsoleColor MidColor = ConsoleColor.Red)
        {
            string str = DateTime.Now.ToString("[HH:mm:ss:ml] -> ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(str + "[Unknown Client]");
            Console.ForegroundColor = MidColor;
            Console.Write(" -> ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(MessageToLog);
            Console.ResetColor();
            OscChatbox.SendMessage("[Unknown Client] " + MessageToLog, true, false);
        }

        internal static void Error(string MessageToLog, ConsoleColor NameColor = ConsoleColor.White, ConsoleColor TextColor = ConsoleColor.Blue, ConsoleColor MidColor = ConsoleColor.Gray)
        {
            string str = DateTime.Now.ToString("[HH:mm:ss:ml] -> ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(str + "[Error]");
            Console.ForegroundColor = MidColor;
            Console.Write(" -> ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(MessageToLog);
            Console.ResetColor();
            OscChatbox.SendMessage("[Unknown Client] " + MessageToLog, true, false);
        }

        internal static void JN(string MessageToLog, ConsoleColor NameColor = ConsoleColor.White, ConsoleColor TextColor = ConsoleColor.Blue, ConsoleColor MidColor = ConsoleColor.Gray)
        {
            string str = DateTime.Now.ToString("[HH:mm:ss:ml] -> ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(str + "[Joined]");
            Console.ForegroundColor = MidColor;
            Console.Write(" -> ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(MessageToLog);
            OscChatbox.SendMessage("[Unknown Client] " + MessageToLog + " Has Joined", true, false);
            Console.ResetColor();
        }

        internal static void LT(string MessageToLog, ConsoleColor NameColor = ConsoleColor.White, ConsoleColor TextColor = ConsoleColor.Blue, ConsoleColor MidColor = ConsoleColor.Gray)
        {
            string str = DateTime.Now.ToString("[HH:mm:ss:ml] -> ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(str + "[Left]");
            Console.ForegroundColor = MidColor;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(MessageToLog);
            OscChatbox.SendMessage("[Unknown Client] " + MessageToLog, true, false);
            Console.ResetColor();
        }

        internal static void VP(string MessageToLog, ConsoleColor NameColor = ConsoleColor.White, ConsoleColor TextColor = ConsoleColor.Blue, ConsoleColor MidColor = ConsoleColor.Gray)
        {
            string str = DateTime.Now.ToString("[HH:mm:ss:ml] -> ");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write(str + "[VideoPlayer]");
            Console.ForegroundColor = MidColor;
            Console.Write(" -> ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(MessageToLog);
            OscChatbox.SendMessage("[Unknown Client] " + MessageToLog, true, false);
            WaveOutEvent waveOutEvent = new WaveOutEvent();
            AudioFileReader audioFileReader = new AudioFileReader("Sound\\VRCX Supported World.wav");
            waveOutEvent.Init(audioFileReader);
            waveOutEvent.Play();
            Console.ResetColor();
        }

        internal static void Key(string MessageToLog, ConsoleColor NameColor = ConsoleColor.White, ConsoleColor TextColor = ConsoleColor.Blue, ConsoleColor MidColor = ConsoleColor.Gray)
        {
            string str = DateTime.Now.ToString("[HH:mm:ss:ml] -> ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(str + "[Key Auth]");
            Console.ForegroundColor = MidColor;
            Console.Write(" -> ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(MessageToLog);
            Console.ResetColor();
        }

        internal static void Auth(string MessageToLog, ConsoleColor NameColor = ConsoleColor.White, ConsoleColor TextColor = ConsoleColor.Blue, ConsoleColor MidColor = ConsoleColor.Gray)
        {
            string str = DateTime.Now.ToString("[HH:mm:ss:ml] -> ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(str + "[Auth]");
            Console.ForegroundColor = MidColor;
            Console.Write(" -> ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(MessageToLog);
            Console.ResetColor();
        }

        internal static void Conected(string MessageToLog, ConsoleColor NameColor = ConsoleColor.White, ConsoleColor TextColor = ConsoleColor.Blue, ConsoleColor MidColor = ConsoleColor.Gray)
        {
            string str = DateTime.Now.ToString("[HH:mm:ss:ml] -> ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(str + "[Success]");
            Console.ForegroundColor = MidColor;
            Console.Write(" -> ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(MessageToLog);
            Console.ResetColor();
        }

        internal static void Xinit(string MessageToLog, ConsoleColor NameColor = ConsoleColor.White, ConsoleColor TextColor = ConsoleColor.Blue, ConsoleColor MidColor = ConsoleColor.Gray)
        {
            string str = DateTime.Now.ToString("[HH:mm:ss:ml] -> ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(str + "[Init]");
            Console.ForegroundColor = MidColor;
            Console.Write(" -> ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(MessageToLog);
            Console.ResetColor();
        }

        internal static void Msg(string MessageToLog, ConsoleColor NameColor = ConsoleColor.White, ConsoleColor TextColor = ConsoleColor.Blue, ConsoleColor MidColor = ConsoleColor.Gray)
        {
            string str = DateTime.Now.ToString("[HH:mm:ss:ml] -> ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write(str + "[Msg]");
            Console.ForegroundColor = MidColor;
            Console.Write(" -> ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(MessageToLog);
            Console.ResetColor();
        }
    }
}