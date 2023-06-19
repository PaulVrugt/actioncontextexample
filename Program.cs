var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new AuthorizeFilter());
});
builder.Services.AddAuthentication()
    .AddScheme<Schema1Options, Schema1>("schema1", null);
//.AddScheme<Schema2Options, Schema2>("schema2", null); <-- enable this line to make the actioncontext available in the AuthenticationHandler classes
builder.Services.AddAuthorization(options =>
{
    //options.DefaultPolicy = new AuthorizationPolicyBuilder("schema1", "schema2").RequireAuthenticatedUser().Build();
    options.DefaultPolicy = new AuthorizationPolicyBuilder("schema1").RequireAuthenticatedUser().Build();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthentication();

app.MapControllers();

app.Run();
