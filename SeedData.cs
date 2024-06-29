using CmsShoppingCart.Data;
using CmsShoppingCart.Models;
using Microsoft.EntityFrameworkCore;

namespace CmsShoppingCart
{
    public class SeedData
    {
        private readonly _DbContextClass dataContext;

        public SeedData(_DbContextClass dataContext) {
            this.dataContext = dataContext;
        }   


        public static void Initialize(IServiceProvider services) {
            using (var context = new _DbContextClass(services.GetRequiredService<DbContextOptions<_DbContextClass>>()))
            {
                if (context.Pages.Any())
                {
                    return;

                }


                context.Pages.AddRange(
                    new Page
                    {
                        Title = "Home",
                        Slug = "home",  
                        Content = "home page",
                        Sorting  = 0
                    },
                    new Page
                    {
                        Title = "About Us",
                        Slug = "about-us",
                        Content = "about us page",
                        Sorting = 100
                    },
                    new Page
                    {
                        Title = "Services",
                        Slug = "services",
                        Content = "services page",
                        Sorting = 100
                    },
                    new Page
                    {
                        Title = "Contact",
                        Slug = "contact",
                        Content = "contact page",
                        Sorting = 100
                    }
                );
                context.SaveChanges();  
            }    
        
        }

    }
}
