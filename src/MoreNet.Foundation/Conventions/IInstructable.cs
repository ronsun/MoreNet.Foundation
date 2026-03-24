namespace MoreNet.Foundation.Conventions
{
    /// <summary>
    /// Represents a request model with optional instructions for a general-purpose API.
    /// </summary>
    /// <typeparam name="TInstruction">
    /// The request-specific instruction model, usually a nested type with boolean flags
    /// that tell the API to skip some work, hide some data, or do extra handling.
    /// </typeparam>
    public interface IInstructable<TInstruction>
    {
        /// <summary>
        /// Gets or sets the optional instructions for this request.
        /// Use <c>null</c> for the normal behavior.
        /// </summary>
        TInstruction Instruction { get; set; }
    }
}
