using System.Text;
using EcommerceConsole.Models;
using Newtonsoft.Json;

namespace EcommerceConsole.Services;

public class ProductService : IProductInterface
{

    // CLASS ACCESSIBLE FIELDS
    private readonly HttpClient _httpClient;
    private readonly string URL = "http://localhost:3000/products";

    // CREATE NEW HttpClient INSTANCE
    public ProductService()
    {
        _httpClient = new HttpClient();
    }

    /*
    *******************
        ADD A PRODUCT
    ********************
    */
    public async Task<ResponseMessage> ADD_PRODUCT_Async(AddProduct product)
    {
        try
        {
            // PARSE CONTENT TO JSON
            string productInfo = JsonConvert.SerializeObject(product);

            StringContent parsedInfo = new StringContent(productInfo, Encoding.UTF8, "application/json");

            // RETURN RESPONSE
            HttpResponseMessage RESPONSE = await _httpClient.PostAsync(URL, parsedInfo);

            // HANDLE SUCCESS & ERROR CASES
            if (RESPONSE.IsSuccessStatusCode)
            {
                return new ResponseMessage
                {
                    Message = "Product added successfully!"
                };
            }
            else
            {
                throw new Exception("Failed to add product...");
            }
        }
        catch (Exception e)
        {
            throw new Exception($"An error occured: {e.Message}");
        }
    }

    /*
    *********************
        DELETE A PRODUCT
    **********************
    */
    public async Task<ResponseMessage> DELETE_PRODUCT_Async(int id)
    {
        try
        {
            HttpResponseMessage RESPONSE = await _httpClient.DeleteAsync($"{URL}/{id}");

            // HANDLE SUCCESS & ERROR MESSAGES
            if (RESPONSE.IsSuccessStatusCode)
            {
                return new ResponseMessage
                {
                    Message = "Product deleted successfully!"
                };
            }
            else
            {
                throw new Exception("Failed to delete product...");
            }
        }
        catch (Exception e)
        {
            throw new Exception($"An error occured: {e.Message}");
        }
    }

    /*
    **********************
        GET ALL PRODUCTS
    **********************
    */
    public async Task<List<Product>> GET_ALL_PRODUCTS_Async()
    {
        try
        {
            var RESPONSE = await _httpClient.GetAsync(URL);

            List<Product>? PRODUCTS = JsonConvert.DeserializeObject<List<Product>>(await RESPONSE.Content.ReadAsStringAsync());

            // HANDLE SUCCESS & ERROR CASES
            if (RESPONSE.IsSuccessStatusCode)
            {
                return PRODUCTS;
            }

            throw new Exception("Failed to get products...");
        }
        catch (Exception e)
        {
            throw new Exception($"An error occured: {e.Message}");
        }
    }

    /*
    **********************
        GET ONE PRODUCT
    **********************
    */
    public async Task<Product> GET_PRODUCT_Async(int id)
    {
        try
        {
            HttpResponseMessage RESPONSE = await _httpClient.GetAsync($"{URL}/{id}");

            Product? PRODUCT = JsonConvert.DeserializeObject<Product>(await RESPONSE.Content.ReadAsStringAsync());

            if (RESPONSE.IsSuccessStatusCode)
            {
                return PRODUCT;
            }

            throw new Exception("Failed to get product...");
        }
        catch (Exception e)
        {
            throw new Exception($"An error occured: {e.Message}");
        }
    }

    public async Task<ResponseMessage> UPDATE_PRODUCT_Async(Product product)
    {
        try
        {
            // PARSE CONTENT TO JSON
            string updatedProductInfo = JsonConvert.SerializeObject(product);

            StringContent parsedInfo = new StringContent(updatedProductInfo, Encoding.UTF8, "application/json");

            HttpResponseMessage RESPONSE = await _httpClient.PutAsync($"{URL}/{product.Id}", parsedInfo);

            if (RESPONSE.IsSuccessStatusCode)
            {
                return new ResponseMessage
                {
                    Message = "Product updated successfully!"
                };
            }

            throw new Exception("Failed to update product...");

        }
        catch (Exception e)
        {
            throw new Exception($"An error occured: {e.Message}");
        }
    }
}
