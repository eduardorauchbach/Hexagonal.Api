using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hexagonal.Common.Entities;

namespace Hexagonal.Domain.Entities.Profiles
{
    public partial class Profile : EntityBase
    {
        public string Name { get; set; }
        public List<ProfileArea> ProfileAreas { get; set; } = new List<ProfileArea>();
    }    
}
