using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralAtivos.Domain.Entities
{
    [Table("OrdemServicoMotivo")]
    public class OrdemServicoMotivo : Base
    {
        [StringLength(150, ErrorMessage = "O campo {0} deve conter, no máximo {1} caracteres")]
        [Required(ErrorMessage = "Campo {0} obrigatório")]
        public string Nome { get; set; }

        public Enums.AcaoFinalOrdemServico AcaoFinal { get; set; }

        [NotMapped]
        public string CamposVinculados { get; set; }
    }
}
