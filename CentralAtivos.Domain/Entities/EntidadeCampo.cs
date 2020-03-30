using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralAtivos.Domain.Entities
{
    [Table("EntidadeCampo")]
    public class EntidadeCampo : Base
    {
        [StringLength(50, ErrorMessage = "O campo {0} deve conter, no máximo {1} caracteres")]
        [Required(ErrorMessage = "Campo {0} obrigatório")]
        public string NomeCampo { get; set; }

        public Enums.Entidade Entidade { get; set; }

        public Enums.TipoItem Tipo { get; set; }

        public bool Obrigatorio { get; set; }

        public int? Tamanho { get; set; }

        [StringLength(200, ErrorMessage = "O campo {0} deve conter, no máximo {1} caracteres")]
        public string Regex { get; set; }
    }
}
