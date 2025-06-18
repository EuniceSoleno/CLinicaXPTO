using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CLinicaXPTO.Model.Enumerados;

namespace CLinicaXPTO.Model
{
    public class ActoClinico
    {
        [Key]
        public int Id { get; set;  }
        [Required]
        public int PedidoMarcacaoId {  get; set; }
        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]

        public TipoConsulta _TipoConsulta { get; set; }
        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]

        public Subsistema _Subsistema { get; set; }
        [Required]
        public int idProfissional {  get; set; }  
       

    }
}
