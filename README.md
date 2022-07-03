# <div align="center">Shopping Project</div>
</br>
An shopping application that I created with .Net 6 where you can create shopping lists.

</br>

# Project Structure

```
src
├── Shopping.API
|   ├── Controllers 
|   └── Extensions 
├── Shopping.Application
|   ├── Auth 
|   ├── Common 
|   ├── Mapping
|   ├── ProductCQRS 
|   ├── ShoppingCQRS 
|   └── UserCQRS 
├── Shopping.Domain 
|   ├── Entities  
|   └── Dtos 
├── Shopping.Infrastructure   
|   └── Persistence
└── Shopping.Tests
    ├── EAuction.Core
    ├── EAuction.Infrastructure
    └── EAuction.UI
```


# 🚀 Building and Running for Production

Follow these steps to get your development environment set up: (Before Run Start the Docker Desktop)

#### With Docker

At the root directory which include docker-compose.yml files, run below command:

        docker-compose up -d --build

#### Without Docker

Make sure MSSQL is running and run project


# ENDPOINTS
* Base Path: http://localhost:8080/
* Swagger: http://localhost:8080/swagger/index.html

* Auth API: http://localhost:8080/Auth
  * /Login/{user} (POST)
* Product API: http://localhost:8080/Product
    * /AddProduct/{CreateProductCommandRequest} (POST)
    * /UpdateProduct/{UpdateProductCommandRequest} (PUT)
    * /DeleteProduct/{DeleteProductCommandRequest} (DELETE)
    * /GetProducts (GET)
    * /GetProductById/{id} (GET)
    * /GetProductByShoppingListId/{id} (GET)
* Shopping List API: http://localhost:8080/ShoppingList
    * /AddShoppingList/{CreateShoppingListCommandRequest} (POST)
    * /CompleteShoppingList/{CompleteShoppingListCommandRequest} (POST)
    * /UpdateShoppingList/{UpdateShoppingListCommandRequest} (PUT)
    * /DeleteShoppingList/{DeleteShoppingListCommandRequest} (DELETE)
    * /GetShoppingLists (GET)
    * /AdminGetShoppingLists (GET)
    * /GetShoppingListsById/{id} (GET)
    * /GetShoppingListsByTitle/{title} (GET)
    * /GetShoppingListsByDescription/{desc} (GET)
    * /GetShoppingListsByCategory/{category} (GET)
    * /GetShoppingListsByCompleted/{completed} (GET)
    * /GetShoppingListsByCreateDate/{date} (GET)
    * /GetShoppingListsByCompleteDate/{date} (GET)
* User API: http://localhost:8080/User
    * /AddUser/{CreateUserCommandRequest} (POST)
