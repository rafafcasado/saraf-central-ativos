using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralAtivos.Domain.Entities
{
    [Table("ItemPropriedadeValor")]
    public class ItemPropriedadeValor : Base
    {
        public int ItemID { get; set; }
        public int PropriedadeID { get; set; }

        [StringLength(100, ErrorMessage = "O campo {0} deve conter, no máximo {1} caracteres")]
        public string Valor { get; set; }
        public int? PropriedadeValorID { get; set; }

        [JsonIgnore]
        public virtual Item Item { get; set; }

        [JsonIgnore]
        public virtual Propriedade Propriedade { get; set; }

        [JsonIgnore]
        public virtual PropriedadeValor PropriedadeValor { get; set; }
    }
}
