namespace PlusPad
{
    public class KeyCode
    {       
        public KeyCode(int code, string name, int param)
        {
            this.Code = code;
            this.Name = name;
            this.Param = param;
        }

        public int Code { get; set; }

        public string Name { get; set; }

        public int Param { get; set; }
    }
}
