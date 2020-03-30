using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralAtivos.Domain.Entities
{
    [Table("EspeciePropriedade")]
    public class EspeciePropriedade : Base
    {
        public int EspecieID { get; set; }
        public int PropriedadeID { get; set; }

        [JsonIgnore]
        public virtual Especie Especie { get; set; }

        public virtual Propriedade Propriedade { get; set; }
    }
}
