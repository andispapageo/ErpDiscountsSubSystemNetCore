# ErpDiscountsSubSystemNetCore

## .NET CORE 6 MVC Application

ErpDiscountsSubSystemNetCore is a ERP and CMS web application that inherits scope of dynamic subscriptions. <br/>
Windows based MVC website based on functional(db) with MSSQL services oriented.<br/>
<br/>

- __IDE__: *Visual Studio Community* <br/>
- __Profiles__: *ErpDiscountsSubSystemNetCore, IIS Express* <br/>

<br/>
The solution's repository is segrated to stages (production, preproduction, development) and contains migrated *ERD Diagrams* on *Infastructure.Diagrams*. <br/> <br/>
The Diagrams are showing in detail the Entity MigrationsHistory, IUser relations of AspNetUser (Identity - Individual Accounts); as along the Entity Relationsh mapping of
Customer, Orders, Subscriptions, CustomerFields, History(..). The main software based on Clean Architecture to enhase Test-driven development (TDD) & Domain-driven development (DDD).<br/>
<br/>
![alt text](https://github.com/andispapageo/ErpDiscountsSubSystemNetCore/blob/master/Infastructure.Diagrams/Screenshots/IdentityERDiagram.png)?raw=true)

**Application Aims**: <br/>
Scalabity, Performance, Testability, Maintenability and Readability.
<br/><br/>

| Architecture Architecture                                                           |
| ------------- |
| Application (Shared) |
| Domains  (Core,Common) |
| Infastructures  (Persistence) |
| Functional Tests  (Business Logic Tests)          |
| Integration Tests  (Repositories, External Testing)                          |
| WebHost Tests  (Mocking webhost )|
| Unit Tests  |
<br/><br/>

The Core design illustrates the clean and scalable project where it reminds a microservice architecture. The application is following design paatern principles::
<br/><br/>
| Principles |
| ------------- |
| Single Responsibility Principle  |
| Open/Closed Principle  |
| Liskov Substitution Principle  |
| Interface Segregation Principle  |
| Dependency Inversion  |
<br/><br/>

The app designed in N-Tiers(3) with scope the Dependency Inversion where: <br/> <br/>
1. ```Application -> depends -> Domain , Infastructure```<br/>
1. ```Infastructure -> depends -> Domain```<br/>
1. ```Domain (Common) ```
<br/><br/>
**Application contains**: <br/>
1. Behaviors,  <br/>
1. Commands, <br/>
1. Events, <br/>
1. Handlers, <br/>
1. Custom Exceptions, <br/>
1. ViewModels

**Application UserFlow**: <br/>

On app initial launch, the software seeds and migrate data in MSSQLLocalDb with EFCore.<br/><br/>
Starting in Home Page, users are able to see the Orders and available discounts with Final discount of subscriptions as along the dynamic binding of customer fields.<br/><br/>
In order for the Unauthorised user to be able to create dynamic fields for Customer, a new user should be registered with Microsoft Identity and login. Once logged in there is a navigation where an authorised user can create dynamic simple string fields and dropdown fields. 
<br/><br/>
*PS. In order to recreate the ERDiagram.Designer and ERFragram.xsd in Visual Studio 22 please open the ERDiagram.xsd in Infastructure.Diagrams Project.<br/> Open SQL Server ObjectExlplorer and connect to the new aspnet-ErpDiscountsSubSystemNetCore-(..) Database. Please drag and drop all the Tables in the ERDiagram.Designer

<br/><br/>
Kind regards


 
