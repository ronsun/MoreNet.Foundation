namespace MoreNet.Foundation.Conventions
{
    /// <summary>
    /// Represents a common API response with response data.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the response data.
    /// </typeparam>
    public class ApiResponse<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiResponse{T}"/> class.
        /// </summary>
        /// <param name="status">The common status value for the response.</param>
        /// <param name="data">The response data.</param>
        public ApiResponse(string status, T data)
        {
            Status = status;
            Data = data;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiResponse{T}"/> class with no response data.
        /// </summary>
        /// <param name="status">The common status value for the response.</param>
        protected ApiResponse(string status)
            : this(status, default)
        {
        }

        /// <summary>
        /// Gets the common status value for the response.
        /// </summary>
        public string Status { get; }

        /// <summary>
        /// Gets the response data.
        /// </summary>
        public T Data { get; }
    }
}
