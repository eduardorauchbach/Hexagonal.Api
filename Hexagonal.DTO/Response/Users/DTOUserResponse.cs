using Newtonsoft.Json;
using Hexagonal.Common.DTO;
using System.ComponentModel;
using System.Runtime.Serialization;
using Hexagonal.Domain.Entities.Users;

namespace Hexagonal.DTOs.Response.Users
{
    [DataContract]
    public partial class DTOUserResponse : DTOResponse
    {
        /// <summary>
        /// User's name.
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// User's email number.
        /// </summary>
        [DataMember]
        public string Email { get; set; }

        /// <summary>
        /// Date and time of the user's last sign-in.
        /// </summary>
        [DataMember]
        [DefaultValue(null)]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime? LastSignIn { get; set; }

        /// <summary>
        /// Type of the user.
        /// </summary>
        [DataMember]
        public UserType Type { get; set; }

        /// <summary>
        /// Additional information about the user's status.
        /// </summary>
        [DataMember]
        public string StatusInfo { get; set; }

        /// <summary>
        /// Profile image name
        /// </summary>
        [DataMember]
        public string ProfileImage { get; set; }

        /// <summary>
        /// Profile image name URL
        /// </summary>
        [DataMember]
        public string ProfileImageUrl { get; private set; }

        /// <summary>
        /// Status of the user.
        /// </summary>
        [DataMember]
        public UserStatus Status { get; set; }
    }

}
