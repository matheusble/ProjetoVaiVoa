# ProjetoVaiVoa

Objetivo:
 Criação de uma API para cartão de crédito virtual com números aleatórios, o usuário digitara o e-mail e tera como retorno o número do cartão.

Pacotes:
EntityFramework, 
EntityFrameworkCore, 
EntityFrameworkCore.SqlServer, 
EntityFrameworkCore.Tools.

Desenvolvimento do projeto:

Contexto:

Uma conexão que permite manipular o banco de dados utilizando code-first em conjunto com a camada model.

       namespace VaiVoa.Data
       {
        public class cardcontext : DbContext
       {
         public cardcontext(DbContextOptions<cardcontext> options)
             : base(options)
         {
         }

         public DbSet<Card> Cards { get; set; }
       }
      }
    
 Connection String:
    
    Conexão com o banco de dados.
     public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<cardcontext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

        }

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

 A string CardNumber é utilizada para armazenar os números gerados aleatoriamente, o DateTime Date é usado para armazenar a data que foi processado e por último a função GenerateCard onde o laço for repete 16 vezes um númerod de 0 á 9 e armazena no RandomCard e depois retorna para a função.

  GetEmail:
                                    
       [HttpGet]
        public async Task<ActionResult<Card>> GetEmail(string email)
        {
            var card = from c in _context.Cards select c;
            if (!String.IsNullOrEmpty(email))
            {
                card = card.Where(c => c.Email.Equals(email)).OrderBy(c => c.Date).Distinct();
            }

            if (card == null)
            {
                return NotFound();
            }

            var user = await card.ToListAsync();
            return CreatedAtAction(nameof(GetEmail), user);
        }
    
   Retorna o Email e os cartões atribuidos ao usuáro, sem repetir e por ordem de data.
    
   PostCard:
    
    [HttpPost]
    public async Task<ActionResult<Card>> PostCard(Card card)
        {
            Card c = new Card();

            card.CardNumber = c.GenerateCard();

            DateTime data = DateTime.Now;
            card.Date = data;

            _context.Cards.Add(card);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCard), new { id = card.Id }, card.CardNumber);
        }
    
   Recebe o e-mail como parâmetro e gera automaticamente um número aleatório do cartão, juntamente com a data de criação.
    
   Por fim utilizar um programa para testar APIs como por exemplo o Postman. 



