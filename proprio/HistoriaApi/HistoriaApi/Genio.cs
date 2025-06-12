namespace HistoriaApi
{
    public class Genio
    {
        public int GenioID { get; set; }

        public string GenioNome { get; set; } = string.Empty;

        public int AnoNascimento { get; set; }

        public int AnoObito {  get; set; } 

        public string Descricao { get; set; } = string.Empty;

        public string Contribuicoes {  get; set; } = string.Empty;
    }
}
