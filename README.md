# Description

This project is a template project that implements a simple Hexagonal Architecture, with some facilitation packages and patterns.

This sample is targeted to Azure and Postgres, to adapt it to other clouds, you will need to change:
- **Configurations**: Any Azure related configuration will be market, so just delete them.
- **Session**: Project responsible to initiate the DBSession, any data base library that extends from this can be used, only changing the package and related objects.
- **Blob**: It's targeted for Azure Containers, change this project for other for any desired blob.

## Patterns

- **DDD** (Domain Driven Design): Really good pattern to decople integration logic from business logic.
  - Domain: Business objects that will be stored.
  - DTOs (Data Transfer Objects): objects responsible for the input / ouput of the application.
  
- **Middleware**: Intercept all the calls and standardize the outputs "CustomControllerBase", used by inheritance
- **Filters**: Intercept all the calls and log the executions, used by injetion
- **Unit of Work**: used to control the data base transactions easier, controlling the transaction workflows automatically.

## Packages

- **Swashbuckle**: Facilitates the Swagger generation for the API web interface.
- **RachTech.Logging**: It's a ILogger extesion library that facilitate the process of logging and tracking the application. [Rauchtech.Logging](https://github.com/eduardorauchbach/Rauchtech.Logging)
- **MethodBoundaryAspect.Fody**: Required to inject logs in any method that uses the [LogAspect] annotation from the RauchTech.Logging.
- **Dapper**: Makes the mapping from the data base easier and cleaner, as for the scripts executions and parameters.

## Security

- **Bearer Token**: One of the most common authentication methods, and it's used to validate the access to certain features and actions.
- **Password and Verification**: Encrypted to be saved and checked directly using the same encrypt key to check the results, not being logged anywhere.

<br/>
<br/>

# DDD

```mermaid
graph LR
    subgraph DTO Request

        ParametersReq(Parameters)
        style ParametersReq stroke:green, stroke-width:2px

        ConversionsReq(Conversions)
        style ConversionsReq stroke:#880000, stroke-width:2px

        MethodsReq(Methods)
        style MethodsReq stroke:orange, stroke-width:2px

        OperatorsReq(Operators)
        style OperatorsReq stroke:dodgerblue, stroke-width:2px

    end

    subgraph Domain

        Parameters(Parameters)
        style Parameters stroke:green, stroke-width:2px

        Methods(Methods)
        style Methods stroke:orange, stroke-width:2px

    end

    subgraph DTO Response

        ParametersRes(Parameters)
        style ParametersRes stroke:green, stroke-width:2px

        ConversionsRes(Conversions)
        style ConversionsRes stroke:#880000, stroke-width:2px

        MethodsRes(Methods)
        style MethodsRes stroke:orange, stroke-width:2px

        OperatorsRes(Operators)
        style OperatorsRes stroke:dodgerblue, stroke-width:2px

    end

    ParametersReq ==> ConversionsReq
    linkStyle 0 stroke:green

    ConversionsReq ==> OperatorsReq
    linkStyle 1 stroke:#880000

    OperatorsReq ==> Parameters
    linkStyle 2 stroke:dodgerblue

    Parameters ==> ConversionsRes
    linkStyle 3 stroke:green

    ConversionsRes ==> OperatorsRes
    linkStyle 4 stroke:#880000

    OperatorsRes ==> ParametersRes
    linkStyle 5 stroke:dodgerblue
```


# Getting Started
TODO: Guide users through getting your code up and running on their own system. In this section you can talk about:
1.	Installation process
2.	Software dependencies
3.	Latest releases
4.	API references

# Build and Test
TODO: Describe and show how to build your code and run the tests. 

# Contribute
TODO: Explain how other users and developers can contribute to make your code better. 

If you want to learn more about creating good readme files then refer the following [guidelines](https://docs.microsoft.com/en-us/azure/devops/repos/git/create-a-readme?view=azure-devops). You can also seek inspiration from the below readme files:
- [ASP.NET Core](https://github.com/aspnet/Home)
- [Visual Studio Code](https://github.com/Microsoft/vscode)
- [Chakra Core](https://github.com/Microsoft/ChakraCore)