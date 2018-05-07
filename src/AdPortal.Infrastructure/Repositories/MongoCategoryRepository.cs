using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdPortal.Core.Domain;
using AdPortal.Core.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace AdPortal.Infrastructure.Repositories
{
    public class MongoCategoryRepository : IMongoRepository, ICategoryRepository
    {
        private readonly IMongoDatabase _database;

        private IMongoCollection<Category> Categories => _database.GetCollection<Category>("Categories"); 
        public MongoCategoryRepository(IMongoDatabase database)
        {
            _database = database;
        }
        public async Task AddCategoryAsync(Category category)
        {
            await Categories.InsertOneAsync(category);
        }

        public async Task<IEnumerable<Category>> BrowseAsync()
            =>  await Categories.AsQueryable().ToListAsync();

        public async Task<Category> GetByIdAsync(Guid id)
            =>  await Categories.AsQueryable().FirstOrDefaultAsync(x=> x.Id ==id);

      
    }
}