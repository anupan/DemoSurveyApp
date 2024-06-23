using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SurveyAPI.Data;
using SurveyAPI.Repositories;
using SurveyAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SurveyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SurveyDBConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
// Register Repository
builder.Services.AddScoped<ISurveyRepository, SurveyRepository>();
// Register Service
builder.Services.AddScoped<ISurveyService, SurveyService>();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SurveyAPI", Version = "v1" });
    c.AddSecurityDefinition("Basic", new OpenApiSecurityScheme
    {
        Description = "Basic Authentication header using the Authorization header.",
        Type = SecuritySchemeType.Http,
        Scheme = "basic"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SurveyAPI v1"));
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();