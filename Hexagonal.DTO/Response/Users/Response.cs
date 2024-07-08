using Newtonsoft.Json;
using Hexagonal.Common.DTO;
using System.ComponentModel;
using System.Runtime.Serialization;
using Hexagonal.Domain.Entities.Users;
using Hexagonal.Domain.Entities.Profiles;

namespace Hexagonal.DTO.Response.Users
{
    [DataContract]
    public partial record Response : DTOResponse
    {
        /// <summary>
        /// Permission Profile Data
        /// </summary>
        [DataMember]
        public Profiles.Response? Profile { get; set; }

        /// <summary>
        /// Permission profile Id
        /// </summary>
        [DataMember]
        public Guid? ProfileId { get; init; }

        /// <summary>
        /// User's name.
        /// </summary>
        [DataMember]
        public string Name { get; init; }

        /// <summary>
        /// User's email.
        /// </summary>
        [DataMember]
        public string Email { get; init; }

        /// <summary>
        /// Date and time of the user's last sign-in.
        /// </summary>
        [DataMember]
        [DefaultValue(null)]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime? LastSignIn { get; init; }

        /// <summary>
        /// User's profile image name
        /// </summary>
        [DataMember]
        public string ProfileImage { get; init; }

        /// <summary>
        /// User's profile image URL
        /// </summary>
        [DataMember]
        public string ProfileImageUrl { get; protected init; }

        /// <summary>
        /// Status of the user.
        /// </summary>
        [DataMember]
        public UserStatus Status { get; init; }

        /// <summary>
        /// If the user has access to Admin Area.
        /// </summary>
        [DataMember]
        public bool IsAdmin { get; init; }
    }
}
