using System.ComponentModel.DataAnnotations.Schema;

namespace CentralAtivos.Domain.Entities
{
    [Table("TelaAcaoRequisicao")]
    public class TelaAcaoRequisicao : Base
    {
        public int TelaAcaoID { get; set; }
        public int RequisicaoID { get; set; }

        public virtual TelaAcao TelaAcao { get; set; }
        public virtual Requisicao Requisicao { get; set; }
    }
}
