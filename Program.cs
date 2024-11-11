using NSwag.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Azure.ServiceBus;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddScoped<IEmailService, EmailService>();

var serviceBusConnectionString = builder.Configuration.GetValue<string>("AzureServiceBus:ConnectionString");
var queueClient = new QueueClient(serviceBusConnectionString, "emailqueue");
builder.Services.AddSingleton(queueClient);

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

todoItems.MapGet("/{id}", GetEmail);
todoItems.MapPost("/send", SendEmail);

app.Run();

static async Task<IResult> GetEmail(Guid id, IEmailService service)
{
    return await service.GetEmail(id)
        is Email email
            ? TypedResults.Ok(new EmailItemDTO(email))
            : TypedResults.NotFound();
}

static async Task<IResult> SendEmail(SendEmailRequest sendEmailRequest, IEmailService service)
{
    var emailItem = await service.SendEmail(sendEmailRequest);

    var response = new EmailItemDTO(emailItem);

    return TypedResults.Created($"/emails/{response.Id}", response);
}
