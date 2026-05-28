# EcommerceApi

API REST para un sistema de ecommerce construida con ASP.NET Core y SQLite.

## Tecnologías

- .NET 8
- ASP.NET Core Minimal APIs
- Entity Framework Core
- SQLite
- Swagger / OpenAPI

## Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)

## Instalación y ejecución

```bash
# Clonar el repositorio
git clone https://github.com/vapecol/ecommerce-api.git
cd ecommerce-api

# Restaurar dependencias
dotnet restore

# Aplicar migraciones y crear la base de datos
dotnet ef database update

# Correr el proyecto
dotnet run
```

La API estará disponible en `http://localhost:5293`  
La documentación Swagger en `http://localhost:5293/swagger`

## Endpoints

### Categorías

| Método | Ruta | Descripción |
|--------|------|-------------|
| GET | `/categorias` | Obtener todas las categorías |
| GET | `/categorias/{id}` | Obtener categoría por ID |
| POST | `/categorias` | Crear una categoría |
| PUT | `/categorias/{id}` | Actualizar una categoría |
| DELETE | `/categorias/{id}` | Eliminar una categoría |

### Productos

| Método | Ruta | Descripción |
|--------|------|-------------|
| GET | `/productos` | Obtener todos los productos |
| GET | `/productos/{id}` | Obtener producto por ID |
| POST | `/productos` | Crear un producto |
| PUT | `/productos/{id}` | Actualizar un producto |
| DELETE | `/productos/{id}` | Eliminar un producto |

## Ejemplos de uso

### Crear una categoría

```json
POST /categorias
{
  "nombre": "Electrónica"
}
```

### Crear un producto

```json
POST /productos
{
  "nombre": "Laptop",
  "precio": 1500.00,
  "categoriaId": 1
}
```

## Estructura del proyecto

```
EcommerceApi/
├── Models/
│   ├── Categoria.cs
│   └── Producto.cs
├── Data/
│   └── AppDbContext.cs
├── Program.cs
├── ecommerce.db
└── EcommerceApi.csproj
```
