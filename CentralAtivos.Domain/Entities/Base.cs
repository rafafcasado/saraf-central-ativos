using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralAtivos.Domain.Entities
{
    public class Base
    {
        public Base()
        {
            DataCadastro = DateTime.Now;
        }

        [Key]
        public int ID { get; set; }

        [JsonIgnore]
        public DateTime DataCadastro { get; set; }

        [NotMapped]
        public string DataCadastroFormatada {
            get { return string.Format("{0:dd/MM/yyyy HH:mm:ss}", DataCadastro); }
        }

        public DateTime? DataExclusao { get; set; }
    }
}
