using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralAtivos.Domain.Entities
{
    [Table("PerfilTela")]
    public class PerfilTela : Base
    {
        public int PerfilID { get; set; }

        public int TelaID { get; set; }

        [JsonIgnore]
        public virtual Perfil Perfil { get; set; }

        [JsonIgnore]
        public virtual Tela Tela { get; set; }
    }
}
