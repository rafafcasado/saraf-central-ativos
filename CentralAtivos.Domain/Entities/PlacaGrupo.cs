using System.ComponentModel.DataAnnotations.Schema;

namespace CentralAtivos.Domain.Entities
{
    [Table("PlacaGrupo")]
    public class PlacaGrupo : Base
    {
        public int InventarioID { get; set; }
        public int Inicio { get; set; }
        public int Fim { get; set; }
        public int UsuarioID { get; set; }
        public int Tamanho { get; set; }
        public bool AplicaZerosEsquerda { get; set; }

        [NotMapped]
        public string InicioFormatado
        {
            get
            {
                if (AplicaZerosEsquerda)
                    return Inicio.ToString().PadLeft(Tamanho, '0');
                else
                    return Inicio.ToString().PadRight(Tamanho, '0');
            }
        }

        [NotMapped]
        public string FimFormatado
        {
            get
            {
                if (AplicaZerosEsquerda)
                    return Fim.ToString().PadLeft(Tamanho, '0');
                else
                    return Fim.ToString().PadRight(Tamanho, '0');
            }
        }

        public Inventario Inventario { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
