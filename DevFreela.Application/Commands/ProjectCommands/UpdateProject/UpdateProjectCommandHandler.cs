﻿using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infraestructure.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.ProjectCommands
{
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, Unit>
    {
        private readonly IProjectRepository _projectRepository;

        public UpdateProjectCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Unit> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            Project project = await _projectRepository.GetByIdAsync(request.Id);
            project.Update(request.Title, request.Description, request.TotalCost);
            await _projectRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
