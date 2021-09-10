using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.DataAccess.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Category { get; }

        ICoverTypeRepository CoverType { get; }

        IProductRepository Product { get; }

        ICompanyRepository Company { get; }

        IShoppingCartRepository ShoppingCart { get; }

        IOrderHeaderRepository OrderHeader { get; }

        IOrderDetailsRepository OrderDetails { get; }

        IApplicationUserRepository ApplicationUser { get; }
        ISP_Call SP_Call {get;}

        void Save();

    }
}
