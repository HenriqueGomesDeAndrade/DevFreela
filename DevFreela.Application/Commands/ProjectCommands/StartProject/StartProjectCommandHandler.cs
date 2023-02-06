using Dapper;
using DevFreela.Infraestructure.Persistence;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.ProjectCommands
{
    public class StartProjectCommandHandler : IRequestHandler<StartProjectCommand, Unit>
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;


        public StartProjectCommandHandler(DevFreelaDbContext devFreelaDbContext, IConfiguration configuration)
        {
            _dbContext = devFreelaDbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }

        public async Task<Unit> Handle(StartProjectCommand request, CancellationToken cancellationToken)
        {
            var project =  await _dbContext.Projects.SingleOrDefaultAsync(p => p.Id == request.Id);
            project.Start();
            //await _dbContext.SaveChangesAsync();

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.OpenAsync();

                var script = "UPDATE Project SET Status = @status, StartedAt = @startedat WHERE Id = @id";

                await sqlConnection.ExecuteAsync(script, new { status = project.Status, startedat = project.StartedAt, request.Id });
            }

            return Unit.Value;
        }
    }
}
