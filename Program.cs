using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NSTask.Contracts;
using NSTask.Models;
using NSTask.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<NSTaskDataBase>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("NSTaskDataBase")));
builder.Services.AddScoped<DataBaseInitializer>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();

//"Server=localhost;Initial Catalog=NSTaskDataBase;Integrated Security=True;Trusted_Connection=True;");
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,// etebar sanji token samte server
            ValidateAudience = false,// etebar sanji token samte client
            ValidateLifetime = true,// tarikh engheza token
            ValidateIssuerSigningKey = true,// etebar sanji token
            ValidIssuer = "http://localhost:5000",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("OurVerifyTopLearn"))//kilidi ke ramznegari mishe token bar asase oon
        };
    });
ConfigureSwaggerGen(builder.Services);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.Services.DataBaseInitializer();
app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();




static void ConfigureSwaggerGen(IServiceCollection services)
{
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger", Version = "v1" });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
         {
             {
                 new OpenApiSecurityScheme {Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme,Id = "Bearer"}},
                 new string[] {}
             }
         });
    });
}