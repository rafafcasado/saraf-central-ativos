using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralAtivos.Domain.Entities
{
    [Table("InventarioLocal")]
    public class InventarioLocal : Base
    {
        public int InventarioID { get; set; }
        public int LocalID { get; set; }

        [JsonIgnore]
        public virtual Inventario Inventario { get; set; }

        public virtual Local Local { get; set; }
    }
}
