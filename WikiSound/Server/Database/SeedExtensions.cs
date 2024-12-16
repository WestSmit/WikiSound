namespace WikiSound.Server.Database
{
    public static class SeedExtensions
    {
        public static void UseSeedData(this WebApplication app)
        {
            var seed = app.Services.CreateScope().ServiceProvider.GetRequiredService<Seed>();

            seed.SeedData().Wait();
        }
    }
}
