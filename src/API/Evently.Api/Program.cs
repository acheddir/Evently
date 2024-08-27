WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

Assembly[] assemblies =
[
    Evently.Modules.Events.Application.AssemblyReference.Assembly,
    Evently.Modules.Events.Infrastructure.AssemblyReference.Assembly,
    Evently.Modules.Events.Presentation.AssemblyReference.Assembly,
    Evently.Modules.Users.Application.AssemblyReference.Assembly,
    Evently.Modules.Users.Infrastructure.AssemblyReference.Assembly,
    Evently.Modules.Users.Presentation.AssemblyReference.Assembly
];

string dbConnectionString = builder.Configuration.GetConnectionString("Database")!;
string redisConnectionString = builder.Configuration.GetConnectionString("Cache")!;

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

builder.Services.AddCommonApplication(assemblies);
builder.Services.AddCommonInfrastructure(dbConnectionString, redisConnectionString, assemblies);

builder.Configuration.AddModuleConfiguration(["events", "users"]);

builder.Services.AddHealthChecks()
    .AddNpgSql(dbConnectionString)
    .AddRedis(redisConnectionString);

builder.Services.AddEventModule(builder.Configuration);
builder.Services.AddUsersModule(builder.Configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.MapScalarApiReference();

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
