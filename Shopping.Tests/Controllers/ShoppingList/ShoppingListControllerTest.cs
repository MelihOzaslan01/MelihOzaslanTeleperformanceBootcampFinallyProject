using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Shopping.Application.ShoppingListCQRS.Commands.CreateShoppingList;
using Shopping.Application.ShoppingListCQRS.Queries.GetShoppingLists;
using Shopping.Application.UserCQRS.Commands.CreateUser;
using Shopping.Domain.Dtos;
using Xunit;

namespace Shopping.Tests.Controllers.ShoppingList;

public class ShoppingListControllerTest:IClassFixture<FakeApplication>
{
    private readonly HttpClient _httpClient;

    public ShoppingListControllerTest(FakeApplication factory) => _httpClient = factory.CreateClient();
    
    [Fact]
    public async void GetShoppingLists()
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
        
        var response = await _httpClient.GetAsync("/api/ShoppingList/GetShoppingLists");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();

        var shoppingList = JsonConvert.DeserializeObject<GetShoppingListsQueryResponse>(content);
        
        Assert.NotNull(shoppingList);

        var count = shoppingList.ShoppingLists.Count();
        var request = new CreateShoppingListCommandRequest {
            Title = "shopping-list-title",
            Category = "shopping-list-category",
            Description = "shopping-list-description"
        };

        var body = new StringContent(JsonConvert.SerializeObject(request),Encoding.UTF8,"application/json");
        var responseAdd = await _httpClient.PostAsync("/api/ShoppingList/AddShoppingList", body);

        responseAdd.EnsureSuccessStatusCode();
        var contentAdd = await responseAdd.Content.ReadAsStringAsync();

        var shoppingListAdd = JsonConvert.DeserializeObject<CreateShoppingListCommandResponse>(contentAdd);

        Assert.NotNull(shoppingListAdd);
        Assert.True(shoppingListAdd.IsSuccess);
        Assert.NotNull(shoppingListAdd.ShoppingList);

        response = await _httpClient.GetAsync("/api/ShoppingList/GetShoppingLists");
        response.EnsureSuccessStatusCode();
        content = await response.Content.ReadAsStringAsync();
        shoppingList = JsonConvert.DeserializeObject<GetShoppingListsQueryResponse>(content);
        Assert.NotNull(shoppingList);

        var addedCount = shoppingList.ShoppingLists.Count();

        Assert.True(addedCount - count == 1);
    }
}