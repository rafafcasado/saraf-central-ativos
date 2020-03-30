using System.ComponentModel.DataAnnotations.Schema;

namespace CentralAtivos.Domain.Entities
{
    [Table("OrdemServicoItem")]
    public class OrdemServicoItem : Base
    {
        public int OrdemServicoID { get; set; }

        public int ItemID { get; set; }

        public virtual OrdemServico OrdemServico { get; set; }
        public virtual Item Item { get; set; }
    }
}
