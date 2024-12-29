# Sales API

Implementation of a Restfull API for sales handling.

## Architecture Overview

### Domain-Driven Design (DDD)
This project follows DDD principles, structuring the system around the core business domain. The main components of the system include:

Entities like User, Sale which hold business-critical information.
Repositories abstract the interaction with the database, encapsulating the logic for accessing and manipulating the domain objects.


### CQRS (Command Query Responsibility Segregation)
The application uses CQRS to separate the responsibilities of reading and writing data. Write operations (commands) and read operations (queries) are handled independently, allowing for better performance, scalability, and flexibility. This also simplifies the management of complex business logic.

Commands: Handled by command handlers, which are responsible for executing business logic (e.g., creating or updating sales).
Queries: Handled by query handlers, optimized for reading data, ensuring quick retrieval of sale and customer information.

### Strategy Pattern for Discount Application
The Strategy Pattern is used to apply different discount strategies based on business rules. The discount calculation logic is abstracted into multiple strategy classes, each implementing a different discount rule.

 - The DiscountStrategyFactory class dynamically selects the appropriate discount strategy based on quantity products.
 - This approach ensures that new discount strategies can be added without modifying the core business logic.

## Tech Stack

Technologies in this project:

Backend:
- **.NET 8.0**: A free, cross-platform, open source developer platform for building many different types of applications.
  - Git: https://github.com/dotnet/core
- **C#**: A modern object-oriented programming language developed by Microsoft.
  - Git: https://github.com/dotnet/csharplang

Testing:
- **xUnit**: A free, open source, community-focused unit testing tool for the .NET Framework.
  - Git: https://github.com/xunit/xunit

Databases:
- **PostgreSQL**: A powerful, open source object-relational database system.
  - Git: https://github.com/postgres/postgres
- **Redis**: Redis is an in-memory, open-source key-value store used for caching, messaging, and data persistence with high performance and scalability.
  - Git: https://github.com/redis/redis
 
## Frameworks

Frameworks in this project:

Backend:
- **Mediator**: A behavioral design pattern that helps reduce chaotic dependencies between objects. It allows loose coupling by encapsulating object interaction.
  - Git: https://github.com/jbogard/MediatR
- **Automapper**: A convention-based object-object mapper that simplifies the process of mapping one object to another.
  - Git: https://github.com/AutoMapper/AutoMapper

Testing:
- **Faker**: A library for generating fake data for testing purposes, allowing for more realistic and diverse test scenarios.
  - Git: https://github.com/bchavez/Bogus
- **NSubstitute**: A friendly substitute for .NET mocking libraries, used for creating test doubles in unit testing.
  - Git: https://github.com/nsubstitute/NSubstitute

Database:
- **EF Core**: Entity Framework Core, a lightweight, extensible, and cross-platform version of Entity Framework, used for data access and object-relational mapping.
  - Git: https://github.com/dotnet/efcore

## Instructions for use

1) Run docker compose to iniciate the conteiners
2) Run the command update-database to create tables in database. If it doesn't work look the appsettings conection string. By default the port postgresql 5432 is exposed.
3) Use swagger (or another toool) to create your user. Post to /api/Users

## User

 ### Create User. Post to /api/User 
   
Request:

```json
{
  "username": "string",
  "password": "string",
  "phone": "string",
  "email": "string",
  "status": 0,
  "role": 0
}
```

Respose:

```json
{
  "success": true,
  "message": "string",
  "errors": [
    {
      "error": "string",
      "detail": "string"
    }
  ],
  "data": {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "name": "string",
    "email": "string",
    "phone": "string",
    "role": 0,
    "status": 0
  }
}
```

## Get User. Get to /api/User/{id}

Response:

```json
{
  "success": true,
  "message": "string",
  "errors": [
    {
      "error": "string",
      "detail": "string"
    }
  ],
  "data": {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "name": "string",
    "email": "string",
    "phone": "string",
    "role": 0,
    "status": 0
  }
}
```

## Delete User. Delete to /api/User/{id}

```json
{ 
   {
   "success": true,
   "message": "string",
   "errors": [
      {
         "error": "string",
         "detail": "string"
      }
   ]
   }
} 
```

## Authorize

### Auth User. Post to /api/Auth 
   
Request:

```json
{
  "email": "string",
  "password": "string"
}
```

Response:

```json
{
  "success": true,
  "message": "string",
  "errors": [
    {
      "error": "string",
      "detail": "string"
    }
  ],
  "data": {
    "token": "string",
    "email": "string",
    "name": "string",
    "role": "string"
  }
}
```

## Sale

### Create sales. Post to /api/Sale. 
   
Request:

```json
{
  "saleItems": [
    {
      "productId": "string",
      "quantity": 0,
      "unitPrice": 0
    }
  ],
  "customerId":"3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "sellingCompanyId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

Response:

```json
{
  "success": true,
  "message": "string",
  "errors": [
    {
      "error": "string",
      "detail": "string"
    }
  ],
  "data": {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
  }
}
```

### Update sale. Put to /api/Sale. 
   
Request:

```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "sellingCompanyId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "customerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "saleNumber": "string",
  "saleItems": [
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "productId": "string",
      "quantity": 0,
      "unitPrice": 0
    }
  ]
}
```

Response:

```json
{
  "success": true,
  "message": "string",
  "errors": [
    {
      "error": "string",
      "detail": "string"
    }
  ],
  "data": {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "sellingCompanyId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "isCancelled": true,
    "amount": 0,
    "createdAt": "2024-12-29T11:54:54.520Z",
    "updatedAt": "2024-12-29T11:54:54.520Z",
    "saleItems": [
      {
        "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "quantity": 0,
        "discount": 0,
        "unitPrice": 0
      }
    ]
  }
}
```

### Get sale. Get to /api/Sale/{id}. 
   
Response:

```json
{
  "success": true,
  "message": "string",
  "errors": [
    {
      "error": "string",
      "detail": "string"
    }
  ],
  "data": {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "sellingCompanyId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "isCancelled": true,
    "amount": 0,
    "createdAt": "2024-12-29T11:56:51.298Z",
    "updatedAt": "2024-12-29T11:56:51.298Z",
    "saleItems": [
      {
        "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "quantity": 0,
        "discount": 0,
        "unitPrice": 0
      }
    ]
  }
}
```

### Get all sales. Get to /api/Sales/Search. 

Params: page, size, order. Defalt values: 1, 10, empty
example order: Amount desc, Amount asc

Response:

```json
{
  "success": true,
  "message": "string",
  "errors": [
    {
      "error": "string",
      "detail": "string"
    }
  ],
  "data": {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "sellingCompanyId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "isCancelled": true,
    "amount": 0,
    "createdAt": "2024-12-29T11:57:30.307Z",
    "updatedAt": "2024-12-29T11:57:30.307Z",
    "saleItems": [
      {
        "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "quantity": 0,
        "discount": 0,
        "unitPrice": 0
      }
    ]
  },
  "currentPage": 0,
  "totalPages": 0,
  "totalCount": 0
}
```

### Delete Sale. Delete to /api/Sale/{id}

Response:

```json
{
  "success": true,
  "message": "string",
  "errors": [
    {
      "error": "string",
      "detail": "string"
    }
  ],
  "data": {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
  }
}
```

### Cancel Sale. Put to /api/Sale/Cancel/{id}

Response:

```json
{
  "data": {
    "cancel": true,
    "id": "eb5fddcd-a7cc-4c8c-ad3b-db87e4810755"
  },
  "success": true,
  "message": "",
  "errors": []
}
```