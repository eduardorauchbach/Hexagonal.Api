using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagonal.DTO.Request.Profiles
{
    /// <summary>
    /// Represents a profile creation request data transfer object.
    /// </summary>
    public partial record CreateRequest
    {
        /// <summary>
        /// The name of the profile.
        /// </summary>
        public required string Name { get; init; }
    }
}
