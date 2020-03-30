using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralAtivos.Domain.Entities
{
    [Table("OrdemServicoMotivoCampoValor")]
    public class OrdemServicoMotivoCampoValor : Base
    {
        public int OrdemServicoID { get; set; }
        public int OrdemServicoMotivoCampoID { get; set; }

        [StringLength(350, ErrorMessage = "O campo {0} deve conter, no máximo {1} caracteres")]
        [Required(ErrorMessage = "Campo {0} obrigatório")]
        public string Valor { get; set; }

        public virtual OrdemServico OrdemServico { get; set; }
        public virtual OrdemServicoMotivoCampo OrdemServicoMotivoCampo { get; set; }
    }
}
