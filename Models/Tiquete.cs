using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
namespace NgNetCore.Models
{
    public class Tiquete
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        [Required]
        public string  RutaId { get; set; }
        public virtual Ruta Ruta { get; set; }
        public string ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }
        [Required]
        public int Cantidad { get; set; }
        [Required]
        public decimal Total { 
            get
            {
                return Ruta.Costo * Cantidad;
            }
            
         }
         
    }
}