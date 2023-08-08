using HiddenVilla_Api.Extensions;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add our own services
builder.Services.AddMyAppServices(builder.Configuration);
builder.Services.AddMyIdentityServices(builder.Configuration);
builder.Services.AddSwaggerDocumentation();

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(opt => opt.JsonSerializerOptions.PropertyNamingPolicy = null)
    .AddNewtonsoftJson(opt => {
        opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
        opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDocumentation();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicyAllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
