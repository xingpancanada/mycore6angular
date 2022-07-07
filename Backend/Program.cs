using Backend.Data;
using Backend.Entities;
using Backend.Errors;
using Backend.Extensions;
using Backend.Helpers;
using Backend.Interfaces;
using Backend.Middleware;
using Backend.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
ConfigurationManager config = builder.Configuration;
//IWebHostEnvironment environment = builder.Environment;
//IConfigurationRoot config = new ConfigurationBuilder()
            // .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            // .AddJsonFile("appsettings.json")
            // .Build();


// Add services to the container.

/////44. Adding AutoMapper to the API project
builder.Services.AddAutoMapper(typeof(MappingProfiles));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

////13.add db context for myStore.db
builder.Services.AddDbContext<StoreDBContext>(options => options.UseSqlite($"Data Source=myStore.db"));
//builder.Services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlite($"Data Source=identity.db"));
builder.Services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("IdentityConnection")));

builder.Services.AddControllers().AddNewtonsoftJson();

////57
builder.Services.AddSwaggerServicesFromExtension();
builder.Services.AddApplicationServicesFromExtension();
////168
builder.Services.AddIdentityServiceFromExtension(config);

//////68. Adding CORS Support to the API
builder.Services.AddCors(opt => {
    opt.AddPolicy("CorsPolicy", policy => {
        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
    });
});


var app = builder.Build();

////53. use ExceptionMiddleware to replace app.UseDeveloperExceptionPage();  
////must be at the top of app.
app.UseMiddleware<ExceptionMiddleware>(); 


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    ////57
    app.UseSwaggerDocumentation();
}

////52. Adding a not found endpoint error handler
//app.UseStatusCodePagesWithReExecute("errors/{0}");  //do we need this???



app.UseHttpsRedirection();

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
    
    ////169. Adding identity to program class
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    var identityContext = services.GetRequiredService<AppIdentityDbContext>();
    await identityContext.Database.MigrateAsync();
    await AppIdentityDbContextSeed.SeedUsersAsync(userManager);
}
catch (Exception ex)
{
    //var logger = loggerFactory.CreateLogger<Program>();
    logger.LogError(ex, "An error occurred during migration");
    //throw(ex);
}

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

app.UseAuthentication();  ////173.
app.UseAuthorization();

app.MapControllers();

app.Run();
