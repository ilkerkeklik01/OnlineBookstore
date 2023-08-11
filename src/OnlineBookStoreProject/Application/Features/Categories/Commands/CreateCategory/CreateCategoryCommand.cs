using Application.Features.Bookshelves.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Categories.Dtos;

namespace Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest<CreatedCategoryDto>
    {
        public string Name { get; set; }

        public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CreatedCategoryDto>
        {

            private readonly ICategoryRepository _repository;
            private readonly IMapper _mapper;

            public CreateCategoryCommandHandler(ICategoryRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<CreatedCategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
            {
                Category mappedCategory = _mapper.Map<Category>(request);
                Category createdCategory = await _repository.AddAsync(mappedCategory);
                CreatedCategoryDto createdCategoryDto = _mapper.Map<CreatedCategoryDto>(createdCategory);
                return createdCategoryDto;
            }



        }



    }
}
