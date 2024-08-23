using Abdt.ContractBroker.Configuration;
using Abdt.ContractBroker.Domain;
using Abdt.ContractBroker.Domain.Options;
using Abdt.ContractBroker.Repository;
using Abdt.ContractBroker.Repository.Abstractions;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions<MongoDbSettings>()
    .BindConfiguration("MongoDbSettings")
    .ValidateDataAnnotations();

builder.Services.AddSingleton<IMongoClient>(s =>
    new MongoClient(builder.Configuration.GetValue<string>("MongoDbSettings:ConnectionString")));

builder.Services.AddAutoMapper(typeof(ControllersMappingProfile));

builder.Services.AddScoped<IRepository<Contract>, ContractRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
