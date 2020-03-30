using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralAtivos.Domain.Entities
{
    [Table("Item")]
    public class Item : Base
    {
        public Item()
        {
            Imagens = new List<string>();
            StatusID = Enums.StatusItem.NAO_INVENTARIADO;
        }

        public int? ItemEstadoID { get; set; }
        public int? ResponsavelID { get; set; }
        public int LocalID { get; set; }
        public int EmpresaID { get; set; }
        public int EspecieID { get; set; }

        [StringLength(30, ErrorMessage = "O campo {0} deve conter, no máximo {1} caracteres")]
        [Required(ErrorMessage = "Campo {0} obrigatório")]
        public string Nome { get; set; }

        [StringLength(20, ErrorMessage = "O campo {0} deve conter, no máximo {1} caracteres")]
        [Required(ErrorMessage = "Campo {0} obrigatório")]
        public string Codigo { get; set; }

        public int? Incorporacao { get; set; }

        [StringLength(20, ErrorMessage = "O campo {0} deve conter, no máximo {1} caracteres")]
        public string CodigoAnterior { get; set; }

        public int? IncorporacaoAnterior { get; set; }

        [StringLength(20, ErrorMessage = "O campo {0} deve conter, no máximo {1} caracteres")]
        public string CodigoPM { get; set; }

        [StringLength(50, ErrorMessage = "O campo {0} deve conter, no máximo {1} caracteres")]
        public string DadosPM { get; set; }

        [StringLength(100, ErrorMessage = "O campo {0} deve conter, no máximo {1} caracteres")]
        public string LocalPM { get; set; }

        [StringLength(50, ErrorMessage = "O campo {0} deve conter, no máximo {1} caracteres")]
        public string Tag { get; set; }

        [StringLength(50, ErrorMessage = "O campo {0} deve conter, no máximo {1} caracteres")]
        public string Marca { get; set; }

        [StringLength(50, ErrorMessage = "O campo {0} deve conter, no máximo {1} caracteres")]
        public string Modelo { get; set; }

        [StringLength(50, ErrorMessage = "O campo {0} deve conter, no máximo {1} caracteres")]
        public string NumeroSerie { get; set; }

        [StringLength(150, ErrorMessage = "O campo {0} deve conter, no máximo {1} caracteres")]
        public string Observacao { get; set; }

        [StringLength(20, ErrorMessage = "O campo {0} deve conter, no máximo {1} caracteres")]
        public string ImagemUrl { get; set; }

        public decimal? Latitude { get; set; }

        public decimal? Longitude { get; set; }

        public Enums.StatusItem StatusID { get; set; }

        [NotMapped]
        public string StatusNome { get { return StatusID.ToString(); } }

        [NotMapped]
        public List<string> Imagens { get; set; }


        [JsonIgnore]
        public virtual ItemEstado ItemEstado { get; set; }

        [JsonIgnore]
        public virtual Responsavel Responsavel { get; set; }

        public virtual Local Local { get; set; }

        [JsonIgnore]
        public virtual Empresa Empresa { get; set; }

        public virtual Especie Especie { get; set; }

        public virtual ICollection<CampoExtra> CamposExtras { get; set; }
        public virtual ICollection<ItemPropriedadeValor> PropriedadesValores { get; set; }
    }
}
