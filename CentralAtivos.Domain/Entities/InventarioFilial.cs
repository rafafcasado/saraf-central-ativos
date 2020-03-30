using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralAtivos.Domain.Entities
{
    [Table("InventarioFilial")]
    public class InventarioFilial : Base
    {
        public int InventarioID { get; set; }
        public int FilialID { get; set; }

        [JsonIgnore]
        public virtual Inventario Inventario { get; set; }

        public virtual Filial Filial { get; set; }
    }
}
