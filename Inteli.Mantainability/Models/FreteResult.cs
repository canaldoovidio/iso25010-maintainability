namespace Inteli.Mantainability.Models
{
    public struct FreteResult
    {
        public decimal Valor { get; }
        public int PrazoEmDias { get; }

        public FreteResult(decimal valor, int prazoEmDias)
        {
            Valor = valor;
            PrazoEmDias = prazoEmDias;
        }
    }
}
