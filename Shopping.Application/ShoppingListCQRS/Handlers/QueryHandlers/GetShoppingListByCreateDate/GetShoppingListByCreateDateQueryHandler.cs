using AutoMapper;
using MediatR;
using Shopping.Application.Common.Interfaces.Repositories;
using Shopping.Application.ShoppingListCQRS.Queries.GetShoppingListByCreateDate;
using Shopping.Domain.Dtos;

namespace Shopping.Application.ShoppingListCQRS.Handlers.QueryHandlers.GetShoppingListByCreateDate;


public class
    GetShoppingListByCreateDateQueryHandler : IRequestHandler<GetShoppingListByCreateDateQueryRequest,
        GetShoppingListByCreateDateQueryResponse>
{
    private readonly IShoppingListRepository _shoppingListRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetShoppingListByCreateDateQueryHandler(IShoppingListRepository shoppingListRepository,
        IProductRepository productRepository, IMapper mapper)
    {
        _shoppingListRepository = shoppingListRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<GetShoppingListByCreateDateQueryResponse> Handle(GetShoppingListByCreateDateQueryRequest request,
        CancellationToken cancellationToken)
    {
        var getShoppingListByCreateDateQueryResponse = new GetShoppingListByCreateDateQueryResponse();

        var shoppingLists = await _shoppingListRepository.Get(x => x.CreateDate == request.CreateDate);
        if (shoppingLists==null)
        {
            getShoppingListByCreateDateQueryResponse.IsSuccess = false;
            return getShoppingListByCreateDateQueryResponse;
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

        getShoppingListByCreateDateQueryResponse.ShoppingList = shoppingListDtos;
        getShoppingListByCreateDateQueryResponse.IsSuccess = shoppingLists!=null;

        return getShoppingListByCreateDateQueryResponse;
    }
}