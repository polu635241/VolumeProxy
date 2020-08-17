using System;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;


namespace VolumeProxy
{
    public class VolumeController
    {

        [DllImport ("user32.dll")]
        static extern void keybd_event (byte bVk, byte bScan, UInt32 dwFlags, UInt32 dwExtraInfo);

        [DllImport ("user32.dll")]
        static extern Byte MapVirtualKey (UInt32 uCode, UInt32 uMapType);

        private const byte Volume_Mute = 0xAD;
        private const byte Volume_Down = 0xAE;
        private const byte Volume_Up = 0xAF;
        private const UInt32 KeyEvent_ExtenedKey = 0x0001;
        private const UInt32 KeyEvent_KeyUp = 0x0002;

        /// <summary>
        /// 至少要按住幾秒才會觸發 連按
        /// </summary>
        const float holdMinTime = 1f;

        /// <summary>
        /// 連按處發間隔
        /// </summary>
        const float holdInterval = 0.1f;

        float keepHoldTimer;

        Thread updateThread;

        public void Init () 
        {
            updateThread = new Thread (Update);
            updateThread.IsBackground = true;
            updateThread.Start ();
        }

        void Update ()
        {
            while (true) 
            {
                ConsoleKeyInfo key = Console.ReadKey (true);

                OnKeyInput (key);
            }
        }

        void OnKeyInput (ConsoleKeyInfo key)
        {
            ConsoleModifiers modifiers = key.Modifiers;

            //ctrl+shift同時按下 才會觸發快捷鍵
            if (CheckSpecialKey (modifiers, ConsoleModifiers.Shift))
            {
                if (key.Key == ConsoleKey.Add)
                {
                    VolumeUp ();
                }

                if (key.Key == ConsoleKey.Subtract)
                {
                    VolumeDown ();
                }

                if (key.Key == ConsoleKey.Spacebar)
                {
                    SwitchMuteMode ();
                }
            }

        }

        bool CheckSpecialKey (ConsoleModifiers modifiers, ConsoleModifiers checkKey) 
        {
            return ((modifiers & checkKey) == checkKey);
        }


        void VolumeUp ()
        {
            keybd_event (Volume_Up, MapVirtualKey (Volume_Up, 0), KeyEvent_ExtenedKey, 0);
            keybd_event (Volume_Up, MapVirtualKey (Volume_Up, 0), KeyEvent_ExtenedKey | KeyEvent_KeyUp, 0);
        }

        void VolumeDown ()
        {
            keybd_event (Volume_Down, MapVirtualKey (Volume_Down, 0), KeyEvent_ExtenedKey, 0);
            keybd_event (Volume_Down, MapVirtualKey (Volume_Down, 0), KeyEvent_ExtenedKey | KeyEvent_KeyUp, 0);
        }

        void SwitchMuteMode ()
        {
            keybd_event (Volume_Mute, MapVirtualKey (Volume_Mute, 0), KeyEvent_ExtenedKey, 0);
            keybd_event (Volume_Mute, MapVirtualKey (Volume_Mute, 0), KeyEvent_ExtenedKey | KeyEvent_KeyUp, 0);
        }
    }
}
