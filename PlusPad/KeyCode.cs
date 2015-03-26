// <copyright company="KapitanH" file="KeyCode.cs">
// (C) 2015 KapitanH 
// </copyright>

namespace PlusPad
{
    /// <summary>
    /// A key code with additional properties.
    /// </summary>
    public class KeyCode
    {       
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyCode"/> class.
        /// </summary>
        /// <param name="code">The key code.</param>
        /// <param name="name">The name.</param>
        /// <param name="param">A parameter.</param>
        public KeyCode(int code, string name, int param)
        {
            this.Code = code;
            this.Name = name;
            this.Param = param;
        }

        /// <summary>
        /// Gets or sets the key code.
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a parameter.
        /// </summary>
        public int Param { get; set; }
    }
}
