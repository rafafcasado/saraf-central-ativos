using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralAtivos.Domain.Entities
{
    [Table("OrdemServicoCampo")]
    public class OrdemServicoCampo : Base
    {
        [StringLength(150, ErrorMessage = "O campo {0} deve conter, no máximo {1} caracteres")]
        [Required(ErrorMessage = "Campo {0} obrigatório")]
        public string NomeCampo { get; set; }
    }
}
