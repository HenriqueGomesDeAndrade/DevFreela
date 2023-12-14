using DevFreela.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public interface IUnitOfWork
    {
        IProjectRepository Projects { get; }
        IUserRepository User { get; }
        Task<int> CompleteAsync();
    }
}
