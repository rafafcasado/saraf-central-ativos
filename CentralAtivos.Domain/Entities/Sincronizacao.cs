using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralAtivos.Domain.Entities
{
    [Table("Sincronizacao")]
    public class Sincronizacao : Base
    {
        public int InventarioID { get; set; }
        public DateTime DataEnvioArquivo { get; set; }
        public Enums.StatusSincronizacao Status { get; set; }
        public int UsuarioEnvioID { get; set; }
        public int? UsuarioProcessamentoID { get; set; }

        [NotMapped]
        public string StatusNome { get { return Status.ToString(); } }

        [NotMapped]
        public string LinkArquivo { get { return System.Configuration.ConfigurationManager.AppSettings["SincronizacoesLogico"] + ID + "/sinc.json"; } }

        [NotMapped]
        public string DataEnvioArquivoFormatada
        {
            get { return string.Format("{0:dd/MM/yyyy HH:mm:ss}", DataEnvioArquivo); }
        }

        public virtual Inventario Inventario { get; set; }
        public virtual Usuario UsuarioEnvio { get; set; }
        public virtual Usuario UsuarioProcessamento { get; set; }
    }
}
