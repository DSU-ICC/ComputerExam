using DomainService.DBService;
using DomainService.Entity;
using Infrastructure.Common;
using Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}
