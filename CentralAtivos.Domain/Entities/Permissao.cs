using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralAtivos.Domain.Entities
{
    [Table("Permissao")]
    public class Permissao : Base
    {

        [Required(ErrorMessage = "Campo {0} obrigatório")]
        public int PerfilID { get; set; }

        public int FuncionalidadeID { get; set; }

        [Required(ErrorMessage = "Campo {0} obrigatório")]
        [StringLength(50, ErrorMessage = "O campo {0} deve conter, no máximo {1} caracteres")]
        public string Metodos { get; set; }


        [JsonIgnore]
        public virtual Perfil Perfil { get; set; }

        [JsonIgnore]
        public virtual Funcionalidade Funcionalidade { get; set; }

    }
}
