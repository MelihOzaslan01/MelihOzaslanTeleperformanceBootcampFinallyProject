using AutoMapper;
using MediatR;
using Shopping.Application.Common.Interfaces.Repositories;
using Shopping.Application.ShoppingListCQRS.Queries.GetShoppingListByCompleted;
using Shopping.Domain.Dtos;

namespace Shopping.Application.ShoppingListCQRS.Handlers.QueryHandlers.GetShoppingListByCompleted;


public class
    GetShoppingListByCompletedQueryHandler : IRequestHandler<GetShoppingListByCompletedQueryRequest,
        GetShoppingListByCompletedQueryResponse>
{
    private readonly IShoppingListRepository _shoppingListRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetShoppingListByCompletedQueryHandler(IShoppingListRepository shoppingListRepository,
        IProductRepository productRepository, IMapper mapper)
    {
        _shoppingListRepository = shoppingListRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<GetShoppingListByCompletedQueryResponse> Handle(GetShoppingListByCompletedQueryRequest request,
        CancellationToken cancellationToken)
    {
        var getShoppingListByCompletedQueryResponse = new GetShoppingListByCompletedQueryResponse();

        var shoppingLists = await _shoppingListRepository.Get(x => x.IsCompleted == request.IsCompleted);
        var shoppingListDtos = new List<ShoppingListDto>();
        foreach (var shoppingList in shoppingLists)
        {
            var shoppingListDto = _mapper.Map<ShoppingListDto>(shoppingList);
            var shoppingListProducts = await _productRepository.Get(x => x.ShoppingListId == shoppingList.Id);
            var shoppingListProductDtos = _mapper.Map<List<ProductDto>>(shoppingListProducts);
            shoppingListDto.Products = shoppingListProductDtos;
            shoppingListDtos.Add(shoppingListDto);
        }

        getShoppingListByCompletedQueryResponse.ShoppingList = shoppingListDtos;
        getShoppingListByCompletedQueryResponse.IsSuccess = shoppingLists!=null;

        return getShoppingListByCompletedQueryResponse;
    }
}