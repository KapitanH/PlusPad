// <copyright company="Kai Hartmann" file="ComboBoxItem.cs">
// (C) 2015 Kai Hartmann 
// </copyright>

namespace PlusPad
{
    /// <summary>
    /// Ein Item für die Comboboxen.
    /// </summary>
    public class ComboBoxItem
    {
        /// <summary>
        /// Gets or sets the text. Gibt den Text zurück, oder legt diesen fest.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the value. Gibt den Wert zurück, oder legt diesen fest.
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Wird von den Comboboxen für die Anzeige verwendet.
        /// </summary>
        /// <returns>Den Text.</returns>
        public override string ToString()
        {
            return this.Text;
        }
    }
}
