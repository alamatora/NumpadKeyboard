#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
#endregion

internal class NativeMethod {
    [DllImport("kernel32.dll")]
    internal static extern uint GetCurrentThreadID();

    [DllImport("kernel32.dll")]
    internal static extern uint GetCurrentProcessID();
}