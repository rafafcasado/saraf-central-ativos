using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralAtivos.Domain.Entities
{
    [Table("PerfilTelaAcao")]
    public class PerfilTelaAcao : Base
    {
        public int PerfilID { get; set; }

        public int TelaAcaoID { get; set; }

        [JsonIgnore]
        public virtual Perfil Perfil { get; set; }

        [JsonIgnore]
        public virtual TelaAcao TelaAcao { get; set; }
    }
}
