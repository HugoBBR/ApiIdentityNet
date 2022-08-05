using ApiChida;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ApiChida.Models;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options => 
options.JsonSerializerOptions.PropertyNamingPolicy = null
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Add identity config
builder.Services.AddIdentityConf(builder.Configuration);

//Add identity password options
builder.Services.AddIdentityOptions(builder.Configuration);

//Mapper
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseRouting();


app.UseAuthorization();

app.MapControllers();


app.Run();

