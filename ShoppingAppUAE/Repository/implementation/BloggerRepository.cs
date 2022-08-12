using ShoppingAppUAE.Data;
using ShoppingAppUAE.Repository.Interfaces;

namespace ShoppingAppUAE.Repository.implementation
{
    public class BloggerRepository : Repository<Blog, int>, IBloggerRepository
    {
        public BloggerRepository(ApplicationDbContext context) 
            : base(context)
        {
        }
    }
}
