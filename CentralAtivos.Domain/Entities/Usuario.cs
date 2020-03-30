using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralAtivos.Domain.Entities
{
    [Table("Usuario")]
    public class Usuario : Base
    {
        public Usuario()
        {
            PrimeiroAcesso = true;
        }

        public int? EmpresaID { get; set; }

        [Required(ErrorMessage = "Campo {0} obrigatório")]
        public int PerfilID { get; set; }

        [StringLength(50, ErrorMessage = "O campo {0} deve conter, no máximo {1} caracteres")]
        public string Matricula { get; set; }

        [StringLength(150, ErrorMessage = "O campo {0} deve conter, no máximo {1} caracteres")]
        [Required(ErrorMessage = "Campo {0} obrigatório")]
        public string Nome { get; set; }

        [StringLength(150, ErrorMessage = "O campo {0} deve conter, no máximo {1} caracteres")]
        [Required(ErrorMessage = "Campo {0} obrigatório")]
        [EmailAddress(ErrorMessage = "O campo e-mail está em formato incorreto")]
        public string Email { get; set; }

        public string Senha { get; set; }

        public bool PrimeiroAcesso { get; set; }

        public Guid? TokenSolicitacaoSenha { get; set; }

        public DateTime? DataValidadeTokenSenha { get; set; }

        [NotMapped]
        [Compare("Senha", ErrorMessage = "Os campos Senha e Confirmação de Senha devem ser iguais")]
        public string ConfirmacaoSenha { get; set; }

        [NotMapped]
        public bool NotificarUsuario { get; set; }


        public virtual Empresa Empresa { get; set; }

        public virtual Perfil Perfil { get; set; }
    }
}
