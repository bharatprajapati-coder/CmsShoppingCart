using CmsShoppingCart.Models;
using Microsoft.EntityFrameworkCore;

namespace CmsShoppingCart.Data
{
    public class _DbContextClass : DbContext
    {
        public _DbContextClass(DbContextOptions<_DbContextClass> options ) : base(options)
        {
            
        }

        public DbSet<Page> Pages { get; set; }
    }
}
