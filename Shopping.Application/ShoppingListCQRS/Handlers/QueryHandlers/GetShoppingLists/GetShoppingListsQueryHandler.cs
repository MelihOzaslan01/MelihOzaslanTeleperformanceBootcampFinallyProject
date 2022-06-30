using System.Text;
using System.Text.Json;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Shopping.Application.Common.Interfaces.Repositories;
using Shopping.Application.ShoppingListCQRS.Queries.GetShoppingLists;
using Shopping.Domain.Dtos;

namespace Shopping.Application.ShoppingListCQRS.Handlers.QueryHandlers.GetShoppingLists;

public class GetShoppingListsQueryHandler:IRequestHandler<GetShoppingListsQueryRequest,GetShoppingListsQueryResponse>
{
    private readonly IShoppingListRepository _shoppingListRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    private readonly IDistributedCache _distributedCache;

    public GetShoppingListsQueryHandler(IShoppingListRepository shoppingListRepository, IDistributedCache distributedCache, IProductRepository productRepository, IMapper mapper)
    {
        _shoppingListRepository = shoppingListRepository;
        _distributedCache = distributedCache;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<GetShoppingListsQueryResponse> Handle(GetShoppingListsQueryRequest request, CancellationToken cancellationToken)
    {
        var getShoppingListsQueryResponse = new GetShoppingListsQueryResponse();
        
        byte[] cachedBytes = _distributedCache.GetAsync("shoppingLists").Result;
        if (cachedBytes == null)
        {
            var shoppingListDtos = new List<ShoppingListDto>();
            var shoppingLists =  await _shoppingListRepository.GetAll();
            foreach (var shoppingList in shoppingLists)
            {
                var shoppingListDto = _mapper.Map<ShoppingListDto>(shoppingList);
                var shoppingListProducts = await _productRepository.Get(x => x.ShoppingListId == shoppingList.Id);
                var shoppingListProductDtos = _mapper.Map<List<ProductDto>>(shoppingListProducts);
                shoppingListDto.Products = shoppingListProductDtos;
                shoppingListDtos.Add(shoppingListDto);
            }

            getShoppingListsQueryResponse.ShoppingLists = shoppingListDtos;
            getShoppingListsQueryResponse.IsSuccess = true;

            string jsonText = JsonSerializer.Serialize(shoppingListDtos);
            await _distributedCache.SetAsync("shoppingLists", Encoding.UTF8.GetBytes(jsonText), token: cancellationToken);
        }
        else
        {
            string jsonText = Encoding.UTF8.GetString(cachedBytes);
            var shoppingListDtos = JsonSerializer.Deserialize<List<ShoppingListDto>>(jsonText);
            getShoppingListsQueryResponse.ShoppingLists=shoppingListDtos;
            getShoppingListsQueryResponse.IsSuccess = true;
        }

        return getShoppingListsQueryResponse;
        
    }
}