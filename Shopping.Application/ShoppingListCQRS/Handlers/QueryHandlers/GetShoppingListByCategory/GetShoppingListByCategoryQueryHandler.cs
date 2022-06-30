using AutoMapper;
using MediatR;
using Shopping.Application.Common.Interfaces.Repositories;
using Shopping.Application.ShoppingListCQRS.Queries.GetShoppingListByCategory;
using Shopping.Domain.Dtos;

namespace Shopping.Application.ShoppingListCQRS.Handlers.QueryHandlers.GetShoppingListByCategory;

public class
    GetShoppingListByCategoryQueryHandler : IRequestHandler<GetShoppingListByCategoryQueryRequest,
        GetShoppingListByCategoryQueryResponse>
{
    private readonly IShoppingListRepository _shoppingListRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetShoppingListByCategoryQueryHandler(IShoppingListRepository shoppingListRepository,
        IProductRepository productRepository, IMapper mapper)
    {
        _shoppingListRepository = shoppingListRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<GetShoppingListByCategoryQueryResponse> Handle(GetShoppingListByCategoryQueryRequest request,
        CancellationToken cancellationToken)
    {
        var getShoppingListByCategoryQueryResponse = new GetShoppingListByCategoryQueryResponse();

        var shoppingLists = await _shoppingListRepository.Get(x => x.Category == request.Category);
        var shoppingListDtos = new List<ShoppingListDto>();
        foreach (var shoppingList in shoppingLists)
        {
            var shoppingListDto = _mapper.Map<ShoppingListDto>(shoppingList);
            var shoppingListProducts = await _productRepository.Get(x => x.ShoppingListId == shoppingList.Id);
            var shoppingListProductDtos = _mapper.Map<List<ProductDto>>(shoppingListProducts);
            shoppingListDto.Products = shoppingListProductDtos;
            shoppingListDtos.Add(shoppingListDto);
        }

        getShoppingListByCategoryQueryResponse.ShoppingList = shoppingListDtos;
        getShoppingListByCategoryQueryResponse.IsSuccess = true;

        return getShoppingListByCategoryQueryResponse;
    }
}