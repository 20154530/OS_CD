using System;
using System.Runtime.InteropServices;

namespace OS_CD
{
    class Win32Funcs
    {
        public const int WM_SYSCOMMAND = 0x112;
        public const int CS_MOVE = 0xF012;

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int Width, int Height, int flags);
    }
}
