using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace DomainService.DtoModels.Account
{
    public class RolesDto
    {
        public string EmployeeId { get; set; }
        public List<IdentityRole> AllRoles { get; set; }
        public IList<string> EmployeeRoles { get; set; }
        public RolesDto()
        {
            AllRoles = new List<IdentityRole>();
            EmployeeRoles = new List<string>();
        }
    }
}
