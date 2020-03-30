using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralAtivos.Domain.Entities
{
    [Table("Perfil")]
    public class Perfil : Base
    {
        [StringLength(150, ErrorMessage = "O campo {0} deve conter, no máximo {1} caracteres")]
        [Required(ErrorMessage = "Campo {0} obrigatório")]
        public string Nome { get; set; }

        [NotMapped]
        public bool CopiarPerfil { get; set; }

        [NotMapped]
        public int? PerfilCopiaID { get; set; }
    }
}
