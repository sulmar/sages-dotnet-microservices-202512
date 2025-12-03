
# Przykłady ze szkolenia

## Wprowadzenie

Witaj w repozytorium z materiałami do szkolenia **Architektura mikroserwisów z wykorzystaniem .NET**.

Do rozpoczęcia tego kursu potrzebujesz następujących rzeczy:

1. [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
2. [Docker](https://www.docker.com/products/docker-desktop/)

## Przygotowanie
1. Sklonuj repozytorium Git
```
git clone https://github.com/sulmar/sages-dotnet-microservices-...
```
2. Zbuduj
```
cd src
dotnet build
```

## Mapa drogowa
![Roadmap](docs/roadmap.png)

## Struktura projektów

### Clients

#### Blazor.Client
Aplikacja kliencka zbudowana w technologii Blazor WebAssembly. Zawiera:
- **Components**: Komponenty UI (Footer, Header, LoginComponent, MetricRectangle, NavBar, ProductComponent)
- **Pages**: Strony aplikacji (Dashboard, Home)
- **Model**: Modele danych (LoginModel, Product)
- **Layout**: Główny layout aplikacji (MainLayout)
- Aplikacja komunikuje się z API Gateway pod adresem `https://localhost:7011`

### ApiGateway
Brama API - punkt wejścia dla wszystkich żądań klientów (obecnie w przygotowaniu).

### IdentityProvider
Dostawca tożsamości odpowiedzialny za autentykację i autoryzację użytkowników (obecnie w przygotowaniu).

### MicroServices

#### ProductCatalog
Mikroserwis katalogu produktów z architekturą warstwową:
- **ProductCatalog.Api**: Warstwa API z kontrolerami (CategoriesController, ProductsController) i endpointami (CategoriesEndpoints, ProductsEndpoints). Używa Minimal APIs i CORS dla komunikacji z aplikacją Blazor.
- **ProductCatalog.Domain**: Warstwa domenowa zawierająca:
  - **Entities**: Encje domenowe (Product, Category, BaseEntity)
  - **Abstractions**: Interfejsy repozytoriów (IProductRepository, ICategoryRepository, IEntityRepository)
- **ProductCatalog.Infrastructure**: Warstwa infrastruktury z implementacjami repozytoriów (InMemoryProductRepository, FakeCategoryRepository) oraz kontekstem danych (Context)

#### ShoppingCart
Mikroserwis koszyka zakupów z architekturą warstwową:
- **ShoppingCart.Api**: Warstwa API z endpointami do zarządzania koszykiem zakupów. Używa Redis jako magazynu danych.
- **ShoppingCart.Domain**: Warstwa domenowa zawierająca:
  - **Entities**: Encje domenowe (CartItem)
  - **Abstractions**: Interfejsy repozytoriów (ICartRepository)
- **ShoppingCart.Infrastructure**: Warstwa infrastruktury z implementacją repozytorium Redis (RedisCartRepository)

#### Monitoring
Mikroserwis monitoringu systemu (obecnie w przygotowaniu).

#### Ordering
Mikroserwis zamówień (obecnie w przygotowaniu).

#### UserProfile
Mikroserwis profilu użytkownika (obecnie w przygotowaniu).

### Shared.Models
Współdzielone modele danych używane przez różne mikroserwisy (obecnie w przygotowaniu).
