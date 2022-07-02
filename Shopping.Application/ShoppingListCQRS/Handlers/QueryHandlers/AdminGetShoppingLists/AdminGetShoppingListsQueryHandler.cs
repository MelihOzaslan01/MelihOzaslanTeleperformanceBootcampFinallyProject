using System.Text;
using System.Text.Json;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Shopping.Application.Common.Interfaces.Repositories;
using Shopping.Application.ShoppingListCQRS.Queries.AdminGetShoppingLists;
using Shopping.Domain.Dtos;

namespace Shopping.Application.ShoppingListCQRS.Handlers.QueryHandlers.AdminGetShoppingLists;

public class AdminGetShoppingListsQueryHandler:IRequestHandler<AdminGetShoppingListsQueryRequest,AdminGetShoppingListsQueryResponse>
{
    private readonly IShoppingListRepository _shoppingListRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    private readonly IDistributedCache _distributedCache;

    public AdminGetShoppingListsQueryHandler(IShoppingListRepository shoppingListRepository, IDistributedCache distributedCache, IProductRepository productRepository, IMapper mapper)
    {
        _shoppingListRepository = shoppingListRepository;
        _distributedCache = distributedCache;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<AdminGetShoppingListsQueryResponse> Handle(AdminGetShoppingListsQueryRequest request, CancellationToken cancellationToken)
    {
        var adminGetShoppingListsQueryResponse = new AdminGetShoppingListsQueryResponse();
        
        byte[] cachedBytes = _distributedCache.GetAsync("adminShoppingLists").Result;
        if (cachedBytes == null || String.IsNullOrEmpty(Encoding.UTF8.GetString(cachedBytes)))
        {
            var shoppingListDtos = new List<ShoppingListDto>();
            var shoppingLists =  await _shoppingListRepository.AdminGetAll();
            if (shoppingLists==null)
            {
                adminGetShoppingListsQueryResponse.IsSuccess = false;
                return adminGetShoppingListsQueryResponse;
            }
            foreach (var shoppingList in shoppingLists)
            {
                var shoppingListDto = _mapper.Map<ShoppingListDto>(shoppingList);
                var shoppingListProducts = await _productRepository.Get(x => x.ShoppingListId == shoppingList.Id);
                var shoppingListProductDtos = _mapper.Map<List<ProductDto>>(shoppingListProducts);
                shoppingListDto.Products = shoppingListProductDtos;
                shoppingListDtos.Add(shoppingListDto);
            }

            adminGetShoppingListsQueryResponse.ShoppingLists = shoppingListDtos;
            adminGetShoppingListsQueryResponse.IsSuccess = shoppingLists!=null;

            string jsonText = JsonSerializer.Serialize(shoppingListDtos);
            await _distributedCache.SetAsync("adminShoppingLists", Encoding.UTF8.GetBytes(jsonText), token: cancellationToken);
        }
        else
        {
            string jsonText = Encoding.UTF8.GetString(cachedBytes);
            var shoppingListDtos = JsonSerializer.Deserialize<List<ShoppingListDto>>(jsonText);
            adminGetShoppingListsQueryResponse.ShoppingLists=shoppingListDtos;
            adminGetShoppingListsQueryResponse.IsSuccess = shoppingListDtos!=null;
        }

        return adminGetShoppingListsQueryResponse;
        
    }
}