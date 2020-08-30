using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace VolumeProxy
{
    public class InputController
    {
        [DllImport ("user32.dll")]
        static extern bool PostMessage (int hhwnd, uint msg, IntPtr wparam, IntPtr lparam);

        [DllImport ("user32.dll")]
        static extern IntPtr LoadKeyboardLayout (string pwszKLID, uint Flags);

        [DllImport ("user32.dll")]
        static extern IntPtr GetKeyboardLayout (UInt32 idThread);

        const uint WM_InputLangChangeRequest = 0x0050;
        const int HWND_BroadCast = 0xffff;
        const string en_US = "00000409";
        const string cn_TW = "00000404";
        const uint KLF_ACTIVATE = 1;

        /// <summary>
        /// 語系切到英文 不要直接切輸入法 這樣切回中文的時候 會保留本來的輸入法狀態
        /// </summary>
        public static void ChangeToEn ()
        {
            PostMessage (HWND_BroadCast, WM_InputLangChangeRequest, IntPtr.Zero, LoadKeyboardLayout (en_US, KLF_ACTIVATE));
        }

        /// <summary>
        /// 語系切到中文 不要直接切輸入法 這樣切成英文的時候 會保留本來的輸入法狀態
        /// </summary>
        public static void ChangeToCn ()
        {
            PostMessage (HWND_BroadCast, WM_InputLangChangeRequest, IntPtr.Zero, LoadKeyboardLayout (cn_TW, KLF_ACTIVATE));
        }
    }
}
