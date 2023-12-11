using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;



namespace EkstremHava.TimeOut
{
    public class SessionCleanupService : BackgroundService
    {
        private readonly IServiceProvider _provider;

        public SessionCleanupService(IServiceProvider provider)
        {
            _provider = provider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _provider.CreateScope())
                {
                    var session = scope.ServiceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext.Session;
                    if (session.IsAvailable && session.Keys.Contains("UserId"))
                    {
                        // Oturum süresini kontrol et ve belirlediğiniz zaman aşımını geçmişse oturumu sonlandır.
                        var lastAccessed = session.GetString("LastAccessed");
                        if (lastAccessed != null && DateTime.TryParse(lastAccessed, out DateTime lastAccessedTime))
                        {
                            var currentTime = DateTime.Now;
                            var sessionTimeout = TimeSpan.FromMinutes(20); // Örnek olarak 20 dakika oturum süresi
                            if (currentTime - lastAccessedTime > sessionTimeout)
                            {
                                session.Clear();
                            }
                        }
                    }
                }

                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken); // Her 5 dakikada bir kontrol etmek için
            }
        }
    }
}
