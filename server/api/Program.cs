using api;
using api.Dtos;
using api.Dtos.Requests;
using api.Services;
using efscaffold;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        var appOptions = services.AddAppOptions(configuration);

        services.AddScoped<ILibraryService<BookDto, CreateBookDto, UpdateBookDto>, BookService>();
        services.AddScoped<ILibraryService<AuthorDto, CreateAuthorDto, UpdateAuthorDto>, AuthorService>();
        services.AddScoped<ILibraryService<GenreDto, CreateGenreDto, UpdateGenreDto>, GenreService>();
        services.AddScoped<BookDetailsService>();

        services.AddDbContext<MyDbContext>(conf =>
        {
            conf.UseNpgsql(appOptions.DbConnectionString);
        });

        services.AddControllers();
        services.AddOpenApiDocument();
        services.AddProblemDetails();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddCors();
    }

    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        ConfigureServices(builder.Services, builder.Configuration);

        var app = builder.Build();

        app.UseExceptionHandler();

        app.UseCors(config => config
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin()
            .SetIsOriginAllowed(x => true));

        app.MapControllers();
        app.UseOpenApi();
        app.UseSwaggerUi();

        await app.GenerateApiClientsFromOpenApi("/../../client/src/generated-ts-client.ts");

        app.Run();
    }
}