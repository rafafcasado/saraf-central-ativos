using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralAtivos.Domain.Entities
{
    [Table("OrdemServico")]
    public class OrdemServico : Base
    {
        public OrdemServico()
        {
            Itens = new List<Item>();
        }

        public int EmpresaID { get; set; }
        public int OrdemServicoMotivoID { get; set; }
        public Enums.StatusOrdemServico Status { get; set; }

        [NotMapped]
        public string StatusNome { get { return Status.ToString(); } }

        [NotMapped]
        public Dictionary<string, string> Campos { get; set; }

        [NotMapped]
        public List<Item> Itens { get; set; }

        public virtual Empresa Empresa { get; set; }
        public virtual OrdemServicoMotivo OrdemServicoMotivo { get; set; }

        public ICollection<OrdemServicoAnexo> Anexos { get; set; }
    }
}
