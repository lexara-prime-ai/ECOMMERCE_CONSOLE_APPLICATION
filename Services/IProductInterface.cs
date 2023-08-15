using EcommerceConsole.Models;

namespace EcommerceConsole.Services
{
    public interface IProductInterface
    {
        // METHOD IMPLEMENTATIONS
        Task<List<Product>> GET_ALL_PRODUCTS_Async();

        Task<Product> GET_PRODUCT_Async(int id);

        Task<ResponseMessage> ADD_PRODUCT_Async(AddProduct product);

        Task<ResponseMessage> UPDATE_PRODUCT_Async(Product product);

        Task<ResponseMessage> DELETE_PRODUCT_Async(int id);

    }
}