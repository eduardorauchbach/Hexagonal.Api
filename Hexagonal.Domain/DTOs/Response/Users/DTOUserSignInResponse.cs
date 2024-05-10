using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Hexagonal.Domain.DTOs.Response.Users
{
    /// <summary>
    /// Represents a response for user sign-in operation.
    /// </summary>
    [DataContract]
    public class DTOUserSignInResponse
    {
        /// <summary>
        /// The user information associated with the sign-in.
        /// </summary>
        [DataMember]
        public DTOUserResponse User { get; set; }

        /// <summary>
        /// The authentication token generated during the sign-in.
        /// </summary>
        [DataMember]
        public string Token { get; set; }
    }

}
