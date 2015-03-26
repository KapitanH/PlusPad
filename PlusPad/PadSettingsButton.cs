// <copyright company="KapitanH" file="PadSettingsButton.cs">
// (C) 2015 KapitanH 
// </copyright>

namespace PlusPad
{
    using System.Runtime.Serialization;

    /// <summary>
    /// A gamepad button mapped to a key code.
    /// </summary>
    [DataContract]
    public class PadSettingsButton
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PadSettingsButton"/> class. 
        /// </summary>
        /// <param name="keyCode">The assigned key code.</param>
        public PadSettingsButton(KeyCode keyCode)
        {
            this.KeyCode = keyCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PadSettingsButton"/> class. 
        /// </summary>
        /// <param name="keyCode">The assigned key code.</param>
        /// <param name="keyCodeUp">The assigned key code for the released state.</param>
        public PadSettingsButton(KeyCode keyCode, byte keyCodeUp)
        {
            this.KeyCode = keyCode;
            this.KeyCodeUp = keyCodeUp;
        }

        /// <summary>
        /// Gets or sets the key code. 
        /// </summary>
        [DataMember]
        public KeyCode KeyCode { get; set; }

        /// <summary>
        /// Gets or sets the up key code.
        /// </summary>
        [DataMember]
        public byte KeyCodeUp { get; set; }
    }
}
