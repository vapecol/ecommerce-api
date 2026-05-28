using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EcommerceApi.Models;

public class Categoria
{
    public int Id { get; set; }

    [Required]
    public string Nombre { get; set; } = string.Empty;

    // Relación 1 a muchos
    [JsonIgnore]
    public List<Producto> Productos { get; set; } = new();
}