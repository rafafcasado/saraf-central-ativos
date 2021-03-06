﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralAtivos.Domain.Entities
{
    [Table("Grupo")]
    public class Grupo : Base
    {
        [Required(ErrorMessage = "Campo {0} obrigatório")]
        public int EmpresaID { get; set; }

        public int? ContaContabilID { get; set; }

        [StringLength(50, ErrorMessage = "O campo {0} deve conter, no máximo {1} caracteres")]
        [Required(ErrorMessage = "Campo {0} obrigatório")]
        public string Codigo { get; set; }

        [StringLength(150, ErrorMessage = "O campo {0} deve conter, no máximo {1} caracteres")]
        [Required(ErrorMessage = "Campo {0} obrigatório")]
        public string Nome { get; set; }

        [JsonIgnore]
        public virtual Empresa Empresa { get; set; }

        [JsonIgnore]
        public virtual ContaContabil ContaContabil { get; set; }

    }
}
