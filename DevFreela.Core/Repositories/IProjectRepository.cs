using DevFreela.Core.Entities;
using DevFreela.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Core.Repositories
{
    public interface IProjectRepository : IBaseRepository
    {
        Task<PaginationResult<Project>> GetAllAsync(string query, int page = 1);
        Task<Project> GetByIdAsync(int id);
        Task AddAsync(Project project);
        Task AddAsync(ProjectComment comment);
        Task StartAsync(Project project);
    }
}
