using AutoMapper;
using LibraryManagerAPI.Application.UseCases.Book;
using LibraryManagerAPI.Infrastructure.Context;
using LibraryManagerAPI.Infrastructure.Persistance.Config;
using LibraryManagerAPI.Infrastructure.Persistance.Repositories;
using LibraryManagerAPI.Presentation.Filters;
using LibraryManagerAPI.Presentation.Interfaces.Repository.Book;
using LibraryManagerAPI.Presentation.Interfaces.Repository.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var connection = builder.Configuration["MySqlConnection:MySqlConnectionString"];

// Add services to the container.
builder.Services.AddDbContext<MySQLContext>(options => options.UseMySql(
    connection,
    new MySqlServerVersion(new Version(8, 0, 29)),
    mySqlOptions => mySqlOptions.EnableRetryOnFailure(
        maxRetryCount: 5,
        maxRetryDelay: TimeSpan.FromSeconds(5),
        errorNumbersToAdd: null
        )
    )
    .EnableSensitiveDataLogging()
    .LogTo(Console.WriteLine)
);

IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Registering the repository
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();


// Registering all use cases dynamically using reflection 
builder.Services.Scan(scan => scan
    .FromAssemblyOf<RegisterBooksUseCase>()
    .AddClasses(classes => classes.InNamespaces("LibraryManagerAPI.Application.UseCases.Book"))
    .AddClasses(classes => classes.InNamespaces("LibraryManagerAPI.Application.UseCases.User"))
    .AsImplementedInterfaces()
    .WithScopedLifetime()
);

builder.Services.AddControllers(options =>
{
    // Adding our custom validation filter
    options.Filters.Add<ValidationFilter>();
    
}).ConfigureApiBehaviorOptions(opt =>
{
    // Disabling the default ModelState validation filter
    // Because we are using our custom validation filter
    opt.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "LibraryManagerAPI",
        Version = "v1",
        Description = "API for managing a library",
        Contact = new OpenApiContact
        {
            Name = "Rafael Oliveira",
            Email = "rafaelaparecido.oliveirasilva@gmail.com"
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(
        c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LibraryManagerAPI v1")
    );
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
