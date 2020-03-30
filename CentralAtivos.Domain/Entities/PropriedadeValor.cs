using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralAtivos.Domain.Entities
{
    [Table("PropriedadeValor")]
    public class PropriedadeValor : Base
    {
        public int PropriedadeID { get; set; }

        [StringLength(100, ErrorMessage = "O campo {0} deve conter, no máximo {1} caracteres")]
        public string Valor { get; set; }


        [JsonIgnore]
        public virtual Propriedade Propriedade { get; set; }
    }
}
