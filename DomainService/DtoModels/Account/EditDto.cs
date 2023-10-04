using DomainService.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainService.DtoModels.Account
{
    public class EditDto
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string? Password { get; set; }
        public Role? Role { get; set; }
        public int? RoleId { get; set; }
    }
}
