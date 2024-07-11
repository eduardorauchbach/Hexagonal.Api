using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Hexagonal.DTO.Response.Users;

namespace Hexagonal.DTO.Response.Users
{
    /// <summary>
    /// Represents a response for user sign-in operation.
    /// </summary>
    [DataContract]
    public record SignInResponse
    {
        /// <summary>
        /// The user information associated with the sign-in.
        /// </summary>
        [DataMember]
        public Response User { get; init; }

        /// <summary>
        /// The authentication token generated during the sign-in.
        /// </summary>
        [DataMember]
        public string Token { get; init; }
    }

}
