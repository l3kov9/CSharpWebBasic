namespace MyCoolWebServer.ByTheCakeApplication.Services
{
    using ViewModels.Products;
    using System.Collections.Generic;

    public interface IProductService
    {
        void Create(string name, decimal price, string imageUrl);

        IEnumerable<ProductListingViewModel> All(string searchTerm = null);

        ProductDetailViewModel Find(int id);

        bool Exists(int id);

        IEnumerable<ProductInCartViewModel> FindProductsInCart(IEnumerable<int> ids);
    }
}
