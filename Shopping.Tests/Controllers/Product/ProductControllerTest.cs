using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Shopping.Application.ProductCQRS.Commands.CreateProduct;
using Shopping.Application.ProductCQRS.Queries.GetProductById;
using Shopping.Application.UserCQRS.Commands.CreateUser;
using Shopping.Domain.Dtos;
using Xunit;

namespace Shopping.Tests.Controllers.Product;

public class ProductControllerTest:IClassFixture<FakeApplication>
{
    private readonly HttpClient _httpClient;

    public ProductControllerTest(FakeApplication factory) => _httpClient = factory.CreateClient();
    
    [Fact]
    public async void GetProducts()
    {
        //Add user
        var createUser = new CreateUserCommandRequest
        {
            Email = "test@test.com",
            FirstName = "test-user-fn",
            LastName = "test-user-ln",
            Password = "Password1234!+"
        };
        var createUserBody = new StringContent(JsonConvert.SerializeObject(createUser),Encoding.UTF8,"application/json");
        var responseCreateUser = await _httpClient.PostAsync("/api/User/AddUser", createUserBody);
        responseCreateUser.EnsureSuccessStatusCode();
        
        var contentCreateUser = await responseCreateUser.Content.ReadAsStringAsync();
        var user = JsonConvert.DeserializeObject<CreateUserCommandResponse>(contentCreateUser);
        Assert.NotNull(user);

        var userLoginDto = new UserLoginDto
        {
            Email = user.User.Email,
            Password = user.User.Password
        };
        
        //Login
        var userLoginDtoBody = new StringContent(JsonConvert.SerializeObject(userLoginDto),Encoding.UTF8,"application/json");
        var responseUserLogin = await _httpClient.PostAsync("/api/Auth/Login", userLoginDtoBody);
        responseUserLogin.EnsureSuccessStatusCode();
        
        var token = await responseUserLogin.Content.ReadAsStringAsync();
        
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
        
        var response = await _httpClient.GetAsync("/api/Product/GetProducts");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();

        var product = JsonConvert.DeserializeObject<GetProductsQueryResponse>(content);
        
        Assert.NotNull(product);

        var count = product.Products.Count();
        var request = new CreateProductCommandRequest {
            Name = "test-product",
            Amount = "test-amount",
            ShoppingListId = 1
        };

        var body = new StringContent(JsonConvert.SerializeObject(request),Encoding.UTF8,"application/json");
        var responseAdd = await _httpClient.PostAsync("/api/Product/AddProduct", body);

        responseAdd.EnsureSuccessStatusCode();
        var contentAdd = await responseAdd.Content.ReadAsStringAsync();

        var productAdd = JsonConvert.DeserializeObject<CreateProductCommandResponse>(contentAdd);

        Assert.NotNull(productAdd);
        Assert.True(productAdd.IsSuccess);
        Assert.NotNull(productAdd.Product);

        response = await _httpClient.GetAsync("/api/Product/GetProducts");
        response.EnsureSuccessStatusCode();
        content = await response.Content.ReadAsStringAsync();
        product = JsonConvert.DeserializeObject<GetProductsQueryResponse>(content);
        Assert.NotNull(product);

        var addedCount = product.Products.Count();

        Assert.True(addedCount - count == 1);
    }
}