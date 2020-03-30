using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralAtivos.Domain.Entities
{
    [Table("TelaAcao")]
    public class TelaAcao : Base
    {
        public int TelaID { get; set; }

        public int AcaoID { get; set; }

        [JsonIgnore]
        public virtual Tela Tela { get; set; }

        [JsonIgnore]
        public virtual Acao Acao { get; set; }
    }
}
