# ErpDiscountsSubSystemNetCore

## .NET CORE 6 MVC Application

ErpDiscountsSubSystemNetCore is a ERP and CMS web application that inherits scope of dynamic subscriptions. 

The main idea is based on Clean Architecture in order to enhase 
Test-driven development (TDD) & Domain-driven development (DDD).

| Architecture Design |
| ------------- |
| Application (Shared) |
| Domains (Core,Common) |
| Infastructures (Persistence) |
| Functional Tests (Business Logic Tests) |
| Integration Tests  (Repositories, External Testing)|
| WebHost Tests  (Mocking webhost )|
| Unit Tests  |

The app designed purely on Dependency Inversion where: 
```Application -> Inherits -> Domain , Infastructure```
```Infastructure -> Inherits -> Domain```
```Domain (Common).```

Core design of this project which mimics a microservice architecture is based on SOLID:

| Principles |
| ------------- |
| Single Responsibility Principle  |
| Open/Closed Principle  |
| Liskov Substitution Principle  |
| Interface Segregation Principle  |
| Dependency Inversion  |




 
