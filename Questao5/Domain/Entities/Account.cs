// Account.cs
namespace Questao5.Domain.Entities
{
    public class Account
    {
        public string idcontacorrente { get; set; }
        public int numero { get; set; }
        public string nome { get; set; }
        public bool ativo{ get; set; }

        public Account() 
        { 
            idcontacorrente = string.Empty;
            nome = string.Empty;
        }
    }
}
