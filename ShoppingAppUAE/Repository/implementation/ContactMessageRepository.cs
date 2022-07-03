using ShoppingAppUAE.Data;
using ShoppingAppUAE.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAppUAE.Repository.implementation
{
    public class ContactMessageRepository : Repository<ContactMessage, int>, IContactMessageRepository
    {
        public ContactMessageRepository(ApplicationDbContext context) 
            : base(context)
        {
        }
    }
}
