using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
namespace RecetasServices.Domain.Entities
{
    public class DetalleReceta
    {
        [Key, Column(Order = 0)]
        public int codigoReceta { get; set; }
        [Key, Column(Order = 1)]
        public int numero { get; set; }
        public string nombreMedicamento { get; set; }
        public string dosis { get; set; }
        public string frecuencia { get; set; }

        // Relación con Receta
        [JsonIgnore]  // Ignorar la propiedad en la serialización JSON
        [ForeignKey("codigoReceta")]
        public virtual Receta receta { get; set; }

    }
}