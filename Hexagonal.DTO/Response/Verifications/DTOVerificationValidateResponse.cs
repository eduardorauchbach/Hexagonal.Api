using Hexagonal.Common.DTO;
using System.Runtime.Serialization;

namespace Hexagonal.DTOs.Response.Verifications
{
    /// <summary>
    /// Represents a response for a verification operation.
    /// </summary>
    [DataContract]
    public partial class DTOVerificationValidateResponse
    {
        /// <summary>
        /// Indicates whether the verification operation was successful.
        /// </summary>
        [DataMember]
        public bool Success { get; set; }

        /// <summary>
        /// The authentication token generated during the sign-in.
        /// </summary>
        [DataMember]
        public string Token { get; set; }
    }

}
