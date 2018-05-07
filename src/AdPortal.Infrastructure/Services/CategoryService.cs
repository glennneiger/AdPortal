using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdPortal.Core.Domain;
using AdPortal.Core.Repositories;
using AdPortal.Infrastructure.DTO;
using AutoMapper;

namespace AdPortal.Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task AddCategoryAsync(Guid id, string name, string description)
        {
            await _repository.AddCategoryAsync(new Category(id,name,description));
        }

        public async Task<IEnumerable<CategoryDTO>> BrowseDTOAsync()
        { 
            var categories = await _repository.BrowseAsync();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public async Task<CategoryDTO> GetDTOById(Guid id)
        {
            var category = await _repository.GetByIdAsync(id);
            if(category == null)
            {
                throw new Exception("Category does not exist.");
            }
            return _mapper.Map<CategoryDTO>(category);
        }
    }
}