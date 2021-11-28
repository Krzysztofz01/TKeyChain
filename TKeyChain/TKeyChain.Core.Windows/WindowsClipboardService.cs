using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;
using TKeyChain.Core.Abstraction;

namespace TKeyChain.Core.Windows
{
    public class WindowsClipboardService : IClipboardService
    {
        private const uint _cfUnicodeText = 13;

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GlobalLock(IntPtr hMem);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GlobalUnlock(IntPtr hMem);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool OpenClipboard(IntPtr hWndNewOwner);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool CloseClipboard();

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr SetClipboardData(uint uFormat, IntPtr data);

        [DllImport("user32.dll")]
        static extern bool EmptyClipboard();

        public void CopyToClipboard(string value)
        {
            AvailableCheck();

            SetClipboardText(value);
        }

        private static void AvailableCheck()
        {
            int trials = 10;
            while(true)
            {
                if (OpenClipboard(default))
                    break;

                if (--trials == 0)
                    throw new Win32Exception(Marshal.GetLastWin32Error());

                Thread.Sleep(100);
            }
        }

        private static void SetClipboardText(string value)
        {
            EmptyClipboard();

            IntPtr hGlobal = default;

            try
            {
                var bytes = (value.Length + 1) * 2;
                hGlobal = Marshal.AllocHGlobal(bytes);

                if (hGlobal == default)
                    throw new Win32Exception(Marshal.GetLastWin32Error());

                var target = GlobalLock(hGlobal);

                if (target == default)
                    throw new Win32Exception(Marshal.GetLastWin32Error());

                try
                {
                    Marshal.Copy(value.ToCharArray(), 0, target, value.Length);
                }
                finally
                {
                    GlobalUnlock(target);
                }

                if (SetClipboardData(_cfUnicodeText, hGlobal) == default)
                    throw new Win32Exception(Marshal.GetLastWin32Error());

                hGlobal = default;
            }
            finally
            {
                if (hGlobal != default)
                    Marshal.FreeHGlobal(hGlobal);

                CloseClipboard();
            }
        }
    }
}
