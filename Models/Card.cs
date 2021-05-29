using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VaiVoa.Models
{
    public class Card
    {
        [Key]
        public int Id { get; set; }
        public string CardNumber { get; set; }
        [Required]
        public string Email { get; set; }
        public DateTime Date { get; set; }
        public string GenerateCard()
        {
            int Num = 16;
            string RandomCard = "";
            Random random = new Random();
            for (int i = 0; i < Num; i++)
            {
                int digito = random.Next(0, 9);
                RandomCard += digito;
            }
            return RandomCard;

        }

    }
}
