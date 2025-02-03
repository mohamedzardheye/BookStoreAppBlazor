using BookStoreApp.Api.Configurations;
using BookStoreApp.Api.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connString = builder.Configuration.GetConnectionString("BookStoreAppDbConnection");
builder.Services.AddDbContext<BookStoreDbContext>(options => options.UseSqlServer(connString));


builder.Services.AddAutoMapper(typeof(MapperConfig));



builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));


builder.Services.AddSingleton<IMongoDatabase>(sp => {

    var databaseSettings = sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;
    var mongoDbClient = new MongoClient(databaseSettings.ConnectionURI);
    var mongoDb = mongoDbClient.GetDatabase(databaseSettings.DatabaseName);

    return mongoDb;
});



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddIdentityCore<ApiUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<BookStoreDbContext>();




builder.Host.UseSerilog((ctx, lc) =>
    lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));




builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", 
        b => b.AllowAnyMethod()
        .AllowAnyHeader()
        .AllowAnyOrigin());
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {

        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
