using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hexagonal.Domain.Entities.Profiles;

namespace Hexagonal.DTO.Request.Profiles
{
    /// <summary>
    /// Represents a profile area request data transfer object.
    /// </summary>
    public partial record AreaRequest
    {
        /// <summary>
        /// The area identifier.
        /// </summary>
        public required ProfileAreaType Area { get; init; }

        /// <summary>
        /// Indicates if the profile area can add.
        /// </summary>
        public required bool CanAdd { get; init; }

        /// <summary>
        /// Indicates if the profile area can edit.
        /// </summary>
        public required bool CanUpdate { get; init; }

        /// <summary>
        /// Indicates if the profile area can delete.
        /// </summary>
        public required bool CanDelete { get; init; }
    }
}
