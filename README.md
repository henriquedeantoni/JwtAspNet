# userJwtApp

Para clonar o projeto:
```git clone```

Após criação do banco e configuração das variáveis de ambiente, compile no terminal do VS Code:

```dotnet build```

Rode a aplicação:

```dotnet run```

### VARIÁVEIS DE AMBIENTE

em produção:

```
JWT_SYMETRIC_KEY=
CLAIM_ID=ID
JWT_ISSUER=
MYSQL_CONNECTION=
PASSWORD=
```

## Banco de Dados

Esta aplicação foi configurada para trabalhar com o banco de dados mySQL

### Banco de testes

Para execução dos testes, utilizar um banco de testes

Criaçao do banco:
```
CREATE DATABASE userJwtApp_db;
```
Criação das tabelas:
- Users
```
CREATE TABLE users (
    ID CHAR(36) NOT NULL PRIMARY KEY,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    Username VARCHAR(50) NOT NULL UNIQUE,
    Email VARCHAR(50) NOT NULL,
    Password VARCHAR(255) NOT NULL,
    createdAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);
```
- Products
```
CREATE TABLE Products (
    ID CHAR(36) NOT NULL PRIMARY KEY,
    ProductName VARCHAR(100) NOT NULL,
    Serial VARCHAR(100) NOT NULL,
    Price DECIMAL(18,2) NOT NULL,
    CreatedById INT NOT NULL,
    FOREIGN KEY (CreatedById) REFERENCES User(ID),
    createdAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);
```
