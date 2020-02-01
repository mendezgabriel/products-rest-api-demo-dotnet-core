# products-rest-api-demo-dotnet-core

## Description:
A RESTful API demo project developed with .Net Core following SOLID principles and best practices.

Developed on Dec 2019 using VS2017 by Gabriel Mendez (Github user [mendezgabriel](https://github.com/mendezgabriel)) as a RESTFul API using .Net Core 2.1. 

## Overview:
The API is a demo that could be used in a e-commerce or shopping cart-like application and its goal is to manage products and product options using [CRUD](https://en.wikipedia.org/wiki/Create,_read,_update_and_delete) operations. This API must be consumed with any HTTP client such as a Web application, mobile app, another service, a console app, etc. 

It uses decoupling of dependecies via interfaces and their implementations as stated by the [SOLID](https://en.wikipedia.org/wiki/SOLID) principles and the architecture pattern known as the [Onion Architecture](http://jeffreypalermo.com/blog/the-onion-architecture-part-1/)

## Details:
- It uses dependency injection to resolve all dependencies.

- Basic logging to console with [Serilog](https://github.com/serilog/serilog) has been added as an example.

- It uses SQL server the for the data store. An .mdf file of the database is included in the `Products.Service.Repositories\Data` folder. [Automapper](https://github.com/AutoMapper/AutoMapper) is used for easy mapping between the data model and the application's domain model.

- To access the SQL data, [Entity Framework Core](https://github.com/dotnet/efcore) is used.

The following endpoints are included:

### Products
* `GET /products` - Gets all products.
* `GET /products?name={name}` - Finds all products matching the specified name.
* `GET /products/{id}` - Gets the project that matches the specified ID - ID is a GUID.
* `POST /products` - Creates a new product.
* `PUT /products/{id}` - Updates a product.
* `DELETE /products/{id}` - Deletes a product and its options.

### Product Options
* `GET /products/{id}/options` - Finds all options for a specified product.
* `GET /products/{id}/options/{optionId}` - Finds the specified product option for the specified product.
* `POST /products/{id}/options` - Adds a new product option to the specified product.
* `PUT /products/{id}/options/{optionId}` - Updates the specified product option.
* `DELETE /products/{id}/options/{optionId}` - Deletes the specified product option.

Models conform to:

**Product:**
```
{
  "id": "01234567-89ab-cdef-0123-456789abcdef",
  "name": "Product name",
  "description": "Product description",
  "price": 123.45,
  "deliveryPrice": 12.34
}
```

**Products:**
```
{
  "items": [
    {
      // product
    },
    {
      // product
    }
  ]
}
```

**Product Option:**
```
{
  "id": "01234567-89ab-cdef-0123-456789abcdef",
  "name": "Product name",
  "description": "Product description"
}
```

**Product Options:**
```
{
  "items": [
    {
      // product option
    },
    {
      // product option
    }
  ]
}
```

## Unit Tests:
Only a sample test class was included for the common project. However, a proposed VS project solution structure is provided for reference. Test harness is created using [XUnit](https://xunit.net/) testing framework, [Autofixture](https://github.com/AutoFixture/AutoFixture) for test setup and [NSubstitue](https://github.com/nsubstitute/NSubstitute) for dependencies mocking. [FluentAssertions](https://github.com/fluentassertions/fluentassertions) is used for readable, fluent-like-syntax assertions.

## Limitations:
Because of scope limitations for this demo, only basic functionality is included. For a real world scenario improvements should be made to this implementation in order to follow other enterprise-grade best practices like pagination, caching, HATEOAS metadata to the response payloads, schema validation, better exception handling, integration testing (with Postman) and of course automated CI/CD pipelines for deployment.

Limited unit testing has been added to demonstrate the concepts.

## Requirements:
- VS 2017 or newer for debugging.
- This application requires an SQL Server LocalDB instance installed in the local development machine in order to run
successfully. It uses the default instance name of `(localdb)\MSSQLLocalDB`
- In order to Run the application from a development machine, download the source code, use VS or VS Code to build it and access the API operations through an HTTP Client such as [Postman](https://www.getpostman.com/) or build your own!.

