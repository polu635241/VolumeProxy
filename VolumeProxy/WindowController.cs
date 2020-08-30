using System;
using System.Text;
using System.Runtime.InteropServices;

namespace VolumeProxy
{
    public class WindowController
    {
        delegate bool ConsoleCtrlDelegate (int dwCtrlType);

        [DllImport ("Kernel32.dll")]
        static extern IntPtr GetConsoleWindow ();

        [DllImport ("User32.dll")]
        static extern bool ShowWindow (IntPtr hWnd, int cmdShow);

        [DllImport ("kernel32.dll")]
        static extern bool SetConsoleCtrlHandler (ConsoleCtrlDelegate HandlerRoutine, bool Add);

        const int CloseCtrlType = 2;

        public void Init () 
        {
            //抓到當前視窗的指標
            IntPtr hWnd = GetConsoleWindow ();

            InputController.ChangeToEn ();

            ConsoleCtrlDelegate consoleCtrlDelegate = new ConsoleCtrlDelegate (HandRoutine);

            SetConsoleCtrlHandler (consoleCtrlDelegate, true);

            //隱藏視窗
            ShowWindow (hWnd, 2);
        }

        bool HandRoutine (int dwCtrlType) 
        {
            if (dwCtrlType == CloseCtrlType)
            {
                InputController.ChangeToCn ();
            }

            Console.WriteLine (dwCtrlType);
            return false;
        }

    }
}
