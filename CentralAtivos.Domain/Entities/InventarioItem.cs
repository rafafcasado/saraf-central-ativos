using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralAtivos.Domain.Entities
{
    [Table("InventarioItem")]
    public class InventarioItem : Base
    {
        public int InventarioID { get; set; }
        public int ItemID { get; set; }
        public DateTime DataColeta { get; set; }
        public int UsuarioColetaID { get; set; }
        public DateTime DataUltimaAtualizacao { get; set; }
        public int UsuarioUltimaAtualizacaoID { get; set; }

        public Inventario Inventario { get; set; }
        public Item Item { get; set; }
        public Usuario UsuarioColeta { get; set; }
        public Usuario UsuarioUltimaAtualizacao { get; set; }
    }
}
