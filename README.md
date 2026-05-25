# BookRight Klinik

BookRight er et bookingsystem til klinikker inden for fysioterapi, massage, akupunktur og lignende behandlinger. Systemet bruges af receptionisten til at oprette kunder, oprette bookinger, administrere bookingstatus, se kundehistorik og beregne rabatter automatisk.

Projektet er bygget med Clean Architecture og er opdelt i Domain, UseCases, Facade, Infrastructure og UI.

## Funktioner

- Opret kunder med kontaktoplysninger og helbredsnotater.
- Opret behandlere med autorisationstype, kliniktilknytning og automatisk tilknyttede behandlingstyper.
- Opret bookinger med kunde, klinik, behandler, behandlingstype, dato og tidspunkt.
- Validering af dobbeltbookinger for behandlere.
- Validering af ledige rum på klinikken.
- Validering af om behandleren må udføre behandlingstypen.
- Automatisk rabatberegning med loyalitet, fødselsdag og kampagne.
- Visning af pris før rabat, rabatprocent, anvendt rabattype og pris efter rabat.
- Bookingadministration med aflys, ankommet, no-show og afslut.
- Kundehistorik.
- Dashboard med bookinger og nøgletal.

## Teknologier

- .NET 10
- Blazor Server
- Entity Framework Core
- SQL Server LocalDB
- xUnit
- Moq

## Projektstruktur

```text
BookRight.Domain
  Domæneobjekter, value objects, enums, interfaces og rabatlogik.

BookRight.UseCases
  Commands, queries og handlers for systemets handlinger.

BookRight.Facade
  DTOs og facades, som UI kalder.

BookRight.Infrastructure
  EF Core DbContext, repositories, migrations og databaseadgang.

BookRight.UI
  Blazor-sider og brugergrænseflade.

BookRight.Domain.Tests
BookRight.UseCases.Tests
BookRight.Infrastructure.Tests
  Automatiske tests.
```

## Krav før du starter

Installer:

- Visual Studio 2026 eller nyere med ASP.NET workload
- .NET 10 SDK
- SQL Server LocalDB

Du kan tjekke .NET-version med:

```powershell
dotnet --version
```

## Hent projektet

```powershell
git clone <repository-url>
cd BookRight-Klinik
```

Åbn derefter solution-filen:

```text
BookRight Klinik.slnx
```

## Connection string

Connection string ligger i:

```text
BookRight.UI/appsettings.json
```

Standardopsætningen bruger SQL Server LocalDB:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=BookRightDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Hvis du bruger en almindelig SQL Server-instans, kan den fx ændres til:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=BookRightDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Hvis du bruger SQL-login:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=BookRightDb;User Id=sa;Password=DIN_ADGANGSKODE;TrustServerCertificate=True;"
  }
}
```

Undgå at committe rigtige adgangskoder.

## Opret database

Kør migrations fra Package Manager Console i Visual Studio:

```powershell
Update-Database -Project BookRight.Infrastructure -StartupProject BookRight.UI
```

Eller fra terminal:

```powershell
dotnet ef database update --project BookRight.Infrastructure --startup-project BookRight.UI
```

Hvis `dotnet ef` ikke findes:

```powershell
dotnet tool install --global dotnet-ef
```

## Kør programmet

Fra Visual Studio:

1. Vælg `BookRight.UI` som startup project.
2. Start med HTTP eller HTTPS-profilen.

Fra terminal:

```powershell
dotnet run --project BookRight.UI
```

Standard URL:

```text
http://localhost:5265
```

Hvis porten allerede er i brug, stop den kørende app eller skift port i:

```text
BookRight.UI/Properties/launchSettings.json
```

## Seed data

Projektet seeder blandt andet:

- 3 klinikker:
  - BookRight Vejle, 4 rum
  - BookRight Egtved, 3 rum
  - BookRight Kolding, 3 rum
- Behandlingstyper som fysioterapi, sportsmassage, akupunktur, kostvejledning og holdtræning.
- Behandlere med kliniktilknytning og behandlingstyper.
- Kampagner.

Seed data ligger primært i:

```text
BookRight.Infrastructure/Persistence/BookRightDbContext.cs
```

## Rabatberegning

Rabatberegningen ligger i Domain-laget:

```text
BookRight.Domain/Strategies/Rabatberegner
```

Systemet bruger flere rabatberegnere:

- Loyalitetsrabat
- Fødselsdagsrabat
- Kampagnerabat

`RabatBeregnerService` kører beregnerne parallelt og vælger den bedste rabat. Resultatet bruges ved oprettelse af booking, så booking gemmes med:

- Pris uden rabat
- Pris med rabat
- Rabatprocent
- Anvendt rabattype

## Bookingflow

Når en booking oprettes:

1. UI sender bookingdata til `IBookingFacade`.
2. Facade mapper data til en command.
3. UseCase handler validerer kunde, klinik, behandler og behandlingstype.
4. Systemet tjekker dobbeltbooking.
5. Systemet tjekker ledige rum.
6. Rabat beregnes.
7. Booking gemmes i databasen.
8. UI viser popup med resultat og pris.

## Behandlerflow

Når en behandler oprettes:

1. UI kalder `IBehandlerFacade`.
2. UI vælger autorisationstype.
3. Behandlingstyper vælges automatisk ud fra autorisationstypen.
4. Brugeren vælger tilknyttede klinikker.
5. UseCase opretter behandler og tilknytter klinikker og behandlingstyper.
6. UI viser success- eller error-popup.

## Tests

Kør alle tests:

```powershell
dotnet test
```

Kør et bestemt testprojekt:

```powershell
dotnet test BookRight.Domain.Tests
dotnet test BookRight.UseCases.Tests
dotnet test BookRight.Infrastructure.Tests
```

## Clean Architecture

Projektet følger denne afhængighedsretning:

```text
UI -> Facade -> UseCases -> Domain
Infrastructure -> Domain / UseCases interfaces
```

Domain indeholder forretningsregler og må ikke afhænge af UI, database eller Facade.

UI må ikke bruge Domain eller repositories direkte. UI skal gå gennem Facade.

## Almindelige fejl

### Porten er allerede i brug

Fejl:

```text
Failed to bind to address http://127.0.0.1:5265: address already in use
```

Løsning:

Stop den kørende app i Visual Studio, eller find processen:

```powershell
netstat -ano | findstr :5265
```

Stop processen med:

```powershell
taskkill /PID <PID> /F
```

### Database findes ikke

Kør:

```powershell
Update-Database -Project BookRight.Infrastructure -StartupProject BookRight.UI
```

### Connection string virker ikke

Tjek at SQL Server LocalDB er installeret, og at connection string i `BookRight.UI/appsettings.json` passer til din maskine.

## Branch og commit workflow

Anbefalet workflow:

```powershell
git checkout -b feature/navn-paa-feature
git add .
git commit -m "kort beskrivelse"
git push
```

Merge helst gennem pull request, så konflikter og fejl opdages tidligere.
