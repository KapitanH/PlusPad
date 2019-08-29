// <copyright company="KapitanH" file="PadSettings.cs">
// (C) 2015 KapitanH 
// </copyright>

namespace PlusPad
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Windows.Forms;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    /// <summary>
    /// The gamepad settings.
    /// </summary>
    [DataContract]
    public class PadSettings
    {
        /// <summary>
        /// The mouse speed.
        /// </summary>
        private const float MouseSpeed = 3f;

        /// <summary>
        /// The dead zone area.
        /// </summary>
        private const float ThumbStickThreshold = 0.35f;

        /// <summary>
        /// The current position in the list of keys.
        /// </summary>
        private int keyIndex;

        /// <summary>
        /// Confirms the input of a key.
        /// </summary>
        private bool nextKeyModifier;

        /// <summary>
        /// The mouse position.
        /// </summary>
        private Vector2 mousePosition;

        /// <summary>
        /// Defines the pressed state of the right analogue stick.
        /// </summary>
        private bool rightStickClicked = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="PadSettings"/> class.
        /// </summary>
        /// <param name="keyCodes">The key codes the settings are loaded with.</param>
        public PadSettings(List<KeyCode> keyCodes)
        {
            this.InitializePadKeyCodes(keyCodes);
            this.InitializeTextKeyCodes(keyCodes);

            this.mousePosition = Vector2.Zero;
        }

        /// <summary>
        /// Gets or sets the key codes used for text input. 
        /// </summary>
        public List<KeyCode> TextKeyCodes { get; set; }

        #region Buttons
        /// <summary>
        /// Gets or sets button L3.
        /// </summary>
        [DataMember]
        public PadSettingsButton L3 { get; set; }

        /// <summary>
        /// Gets or sets button R3.
        /// </summary>
        [DataMember]
        public PadSettingsButton R3 { get; set; }

        /// <summary>
        /// Gets or sets button X. 
        /// </summary>
        [DataMember]
        public PadSettingsButton X { get; set; }

        /// <summary>
        /// Gets or sets button Y. 
        /// </summary>
        [DataMember]
        public PadSettingsButton Y { get; set; }

        /// <summary>
        /// Gets or sets button A. 
        /// </summary>
        [DataMember]
        public PadSettingsButton A { get; set; }

        /// <summary>
        /// Gets or sets button B. 
        /// </summary>
        [DataMember]
        public PadSettingsButton B { get; set; }

        /// <summary>
        /// Gets or sets button Back.
        /// </summary>
        [DataMember]
        public PadSettingsButton Back { get; set; }

        /// <summary>
        /// Gets or sets button Start.
        /// </summary>
        [DataMember]
        public PadSettingsButton Start { get; set; }

        /// <summary>
        /// Gets or sets button LB. 
        /// </summary>
        [DataMember]
        public PadSettingsButton LB { get; set; }

        /// <summary>
        /// Gets or sets button RB. 
        /// </summary>
        [DataMember]
        public PadSettingsButton RB { get; set; }

        /// <summary>
        /// Gets or sets button LT. 
        /// </summary>
        [DataMember]
        public PadSettingsButton LT { get; set; }

        /// <summary>
        /// Gets or sets button RT. 
        /// </summary>
        [DataMember]
        public PadSettingsButton RT { get; set; }

        /// <summary>
        /// Gets or sets button D-Pad up.
        /// </summary>
        [DataMember]
        public PadSettingsButton DPadUp { get; set; }

        /// <summary>
        /// Gets or sets button D-Pad down.
        /// </summary>
        [DataMember]
        public PadSettingsButton DPadDown { get; set; }

        /// <summary>
        /// Gets or sets button D-Pad left.
        /// </summary>
        [DataMember]
        public PadSettingsButton DPadLeft { get; set; }

        /// <summary>
        /// Gets or sets button D-Pad right.
        /// </summary>
        [DataMember]
        public PadSettingsButton DPadRight { get; set; }

        /// <summary>
        /// Gets or sets left stick up. 
        /// </summary>
        [DataMember]
        public PadSettingsButton LeftStickUp { get; set; }

        /// <summary>
        /// Gets or sets left stick up. 
        /// </summary>
        [DataMember]
        public PadSettingsButton LeftStickDown { get; set; }

        /// <summary>
        /// Gets or sets left stick left.
        /// </summary>
        [DataMember]
        public PadSettingsButton LeftStickLeft { get; set; }

        /// <summary>
        /// Gets or sets left stick right.
        /// </summary>
        [DataMember]
        public PadSettingsButton LeftStickRight { get; set; }

        /// <summary>
        /// Gets or sets right stick up.
        /// </summary>
        [DataMember]
        public PadSettingsButton RightStickUp { get; set; }

        /// <summary>
        /// Gets or sets right stick up.
        /// </summary>
        [DataMember]
        public PadSettingsButton RightStickDown { get; set; }

        /// <summary>
        /// Gets or sets right stick left.
        /// </summary>
        [DataMember]
        public PadSettingsButton RightStickLeft { get; set; }

        /// <summary>
        /// Gets or sets right stick right.
        /// </summary>
        [DataMember]
        public PadSettingsButton RightStickRight { get; set; }
        #endregion

        /// <summary>
        /// Resets the access parameters for the list of keys.
        /// </summary>
        public void ResetKeys()
        {
            this.nextKeyModifier = true;
            this.keyIndex = 0;
        }

        /// <summary>
        /// Processes Movement.
        /// </summary>
        /// <param name="gamePadState">The current gamepad state.</param>
        /// <param name="previousGamePadState">The previous gamepad state.</param>
        public void PerformMove(GamePadState gamePadState, GamePadState previousGamePadState)
        {
            // A Key by negative Left ThumbStick X
            if (gamePadState.ThumbSticks.Left.X < -ThumbStickThreshold && previousGamePadState.ThumbSticks.Left.X >= -ThumbStickThreshold)
            {
                PressKey(Win32.ScanCodeShort.KEY_A, Win32.VirtualKeyShort.KEY_A);
            }
            else if (previousGamePadState.ThumbSticks.Left.X < -ThumbStickThreshold && gamePadState.ThumbSticks.Left.X >= -ThumbStickThreshold)
            {
                ReleaseKey(Win32.ScanCodeShort.KEY_A, Win32.VirtualKeyShort.KEY_A);
            }

            // D Key by positive Left ThumbStick X
            if (gamePadState.ThumbSticks.Left.X > ThumbStickThreshold && previousGamePadState.ThumbSticks.Left.X <= ThumbStickThreshold)
            {
                PressKey(Win32.ScanCodeShort.KEY_D, Win32.VirtualKeyShort.KEY_D);
            }
            else if (previousGamePadState.ThumbSticks.Left.X > ThumbStickThreshold && gamePadState.ThumbSticks.Left.X <= ThumbStickThreshold)
            {
                ReleaseKey(Win32.ScanCodeShort.KEY_D, Win32.VirtualKeyShort.KEY_D);
            }

            // S Key by negative Left ThumbStick Y
            if (gamePadState.ThumbSticks.Left.Y < -ThumbStickThreshold && previousGamePadState.ThumbSticks.Left.Y >= -ThumbStickThreshold)
            {
                PressKey(Win32.ScanCodeShort.KEY_S, Win32.VirtualKeyShort.KEY_S);
            }
            else if (previousGamePadState.ThumbSticks.Left.Y < -ThumbStickThreshold && gamePadState.ThumbSticks.Left.Y >= -ThumbStickThreshold)
            {
                ReleaseKey(Win32.ScanCodeShort.KEY_S, Win32.VirtualKeyShort.KEY_S);
            }

            // W Key by positive Left ThumbStick Y
            if (gamePadState.ThumbSticks.Left.Y > ThumbStickThreshold && previousGamePadState.ThumbSticks.Left.Y <= ThumbStickThreshold)
            {
                PressKey(Win32.ScanCodeShort.KEY_W, Win32.VirtualKeyShort.KEY_W);
            }
            else if (previousGamePadState.ThumbSticks.Left.Y > ThumbStickThreshold && gamePadState.ThumbSticks.Left.Y <= ThumbStickThreshold)
            {
                ReleaseKey(Win32.ScanCodeShort.KEY_W, Win32.VirtualKeyShort.KEY_W);
            }

            // Space Key by A Button
            if (gamePadState.Buttons.A == Microsoft.Xna.Framework.Input.ButtonState.Pressed
                && previousGamePadState.Buttons.A == Microsoft.Xna.Framework.Input.ButtonState.Released)
            {
                PressKey(Win32.ScanCodeShort.SPACE, Win32.VirtualKeyShort.SPACE);
            }
            else if (previousGamePadState.Buttons.A == Microsoft.Xna.Framework.Input.ButtonState.Pressed
                && gamePadState.Buttons.A == Microsoft.Xna.Framework.Input.ButtonState.Released)
            {
                ReleaseKey(Win32.ScanCodeShort.SPACE, Win32.VirtualKeyShort.SPACE);
            }
        }

        /// <summary>
        /// Processes actions.
        /// </summary>
        /// <param name="gamePadState">The current gamepad state.</param>
        /// <param name="previousGamePadState">The previous gamepad state.</param>
        public void PerformAction(GamePadState gamePadState, GamePadState previousGamePadState)
        {
            // Q by B
            if (gamePadState.Buttons.B == Microsoft.Xna.Framework.Input.ButtonState.Pressed
                && previousGamePadState.Buttons.B == Microsoft.Xna.Framework.Input.ButtonState.Released)
            {
                PressKey(Win32.ScanCodeShort.KEY_Q, Win32.VirtualKeyShort.KEY_Q);
                ReleaseKey(Win32.ScanCodeShort.KEY_Q, Win32.VirtualKeyShort.KEY_Q);
            }

            // E by Y
            if (gamePadState.Buttons.Y == Microsoft.Xna.Framework.Input.ButtonState.Pressed
                && previousGamePadState.Buttons.Y == Microsoft.Xna.Framework.Input.ButtonState.Released)
            {
                PressKey(Win32.ScanCodeShort.KEY_E, Win32.VirtualKeyShort.KEY_E);
                ReleaseKey(Win32.ScanCodeShort.KEY_E, Win32.VirtualKeyShort.KEY_E);

                ReleaseKey(Win32.ScanCodeShort.LSHIFT, Win32.VirtualKeyShort.LSHIFT);

                this.rightStickClicked = false;
            }

            // LCTRL by L3
            if (gamePadState.Buttons.LeftStick == Microsoft.Xna.Framework.Input.ButtonState.Pressed
                && previousGamePadState.Buttons.LeftStick == Microsoft.Xna.Framework.Input.ButtonState.Released)
            {
                PressKey(Win32.ScanCodeShort.LCONTROL, Win32.VirtualKeyShort.LCONTROL);
            }
            else if (gamePadState.Buttons.LeftStick == Microsoft.Xna.Framework.Input.ButtonState.Released
                && previousGamePadState.Buttons.LeftStick == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                ReleaseKey(Win32.ScanCodeShort.LCONTROL, Win32.VirtualKeyShort.LCONTROL);
            }

            // LSHIFT by R3
            if (gamePadState.Buttons.RightStick == Microsoft.Xna.Framework.Input.ButtonState.Pressed
                && previousGamePadState.Buttons.RightStick == Microsoft.Xna.Framework.Input.ButtonState.Released)
            {
                if (!this.rightStickClicked)
                {
                    PressKey(Win32.ScanCodeShort.LSHIFT, Win32.VirtualKeyShort.LSHIFT);
                    this.rightStickClicked = true;
                }
                else
                {
                    ReleaseKey(Win32.ScanCodeShort.LSHIFT, Win32.VirtualKeyShort.LSHIFT);
                    this.rightStickClicked = false;
                }
            }

            // D-Pad left
            if (gamePadState.DPad.Left == Microsoft.Xna.Framework.Input.ButtonState.Pressed
                && previousGamePadState.DPad.Left == Microsoft.Xna.Framework.Input.ButtonState.Released)
            {
                Win32.keybd_event((byte)this.DPadLeft.KeyCode.Code, (byte)this.DPadLeft.KeyCode.Code, 0, 0);
                Win32.keybd_event((byte)this.DPadLeft.KeyCode.Code, (byte)this.DPadLeft.KeyCode.Code, Win32.KeyEventfKeyUp, 0);

                this.ResetKeys();
            }

            // D-Pad right
            if (gamePadState.DPad.Right == Microsoft.Xna.Framework.Input.ButtonState.Pressed
                && previousGamePadState.DPad.Right == Microsoft.Xna.Framework.Input.ButtonState.Released)
            {
                this.ResetKeys();
            }

            // D-Pad up
            if (gamePadState.DPad.Up == Microsoft.Xna.Framework.Input.ButtonState.Pressed
                && previousGamePadState.DPad.Up == Microsoft.Xna.Framework.Input.ButtonState.Released)
            {
                if (!this.nextKeyModifier)
                {
                    this.keyIndex++;
                    if (this.keyIndex >= this.TextKeyCodes.Count)
                    {
                        this.keyIndex = 0;
                    }

                    Win32.keybd_event(Win32.VKBACK, Win32.VKBACK, 0, 0);
                    Win32.keybd_event(Win32.VKBACK, Win32.VKBACK, Win32.KeyEventfKeyUp, 0);
                }

                Win32.keybd_event((byte)this.TextKeyCodes[this.keyIndex].Code, (byte)this.TextKeyCodes[this.keyIndex].Code, 0, 0);
                Win32.keybd_event((byte)this.TextKeyCodes[this.keyIndex].Code, (byte)this.TextKeyCodes[this.keyIndex].Code, Win32.KeyEventfKeyUp, 0);

                this.nextKeyModifier = false;
            }

            // D-Pad down
            if (gamePadState.DPad.Down == Microsoft.Xna.Framework.Input.ButtonState.Pressed
                && previousGamePadState.DPad.Down == Microsoft.Xna.Framework.Input.ButtonState.Released)
            {
                this.keyIndex--;
                if (this.keyIndex < 0)
                {
                    this.keyIndex = this.TextKeyCodes.Count - 1;
                }

                if (!this.nextKeyModifier)
                {
                    Win32.keybd_event(Win32.VKBACK, Win32.VKBACK, 0, 0);
                    Win32.keybd_event(Win32.VKBACK, Win32.VKBACK, Win32.KeyEventfKeyUp, 0);
                }

                Win32.keybd_event((byte)this.TextKeyCodes[this.keyIndex].Code, (byte)this.TextKeyCodes[this.keyIndex].Code, 0, 0);
                Win32.keybd_event((byte)this.TextKeyCodes[this.keyIndex].Code, (byte)this.TextKeyCodes[this.keyIndex].Code, Win32.KeyEventfKeyUp, 0);

                this.nextKeyModifier = false;
            }
        }

        /// <summary>
        /// Processes mouse movement.
        /// </summary>
        /// <param name="gamePadState">The current gamepad state.</param>
        public void PerformLook(GamePadState gamePadState)
        {
            // Mouse position by right ThumbStick
            int oldX = Cursor.Position.X;
            int oldY = Cursor.Position.Y;

            if (gamePadState.ThumbSticks.Right.X != 0 || gamePadState.ThumbSticks.Right.Y != 0)
            {
                this.mousePosition.X += gamePadState.ThumbSticks.Right.X * MouseSpeed;
                this.mousePosition.Y += gamePadState.ThumbSticks.Right.Y * MouseSpeed;
            }

            Cursor.Position = new System.Drawing.Point(Cursor.Position.X + (int)this.mousePosition.X, Cursor.Position.Y - (int)this.mousePosition.Y);

            MoveMouse();

            if (oldX != Cursor.Position.X)
            {
                this.mousePosition.X = 0;
            }

            if (oldY != Cursor.Position.Y)
            {
                this.mousePosition.Y = 0;
            }
        }

        /// <summary>
        /// Processes mouse actions.
        /// </summary>
        /// <param name="gamePadState">The current gamepad state.</param>
        /// <param name="previousGamePadState">The previous gamepad state.</param>
        public void PerformMouseAction(GamePadState gamePadState, GamePadState previousGamePadState)
        {
            // RT
            if (gamePadState.Triggers.Right > 0f && previousGamePadState.Triggers.Right <= 0f)
            {
                Win32.mouse_event((byte)this.RT.KeyCode.Code, Cursor.Position.X, Cursor.Position.Y, 0, 0);
            }
            else if (previousGamePadState.Triggers.Right > 0f && gamePadState.Triggers.Right <= 0f)
            {
                Win32.mouse_event(this.RT.KeyCodeUp, Cursor.Position.X, Cursor.Position.Y, 0, 0);
                this.ResetKeys();
            }

            // LT
            if (gamePadState.Triggers.Left > 0f && previousGamePadState.Triggers.Left <= 0f)
            {
                Win32.mouse_event((byte)this.LT.KeyCode.Code, Cursor.Position.X, Cursor.Position.Y, 0, 0);
            }
            else if (previousGamePadState.Triggers.Left > 0f && gamePadState.Triggers.Left <= 0f)
            {
                Win32.mouse_event(this.LT.KeyCodeUp, Cursor.Position.X, Cursor.Position.Y, 0, 0);
            }

            // LB
            if (gamePadState.Buttons.LeftShoulder == Microsoft.Xna.Framework.Input.ButtonState.Pressed
                && previousGamePadState.Buttons.LeftShoulder == Microsoft.Xna.Framework.Input.ButtonState.Released)
            {
                Win32.mouse_event((uint)this.LB.KeyCode.Code, Cursor.Position.X, Cursor.Position.Y, this.LB.KeyCode.Param, 0);
            }

            // RB
            if (gamePadState.Buttons.RightShoulder == Microsoft.Xna.Framework.Input.ButtonState.Pressed
                && previousGamePadState.Buttons.RightShoulder == Microsoft.Xna.Framework.Input.ButtonState.Released)
            {
                Win32.mouse_event((uint)this.RB.KeyCode.Code, Cursor.Position.X, Cursor.Position.Y, this.RB.KeyCode.Param, 0);
            }
        }

        /// <summary>
        /// Initializes the pad buttons used for triggering the keys.
        /// </summary>
        /// <param name="keyCodes">The list of all key codes.</param>
        private void InitializePadKeyCodes(List<KeyCode> keyCodes)
        {
            this.L3 = new PadSettingsButton(keyCodes.FirstOrDefault(code => code.Name == "LCTRL"));
            this.R3 = new PadSettingsButton(keyCodes.FirstOrDefault(code => code.Name == "LSHIFT"));
            this.A = new PadSettingsButton(keyCodes.FirstOrDefault(code => code.Name == "SPACE"));
            this.B = new PadSettingsButton(keyCodes.FirstOrDefault(code => code.Name == "Q"));
            this.X = new PadSettingsButton(keyCodes.FirstOrDefault(code => code.Name == "F5"));
            this.Y = new PadSettingsButton(keyCodes.FirstOrDefault(code => code.Name == "E"));
            this.LB = new PadSettingsButton(keyCodes.FirstOrDefault(code => code.Name == "Mouse Wheel Up"));
            this.RB = new PadSettingsButton(keyCodes.FirstOrDefault(code => code.Name == "Mouse Wheel Down"));
            this.LT = new PadSettingsButton(keyCodes.FirstOrDefault(code => code.Name == "Mouse 2"), Win32.MouseEventfRightUp);
            this.RT = new PadSettingsButton(keyCodes.FirstOrDefault(code => code.Name == "Mouse 1"), Win32.MouseEventfLeftUp);
            this.Back = new PadSettingsButton(keyCodes.FirstOrDefault(code => code.Name == "F3"));
            this.Start = new PadSettingsButton(keyCodes.FirstOrDefault(code => code.Name == "ESC"));
            this.DPadUp = new PadSettingsButton(keyCodes.FirstOrDefault(code => code.Name == "Text - Next Char"));
            this.DPadDown = new PadSettingsButton(keyCodes.FirstOrDefault(code => code.Name == "Text - Prev Char"));
            this.DPadLeft = new PadSettingsButton(keyCodes.FirstOrDefault(code => code.Name == "BACKSPACE"));
            this.DPadRight = new PadSettingsButton(keyCodes.FirstOrDefault(code => code.Name == "Text - Accept Char"));
            this.LeftStickUp = new PadSettingsButton(keyCodes.FirstOrDefault(code => code.Name == "W"));
            this.LeftStickDown = new PadSettingsButton(keyCodes.FirstOrDefault(code => code.Name == "S"));
            this.LeftStickLeft = new PadSettingsButton(keyCodes.FirstOrDefault(code => code.Name == "A"));
            this.LeftStickRight = new PadSettingsButton(keyCodes.FirstOrDefault(code => code.Name == "D"));
            this.RightStickUp = new PadSettingsButton(keyCodes.FirstOrDefault(code => code.Name == "Mouse +Y"));
            this.RightStickDown = new PadSettingsButton(keyCodes.FirstOrDefault(code => code.Name == "Mouse -Y"));
            this.RightStickLeft = new PadSettingsButton(keyCodes.FirstOrDefault(code => code.Name == "Mouse -X"));
            this.RightStickRight = new PadSettingsButton(keyCodes.FirstOrDefault(code => code.Name == "Mouse +X"));
        }

        /// <summary>
        /// Initializes the key codes used for text input.
        /// </summary>
        /// <param name="keyCodes">The list of all key codes.</param>
        private void InitializeTextKeyCodes(List<KeyCode> keyCodes)
        {
            this.TextKeyCodes = new List<KeyCode>
                                    {
                                        keyCodes.FirstOrDefault(code => code.Name == "A"),
                                        keyCodes.FirstOrDefault(code => code.Name == "B"),
                                        keyCodes.FirstOrDefault(code => code.Name == "C"),
                                        keyCodes.FirstOrDefault(code => code.Name == "D"),
                                        keyCodes.FirstOrDefault(code => code.Name == "E"),
                                        keyCodes.FirstOrDefault(code => code.Name == "F"),
                                        keyCodes.FirstOrDefault(code => code.Name == "G"),
                                        keyCodes.FirstOrDefault(code => code.Name == "H"),
                                        keyCodes.FirstOrDefault(code => code.Name == "I"),
                                        keyCodes.FirstOrDefault(code => code.Name == "J"),
                                        keyCodes.FirstOrDefault(code => code.Name == "K"),
                                        keyCodes.FirstOrDefault(code => code.Name == "L"),
                                        keyCodes.FirstOrDefault(code => code.Name == "M"),
                                        keyCodes.FirstOrDefault(code => code.Name == "N"),
                                        keyCodes.FirstOrDefault(code => code.Name == "O"),
                                        keyCodes.FirstOrDefault(code => code.Name == "P"),
                                        keyCodes.FirstOrDefault(code => code.Name == "Q"),
                                        keyCodes.FirstOrDefault(code => code.Name == "R"),
                                        keyCodes.FirstOrDefault(code => code.Name == "S"),
                                        keyCodes.FirstOrDefault(code => code.Name == "T"),
                                        keyCodes.FirstOrDefault(code => code.Name == "U"),
                                        keyCodes.FirstOrDefault(code => code.Name == "V"),
                                        keyCodes.FirstOrDefault(code => code.Name == "W"),
                                        keyCodes.FirstOrDefault(code => code.Name == "X"),
                                        keyCodes.FirstOrDefault(code => code.Name == "Y"),
                                        keyCodes.FirstOrDefault(code => code.Name == "Z"),
                                        keyCodes.FirstOrDefault(code => code.Name == "0"),
                                        keyCodes.FirstOrDefault(code => code.Name == "1"),
                                        keyCodes.FirstOrDefault(code => code.Name == "2"),
                                        keyCodes.FirstOrDefault(code => code.Name == "3"),
                                        keyCodes.FirstOrDefault(code => code.Name == "4"),
                                        keyCodes.FirstOrDefault(code => code.Name == "5"),
                                        keyCodes.FirstOrDefault(code => code.Name == "6"),
                                        keyCodes.FirstOrDefault(code => code.Name == "7"),
                                        keyCodes.FirstOrDefault(code => code.Name == "8"),
                                        keyCodes.FirstOrDefault(code => code.Name == "9"),
                                        keyCodes.FirstOrDefault(code => code.Name == ","),
                                        keyCodes.FirstOrDefault(code => code.Name == "."),
                                        keyCodes.FirstOrDefault(code => code.Name == "SPACE")
                                    };
        }

        void PressKey(Win32.ScanCodeShort scanCode, Win32.VirtualKeyShort virtualKey)
        {
            var keybInput = new Win32.KEYBDINPUT();
            keybInput.wScan = scanCode;
            keybInput.wVk = virtualKey;

            var inputunion = new Win32.InputUnion();
            inputunion.ki = keybInput;

            var input = new Win32.INPUT();
            input.type = (uint)Win32.InputTypes.INPUT_KEYBOARD; 
            input.U = inputunion;

            Win32.INPUT[] inputs = { input };

            Win32.SendInput((uint)inputs.Length, inputs, Win32.INPUT.Size);
        }

        void ReleaseKey(Win32.ScanCodeShort scanCode, Win32.VirtualKeyShort virtualKey)
        {
            var keybInput = new Win32.KEYBDINPUT();
            keybInput.wScan = scanCode;
            keybInput.wVk = virtualKey;
            keybInput.dwFlags = Win32.KEYEVENTF.KEYUP;

            var inputunion = new Win32.InputUnion();
            inputunion.ki = keybInput;

            var input = new Win32.INPUT();
            input.type = (uint)Win32.InputTypes.INPUT_KEYBOARD;
            input.U = inputunion;

            Win32.INPUT[] inputs = { input };

            Win32.SendInput((uint)inputs.Length, inputs, Win32.INPUT.Size);
        }

        void MoveMouse()
        {
            var mouseInput = new Win32.MOUSEINPUT();
            mouseInput.dx = (int)mousePosition.X;
            mouseInput.dy = -(int)mousePosition.Y;
            mouseInput.dwFlags = Win32.MOUSEEVENTF.MOVE;

            var inputunion = new Win32.InputUnion();
            inputunion.mi = mouseInput;

            var input = new Win32.INPUT();
            input.type = (uint)Win32.InputTypes.INPUT_MOUSE;
            input.U = inputunion;

            Win32.INPUT[] inputs = { input };

            Win32.SendInput((uint)inputs.Length, inputs, Win32.INPUT.Size);
        }
    }
}
