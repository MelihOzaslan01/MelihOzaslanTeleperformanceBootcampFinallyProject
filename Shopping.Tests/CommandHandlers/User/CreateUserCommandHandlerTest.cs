using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using Shopping.Application.Common.Interfaces.Repositories;
using Shopping.Application.ProductCQRS.Commands.CreateProduct;
using Shopping.Application.ProductCQRS.Handlers.CommandHandlers.CreateProduct;
using Shopping.Application.UserCQRS.Commands.CreateUser;
using Shopping.Application.UserCQRS.Handlers.CommandHandlers.CreateUser;
using Xunit;

namespace Shopping.Tests.CommandHandlers.User;

public class CreateUserCommandHandlerTest
{
    // [Fact]
    // public async void CreateUser_IsSuccess()
    // {
    //     var createUserCommandRequest = new Bogus.Faker<CreateUserCommandRequest>();
    //
    //     var mockRepository = new Mock<UserManager<Domain.Entities.User>>(Mock.Of<IUserStore<Domain.Entities.User>>(), null, null, null, null, null, null, null, null);
    //     mockRepository.Setup(c => c.FindByEmailAsync(It.IsAny<String>())).ReturnsAsync(()=>null);
    //     mockRepository.Setup(c => c.CreateAsync(It.IsAny<Domain.Entities.User>(), It.IsAny<String>()))
    //         .ReturnsAsync(IdentityResult.Success);
    //     mockRepository.Setup(c => c.AddClaimsAsync(It.IsAny<Domain.Entities.User>(), It.IsAny<List<Claim>>()))
    //         .ReturnsAsync(IdentityResult.Success);
    //     var mockCache = new Mock<IDistributedCache>();
    //
    //     var command = new CreateUserCommandHandler(mockRepository.Object, mockCache.Object);
    //     
    //     var response = await command.Handle(createUserCommandRequest.Generate(), CancellationToken.None);
    //     
    //     Assert.True(response.IsSuccess);
    //     Assert.NotNull(response.User);
    // }
}