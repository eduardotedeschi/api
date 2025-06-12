using Microsoft.AspNetCore.Mvc;

namespace HistoriaApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenioController : ControllerBase
    {
        private static List<Genio> genios = new List<Genio>
        {
            new Genio { GenioID = 1, GenioNome = "Stephen Hawking", AnoNascimento = 1942, AnoObito = 2018, Descricao = "Stephen William Hawking foi um f�sico te�rico, cosm�logo e autor brit�nico, reconhecido internacionalmente por sua contribui��o � ci�ncia, sendo um dos mais renomados cientistas do s�culo.", Contribuicoes = "Descobridor da Radia��o Hawking, que mostrou que buracos negros podem emitir radia��o. Pioneiro na unifica��o da relatividade geral com a mec�nica qu�ntica, revolucionando a cosmologia moderna."},
            new Genio { GenioID = 2, GenioNome = "Pedro �lvares Cabral", AnoNascimento = 1467, AnoObito = 1520, Descricao = "Pedro �lvares Cabral foi um fidalgo, comandante militar, navegador e explorador portugu�s, creditado como o descobridor do Brasil. Realizou significativa explora��o da costa nordeste da Am�rica do Sul, reivindicando-a para Portugal.", Contribuicoes = "Descobridor do Brasil em 1500, liderou a primeira expedi��o europeia a reivindicar terras na Am�rica do Sul para Portugal. Sua explora��o ajudou a expandir o imp�rio colonial portugu�s."},
            new Genio { GenioID = 3, GenioNome = "Malala Yousafzai", AnoNascimento = 1997, AnoObito = 2024, Descricao = "� uma ativista paquistanesa. Foi a pessoa mais nova a ser laureada com um pr�mio Nobel. � conhecida principalmente pela defesa dos direitos humanos das mulheres e do acesso � educa��o no Paquist�o", Contribuicoes = "Defensora global da educa��o feminina, sobreviveu a um atentado do Talib� e ganhou o Pr�mio Nobel da Paz em 2014, tornando-se a mais jovem laureada da hist�ria."},
            new Genio { GenioID = 4, GenioNome = "Alan Turing", AnoNascimento = 1912, AnoObito = 1954, Descricao = "Alan Mathison Turing foi um matem�tico, cientista da computa��o, l�gico, criptoanalista, fil�sofo e bi�logo te�rico brit�nico.", Contribuicoes = "Pai da computa��o moderna, concebeu a \"M�quina de Turing\" e ajudou a decifrar c�digos nazistas durante a Segunda Guerra Mundial."},
            new Genio { GenioID = 5, GenioNome = "Ptolomeu ", AnoNascimento = 100, AnoObito = 170, Descricao = "Cl�udio Ptolemeu, ou apenas Ptolemeu ou Ptolomeu, foi um cientista grego que viveu em Alexandria, uma cidade do Egito. Ele � reconhecido pelos seus trabalhos em matem�tica, astronomia, geografia e cartografia. Realizou tamb�m trabalhos importantes em �ptica e teoria musical.", Contribuicoes = "Autor do Almagesto, onde apresentou um modelo geoc�ntrico do universo, que dominou a astronomia at� o Renascimento."},

        };

        private readonly ILogger<GenioController> _logger;

        public GenioController(ILogger<GenioController> logger)
        {
            _logger = logger;
        }

        [HttpGet("Exibir")]
        public IActionResult ListarTodas()
        {
            return Ok(genios);
        }

        [HttpGet("Procurar/{ano}", Name = "GetGenio")]
        public IActionResult GetGenio(int ano)
        {
            Random rand = new Random();

            var geniosFiltrados = genios.Where(g => ano >= g.AnoNascimento && ano <= g.AnoObito).ToList();
            Genio genioAleatorio = geniosFiltrados[rand.Next(geniosFiltrados.Count)];
            return new JsonResult(genioAleatorio);
        }

        [HttpPost("Adicionar")]
        public IActionResult Post([FromBody] Genio novoGenio)
        {
            if(novoGenio == null)
            {
                return BadRequest("Preencha os campos corretamente!");
            }

            int novoId = genios.Max(r => r.GenioID) + 1;
            novoGenio.GenioID = novoId;
            genios.Add(novoGenio);
            return Ok(novoGenio);
        }

        [HttpPut("Atualizar/{id}")]
        public IActionResult Put(int id, [FromBody] Genio GenioAtt)
        {
            if(GenioAtt == null)
            {
                return BadRequest("Preencha os campos corretamente!");
            }

            var GenioAtual = genios.FirstOrDefault(r => r.GenioID == id);
            if(GenioAtual == null)
            {
                return NotFound("ID n�o existente");
            }

            GenioAtual.GenioNome = GenioAtt.GenioNome;
            GenioAtual.AnoNascimento = GenioAtt.AnoNascimento;
            GenioAtual.AnoObito = GenioAtt.AnoObito;
            GenioAtual.Descricao = GenioAtt.Descricao;
            GenioAtual.Contribuicoes = GenioAtt.Contribuicoes;

            return Ok(GenioAtual);
        }

        [HttpDelete("Deletar/{id}")]
        public IActionResult Delete(int id)
        {
            var GenioDel = genios.FirstOrDefault(r => r.GenioID == id);
            if(GenioDel == null)
            {
                return NotFound("ID n�o existente");
            }
            genios.Remove(GenioDel);
            return NoContent();
        }
    }
}
