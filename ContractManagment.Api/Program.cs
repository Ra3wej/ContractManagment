using ContractManagment.Api.Data;
using ContractManagment.Api.Middlewares;
using ContractManagment.Api.Services.CategoryServices;
using ContractManagment.Api.Services.ClientServices;
using ContractManagment.Api.Services.ContractDocumentServices;
using ContractManagment.Api.Services.ContractDocumentTypeServices;
using ContractManagment.Api.Services.ContractServices;
using ContractManagment.Api.Services.IndustryServices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(corsPolicyBuilder =>
    {
        corsPolicyBuilder.WithOrigins(
                "https://smarthealth.group",
                "http://localhost:3000",
                "http://localhost:5173",
                "https://localhost:5173",
                "http://127.0.0.1:5173",
                "https://127.0.0.1:5173",
                "https://dashboard.smarthealth.group/",
                "https://dashboard.smarthealth.group",
                "https://test.smarthealth.group"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

/// Scoped
builder.Services.AddScoped<IContractsServices, ContractsServices>();
builder.Services.AddScoped<IIndustriesServices, IndustriesServices>();
builder.Services.AddScoped<IContractDocumentTypesService, ContractDocumentTypesService>();
builder.Services.AddScoped<IContractDocumentsServices, ContractDocumentsServices>();
builder.Services.AddScoped<IClientsServices, ClientsServices>();
builder.Services.AddScoped<ICategoriesServices, CategoriesServices>();


var app = builder.Build();
app.UseCors();
app.UseRouting();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
    });
}

var logger = app.Services.GetRequiredService<ILogger<Program>>();
app.ConfigureExceptionHandler(logger);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
