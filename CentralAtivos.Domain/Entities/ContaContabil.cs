using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralAtivos.Domain.Entities
{
    [Table("ContaContabil")]
    public class ContaContabil : Base
    {
        public ContaContabil()
        {
            Filhos = new List<ContaContabil>();
        }

        [Required(ErrorMessage = "Campo {0} obrigatório")]
        public int EmpresaID { get; set; }

        public int? PaiID { get; set; }

        [StringLength(50, ErrorMessage = "O campo {0} deve conter, no máximo {1} caracteres")]
        [Required(ErrorMessage = "Campo {0} obrigatório")]
        public string Codigo { get; set; }

        [StringLength(150, ErrorMessage = "O campo {0} deve conter, no máximo {1} caracteres")]
        [Required(ErrorMessage = "Campo {0} obrigatório")]
        public string Nome { get; set; }

        [StringLength(80, ErrorMessage = "O campo {0} deve conter, no máximo {1} caracteres")]
        [Required(ErrorMessage = "Campo {0} obrigatório")]
        public string NomeAbreviado { get; set; }

        public double CodigoInterno { get; set; }

        [NotMapped]
        public int Nivel
        {
            get { return CodigoInterno.ToString().Split(',').Length == 1 ? 1 : CodigoInterno.ToString().Split(',')[1].Length + 1; }
        }

        [NotMapped]
        public string CaminhoPao { get; set; }

        [JsonIgnore]
        public virtual Empresa Empresa { get; set; }

        [JsonIgnore]
        public virtual ContaContabil Pai { get; set; }

        [JsonIgnore]
        public virtual ICollection<ContaContabil> Filhos { get; set; }
    }
}
