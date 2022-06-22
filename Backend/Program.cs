using Backend.Data;
using Backend.Helpers;
using Backend.Interfaces;
using Backend.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

/////44. Adding AutoMapper to the API project
builder.Services.AddAutoMapper(typeof(MappingProfiles));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

////13.add db context for myStore.db
builder.Services.AddDbContext<StoreDBContext>(options => options.UseSqlite($"Data Source=myStore.db"));

////23.adding a repository and interface
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

////28. Applying the migrations and creating the Database at app startup
using var scope = app.Services.CreateAsyncScope();
var services = scope.ServiceProvider;
ILogger logger = app.Logger;
//var loggerFactory = services.GetRequiredService<ILoggerFactory>;
try 
{
    //28. Applying the migrations and creating the Database at app startup
    var storeDbContext = services.GetRequiredService<StoreDBContext>();
    await storeDbContext.Database.MigrateAsync();
    //29. Adding Seed data
    await StoreDBContextSeed.SeedAsync(storeDbContext);
    
    //////169. Adding identity to program class
    // var userManager = services.GetRequiredService<UserManager<AppUser>>();
    // var identityContext = services.GetRequiredService<AppIdentityDbContext>();
    // await identityContext.Database.MigrateAsync();
    // await AppIdentityDbContextSeed.SeedUsersAsync(userManager);
}
catch (Exception ex)
{
    //var logger = loggerFactory.CreateLogger<Program>();
    logger.LogError(ex, "An error occurred during migration");
    //throw(ex);
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

////68. Adding CORS Support to the API
app.UseCors("CorsPolicy");

////47. Serving static content from the API
app.UseStaticFiles();
// app.UseStaticFiles(new StaticFileOptions
// {
//     FileProvider = new PhysicalFileProvider(
//         Path.Combine(Directory.GetCurrentDirectory(), "Content")
//     ), RequestPath = "/content"
// });

app.UseAuthorization();

app.MapControllers();

app.Run();
