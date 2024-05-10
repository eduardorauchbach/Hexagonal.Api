using Hexagonal.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagonal.Domain.Domain.Entities.Verifications
{
    public partial class Verification : EntityBase
    {
        public string Value { get; set; }
        public string Origin { get; set; }
        public DateTime ExpiresAt { get; set; }
        public VerificationType Type { get; set; }
    }
}
