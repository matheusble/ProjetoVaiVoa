# ProjetoVaiVoa

Pacotes:
EntityFramework.
EntityFrameworkCore.
EntityFrameworkCore.SqlServer.
EntityFrameworkCore.Tools.

Camada Model:

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

Criado um Id como Key, uma string CardNumber para armazenar os números gerados aleatoriamente, um DateTime Date para armazenar a data que foi processado e por último a função GenerateCard onde o laço for repete 16 vezes um númerod de 0 á 9 e armazena no RandomCard e depois retorna para a função.

