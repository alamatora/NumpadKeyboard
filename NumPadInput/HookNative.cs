#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
#endregion

internal delegate int HookProc(int nCode, IntPtr wParam, IntPtr lParam);

internal class NativeMethods {
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SetWindowsHookEx(HookType hookType, HookProc callback, IntPtr hMod, uint dwThreadId);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern bool UnhookWindowsHookEx(IntPtr hhk);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern int CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);
}

internal static class HookCodes {
    public const int HC_ACTION = 0;
    public const int HC_GETNEXT = 1;
    public const int HC_SKIP = 2;
    public const int HC_NOREMOVE = 3;
    public const int HC_NOREM = HC_NOREMOVE;
    public const int HC_SYSMODALON = 4;
    public const int HC_SYSMODALOFF = 5;
}

internal enum HookType {
    WH_KEYBOARD = 2,
    WH_KEYBOARD_LL = 13,
}

[StructLayout(LayoutKind.Sequential)]
internal class POINT {
    public int x;
    public int y;
}

[StructLayout(LayoutKind.Sequential)]
internal struct KBDLLHOOKSTRUCT {
    public int vkCode;
    public int scanCode;
    public int flags;
    public int time;
    public int dwExtraInfo;
}

internal enum KeyboardMessage {
    WM_KEYDOWN = 0x0100,
    WM_KEYUP = 0x0101,
    WM_SYSKEYDOWN = 0x0104,
    WM_SYSKEYUP = 0x0105
}