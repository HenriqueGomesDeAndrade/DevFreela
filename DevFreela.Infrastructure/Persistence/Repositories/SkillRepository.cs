using Dapper;
using DevFreela.Core.DTOs;
using DevFreela.Core.Repositories;
using DevFreela.Infraestructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class SkillRepository : BaseRepository, ISkillRepository
    {

        private readonly string _connectionString;
        public SkillRepository(DevFreelaDbContext dbContext, IConfiguration configuration)
            :base(dbContext)
        {
            _connectionString = configuration.GetConnectionString("DevFreelaCs");

        }

        public async Task<List<SkillDto>> GetAllAsync()
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                string script = "SELECT Id, Description FROM Skills";

                IEnumerable<SkillDto> skills = await sqlConnection.QueryAsync<SkillDto>(script);
                return skills.ToList();
            }

            //var skills = _dbContext.Skills;
            //var skillsViewModel = skills
            //                        .Select(s => new SkillViewModel(s.Id, s.Description)).ToList();

            //return skillsViewModel;
        }
    }
}
