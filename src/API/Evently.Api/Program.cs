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

Assembly[] assemblies = [
    Evently.Modules.Events.Application.AssemblyReference.Assembly,
    AssemblyReference.Assembly,
    Evently.Modules.Events.Presentation.AssemblyReference.Assembly
];

builder.Services.AddApplication(assemblies);

builder.Services.AddInfrastructure(
    builder.Configuration.GetConnectionString("Database")!,
    assemblies);

builder.Configuration.AddModuleConfiguration(["events"]);

builder.Services.AddEventModule(builder.Configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.MapScalarApiReference();

    app.ApplyMigrations();
}

app.UseHttpsRedirection();

EventsModule.MapEndpoints(app);

app.UseSerilogRequestLogging();
app.UseExceptionHandler();

await app.RunAsync();
