using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hexagonal.Common.Entities;

namespace Hexagonal.Domain.Entities.Profiles
{
    public partial class ProfileArea
    {
        public Guid ProfileId { get; set; }
        public ProfileAreaType Area { get; set; }
        public bool CanAdd { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }        
    }
}
