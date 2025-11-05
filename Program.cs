using System.ComponentModel.DataAnnotations;
using InventoryHubAPI.Models;
using InventoryHubAPI.Repositories;
using InventoryHubAPI.Middleware;
using InventoryHubAPI.Dtos;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddSingleton<IProductRepository, InMemoryProductRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<SimpleAuthMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

const string baseRoute = "/api/products";

app.MapGet(baseRoute, (IProductRepository repo) =>
{
    var list = repo.GetAll();
    // Structured JSON response
    var response = new {
        success = true,
        count = list.Count(),
        data = list
    };
    return Results.Ok(response);
});

app.MapGet(baseRoute + "/{id:guid}", (IProductRepository repo, Guid id) =>
{
    var product = repo.Get(id);
    return product is null ? Results.NotFound(new { success = false, message = "Product not found" }) : Results.Ok(new { success = true, data = product });
});

app.MapPost(baseRoute, async (IProductRepository repo, HttpRequest request) =>
{
    var dto = await JsonSerializer.DeserializeAsync<ProductCreateDto>(request.Body, new JsonSerializerOptions{PropertyNameCaseInsensitive=true});
    if (dto is null) return Results.BadRequest(new { success = false, message = "Invalid payload." });
    var validation = dto.Validate();
    if (validation.Any()) return Results.BadRequest(new { success = false, errors = validation });

    var product = new Product
    {
        Id = Guid.NewGuid(),
        Name = dto.Name,
        Quantity = dto.Quantity,
        Price = dto.Price,
        CreatedAt = DateTime.UtcNow
    };
    repo.Create(product);
    return Results.Created($"/api/products/{product.Id}", new { success = true, data = product });
});

app.MapPut(baseRoute + "/{id:guid}", async (IProductRepository repo, Guid id, HttpRequest request) =>
{
    var dto = await JsonSerializer.DeserializeAsync<ProductUpdateDto>(request.Body, new JsonSerializerOptions{PropertyNameCaseInsensitive=true});
    if (dto is null) return Results.BadRequest(new { success = false, message = "Invalid payload." });
    var validation = dto.Validate();
    if (validation.Any()) return Results.BadRequest(new { success = false, errors = validation });

    var existing = repo.Get(id);
    if (existing is null) return Results.NotFound(new { success = false, message = "Product not found" });

    existing.Name = dto.Name;
    existing.Quantity = dto.Quantity;
    existing.Price = dto.Price;
    repo.Update(existing);
    return Results.Ok(new { success = true, data = existing });
});

app.MapDelete(baseRoute + "/{id:guid}", (IProductRepository repo, Guid id) =>
{
    var deleted = repo.Delete(id);
    if (!deleted) return Results.NotFound(new { success = false, message = "Product not found" });
    return Results.Ok(new { success = true });
});

app.Run();
