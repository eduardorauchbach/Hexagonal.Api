using System.Runtime.Serialization;
using Hexagonal.Common.DTO;

namespace Hexagonal.DTO.Response.Profiles
{
    /// <summary>
    /// Represents a profile response data transfer object.
    /// </summary>
    [DataContract]
    public partial record Response : DTOResponse
    {
        /// <summary>
        /// The name of the profile.
        /// </summary>
        [DataMember]
        public string Name { get; init; }

        /// <summary>
        /// The areas associated with the profile.
        /// </summary>
        [DataMember]
        public List<AreaResponse>? ProfileAreas { get; init; }
    }

}
