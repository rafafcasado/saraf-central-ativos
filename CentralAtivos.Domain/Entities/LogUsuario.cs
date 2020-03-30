using Newtonsoft.Json;

namespace CentralAtivos.Domain.Entities
{
    public class LogUsuario : Base
    {
        public int UsuarioID { get; set; }
        public int RequisicaoID { get; set; }

        [JsonIgnore]
        public Usuario Usuario { get; set; }

        [JsonIgnore]
        public Requisicao Requisicao { get; set; }
    }
}
