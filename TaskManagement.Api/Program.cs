using TaskManagement.Api.Extensions;
using TaskManagement.Api.OptionsSetup;
using TaskManagement.Application;
using TaskManagement.Application.Abstractions.Interfaces;
using TaskManagement.Infrastructure;
using TaskManagement.Infrastructure.Authentication;
using TaskManagement.Infrastructure.Data;
using TaskManagement.Infrastructure.Data.Seeding;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers(options => { options.Filters.Add<GlobalExceptionFilter>(); });
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var jwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtOptions>();
builder.Services.AddApiAuthentication(jwtOptions);
builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.AddTransient<IJwtProvider, JwtProvider>();
builder.Services.AddAuthorization();
builder.Services.AddScoped<IUserAccessor, UserAccessor>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Task Management v1"));
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<TaskManagementDbContext>();
    SeedData.Seed(dbContext);
}

app.Run();