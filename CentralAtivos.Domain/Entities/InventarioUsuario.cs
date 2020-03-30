using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralAtivos.Domain.Entities
{
    [Table("InventarioUsuario")]
    public class InventarioUsuario : Base
    {
        public int InventarioID { get; set; }
        public int UsuarioID { get; set; }
        public int PerfilID { get; set; }


        [JsonIgnore]
        public Inventario Inventario { get; set; }

        public Usuario Usuario { get; set; }

        public Perfil Perfil { get; set; }
    }
}
