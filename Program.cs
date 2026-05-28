using EcommerceApi.Data;
using EcommerceApi.Models;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=ecommerce.db"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureHttpJsonOptions(options => {
    options.SerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    options.SerializerOptions.WriteIndented = true;
});

// Si usas Swagger también agrega esto:
builder.Services.AddControllers().AddJsonOptions(options => {
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();




// Obtener todas las categorías
app.MapGet("/categorias", async (AppDbContext db) =>
{
    return await db.Categorias
        .Include(c => c.Productos)
        .ToListAsync();
});

// Obtener categoría por ID
app.MapGet("/categorias/{id}", async (int id, AppDbContext db) =>
{
    var categoria = await db.Categorias
        .Include(c => c.Productos)
        .FirstOrDefaultAsync(c => c.Id == id);

    return categoria is not null
        ? Results.Ok(categoria)
        : Results.NotFound();
});

// Crear categoría
app.MapPost("/categorias", async (Categoria categoria, AppDbContext db) =>
{
    db.Categorias.Add(categoria);
    await db.SaveChangesAsync();

    return Results.Created($"/categorias/{categoria.Id}", categoria);
});

// Actualizar categoría
app.MapPut("/categorias/{id}", async (int id, Categoria input, AppDbContext db) =>
{
    var categoria = await db.Categorias.FindAsync(id);

    if (categoria is null)
        return Results.NotFound();

    categoria.Nombre = input.Nombre;

    await db.SaveChangesAsync();

    return Results.Ok(categoria);
});

// Eliminar categoría
app.MapDelete("/categorias/{id}", async (int id, AppDbContext db) =>
{
    var categoria = await db.Categorias.FindAsync(id);

    if (categoria is null)
        return Results.NotFound();
        
    categoria.Productos = new();    
    db.Categorias.Remove(categoria);
    await db.SaveChangesAsync();

    return Results.Ok();
});


// =========================
// CRUD PRODUCTOS
// =========================

// Obtener productos
app.MapGet("/productos", async (AppDbContext db) =>
{
    return await db.Productos
        .Include(p => p.Categoria)
        .ToListAsync();
});

// Obtener producto por ID
app.MapGet("/productos/{id}", async (int id, AppDbContext db) =>
{
    var producto = await db.Productos
        .Include(p => p.Categoria)
        .FirstOrDefaultAsync(p => p.Id == id);

    return producto is not null
        ? Results.Ok(producto)
        : Results.NotFound();
});

// Crear producto
app.MapPost("/productos", async (Producto producto, AppDbContext db) =>
{
    var categoriaExiste = await db.Categorias
        .AnyAsync(c => c.Id == producto.CategoriaId);

    if (!categoriaExiste)
        return Results.BadRequest("La categoría no existe");
    
    db.Productos.Add(producto);
    await db.SaveChangesAsync();

    return Results.Created($"/productos/{producto.Id}", producto);
});

// Actualizar producto
app.MapPut("/productos/{id}", async (int id, Producto input, AppDbContext db) =>
{
    var producto = await db.Productos.FindAsync(id);

    if (producto is null)
        return Results.NotFound();

    producto.Nombre = input.Nombre;
    producto.Precio = input.Precio;
    producto.CategoriaId = input.CategoriaId;

    await db.SaveChangesAsync();

    return Results.Ok(producto);
});

// Eliminar producto
app.MapDelete("/productos/{id}", async (int id, AppDbContext db) =>
{
    var producto = await db.Productos.FindAsync(id);

    if (producto is null)
        return Results.NotFound();

    db.Productos.Remove(producto);
    await db.SaveChangesAsync();

    return Results.Ok();
});

app.Run();