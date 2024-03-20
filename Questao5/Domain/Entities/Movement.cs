using Questao5.Domain.Enumerators;

namespace Questao5.Domain.Entities
{
    public class Movement
    {
        public string Id { get; set; }
        public string AccountId { get; set; }
        public DateTime MovementDate { get; set; }
        public MovementType MovementType { get; set; }
        public decimal MovementValue { get; set; }

        public Movement()
        {
            // Inicialize a propriedade AccountId para evitar o aviso CS8618
            AccountId = string.Empty; // Ou você pode atribuir outro valor inicial, se apropriado
            Id = string.Empty; // Ou você pode atribuir outro valor inicial, se apropriado
        }
    }
}
