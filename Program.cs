using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SchoolAPI.Models;
using SchoolAPI.Models.Data;
using SchoolAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<SchoolDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("Starter")));

builder.Services.AddScoped<IAuthenticateRepository, AuthenticateRepository>();
builder.Services.AddScoped<IAttendenceRepository, AttendenceRepository>();
builder.Services.AddScoped<IUtilityHelper, UtilityHelper>();

builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IDistrictsRepository, DistrictsRepository>();
builder.Services.AddScoped<IFeeRepository, FeeRepository>();
builder.Services.AddScoped<IGendersRepository, GendersRepository>();
builder.Services.AddScoped<IMandalsRepository, MandalsRepository>();
builder.Services.AddScoped<IPrivilegesRepository, PrivilegesRepository>();
builder.Services.AddScoped<IStaffRepository, StaffRepository>();
builder.Services.AddScoped<IStatesRepository, StatesRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudents_StatusRepository, Students_StatusRepository>();
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
builder.Services.AddScoped<ITeachers_StatusRepository, Teachers_StatusRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IUsers_TypesRepository, Users_TypesRepository>();
builder.Services.AddScoped<IUserType_PrivilegesRepository, UserType_PrivilegesRepository>();
builder.Services.AddScoped<IVillagesRepository, VillagesRepository>();


//builder.Services.AddScoped<IDistrictsRepository, DistrictsRepository>();
//builder.Services.AddScoped<IGenderRepository, GenderRepository>();
////builder.Services.AddScoped<ILoginsRepository, LoginsRepository>();
//builder.Services.AddScoped<IMandalRepository, MandalRepository>();
//builder.Services.AddScoped<IPrivilegesRepository, PrivilegesRepository>();
//builder.Services.AddScoped<IRole_PrivilegesRepository, Role_PrivilegesRepository>();
//builder.Services.AddScoped<IRolesRepository, RolesRepository>();
//builder.Services.AddScoped<IStatesRepository, StatesRepository>();
//builder.Services.AddScoped<IVillagesRepository, VillagesRepository>();
//builder.Services.AddScoped<IUsersRepository, UsersRepository>();
//builder.Services.AddScoped<IUsers_TypesRepository, Users_TypesRepository>();


builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddCors(options =>
{
    options.AddPolicy("allow-cors", policy =>
    {
        policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
    });
});

string corsDomains = "http://localhost:4200";
string[] domains = corsDomains.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
builder.Services.AddCors(o => o.AddPolicy("AppCORSPolicy", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader()
           .AllowCredentials()
           .WithOrigins(domains);
}));



// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

var app = builder.Build();
app.UseMiddleware<AuthenticationMiddleware>();
app.UseMiddleware<AuthorizationMiddleware>();

app.MapHealthChecks("/healthChecks");

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapControllers();

app.UseCors("allow-cors");
app.UseCors("AppCORSPolicy");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
