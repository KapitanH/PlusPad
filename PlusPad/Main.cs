// <copyright company="KapitanH" file="Main.cs">
// (C) 2015 KapitanH 
// </copyright>

namespace PlusPad
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Threading;
    using System.Windows.Forms;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    /// <summary>
    /// The main form.
    /// </summary>
    public partial class Main : Form
    {
        /// <summary>
        /// All key codes.
        /// </summary>
        private readonly List<KeyCode> allKeyCodes = new List<KeyCode>();

        /// <summary>
        /// The gamepad settings.
        /// </summary>
        private PadSettings padSettings;

        /// <summary>
        /// The worker for the main loop.
        /// </summary>
        private BackgroundWorker worker;

        /// <summary>
        /// The gamepad state since last loop iteration.
        /// </summary>
        private GamePadState previousGamePadState;

        /// <summary>
        /// Defines, if the pad buttons are checked or not.
        /// </summary>
        private bool isOn;

        /// <summary>
        /// Initializes a new instance of the <see cref="Main"/> class. 
        /// </summary>
        public Main()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Loads the main form and initializes mouse, keyboard, and background worker.
        /// </summary>
        /// <param name="sender">The calling object.</param>
        /// <param name="e">The calling parameters.</param>
        private void Main_Load(object sender, EventArgs e)
        {
            this.InitializeAllKeyCodes();
            
            this.padSettings = new PadSettings(this.allKeyCodes);
            this.padSettings.ResetKeys();
            
            this.worker = new BackgroundWorker();
            this.worker.DoWork += new DoWorkEventHandler(this.Worker_DoWork);
            this.worker.RunWorkerAsync();

            this.isOn = true;
        }

        /// <summary>
        /// Processes user input on a different thread.
        /// </summary>
        /// <param name="sender">The calling object.</param>
        /// <param name="e">The calling parameters.</param>
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);

                // Switch on/off
                if ((gamePadState.Buttons.Start == Microsoft.Xna.Framework.Input.ButtonState.Pressed
                    && gamePadState.Buttons.Back == Microsoft.Xna.Framework.Input.ButtonState.Pressed
                    && this.previousGamePadState.Buttons.Back == Microsoft.Xna.Framework.Input.ButtonState.Released)
                    || (gamePadState.Buttons.Back == Microsoft.Xna.Framework.Input.ButtonState.Pressed
                    && gamePadState.Buttons.Start == Microsoft.Xna.Framework.Input.ButtonState.Pressed
                    && this.previousGamePadState.Buttons.Start == Microsoft.Xna.Framework.Input.ButtonState.Released))
                {
                    this.isOn = !this.isOn;

                    this.lblStatus.Invoke((Action)delegate { this.lblStatus.Text = this.isOn ? "Enabled" : "Disabled"; });
                    this.lblStatus.Invoke((Action)delegate { this.lblStatus.ForeColor = this.isOn ? System.Drawing.Color.LimeGreen : System.Drawing.Color.Red; });
                }

                if (this.isOn)
                { 
                    // ESCAPE by Start Button
                    if (gamePadState.Buttons.Start == Microsoft.Xna.Framework.Input.ButtonState.Pressed
                        && this.previousGamePadState.Buttons.Start == Microsoft.Xna.Framework.Input.ButtonState.Released)
                    {
                        padSettings.PressKey(Win32.ScanCodeShort.ESCAPE, Win32.VirtualKeyShort.ESCAPE);
                        padSettings.ReleaseKey(Win32.ScanCodeShort.ESCAPE, Win32.VirtualKeyShort.ESCAPE);
                    }

                    // F3 by Back Button
                    if (gamePadState.Buttons.Back == Microsoft.Xna.Framework.Input.ButtonState.Pressed
                        && this.previousGamePadState.Buttons.Back == Microsoft.Xna.Framework.Input.ButtonState.Released)
                    {
                        padSettings.PressKey(Win32.ScanCodeShort.F3, Win32.VirtualKeyShort.F3);
                        padSettings.ReleaseKey(Win32.ScanCodeShort.F3, Win32.VirtualKeyShort.F3);
                    }

                    // X Button
                    if (gamePadState.Buttons.X == Microsoft.Xna.Framework.Input.ButtonState.Pressed
                        && this.previousGamePadState.Buttons.X == Microsoft.Xna.Framework.Input.ButtonState.Released)
                    {
                        Win32.keybd_event((byte)this.padSettings.X.KeyCode.Code, (byte)this.padSettings.X.KeyCode.Code, 0, 0);
                        Win32.keybd_event((byte)this.padSettings.X.KeyCode.Code, (byte)this.padSettings.X.KeyCode.Code, Win32.KeyEventfKeyUp, 0);
                    }

                    this.padSettings.PerformMove(gamePadState, this.previousGamePadState);

                    this.padSettings.PerformAction(gamePadState, this.previousGamePadState);

                    this.padSettings.PerformLook(gamePadState);

                    this.padSettings.PerformMouseAction(gamePadState, this.previousGamePadState);
                }

                this.previousGamePadState = gamePadState;

                Thread.Sleep(1);
            }
        }

        /// <summary>
        /// Fills the list of keys.
        /// </summary>
        private void InitializeAllKeyCodes()
        {
            this.allKeyCodes.Add(new KeyCode(Win32.KA, "A", 0));
            this.allKeyCodes.Add(new KeyCode(Win32.KB, "B", 0));
            this.allKeyCodes.Add(new KeyCode(Win32.KC, "C", 0));
            this.allKeyCodes.Add(new KeyCode(Win32.KD, "D", 0));
            this.allKeyCodes.Add(new KeyCode(Win32.KE, "E", 0));
            this.allKeyCodes.Add(new KeyCode(Win32.KF, "F", 0));
            this.allKeyCodes.Add(new KeyCode(Win32.KG, "G", 0));
            this.allKeyCodes.Add(new KeyCode(Win32.KH, "H", 0));
            this.allKeyCodes.Add(new KeyCode(Win32.KI, "I", 0));
            this.allKeyCodes.Add(new KeyCode(Win32.KJ, "J", 0));
            this.allKeyCodes.Add(new KeyCode(Win32.KK, "K", 0));
            this.allKeyCodes.Add(new KeyCode(Win32.KL, "L", 0));
            this.allKeyCodes.Add(new KeyCode(Win32.KM, "M", 0));
            this.allKeyCodes.Add(new KeyCode(Win32.KN, "N", 0));
            this.allKeyCodes.Add(new KeyCode(Win32.KO, "O", 0));
            this.allKeyCodes.Add(new KeyCode(Win32.KP, "P", 0));
            this.allKeyCodes.Add(new KeyCode(Win32.KQ, "Q", 0));
            this.allKeyCodes.Add(new KeyCode(Win32.KR, "R", 0));
            this.allKeyCodes.Add(new KeyCode(Win32.KS, "S", 0));
            this.allKeyCodes.Add(new KeyCode(Win32.KT, "T", 0));
            this.allKeyCodes.Add(new KeyCode(Win32.KU, "U", 0));
            this.allKeyCodes.Add(new KeyCode(Win32.KV, "V", 0));
            this.allKeyCodes.Add(new KeyCode(Win32.KW, "W", 0));
            this.allKeyCodes.Add(new KeyCode(Win32.KX, "X", 0));
            this.allKeyCodes.Add(new KeyCode(Win32.KY, "Y", 0));
            this.allKeyCodes.Add(new KeyCode(Win32.KZ, "Z", 0));
            this.allKeyCodes.Add(new KeyCode(Win32.K0, "0", 0));
            this.allKeyCodes.Add(new KeyCode(Win32.K1, "1", 0));
            this.allKeyCodes.Add(new KeyCode(Win32.K2, "2", 0));
            this.allKeyCodes.Add(new KeyCode(Win32.K3, "3", 0));
            this.allKeyCodes.Add(new KeyCode(Win32.K4, "4", 0));
            this.allKeyCodes.Add(new KeyCode(Win32.K5, "5", 0));
            this.allKeyCodes.Add(new KeyCode(Win32.K6, "6", 0));
            this.allKeyCodes.Add(new KeyCode(Win32.K7, "7", 0));
            this.allKeyCodes.Add(new KeyCode(Win32.K8, "8", 0));
            this.allKeyCodes.Add(new KeyCode(Win32.K9, "9", 0));
            this.allKeyCodes.Add(new KeyCode(Win32.VKOEMCOMMA, ",", 0));
            this.allKeyCodes.Add(new KeyCode(Win32.VKOEMPERIOD, ".", 0));
            this.allKeyCodes.Add(new KeyCode(Win32.VKSPACE, "SPACE", 0));
            this.allKeyCodes.Add(new KeyCode(Win32.VKLCONTROL, "LCTRL", 0));
            this.allKeyCodes.Add(new KeyCode(Win32.VKLSHIFT, "LSHIFT", 0));
            this.allKeyCodes.Add(new KeyCode(Win32.VKF5, "F5", 0));
            this.allKeyCodes.Add(new KeyCode(Win32.MouseEventfWheel, "Mouse Wheel Down", -1));
            this.allKeyCodes.Add(new KeyCode(Win32.MouseEventfWheel, "Mouse Wheel Up", 1));
            this.allKeyCodes.Add(new KeyCode(Win32.MouseEventfRightDown, "Mouse 2", 0));
            this.allKeyCodes.Add(new KeyCode(Win32.MouseEventfLeftDown, "Mouse 1", 0));
            this.allKeyCodes.Add(new KeyCode(Win32.VKF3, "F3", 0));
            this.allKeyCodes.Add(new KeyCode(Win32.VKESCAPE, "ESC", 0));
            this.allKeyCodes.Add(new KeyCode(0, string.Empty, 0));
            this.allKeyCodes.Add(new KeyCode(Win32.VKBACK, "BACKSPACE", 0));
            this.allKeyCodes.Add(new KeyCode(0, "Mouse +X", 0));
            this.allKeyCodes.Add(new KeyCode(0, "Mouse -X", 0));
            this.allKeyCodes.Add(new KeyCode(0, "Mouse -Y", 0));
            this.allKeyCodes.Add(new KeyCode(0, "Mouse +Y", 0));
            this.allKeyCodes.Add(new KeyCode(0, "Text - Next Char", 0));
            this.allKeyCodes.Add(new KeyCode(0, "Text - Prev Char", 0));
            this.allKeyCodes.Add(new KeyCode(0, "Text - Accept Char", 0));
        }
    }
}
