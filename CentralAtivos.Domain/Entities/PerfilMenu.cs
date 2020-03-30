using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralAtivos.Domain.Entities
{
    [Table("PerfilMenu")]
    public class PerfilMenu : Base
    {
        public int PerfilID { get; set; }

        public int MenuID { get; set; }

        [JsonIgnore]
        public virtual Perfil Perfil { get; set; }

        public virtual Menu Menu { get; set; }
    }
}
