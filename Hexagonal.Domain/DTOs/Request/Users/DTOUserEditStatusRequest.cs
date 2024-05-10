using Hexagonal.Common.Entities;
using Hexagonal.Domain.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagonal.Domain.DTOs.Request.Users
{
    public class DTOUserEditStatusRequest
    {
        public required UserStatus Status { get; set; }
    }
}
