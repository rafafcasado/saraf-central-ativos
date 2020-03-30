using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralAtivos.Domain.Entities
{
    [Table("Inventario")]
    public class Inventario : Base
    {
        public Inventario()
        {
            InventarioFiliais = new List<InventarioFilial>();
            InventarioLocais = new List<InventarioLocal>();
            Usuarios = new List<InventarioUsuario>();

            AplicarMascara = false;
        }

        [StringLength(50, ErrorMessage = "O campo {0} deve conter, no máximo {1} caracteres")]
        [Required(ErrorMessage = "Campo {0} obrigatório")]
        public string Codigo { get; set; }

        [StringLength(150, ErrorMessage = "O campo {0} deve conter, no máximo {1} caracteres")]
        [Required(ErrorMessage = "Campo {0} obrigatório")]
        public string Nome { get; set; }

        public int StatusID { get; set; }
        public int EmpresaID { get; set; }
        public bool Geral { get; set; }

        [StringLength(150, ErrorMessage = "O campo {0} deve conter, no máximo {1} caracteres")]
        public string MascaraNomeItem { get; set; }

        public bool? AplicarMascara { get; set; }

        public virtual Status Status { get; set; }

        public virtual Empresa Empresa { get; set; }

        [NotMapped]
        public List<Filial> Filiais { get; set; }
        [NotMapped]
        public List<Local> Locais { get; set; }

        public virtual ICollection<InventarioFilial> InventarioFiliais { get; set; }
        public virtual ICollection<InventarioLocal> InventarioLocais { get; set; }
        public virtual ICollection<InventarioUsuario> Usuarios { get; set; }

        [NotMapped]
        public ICollection<InventarioConfig> Configs { get; set; }
    }
}
