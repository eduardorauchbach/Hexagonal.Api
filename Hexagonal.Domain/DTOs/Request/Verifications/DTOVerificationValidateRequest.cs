using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagonal.Domain.DTOs.Request.Verifications
{
    public class DTOVerificationValidateRequest
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
    }
}
