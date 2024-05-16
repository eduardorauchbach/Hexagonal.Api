using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Hexagonal.Common.DTO
{
    [DataContract]
    public abstract record DTOResponse
    {
        //
        // Summary:
        //     Main object Id
        [DataMember]
        [DefaultValue(null)]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public virtual Guid? Id { get; set; }

        //
        // Summary:
        //     Main object Created at Property
        [DataMember]
        [DefaultValue(null)]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public virtual DateTime CreatedAt { get; set; }

        //
        // Summary:
        //     Main object Updated at Property
        [DataMember]
        [DefaultValue(null)]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public virtual DateTime? UpdatedAt { get; set; }
    }
}
