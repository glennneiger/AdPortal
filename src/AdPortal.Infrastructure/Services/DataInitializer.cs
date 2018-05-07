using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdPortal.Core.Domain;
using Microsoft.Extensions.Logging;


namespace AdPortal.Infrastructure.Services
{
    public class DataInitializer : IDataInitializer
    {
        private readonly IUserService _userService;
        private readonly IUserAdService _userAdService;
        private readonly ICategoryService _categoryService;
        private readonly ILogger _logger;
        public DataInitializer(IUserService userService, IUserAdService userAdService,
            ICategoryService categoryService, ILoggerFactory logger)
        {
            _userService = userService;
            _userAdService = userAdService;
            _categoryService = categoryService;
            _logger = logger.CreateLogger("Data Initialize");
        }
        public async Task SeedAsync()
        {
            _logger.LogTrace("Initializing data..");
            await SeedCategories();
            await SeedUsers();

        }

        private async Task SeedCategories()
        {
            var categories = await _categoryService.BrowseDTOAsync();
            if(categories.Any())
            {
                return;
            }
            _logger.LogTrace("Adding categories...");
            await _categoryService.AddCategoryAsync(Guid.NewGuid(),"Sport","");
            await _categoryService.AddCategoryAsync(Guid.NewGuid(),"Muzyka","");
            await _categoryService.AddCategoryAsync(Guid.NewGuid(),"Zdrowie i Uroda","");
            await _categoryService.AddCategoryAsync(Guid.NewGuid(),"Motoryzacja","");
            await _categoryService.AddCategoryAsync(Guid.NewGuid(),"Technologia","");
            
        }

        private async Task SeedUsers()
        {
            var users = await _userService.BrowswseDTOAsync();
            if(users.Any())
            {
                return;
            }
            {   
                Random random = new Random();
                for(int i=0;i<5;i++)
                {
                    await _userService.RegisterAsync($"user{i}",$"user{i}@email.com","secret",Role.User);
                    _logger.LogTrace($"Created user{i}");
                    for(int j=0;j<5;j++)
                    {
                        
                        var categories = (await _categoryService.BrowseDTOAsync()).ToList();
                        var count = categories.Count -1;
                        var category = categories[random.Next(0,count)];
                        var user = await _userService.GetUserDTO($"user{i}@email.com");
                        await _userAdService.AddAsync(user.Id,category.Id
                        ,$"ad by user{i}",Guid.NewGuid().ToString(), DateTime.UtcNow.AddYears(3));
                        _logger.LogTrace($"Created {j}th ad for user{i}");
                    }
                }
                await _userService.RegisterAsync($"admin",$"admin@email.com","secret",Role.Admin);
                _logger.LogTrace($"Created admin");
                for(int j=0;j<5;j++)
                {
                    
                    var categories = (await _categoryService.BrowseDTOAsync()).ToList();
                    var count = categories.Count -1;
                    var category = categories[random.Next(0,count)];
                    var user = await _userService.GetUserDTO($"admin@email.com");
                    await _userAdService.AddAsync(user.Id,category.Id
                    ,$"ad by admin",Guid.NewGuid().ToString(), DateTime.UtcNow.AddYears(3) );
                    _logger.LogTrace($"Created ad for admin");
                }
                

            }
        }
    }
}