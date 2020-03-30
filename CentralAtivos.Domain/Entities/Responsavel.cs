using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralAtivos.Domain.Entities
{
    [Table("Responsavel")]
    public class Responsavel : Base
    {
        [Required(ErrorMessage = "Campo {0} obrigatório")]
        public int EmpresaID { get; set; }

        public int? UsuarioID { get; set; }

        [StringLength(150, ErrorMessage = "O campo {0} deve conter, no máximo {1} caracteres")]
        [Required(ErrorMessage = "Campo {0} obrigatório")]
        public string Nome { get; set; }

        [StringLength(100, ErrorMessage = "O campo {0} deve conter, no máximo {1} caracteres")]
        public string Cargo { get; set; }

        [StringLength(20, ErrorMessage = "O campo {0} deve conter, no máximo {1} caracteres")]
        public string Matricula { get; set; }

        [JsonIgnore]
        public virtual Empresa Empresa { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
