using System.ComponentModel.DataAnnotations.Schema;

namespace CentralAtivos.Domain.Entities
{
    [Table("InventarioConfig")]
    public class InventarioConfig : Base
    {
        public int InventarioID { get; set; }
        public int EntidadeCampoID { get; set; }
        public bool Visivel { get; set; }
        public bool Obrigatorio { get; set; }

        public virtual Inventario Inventario { get; set; }
        public virtual EntidadeCampo EntidadeCampo { get; set; }
    }
}
