using Microsoft.EntityFrameworkCore;
using SurveyAPI.Data;

public class TestSurveyDbContext
{
    public SurveyDbContext Context { get; private set; }

    public TestSurveyDbContext()
    {
        var options = new DbContextOptionsBuilder<SurveyDbContext>()
            .UseInMemoryDatabase(databaseName: "TestSurveyAppDB")
            .Options;

        Context = new SurveyDbContext(options);
    }

    public SurveyDbContext CreateContext()
    {
        return new SurveyDbContext(
            new DbContextOptionsBuilder<SurveyDbContext>()
            .UseInMemoryDatabase(databaseName: "TestSurveyAppDB")
            .Options);
    }
}