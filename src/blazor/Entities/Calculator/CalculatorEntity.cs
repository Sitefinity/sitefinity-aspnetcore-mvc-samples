using System.ComponentModel;

namespace blazor.Entities.Calculator
{
    /// <summary>
    /// The test model for the load tests widget.
    /// </summary>
    public class CalculatorEntity
    {
        /// <summary>
        /// Gets or sets a value indicating whether a boolean property is true or false.
        /// </summary>
        [DefaultValue("Basic Calculator Demo Using Blazor")]
        public string Title { get; set; }
    }
}
