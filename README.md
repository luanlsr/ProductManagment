# 📦 Sistema de Gestão de Produtos

## 🚀 Visão Geral
Este é um **Sistema de Gestão de Produtos** desenvolvido com **ASP.NET Core MVC**, seguindo os princípios do **Domain-Driven Design (DDD)**.  
O projeto inclui **Autenticação de Usuários com Identity**, **Gestão de Produtos, Pedidos e Estoque**, além do uso do **Padrão de Repositório** para acesso ao banco de dados.

---

## 📂 Estrutura do Projeto
Este projeto segue os princípios de **DDD** e está estruturado em várias camadas:

ProductManagment  
├── Apresentation  
│   ├── ProductManagment.Web
│   ├── Controllers  
│	├── Views  
├── Application  
│   ├── ProductManagment.Application  
│   ├── DTOs 
│   ├── Services  
├── Domain  
│   ├── ProductManagment.Domain  
│   ├── Entities
│   ├── Exceptions
│   ├── Interfaces
│   └── ValueObjects  
├── Infrastructure  
│   ├── ProductManagment.Infrastructure.Data  
│   ├── Repositories  
│   └── Context  
├── Tests  
│   ├── TaskFlow.Tests.Unit  

- **`Web`**: Camada de apresentação, responsável por lidar com a interação do usuário (**Controllers, Views e Razor Pages**).
- **`Application`**: Contém a lógica de negócios centralizada em **Serviços**.
- **`Domain`**: Camada principal do sistema, contendo **Entidades, Agregados e Interfaces**.
- **`Infrastructure`**: Responsável pela persistência no banco de dados, implementando **Entity Framework Core** e **Padrão de Repositório**.
- **`Tests`**: Contém **testes unitários e de integração** utilizando **xUnit, Moq e FluentAssertions**.

---

## 🛠️ Tecnologias Utilizadas
- **.NET 7+**
- **ASP.NET Core MVC**
- **Entity Framework Core**
- **SQL Server**
- **Identity para Autenticação**
- **FluentValidation** (para validação de entrada)
- **MediatR** (para CQRS e eventos de domínio)
- **Serilog** (para logs)
- **Swagger** (para documentação da API)
- **xUnit e Moq** (para testes unitários)

---

## ⚙️ Configuração e Execução

### 🔹 **Pré-requisitos**
Antes de executar o projeto, certifique-se de ter instalado:
- **.NET 8+** ([Baixar SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0))
- **SQL Server** ([Baixar](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads))
- **Visual Studio 2022** ou **VS Code**

---

### 🔹 **Configuração do Banco de Dados**
1️⃣ **Abra o arquivo `appsettings.json`** e configure a **String de Conexão** do SQL Server:
```json
"ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ProductManagementDB;Trusted_Connection=True;"
}
```

2️⃣ Execute as migrations do banco de dados:
```
dotnet ef database update

```

## ⚙️  Executando o Projeto
1️⃣ Clone o repositório:

```
git clone https://github.com/luanlsr/ProductManagment.git
cd ProductManagement
```

2️⃣ Restaure os pacotes do projeto:

```
dotnet restore
```

3️⃣ Compile e execute a aplicação:

```
dotnet run --project ProductManagement.Web
```

4️⃣ Acesse a aplicação no navegador:

```
http://localhost:5183
```