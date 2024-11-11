using System.Text.Json;
using Azure.Messaging.ServiceBus;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var serviceBusConnectionString = builder.Configuration.GetValue<string>("AzureServiceBus:ConnectionString");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "EmailAPI";
    config.Title = "EmailAPI v1";
    config.Version = "v1";
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "EmailAPI";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}

RouteGroupBuilder todoItems = app.MapGroup("/emails");

todoItems.MapPost("/send", QueueEmail);

app.Run();


static async Task<IResult> QueueEmail (SendEmailRequest sendEmailRequest)
{
    string queueName = "emails";

    var payload = JsonSerializer.Serialize(sendEmailRequest);

    await using ServiceBusClient client = new("***");

    var sender = client.CreateSender(queueName);

    var messageId = Guid.NewGuid().ToString();

    var message = new ServiceBusMessage(payload) { ContentType = "application/json", MessageId = messageId};

    await sender.SendMessageAsync(message);

    return TypedResults.Created($"/emails/{messageId}"); 
}