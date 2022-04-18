using Cognizant.Training.OrderProcessing.API.Config;
using Cognizant.Training.OrderProcessing.API.Dto.Validators;
using Cognizant.Training.OrderProcessing.API.Infrastructure;
using Cognizant.Training.OrderProcessing.API.Mapper;
using Cognizant.Training.OrderProcessing.API.Repository;
using Cognizant.Training.OrderProcessing.API.Repository.Abstractions;
using Cognizant.Training.OrderProcessing.API.UseCases;
using Cognizant.Training.OrderProcessing.API.UseCases.Abstractions;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddFluentValidation();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Configuration
builder.Services.Configure<DataStoreConfig>(builder.Configuration.GetSection("DataStore"));

// Add Fluent Validation
builder.Services.AddValidatorsFromAssembly(typeof(CreateOrderDtoValidator).Assembly);

// Add AutoMapper
builder.Services.AddAutoMapper(opt => opt.AddProfile<OrderProcessingProfile>());

// Add DbContext
builder.Services.AddDbContextPool<OrdersDbContext>((provider, options) =>
{
    var datastore = provider.GetRequiredService<IOptions<DataStoreConfig>>();
    options.UseCosmos(
        datastore.Value.Endpoint,
        datastore.Value.AccountKey,
        datastore.Value.Database);
});

// Add Repositories
builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();

// Add Business Layer
builder.Services.AddScoped<ICreateOrder, CreateOrder>();
builder.Services.AddScoped<IGetOrderById, GetOrderById>();

// Add Message Sender
builder.Services.AddScoped<IMessageSender, MessageSender>();

var app = builder.Build();

// Use Swagger 
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();