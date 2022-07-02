using AutoMapper;
using MediatR;
using Shopping.Application.Common.Interfaces.Repositories;
using Shopping.Application.ShoppingListCQRS.Queries.GetShoppingListById;
using Shopping.Domain.Dtos;

namespace Shopping.Application.ShoppingListCQRS.Handlers.QueryHandlers.GetShoppingListById;

public class GetShoppingListByIdQueryHandler:IRequestHandler<GetShoppingListByIdQueryRequest,GetShoppingListByIdQueryResponse>
{
    private readonly IShoppingListRepository _shoppingListRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetShoppingListByIdQueryHandler(IMapper mapper, IProductRepository productRepository, IShoppingListRepository shoppingListRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
        _shoppingListRepository = shoppingListRepository;
    }

    public async Task<GetShoppingListByIdQueryResponse> Handle(GetShoppingListByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var getShoppingListByIdQueryResponse = new GetShoppingListByIdQueryResponse();

        var shoppingLists = await _shoppingListRepository.Get(x => x.Id == request.Id);
        var products = await _productRepository.Get(x => x.ShoppingListId == shoppingLists.First().Id);
        var productDtos = _mapper.Map<List<ProductDto>>(products);
        var shoppingListDto = _mapper.Map<ShoppingListDto>(shoppingLists.SingleOrDefault());
        shoppingListDto.Products = productDtos;
       

        getShoppingListByIdQueryResponse.ShoppingList = shoppingListDto;
        getShoppingListByIdQueryResponse.IsSuccess = shoppingLists!=null;

        return getShoppingListByIdQueryResponse;
    }
}