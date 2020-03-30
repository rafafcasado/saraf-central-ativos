using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralAtivos.Domain.Entities
{
    [Table("Upload")]
    public class Upload : Base
    {
        public int EmpresaID { get; set; }

        [StringLength(200, ErrorMessage = "O campo {0} deve conter, no máximo {1} caracteres")]
        [Required(ErrorMessage = "Campo {0} obrigatório")]
        public string NomeArquivo { get; set; }
        public int UsuarioID { get; set; }
        public DateTime? DataInicioProcessamento { get; set; }
        public Enums.Entidade Entidade { get; set; }
        public Enums.StatusUpload Status { get; set; }

        [StringLength(200, ErrorMessage = "O campo {0} deve conter, no máximo {1} caracteres")]
        public string NomeArquivoCriticas { get; set; }

        [StringLength(350, ErrorMessage = "O campo {0} deve conter, no máximo {1} caracteres")]
        public string Observacoes { get; set; }

        public int? RegraUploadID { get; set; }

        [NotMapped]
        public string EntidadeNome { get { return Entidade.ToString(); } }

        [NotMapped]
        public string StatusNome { get { return Status.ToString(); } }

        [NotMapped]
        public string LinkArquivo { get { return System.Configuration.ConfigurationManager.AppSettings["UploadsLogico"] + ID + "/" + NomeArquivo; } }

        [NotMapped]
        public string LinkArquivoCriticas { get { return System.Configuration.ConfigurationManager.AppSettings["UploadsLogico"] + ID + "/Criticas/" + NomeArquivoCriticas; } }

        public virtual Empresa Empresa { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
