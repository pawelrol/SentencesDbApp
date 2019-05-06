using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SentencesDbApp.Models
{
    public class Sentence
    {
        [Key]
        public int SentenceId { get; set; }
        public string SentencePhrase { get; set; }
    }
}
