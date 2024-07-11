using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagonal.DTOs.Request.Verifications
{
    public class ValidateRequest
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
    }
}
