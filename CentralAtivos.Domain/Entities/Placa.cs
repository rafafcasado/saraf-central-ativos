using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralAtivos.Domain.Entities
{
    [Table("Placa")]
    public class Placa : Base
    {
        public int PlacaGrupoID { get; set; }
        public int NumeroPlaca { get; set; }
        public int? ItemID { get; set; }

        [StringLength(250, ErrorMessage = "O campo {0} deve conter, no máximo {1} caracteres")]
        public string Observacao { get; set; }

        [NotMapped]
        public string NumeroPlacaFormatada { get; set; }

        public PlacaGrupo PlacaGrupo { get; set; }
    }
}
