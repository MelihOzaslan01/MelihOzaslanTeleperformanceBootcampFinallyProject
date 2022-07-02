using AutoMapper;
using MediatR;
using Shopping.Application.Common.Interfaces.Repositories;
using Shopping.Application.ShoppingListCQRS.Queries.GetShoppingListByDescription;
using Shopping.Domain.Dtos;

namespace Shopping.Application.ShoppingListCQRS.Handlers.QueryHandlers.GetShoppingListByDescription;


public class
    GetShoppingListByDescriptionQueryHandler : IRequestHandler<GetShoppingListByDescriptionQueryRequest,
        GetShoppingListByDescriptionQueryResponse>
{
    private readonly IShoppingListRepository _shoppingListRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetShoppingListByDescriptionQueryHandler(IShoppingListRepository shoppingListRepository,
        IProductRepository productRepository, IMapper mapper)
    {
        _shoppingListRepository = shoppingListRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<GetShoppingListByDescriptionQueryResponse> Handle(GetShoppingListByDescriptionQueryRequest request,
        CancellationToken cancellationToken)
    {
        var getShoppingListByDescriptionQueryResponse = new GetShoppingListByDescriptionQueryResponse();

        var shoppingLists = await _shoppingListRepository.Get(x => x.Description == request.Description);
        if (shoppingLists==null)
        {
            getShoppingListByDescriptionQueryResponse.IsSuccess = false;
            return getShoppingListByDescriptionQueryResponse;
        }
        var shoppingListDtos = new List<ShoppingListDto>();
        foreach (var shoppingList in shoppingLists)
        {
            var shoppingListDto = _mapper.Map<ShoppingListDto>(shoppingList);
            var shoppingListProducts = await _productRepository.Get(x => x.ShoppingListId == shoppingList.Id);
            var shoppingListProductDtos = _mapper.Map<List<ProductDto>>(shoppingListProducts);
            shoppingListDto.Products = shoppingListProductDtos;
            shoppingListDtos.Add(shoppingListDto);
        }

        getShoppingListByDescriptionQueryResponse.ShoppingList = shoppingListDtos;
        getShoppingListByDescriptionQueryResponse.IsSuccess = shoppingLists!=null;

        return getShoppingListByDescriptionQueryResponse;
    }
}