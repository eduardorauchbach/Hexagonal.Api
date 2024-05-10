using Hexagonal.Domain.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagonal.Domain.DTOs.Request.Users
{
    /// <summary>
    /// Represents a user creation request data transfer object.
    /// </summary>
    public partial class DTOUserCreateRequest
    {
        /// <summary>
        /// The name of the user.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// The email number of the user.
        /// </summary>
        public required string Email { get; set; }

        /// <summary>
        /// The password of the user.
        /// </summary>
        public required string Password { get; set; }
    }

}
