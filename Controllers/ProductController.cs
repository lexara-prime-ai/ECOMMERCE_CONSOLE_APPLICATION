using EcommerceConsole.Helpers;
using EcommerceConsole.Models;
using EcommerceConsole.Services;

namespace EcommerceConsole.Controllers;
public class ProductController
{
    // CREATE NEW ProductService INSTANCE
    ProductService productService = new ProductService();

    public async static Task rax_INIT()
    {
        // DISPLAY WELCOME MESSAGE & INSTRUCTIONS TO USER
        string rdx_WELCOME_MESSAGE = @"
            
█░█░█ █▀▀ █░░ █▀▀ █▀█ █▀▄▀█ █▀▀
▀▄▀▄▀ ██▄ █▄▄ █▄▄ █▄█ █░▀░█ ██▄
            ";

        Console.WriteLine($"{Environment.NewLine}{rdx_WELCOME_MESSAGE}");
        Console.WriteLine($"{Environment.NewLine}Please select an option to proceed....");

        // DISPLAY OPTIONS
        Console.WriteLine("1. Add a product >");
        Console.WriteLine("2. View all products >");
        Console.WriteLine("3. View a product >");
        Console.WriteLine("4. Update a product >");
        Console.WriteLine("5. Delete a product >");

        // RECEIVE USER INPUT
        string? rdx_SELECTED_OPTION = Console.ReadLine();
        // CHECK IF INPUT IS VALID
        bool IS_VALID_OPTION = VALIDATOR.VALIDATE(new List<string> { rdx_SELECTED_OPTION });

        if (!IS_VALID_OPTION)
        {
            // RE-DISPLAY OPTIONS
            await ProductController.rax_INIT();
        }
        else
        {
            // HANDLE SELECTED OPTION | REDIRECT TO NEXT STEP
            await new ProductController().REDIRECT_TO_SELECTED_OPTION(rdx_SELECTED_OPTION);
        }
    }

    // PROCEED TO NEXT STEP
    private async Task REDIRECT_TO_SELECTED_OPTION(string rdx_SELECTED_OPTION)
    {
        Console.WriteLine($"{Environment.NewLine}Selected option: {rdx_SELECTED_OPTION}{Environment.NewLine}");

        switch (rdx_SELECTED_OPTION)
        {
            case "1":
                await rax_ADD_PRODUCT();
                break;
            case "2":
                await rax_VIEW_PRODUCTS();
                break;
            case "3":
                await rax_VIEW_PRODUCT();
                break;
            case "4":
                await rax_UPDATE_PRODUCT();
                break;
            case "5":
                await rax_DELETE_PRODUCT();
                break;

            default:
                await ProductController.rax_INIT();
                break;
        }
    }

    /********************
    *   DELETE A PRODUCT
    **********************/
    private async Task rax_DELETE_PRODUCT()
    {
        // DISPLAY AVAILABLE PRODUCTS | IF ANY
        await rax_VIEW_PRODUCTS();
        // RECEIVE USER INPUT

        Console.WriteLine($"{Environment.NewLine}Enter PRODUCT ID in order to delete a product e.g 1, 2, 3 etc....");

        int id = Int32.Parse(Console.ReadLine());

        // CALL SERVICES
        try
        {
            ResponseMessage RESPONSE = await productService.DELETE_PRODUCT_Async(id);

            Console.WriteLine(RESPONSE.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occured: {e.Message}");
        }
    }

    /********************
    *   UPDATE A PRODUCT
    **********************/
    private async Task rax_UPDATE_PRODUCT()
    {
        // DISPLAY AVAILABLE PRODUCTS | IF ANY
        await rax_VIEW_PRODUCTS();
        // RECEIVE USER INPUT

        Console.WriteLine($"{Environment.NewLine}Enter PRODUCT ID in order to update a product e.g 1, 2, 3 etc....");

        int id = Int32.Parse(Console.ReadLine());

        // TITLE
        Console.WriteLine("Update product title >");
        string? productTitle = Console.ReadLine();

        // DESCRIPTION
        Console.WriteLine("Update product description >");
        string? productDescription = Console.ReadLine();

        // PRICE
        Console.WriteLine("Update product price >");
        string? productPrice = Console.ReadLine();

        // CREATE NEW PRODUCT INSTANCE
        Product updatedProduct = new Product
        {
            Id = id,
            Title = productTitle,
            Description = productDescription,
            Price = productPrice
        };

        // CALL SERVICES
        try
        {
            ResponseMessage RESPONSE = await productService.UPDATE_PRODUCT_Async(updatedProduct);

            Console.WriteLine(RESPONSE.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occured: {e.Message}");
        }
    }

    /********************
    *  VIEW ONE PRODUCT
    **********************/
    private async Task rax_VIEW_PRODUCT()
    {
        try
        {
            // DISPLAY AVAILABLE PRODUCTS | IF ANY
            await rax_VIEW_PRODUCTS();

            // DISPLAY MESSAGE
            Console.WriteLine($"{Environment.NewLine}Enter PRODUCT ID to view additional information e.g 1, 2, 3 etc....");

            int id = Int32.Parse(Console.ReadLine());

            Product PRODUCT = await productService.GET_PRODUCT_Async(id);

            // LINE BREAK | For Readability
            Console.WriteLine(Environment.NewLine);

            // DISPLAY ADDITIONAL INFORMATION
            Console.WriteLine($"Title: {PRODUCT.Title}");
            Console.WriteLine($"Description: {PRODUCT.Description}");
            Console.WriteLine($"Price: ${PRODUCT.Price}");

            // LINE BREAK | For Readability
            Console.WriteLine(Environment.NewLine);
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occured: {e.Message}");
        }
    }

    /********************
    *   VIEW ALL PRODUCTS
    **********************/
    private async Task rax_VIEW_PRODUCTS()
    {
        try
        {
            // DISPLAY MESSAGE
            Console.WriteLine("**** AVAILABLE PRODUCTS ****");

            List<Product> PRODUCTS = await productService.GET_ALL_PRODUCTS_Async();

            // LOOP THROUGH PRODUCT LIST
            foreach (var product in PRODUCTS)
            {
                Console.WriteLine($"{product.Id}. {product.Title}");
            }

            Console.WriteLine(Environment.NewLine);
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occured: {e.Message}");
        }
    }

    /********************
    *   ADD A PRODUCT
    **********************/
    private async Task rax_ADD_PRODUCT()
    {
        // RECEIVE USER INPUT
        // TITLE
        Console.WriteLine("Enter product title >");
        string? productTitle = Console.ReadLine();

        // DESCRIPTION
        Console.WriteLine("Enter product description >");
        string? productDescription = Console.ReadLine();

        // PRICE
        Console.WriteLine("Enter product price >");
        string? productPrice = Console.ReadLine();

        // CREATE NEW PRODUCT INSTANCE
        AddProduct newProduct = new AddProduct
        {
            Title = productTitle,
            Description = productDescription,
            Price = productPrice
        };

        // CALL SERVICES
        try
        {
            ResponseMessage RESPONSE = await productService.ADD_PRODUCT_Async(newProduct);

            Console.WriteLine(RESPONSE.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occured: {e.Message}");
        }
    }
}