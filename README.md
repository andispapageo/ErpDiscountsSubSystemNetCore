# ErpDiscountsSubSystemNetCore

## .NET CORE 6 MVC Application

ErpDiscountsSubSystemNetCore is a ERP and CMS web application that inherits scope of dynamic subscriptions. 
Windows based MVC website based on functional(db) service oriented.

The main idea is based on Clean Architecture in order to enhase  Test-driven development (TDD) & Domain-driven development (DDD).

| Architecture Design |
| ------------- |
| Application (Shared) |
| Domains (Core,Common) |
| Infastructures (Persistence) |
| Functional Tests (Business Logic Tests) |
| Integration Tests  (Repositories, External Testing)|
| WebHost Tests  (Mocking webhost )|
| Unit Tests  |

The Core design illustrates the clean and scalable project where it reminds a microservice architecture. The application is following design paatern principles::

| Principles |
| ------------- |
| Single Responsibility Principle  |
| Open/Closed Principle  |
| Liskov Substitution Principle  |
| Interface Segregation Principle  |
| Dependency Inversion  |


The app designed in N-Tiers(3) with scope the Dependency Inversion where: <br/> <br/>
1. ```Application -> depends -> Domain , Infastructure```<br/>
1. ```Infastructure -> depends -> Domain```<br/>
1. ```Domain (Common) ```

**Application contains**: <br/>
1. Behaviors,  <br/>
1. Commands, <br/>
1. Events, <br/>
1. Handlers, <br/>
1. Custom Exceptions, <br/>
1. ViewModels




 
