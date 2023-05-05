using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainService.DtoModels
{
    public class EditDto
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string? Password { get; set; }
    }
}
