using System.Runtime.Serialization;
using Hexagonal.Domain.Entities.Profiles;

namespace Hexagonal.DTO.Response.Profiles
{
    /// <summary>
    /// Represents a profile area response data transfer object.
    /// </summary>
    [DataContract]
    public partial record AreaResponse
    {
        /// <summary>
        /// The area identifier.
        /// </summary>
        [DataMember]
        public ProfileAreaType Area { get; init; }

        /// <summary>
        /// Indicates if the profile area can add.
        /// </summary>
        [DataMember]
        public bool CanAdd { get; init; }

        /// <summary>
        /// Indicates if the profile area can edit.
        /// </summary>
        [DataMember]
        public bool CanUpdate { get; init; }

        /// <summary>
        /// Indicates if the profile area can delete.
        /// </summary>
        [DataMember]
        public bool CanDelete { get; init; }
    }
}
