
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

#### YarpApiGateway
Brama API zbudowana z wykorzystaniem YARP (Yet Another Reverse Proxy) - punkt wejścia dla wszystkich żądań klientów. Zawiera:
- Konfigurację reverse proxy z obsługą Service Discovery
- Middleware do logowania żądań i odpowiedzi
- Endpoint `/ping` do weryfikacji dostępności

### IdentityProvider
Dostawca tożsamości odpowiedzialny za autentykację i autoryzację użytkowników (obecnie w przygotowaniu).

### MicroServices

#### Dashboard
Mikroserwis dashboardu agregujący dane z różnych mikroserwisów:
- **Dashboard.Api**: Warstwa API z endpointem `/api/dashboard` agregującym dane z ProductCatalog i ShoppingCart
- **Services**: Serwisy do komunikacji z innymi mikroserwisami (ApiProductService, ApiCartService)
- Używa Service Discovery do odnajdywania innych mikroserwisów
- Wykonuje równoległe zapytania do różnych serwisów używając `Task.WhenAll`

#### Document
Mikroserwis przetwarzania dokumentów zbudowany jako aplikacja Worker/Hosted Service:
- **Document.Api**: Aplikacja hostująca workerów do przetwarzania zdarzeń
- **Channels**: Kanały komunikacji (OrderPlacedEventChannel)
- **Workers**: Workerzy do przetwarzania zdarzeń (OrderProcessingWorker, RedisStreamWorker)
- Używa Redis Streams do odbierania i przetwarzania zdarzeń z systemu

#### Ordering
Mikroserwis zamówień odpowiedzialny za tworzenie i przetwarzanie zamówień:
- **Ordering.Api**: Warstwa API z endpointem `/api/orders` do tworzenia zamówień
- **Services**: Serwisy do komunikacji z Payment (PaymentService) i publikowania zdarzeń (RedisProducer)
- Używa gRPC do komunikacji z mikroserwisem Payment
- Publikuje zdarzenia OrderPlaced do Redis Streams po pomyślnym przetworzeniu płatności
- Generuje unikalne identyfikatory zamówień używając Nanoid

#### Payment
Mikroserwis płatności udostępniający funkcjonalność płatności przez gRPC:
- **Payment.Api**: Serwis gRPC implementujący interfejs płatności
- **Services**: Implementacja serwisu płatności (PaymentServiceImplementation)
- **Protos**: Definicje protobuf dla komunikacji gRPC (payment.proto)
- Umożliwia weryfikację i przetwarzanie płatności dla zamówień

#### ProductCatalog
Mikroserwis katalogu produktów z architekturą warstwową:
- **ProductCatalog.Api**: Warstwa API z kontrolerami (CategoriesController, ProductsController) i endpointami (CategoriesEndpoints, ProductsEndpoints). Używa Minimal APIs i CORS dla komunikacji z aplikacją Blazor.
- **ProductCatalog.Domain**: Warstwa domenowa zawierająca:
  - **Entities**: Encje domenowe (Product, Category, BaseEntity)
  - **Abstractions**: Interfejsy repozytoriów (IProductRepository, ICategoryRepository, IEntityRepository)
- **ProductCatalog.Infrastructure**: Warstwa infrastruktury z implementacjami repozytoriów (InMemoryProductRepository, FakeCategoryRepository) oraz kontekstem danych (Context)

#### ShoppingCart
Mikroserwis koszyka zakupów z architekturą warstwową:
- **ShoppingCart.Api**: Warstwa API z endpointami do zarządzania koszykiem zakupów. Używa Redis jako magazynu danych i Service Discovery do komunikacji z Ordering API.
- **ShoppingCart.Domain**: Warstwa domenowa zawierająca:
  - **Entities**: Encje domenowe (CartItem)
  - **Abstractions**: Interfejsy repozytoriów i serwisów (ICartRepository, ICartService)
  - **CartService**: Logika biznesowa koszyka (checkout)
- **ShoppingCart.Infrastructure**: Warstwa infrastruktury z implementacją repozytorium Redis (RedisCartRepository)

#### Monitoring
Mikroserwis monitoringu systemu (obecnie w przygotowaniu).

#### UserProfile
Mikroserwis profilu użytkownika (obecnie w przygotowaniu).

### Shared.Models
Współdzielone modele danych używane przez różne mikroserwisy (obecnie w przygotowaniu).
