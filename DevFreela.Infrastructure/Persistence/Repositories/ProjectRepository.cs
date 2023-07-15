using Dapper;
using DevFreela.Core.Entities;
using DevFreela.Core.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infraestructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class ProjectRepository : BaseRepository, IProjectRepository
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;
        private const int PAGE_SIZE = 2;

        public ProjectRepository(DevFreelaDbContext dbContext, IConfiguration configuration)
            :base(dbContext)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }


        public async Task<PaginationResult<Project>> GetAllAsync(string query, int page = 1)
        {
            IQueryable<Project> projects = _dbContext.Projects;
            if (!string.IsNullOrWhiteSpace(query))
            {
                projects = projects
                    .Where(p => p.Title.Contains(query) ||
                                p.Description.Contains(query));
            }
            return await projects.GetPaged<Project>(page, PAGE_SIZE);
        }

        public async Task<Project> GetByIdAsync(int id)
        {
            return await _dbContext.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Project project)
        {
            await _dbContext.Projects.AddAsync(project);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddAsync(ProjectComment comment)
        {
            await _dbContext.ProjectComments.AddAsync(comment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task StartAsync(Project project)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.OpenAsync();

                string script = "UPDATE Project SET Status = @status, StartedAt = @startedat WHERE Id = @id";

                await sqlConnection.ExecuteAsync(script, new { status = project.Status, startedat = project.StartedAt, project.Id });
            }
        }
    }
}
