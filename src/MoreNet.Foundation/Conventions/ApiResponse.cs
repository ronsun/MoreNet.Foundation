namespace MoreNet.Foundation.Conventions
{
    /// <summary>
    /// Represents a common API response without response data.
    /// </summary>
    public class ApiResponse : ApiResponse<object>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiResponse"/> class.
        /// </summary>
        /// <param name="status">The common status value for the response.</param>
        public ApiResponse(string status)
            : base(status)
        {
        }
    }
}
