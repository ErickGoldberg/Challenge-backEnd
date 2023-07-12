using System.ComponentModel.DataAnnotations;

namespace Challenge_backEnd.Models
{
    public class Receita
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "O tamanho máximo é 100")]
        public string Descricao { get; set; }
        [Required]
        public double Valor { get; set; }
        [Required]
        public DateTime Data { get; set; }
    }
} 
