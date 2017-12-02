using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace KeyLogger_new
{
    public static class NativeWin32
    {
        public delegate int EnumWindowProcDelegate(int hWnd, int lparam);
        public const int WM_SYSCOMMAND = 274;
        public const int SC_CLOSE = 61536;
        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_TOOLWINDOW = 128;
        private const int WS_EX_APPWINDOW = 262144;
        public const int GW_HWNDFIRST = 0;
        public const int GW_HWNDLAST = 1;
        public const int GW_HWNDNEXT = 2;
        public const int GW_HWNDPREV = 3;
        public const int GW_OWNER = 4;
        public const int GW_CHILD = 5;

        [DllImport("user32.dll")]
        public static extern int FindWindow(string lpClassName,string lpWindowName);
        [DllImport("user32.dll")]
        public static extern int SendMessage(int hWnd, uint Msg,int wParam,int lParam);
        [DllImport("user32.dll")]
        public static extern int SetForegroundWindow(int hWnd);
        [DllImport("user32.dll")]
        public static extern int EnumWindows(NativeWin32.EnumWindowProcDelegate lpEnumfunc,int lParam);
        [DllImport("user32.dll")]
        public static extern void GetWindowText(int h,StringBuilder s,int nMaxCount);
        [DllImport("user32.dll", EntryPoint="GetWindowLongA")]
        public static extern int GetWindowLongPtr(int hwnd,int nIndex);
        [DllImport("user32.dll")]
        public static extern int GetParent(int hwnd);
        [DllImport("user32.dll")]
        public static extern int GetWindow(int hwnd,int wCmd);
        [DllImport("user32.dll")]
        public static extern int IsWindowvisible(int hwnd);
        [DllImport("user32.dll")]
        public static extern int GetDesktopWindow();
    }
}
