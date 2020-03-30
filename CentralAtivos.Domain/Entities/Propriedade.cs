using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralAtivos.Domain.Entities
{
    [Table("Propriedade")]
    public class Propriedade : Base
    {
        public Propriedade()
        {
            Valores = new List<PropriedadeValor>();
        }
        public int EmpresaID { get; set; }

        [StringLength(50, ErrorMessage = "O campo {0} deve conter, no máximo {1} caracteres")]
        [Required(ErrorMessage = "Campo {0} obrigatório")]
        public string Codigo { get; set; }

        [StringLength(150, ErrorMessage = "O campo {0} deve conter, no máximo {1} caracteres")]
        [Required(ErrorMessage = "Campo {0} obrigatório")]
        public string Nome { get; set; }

        public bool Fixa { get; set; }

        [NotMapped]
        public bool VincularEspecies { get; set; }


        public virtual ICollection<PropriedadeValor> Valores { get; set; }

        [JsonIgnore]
        public virtual Empresa Empresa { get; set; }

    }
}
