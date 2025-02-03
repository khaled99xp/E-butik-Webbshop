# API - Onlinebutik (VG-projekt)

## Projektbeskrivning

Detta projekt är en onlinebutik-applikation som implementerar ett REST API med ASP.NET Core samt en dynamisk frontend byggd med HTML, CSS (Bootstrap och egna stilar) och JavaScript. Projektet uppfyller kraven för både G- och VG-nivåer. För VG-kraven har vi bland annat implementerat:

- **Säkerhet och roller:**  
  - Lösenord krypteras med SHA256 innan de sparas i databasen (alternativt kan ASP.NET Core Identity användas).  
  - Administratörens endpoints skyddas med en API-nyckel som läses från konfigurationen (nyckeln lagras aldrig i databasen).  
  - Användarroller hanteras med en flagga `IsAdmin` i User-modellen, och vid inloggning returneras ett specifikt token för admin ("1234test1234") och ett annat token för vanliga användare.  
  - Vid inloggning sparas även sessiondata (användarens e-post och roll).

- **Datahantering:**  
  - All data lagras i en InMemory-databas via Entity Framework Core.  
  - Produkter tillhör olika kategorier, och kategorier kan skapas dynamiskt.  
  - Vid orderläggning minskar lagersaldo och om lagret inte räcker returneras ett felmeddelande med status 409.

- **Frontend:**  
  - Den dynamiska frontenden består av flera sidor:
    - **register.html:** Registreringssida.
    - **login.html:** Inloggningssida.
    - **index.html:** Startsida med produktöversikt.
    - **product.html:** Produktsida med detaljerad information.
    - **admin.html:** Administratörspanel där admin kan lägga till nya produkter och kategorier.
  - Frontend-filerna använder Bootstrap för responsiv design, egna CSS-filer för ytterligare stilar, och JavaScript (Fetch API) för att kommunicera med backend.

## Frontend-filer

Frontend-mappen innehåller följande filer:

- **index.html:** Startsida där alla produkter visas.
- **login.html:** Sidan för inloggning.
- **register.html:** Sidan för att registrera en ny användare.
- **product.html:** Sidan som visar detaljerad information om en specifik produkt.
- **admin.html:** En administratörspanel som endast är åtkomlig för admin (baserat på inloggning) där man kan lägga till produkter och kategorier.
- **styles.css:** Egna stilar som kompletterar Bootstrap och ger en unik design.
- **app.js:** JavaScript-kod som hanterar användarregistrering, inloggning, hämtning av produkter, visning av produktdetaljer samt interaktioner i admin-panelen.

## Version och Teknologi

- **Målramverk:** .NET 9.0 (ASP.NET Core med TargetFramework net9.0)  
- **Version:** 1.0.0  
- **Utvecklingsdatum:** 2025-02-01  
- **Författare:** [Khaled Khalosi]

## Använda Bibliotek & Paket

Projektet använder följande bibliotek och paket:

- **Microsoft.AspNetCore.OpenApi** (Version 9.0.1)  
- **Microsoft.EntityFrameworkCore** (Version 9.0.1)  
- **Microsoft.EntityFrameworkCore.InMemory** (Version 9.0.1)  
- **Swashbuckle.AspNetCore** (Version 7.2.0)  
- **Distributed Memory Cache** – används för sessionhantering via `AddDistributedMemoryCache()`
- **Bootstrap 4.5.2** – för frontend-design
- **JavaScript (Fetch API)** – för att kommunicera med backend
- **Google Fonts (Cairo)** – för typsnitt
- **SHA256** – för lösenordskryptering

### Paketinstallation med dotnet CLI

Du kan installera de nödvändiga paketen genom att köra följande kommandon i terminalen i projektets rotmapp:

```bash
dotnet add package Microsoft.AspNetCore.OpenApi --version 9.0.1
dotnet add package Microsoft.EntityFrameworkCore --version 9.0.1
dotnet add package Microsoft.EntityFrameworkCore.InMemory --version 9.0.1
dotnet add package Swashbuckle.AspNetCore --version 7.2.0
