using DevFreela.Core.Repositories;
using DevFreela.Infraestructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DevFreelaDbContext _dbContext;

        public UnitOfWork(
            DevFreelaDbContext dbContext,
            IProjectRepository projects,
            IUserRepository user)
        {
            _dbContext = dbContext;
            Projects = projects;
            User = user;
        }

        public IProjectRepository Projects { get; }
        public IUserRepository User { get; }

        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
    }
}
