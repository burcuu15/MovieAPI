using MovieAPI.Models;

namespace MovieAPI.Services
{
    public class MovieUpdateService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly IServiceScopeFactory _scopeFactory;

        public MovieUpdateService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Timer oluşturulur ve UpdateMovies metodu belirli aralıklarla çalıştırılır
            _timer = new Timer(UpdateMovies, null, TimeSpan.Zero, TimeSpan.FromHours(12));
            return Task.CompletedTask;
        }

        private async void UpdateMovies(object state)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<MovieDbContext>();

                // Burada TheMovieDB API'dan veri çekme ve güncelleme işlemleri gerçekleştirilebilir
                // Örnek olarak, maksimum film sayısını kontrol edip güncelleme işlemleri yapılabilir
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            //timer durdurulur
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            //timer kaynakları temizlenir
            _timer?.Dispose();
        }
    }
}
