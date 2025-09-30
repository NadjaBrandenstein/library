using api;
using api.Dtos;
using api.Dtos.Requests;
using api.Services;
using efscaffold;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var appOptions = builder.Services.AddAppOptions(builder.Configuration);

builder.Services.AddScoped<ILibraryService<BookDto, CreateBookDto, UpdateBookDto>, BookService>();
builder.Services.AddScoped<ILibraryService<AuthorDto, CreateAuthorDto, UpdateAuthorDto>, AuthorService>();
builder.Services.AddScoped<ILibraryService<GenreDto, CreateGenreDto, UpdateGenreDto>, GenreService>();

builder.Services.AddDbContext<MyDbContext>(conf =>
{
    conf.UseNpgsql(appOptions.DbConnectionString);
});

builder.Services.AddControllers();
builder.Services.AddOpenApiDocument();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddCors();

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
