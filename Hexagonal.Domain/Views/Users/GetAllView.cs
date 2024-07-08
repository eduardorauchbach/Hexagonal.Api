using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hexagonal.Domain.Entities.Profiles;
using Hexagonal.Domain.Entities.Users;

namespace Hexagonal.Domain.Views.Users
{
    public class GetAllView : User
    {
        public string ProfileName { get; set; }
        public bool HasSchoolAccess { get; set; }
        public bool HasStockAccess { get; set; }
        public bool HasOrdersAccess { get; set; }
    }
}
