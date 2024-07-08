using Hexagonal.Common.Pagging;
using System.Runtime.Serialization;

namespace Hexagonal.DTO.Request.Users
{
    /// <summary>
    /// Represents a request to get all orders with filters.
    /// </summary>
    [DataContract]
    public partial record GetAllRequest : PageRequest
    {
        /// <summary>
        /// Name or part of the name of the user
        /// </summary>
        [DataMember]
        public string? Name { get; set; }

        /// <summary>
        /// If the user is Admin or Not
        /// </summary>
        [DataMember]
        public required bool IsAdmin { get; init; }
    }
}
