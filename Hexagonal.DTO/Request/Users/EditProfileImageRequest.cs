using Microsoft.AspNetCore.Http;
using Hexagonal.Common.Entities;
using Hexagonal.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagonal.DTO.Request.Users
{
    public record EditProfileImageRequest
    {
        public required IFormFile FileData { get; init; }
    }
}
