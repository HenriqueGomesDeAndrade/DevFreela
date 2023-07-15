using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Core.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infraestructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries.ProjectQueries
{
    public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, PaginationResult<ProjectViewModel>>
    {
        private readonly IProjectRepository _projectRepository;


        public GetAllProjectsQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;

        }

        public async Task<PaginationResult<ProjectViewModel>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            PaginationResult<Project> paginationProjects = await _projectRepository.GetAllAsync(request.Query, request.Page);
            var projectsViewModel = paginationProjects
                                    .Data
                                    .Select(p => new ProjectViewModel(p.Title, p.CreatedAt))
                                    .ToList();

            var paginationProjectsViewModel = new PaginationResult<ProjectViewModel>(
                    paginationProjects.Page,
                    paginationProjects.TotalPages,
                    paginationProjects.PageSize,
                    paginationProjects.ItemsCount,
                    projectsViewModel);

            return paginationProjectsViewModel;
        }
    }
}
