using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainService.Entity;

namespace DomainService.DtoModels.Account
{
    public class RegistrationDto
    {
        [Required]
        [Display(Name = "Логин")]
        public string Login { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; } = null!;

        [Display(Name = "Роль")]
        public Role? Role { get; set; }

        [Display(Name = "Id Роли")]
        public Guid? RoleId { get; set; }
    }
}
