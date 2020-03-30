using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralAtivos.Domain.Entities
{
    [Table("Local")]
    public class Local : Base
    {
        public Local()
        {
            Filhos = new List<Local>();
        }

        public int? ResponsavelID { get; set; }

        public int? CentroCustoID { get; set; }

        [Required(ErrorMessage = "Campo {0} obrigatório")]
        public int FilialID { get; set; }

        [StringLength(50, ErrorMessage = "O campo {0} deve conter, no máximo {1} caracteres")]
        [Required(ErrorMessage = "Campo {0} obrigatório")]
        public string Codigo { get; set; }

        [StringLength(150, ErrorMessage = "O campo {0} deve conter, no máximo {1} caracteres")]
        [Required(ErrorMessage = "Campo {0} obrigatório")]
        public string Nome { get; set; }

        public int? PaiID { get; set; }

        public double CodigoInterno { get; set; }

        [NotMapped]
        public int Nivel
        {
            get { return (CodigoInterno.ToString().Split(',').Length == 1 && CodigoInterno.ToString().Split('.').Length == 1) ? 1 : (CodigoInterno.ToString().Split(',').Length > 1 ? CodigoInterno.ToString().Split(',')[1].Length + 1 : CodigoInterno.ToString().Split('.')[1].Length + 1); }
        }

        [NotMapped]
        public string CaminhoPao { get; set; }

        [JsonIgnore]
        public virtual Responsavel Responsavel { get; set; }

        [JsonIgnore]
        public virtual CentroCusto CentroCusto { get; set; }

        public virtual Filial Filial { get; set; }

        [JsonIgnore]
        public virtual Local Pai { get; set; }

        [JsonIgnore]
        public virtual ICollection<Local> Filhos { get; set; }

    }
}
