using Hexagonal.Common.DTO;
using System.Runtime.Serialization;

namespace Hexagonal.DTOs.Response.Verifications
{
    /// <summary>
    /// Represents a response for creating a verification.
    /// </summary>
    [DataContract]
    public partial class DTOVerificationCreateResponse : DTOResponse
    {
        /// <summary>
        /// Expiration date and time of the verification.
        /// </summary>
        [DataMember]
        public DateTime ExpiresAt;
    }

}
