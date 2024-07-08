using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagonal.DTO.Request.Profiles
{
    /// <summary>
    /// Represents a profile patch request data transfer object.
    /// </summary>
    public partial record PatchRequest
    {
        /// <summary>
        /// The name of the profile.
        /// </summary>
        public string? Name { get; init; }

        /// <summary>
        /// Indicates if the profile has admin privileges.
        /// </summary>
        public bool? IsAdmin { get; init; }

        /// <summary>
        /// The areas associated with the profile.
        /// </summary>
        public List<AreaRequest>? ProfileAreas { get; init; }
    }
}
