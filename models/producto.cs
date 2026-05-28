using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EcommerceApi.Models;

public class Producto
{
    public int Id { get; set; }

    [Required]
    public string Nombre { get; set; } = string.Empty;

    public decimal Precio { get; set; }

    // FK
    public int CategoriaId { get; set; }

    [ForeignKey("CategoriaId")]
    [JsonIgnore] 
        public Categoria? Categoria { get; set; }
}