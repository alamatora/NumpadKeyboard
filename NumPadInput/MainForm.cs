using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Timers;
using System.Reflection;
using System.Diagnostics;
using WindowsInput;

namespace NumPadInput {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
            SetTimer();
        }

        private const int WM_KEYDOWN = 0x100;
        private const int WM_SYSKEYDOWN = 0x104;

        #region Global Low-level Keyboard Hook
        private IntPtr hGlobalLLKeyboardHook = IntPtr.Zero;
        private HookProc globalLLKeyboardHookCallback = null;

        private Keys currentVKCode = 0;

        private bool SetGlobalLLKeyboardHook() {
            tbLog.AppendText("Setting Hook\r\n");
            globalLLKeyboardHookCallback = new HookProc(this.LowLevelKeyboardProc);

            hGlobalLLKeyboardHook = NativeMethods.SetWindowsHookEx(
                HookType.WH_KEYBOARD_LL,
                globalLLKeyboardHookCallback,
                Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]),
                0);
            return hGlobalLLKeyboardHook != IntPtr.Zero;
        }

        private bool RemoveGlobalLLKeyboardHook() {
            tbLog.AppendText("Removing Hook\r\n");
            if(hGlobalLLKeyboardHook != IntPtr.Zero) {
                if(!NativeMethods.UnhookWindowsHookEx(hGlobalLLKeyboardHook))
                    return false;
                hGlobalLLKeyboardHook = IntPtr.Zero;
            }
            return true;
        }

        public int LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam) {
            if(nCode >= 0) {
                KBDLLHOOKSTRUCT keyboardLLHookStruct = (KBDLLHOOKSTRUCT) Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));

                Keys vkCode = (Keys) keyboardLLHookStruct.vkCode;
                KeyboardMessage wmKeyboard = (KeyboardMessage) wParam;

                string log = string.Format("Virtual-Key code: {0} ({1})\r\n", vkCode, wmKeyboard);
                tbLog.AppendText(log);

                if(wmKeyboard == KeyboardMessage.WM_KEYDOWN) {
                    tbLog.AppendText("Key is down!\r\n");
                    if(!vkCode.Equals(currentVKCode)) {     //TODO: Fix - currentVK code refers to letter, not new key input
                        log = string.Format("CurrentVKCode = {0}\r\n", currentVKCode);
                        tbLog.AppendText(log);
                        tbLog.AppendText("Test\r\n");
                        if(delayTimer != null && delayTimer.Enabled == true) {
                            tbLog.AppendText("Stopping timer\r\n");
                            delayTimer.Stop();
                        }
                        if(currentVKCode != 0) {
                            tbLog.AppendText("Inputing new character!\r\n");
                            RemoveGlobalLLKeyboardHook();
                            InputSimulator.SimulateKeyPress((VirtualKeyCode) currentVKCode);
                            SetGlobalLLKeyboardHook();
                            currentVKCode = 0;
                        }

                        if(vkCode >= Keys.NumPad1 && vkCode <= Keys.NumPad9) {
                            tbLog.AppendText("I got here\r\n");


                            //switch(vkCode) {
                            //    case Keys.NumPad7:
                            //        currentVKCode = Keys.A;
                            //        break;
                            //    case Keys.NumPad8:
                            //        currentVKCode = Keys.D;
                            //        break;
                            //    case Keys.NumPad9:
                            //        currentVKCode = Keys.G;
                            //        break;
                            //    case Keys.NumPad4:
                            //        currentVKCode = Keys.J;
                            //        break;
                            //    case Keys.NumPad5:
                            //        currentVKCode = Keys.M;
                            //        break;
                            //    case Keys.NumPad6:
                            //        currentVKCode = Keys.P;
                            //        break;
                            //    case Keys.NumPad1:
                            //        currentVKCode = Keys.S;
                            //        break;
                            //    case Keys.NumPad2:
                            //        currentVKCode = Keys.V;
                            //        break;
                            //    case Keys.NumPad3:
                            //        currentVKCode = Keys.Y;
                            //        break;
                            //    default:
                            //        currentVKCode = 0;
                            //        break;
                            //}
                            delayTimer.Start();
                            return 1;
                        }
                        else {
                            tbLog.AppendText("Skipped past num switch\r\n");
                            return NativeMethods.CallNextHookEx(hGlobalLLKeyboardHook, nCode, wParam, lParam);
                        }
                    }
                    else {
                        tbLog.AppendText("Incrementing!\r\n");
                        currentVKCode += 1;
                        delayTimer.Start();
                        return 1;
                    }
                }
            }
            tbLog.AppendText("Somehow skipped everything\r\n");
            return NativeMethods.CallNextHookEx(hGlobalLLKeyboardHook, nCode, wParam, lParam);
        }
        #endregion

        #region Delay Timer
        private static double keyDelay = 1d;         //Delay in seconds
        System.Timers.Timer delayTimer;

        private void SetTimer() {
            tbLog.AppendText("Creating new timer!\r\n");
            delayTimer = new System.Timers.Timer(keyDelay * 1000);
            delayTimer.SynchronizingObject = this;
            delayTimer.Elapsed += DelayTimer_Elapsed;
        }

        private void DelayTimer_Elapsed(object sender, ElapsedEventArgs e) {
            if(currentVKCode != 0) {
                RemoveGlobalLLKeyboardHook();
                InputSimulator.SimulateKeyPress((VirtualKeyCode) currentVKCode);
                SetGlobalLLKeyboardHook();
                tbLog.AppendText("Timer elapsed\r\n");
                currentVKCode = 0;
                delayTimer.Stop();
                tbLog.AppendText("Post dispose\r\n");
            }
        }

        #endregion

        #region Layout

        char[][] layout = new char[9][];

        private void AssignLayout() {
            TextBox[] tbNums = new TextBox[] { tbNum1, tbNum2, tbNum3, tbNum4, tbNum5, tbNum6, tbNum7, tbNum8, tbNum9 };
            for(int i = 0; i < 9; ++i) {
                layout[i] = tbNums[i].Text.ToCharArray();
            }
        }

        #endregion

        private void btnGlobalLLKeyboardHook_Click(object sender, EventArgs e) {
            if(hGlobalLLKeyboardHook == IntPtr.Zero) {
                if(SetGlobalLLKeyboardHook()) {
                    btnGlobalLLKeyboardHook.Text = "Unhook";
                    AssignLayout();
                }
                else {
                    MessageBox.Show("SetWindowsHookEx(LL Keyboard) failed");
                }
            }

            else {
                if(RemoveGlobalLLKeyboardHook()) {
                    btnGlobalLLKeyboardHook.Text = "Hook";
                }
                else {
                    MessageBox.Show("UnhookWindowsHookEx(LL Keyboard) failed");
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            RemoveGlobalLLKeyboardHook();
        }
    }
}
