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
                    return ProcessInput(vkCode, hGlobalLLKeyboardHook, nCode, wParam, lParam);
                }
            }
            tbLog.AppendText("Somehow skipped everything\r\n");
            return NativeMethods.CallNextHookEx(hGlobalLLKeyboardHook, nCode, wParam, lParam);
        }
        #endregion

        #region Process Input

        private Keys lastInput = 0;
        private int counter = 0;

        private void OutputQueuedKey() {

            //Check if valid key queued
            if(lastInput < Keys.NumPad1 || lastInput > Keys.NumPad9) {
                return;
            }

            //Convert numpad lastInput value to int
            int lastInputNum = (int) lastInput - 97; //NumPad1 = 0x61 = 97

            //Max out counter at last assigned key
            if(counter >= layout[lastInputNum].Length) {
                counter = layout[lastInputNum].Length - 1;
            }

            //Determine output key using layout
            char outputChar = layout[lastInputNum][counter];
            Keys outputKey = (Keys) char.ToUpper(outputChar);

            //Simulate keypress while avoiding getting caught in LLKB hook
            RemoveGlobalLLKeyboardHook();
            InputSimulator.SimulateKeyPress((VirtualKeyCode) outputKey);
            SetGlobalLLKeyboardHook();

            //Stop delayTimer if enabled
            if(delayTimer != null && delayTimer.Enabled) {
                delayTimer.Stop();
            }

            //Clear lastInput
            lastInput = 0;
            counter = 0;
        }

        private int ProcessInput(Keys vkCode, IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam) {

            //If keypress is on numpad layout
            if(vkCode >= Keys.NumPad1 && vkCode <= Keys.NumPad9) {
                //If this is a repeated keypress
                if(vkCode == lastInput) {
                    //Increment counter, intercept keypress
                    ++counter;
                    return 1;
                }

                //New numpad keypress
                else {
                    //Output queued key, if any.
                    OutputQueuedKey();

                    //Store new input, start delay timer, intercept keypress
                    lastInput = vkCode;
                    delayTimer.Start();
                    return 1;
                }

            }
            //Non-mapped key pressed. Output queued key (if any), and continue to output latest keypress.
            else {
                OutputQueuedKey();
                return NativeMethods.CallNextHookEx(hhk, nCode, wParam, lParam);
            }
        }

        #endregion

        #region Delay Timer

        private static double keyDelay = 0.5d;         //Delay in seconds
        System.Timers.Timer delayTimer;

        private void SetTimer() {
            tbLog.AppendText("Creating new timer!\r\n");
            delayTimer = new System.Timers.Timer(keyDelay * 1000);
            delayTimer.SynchronizingObject = this;
            delayTimer.Elapsed += DelayTimer_Elapsed;
        }

        private void DelayTimer_Elapsed(object sender, ElapsedEventArgs e) {
            tbLog.AppendText("Timer elapsed\r\n");
            OutputQueuedKey();
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

        #region Form Functions

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

        #endregion
    }
}
