using System.ComponentModel.DataAnnotations.Schema;

namespace CentralAtivos.Domain.Entities
{
    [Table("OrdemServicoMotivoCampo")]
    public class OrdemServicoMotivoCampo : Base
    {
        public int OrdemServicoMotivoID { get; set; }
        public int OrdemServicoCampoID { get; set; }

        public virtual OrdemServicoMotivo OrdemServicoMotivo { get; set; }
        public virtual OrdemServicoCampo OrdemServicoCampo { get; set; }
    }
}
