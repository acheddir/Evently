Assembly[] applicationAssemblies =
[
    Evently.Modules.Events.Application.AssemblyReference.Assembly,
    Evently.Modules.Users.Application.AssemblyReference.Assembly,
    Evently.Modules.Ticketing.Application.AssemblyReference.Assembly,
];

Assembly[] presentationAssemblies =
[
    Evently.Modules.Events.Presentation.AssemblyReference.Assembly,
    Evently.Modules.Users.Presentation.AssemblyReference.Assembly,
    Evently.Modules.Ticketing.Presentation.AssemblyReference.Assembly,
];

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfiguration) =>
{
    loggerConfiguration.ReadFrom.Configuration(context.Configuration);
});

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(t => t.FullName!.Replace("+", "."));
});

string dbConnectionString = builder.Configuration.GetConnectionString("Database")!;
string redisConnectionString = builder.Configuration.GetConnectionString("Cache")!;

builder.Services.AddApplication(applicationAssemblies);
builder.Services.AddInfrastructure(
    [TicketingModule.ConfigureConsumers],
    dbConnectionString,
    redisConnectionString,
    presentationAssemblies);

builder.Configuration.AddModuleConfiguration(["events", "users", "ticketing"]);

builder.Services.AddHealthChecks()
    .AddNpgSql(dbConnectionString)
    .AddRedis(redisConnectionString);

builder.Services.AddEventModule(builder.Configuration);
builder.Services.AddUsersModule(builder.Configuration);
builder.Services.AddTicketingModule(builder.Configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.RouteTemplate = "openapi/{documentName}.json";
    });
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("Evently API")
            .WithTheme(ScalarTheme.Mars)
            .WithDarkMode(true)
            .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });

    app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.MapEndpoints();

app.MapHealthChecks("health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

await app.RunAsync();
