using api.Services;
using efscaffold;
using Microsoft.EntityFrameworkCore;

public class TestFixture
{
    public MyDbContext DbContext { get; }
    public GenreService GenreService { get; }
    public BookService BookService { get; }
    public AuthorService AuthorService { get; } 

    public TestFixture()
    {
        // Use an in-memory database for tests
        var options = new DbContextOptionsBuilder<MyDbContext>()
            .UseInMemoryDatabase("TestDb")  // each test run shares this DB
            .Options;

        DbContext = new MyDbContext(options);
        GenreService = new GenreService(DbContext);
        BookService = new BookService(DbContext);
        AuthorService = new AuthorService(DbContext);
    }
}