using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralAtivos.Domain.Entities
{
    [Table("OrdemServicoAnexo")]
    public class OrdemServicoAnexo : Base
    {
        public int OrdemServicoID { get; set; }

        [StringLength(150, ErrorMessage = "O campo {0} deve conter, no máximo {1} caracteres")]
        [Required(ErrorMessage = "Campo {0} obrigatório")]
        public string NomeArquivo { get; set; }

        public bool Imagem { get; set; }

        [NotMapped]
        public string Link
        {
            get
            {
                if (Imagem)
                    return System.Configuration.ConfigurationManager.AppSettings["OrdensServicoLogico"] + OrdemServicoID + "/Imagem/" + NomeArquivo;
                else
                    return System.Configuration.ConfigurationManager.AppSettings["OrdensServicoLogico"] + OrdemServicoID + "/Anexo/" + NomeArquivo;
            }
        }

        public virtual OrdemServico OrdemServico { get; set; }
    }
}
