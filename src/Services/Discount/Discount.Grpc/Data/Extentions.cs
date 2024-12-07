using Microsoft.EntityFrameworkCore;
namespace Discount.Grpc.Data;

public static class Extentions
{
    public static IApplicationBuilder UseMigrations(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<DiscountContext>();
        dbContext.Database.MigrateAsync();
        
        return app;
    }
}