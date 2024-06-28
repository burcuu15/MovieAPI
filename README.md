# MovieAPI

Bu proje, kullanıcıların film tavsiyeleri alabileceği ve filmler hakkında notlar ve puanlar ekleyebileceği basit bir film tavsiye uygulamasıdır. 
API, .NET Core kullanılarak geliştirilmiştir ve aşağıdaki özelliklere sahiptir:

- Kullanıcı kimlik doğrulaması (Auth0 kullanarak)
- Film listeleme
- Filmlere not ve puan ekleme
- Film detaylarını görüntüleme
- E-posta ile film tavsiyesi gönderme
- TheMovieDB API'dan periyodik veri güncelleme
- Swagger kullanarak API dokümantasyonu

## Gereksinimler

- .NET SDK 8.0
- SQL Server 
- Auth0 hesabı
- TheMovieDB API anahtarı

## Kurulum

1. Bu projeyi yerel makinenize klonlayın:
   ```bash
   git clone https://github.com/kullanici_adi/MovieRecommendationAPI.git
   cd MovieRecommendationAPI

2. Gerekli Nuget paketlerini yükleyin.
dotnet restore

3. appsettings.json dosyasını oluşturun ve aşağıdaki gibi yapılandırın:
{
  "Auth0": {
    "Domain": "YOUR_AUTH0_DOMAIN",
    "ClientId": "YOUR_AUTH0_CLIENT_ID",
    "ClientSecret": "YOUR_AUTH0_CLIENT_SECRET",
    "Audience": "YOUR_API_IDENTIFIER"
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=MovieDB;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "TheMovieDB": {
    "ApiKey": "YOUR_THEMOVIEDB_API_KEY",
    "MaxMovies": 100
  },
  "BackgroundTask": {
    "FetchIntervalHours": 12
  }
}

4. Veritabanınızı güncelleyin:
dotnet ef database update

5. Uygulamayı çalıştırın:
dotnet run

6. Swagger UI'yi ziyaret ederek API dokümantasyonunu görüntüleyin:
https://localhost:7248/swagger/index.html



##Kullanım

Film Listeleme
GET /api/Movies: Tüm filmleri listeler.

Film Detay Görüntüleme
GET /api/Movies/{id}: Belirli bir filmin detaylarını görüntüler.

Filme Not ve Puan Ekleme
POST /api/Movies/{id}/ratings: Belirli bir filme not ve puan ekler.

Film Tavsiye Etme
POST /send-email: E-posta ile film tavsiyesi gönderir.

