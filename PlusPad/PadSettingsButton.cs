namespace PlusPad
{
    using System.Runtime.Serialization;

    [DataContract]
    public class PadSettingsButton
    {
        public PadSettingsButton(KeyCode keyCode)
        {
            this.KeyCode = keyCode;
        }

        public PadSettingsButton(KeyCode keyCode, byte keyCodeUp)
        {
            this.KeyCode = keyCode;
            this.KeyCodeUp = keyCodeUp;
        }

        [DataMember]
        public KeyCode KeyCode { get; set; }

        [DataMember]
        public byte KeyCodeUp { get; set; }
    }
}
