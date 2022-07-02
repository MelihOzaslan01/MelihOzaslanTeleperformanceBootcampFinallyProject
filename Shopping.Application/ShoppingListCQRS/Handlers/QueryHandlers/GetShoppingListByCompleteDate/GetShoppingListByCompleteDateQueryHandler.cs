using AutoMapper;
using MediatR;
using Shopping.Application.Common.Interfaces.Repositories;
using Shopping.Application.ShoppingListCQRS.Queries.GetShoppingListByCompleteDate;
using Shopping.Domain.Dtos;

namespace Shopping.Application.ShoppingListCQRS.Handlers.QueryHandlers.GetShoppingListByCompleteDate;

public class
    GetShoppingListByCompleteDateQueryHandler : IRequestHandler<GetShoppingListByCompleteDateQueryRequest,
        GetShoppingListByCompleteDateQueryResponse>
{
    private readonly IShoppingListRepository _shoppingListRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetShoppingListByCompleteDateQueryHandler(IShoppingListRepository shoppingListRepository,
        IProductRepository productRepository, IMapper mapper)
    {
        _shoppingListRepository = shoppingListRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<GetShoppingListByCompleteDateQueryResponse> Handle(GetShoppingListByCompleteDateQueryRequest request,
        CancellationToken cancellationToken)
    {
        var getShoppingListByCompleteDateQueryResponse = new GetShoppingListByCompleteDateQueryResponse();

        var shoppingLists = await _shoppingListRepository.Get(x => x.CompleteDate == request.CompleteDate);
        if (shoppingLists==null)
        {
            getShoppingListByCompleteDateQueryResponse.IsSuccess = false;
            return getShoppingListByCompleteDateQueryResponse;
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

        getShoppingListByCompleteDateQueryResponse.ShoppingList = shoppingListDtos;
        getShoppingListByCompleteDateQueryResponse.IsSuccess = shoppingLists!=null;

        return getShoppingListByCompleteDateQueryResponse;
    }
}