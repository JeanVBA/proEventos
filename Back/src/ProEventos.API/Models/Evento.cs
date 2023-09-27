using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProEventos.API.models
{
    public class Evento
    {
        public int Id { get; set; }
        public string Local { get; set; }
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public string DataEvento { get; set; }
        public string Tema { get; set; }
        public int QtdPessoas { get; set; }
        public string Lote {get; set; }
        public string ImagemURL { get; set; }
    }
}