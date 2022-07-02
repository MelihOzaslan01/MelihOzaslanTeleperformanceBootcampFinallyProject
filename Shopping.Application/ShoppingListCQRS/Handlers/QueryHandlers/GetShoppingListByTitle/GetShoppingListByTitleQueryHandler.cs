using AutoMapper;
using MediatR;
using Shopping.Application.Common.Interfaces.Repositories;
using Shopping.Application.ShoppingListCQRS.Queries.GetShoppingListByTitle;
using Shopping.Domain.Dtos;

namespace Shopping.Application.ShoppingListCQRS.Handlers.QueryHandlers.GetShoppingListByTitle;

public class
    GetShoppingListByTitleQueryHandler : IRequestHandler<GetShoppingListByTitleQueryRequest,
        GetShoppingListByTitleQueryResponse>
{
    private readonly IShoppingListRepository _shoppingListRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetShoppingListByTitleQueryHandler(IShoppingListRepository shoppingListRepository,
        IProductRepository productRepository, IMapper mapper)
    {
        _shoppingListRepository = shoppingListRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<GetShoppingListByTitleQueryResponse> Handle(GetShoppingListByTitleQueryRequest request,
        CancellationToken cancellationToken)
    {
        var getShoppingListByTitleQueryResponse = new GetShoppingListByTitleQueryResponse();

        var shoppingLists = await _shoppingListRepository.Get(x => x.Title == request.Title);
        var shoppingListDtos = new List<ShoppingListDto>();
        foreach (var shoppingList in shoppingLists)
        {
            var shoppingListDto = _mapper.Map<ShoppingListDto>(shoppingList);
            var shoppingListProducts = await _productRepository.Get(x => x.ShoppingListId == shoppingList.Id);
            var shoppingListProductDtos = _mapper.Map<List<ProductDto>>(shoppingListProducts);
            shoppingListDto.Products = shoppingListProductDtos;
            shoppingListDtos.Add(shoppingListDto);
        }

        getShoppingListByTitleQueryResponse.ShoppingList = shoppingListDtos;
        getShoppingListByTitleQueryResponse.IsSuccess = shoppingLists!=null;

        return getShoppingListByTitleQueryResponse;
    }
}