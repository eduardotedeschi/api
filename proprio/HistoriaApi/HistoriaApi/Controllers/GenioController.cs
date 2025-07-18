using HistoriaApi.DataBase;
using Microsoft.AspNetCore.Mvc;

namespace HistoriaApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenioController : ControllerBase
    {

        //Recebe o conteúdo da classe GaniosDataBase
        private readonly GeniosDataBase _geniosData; // _geniosData está vazio
        private readonly ILogger<GenioController> _logger;

        public GenioController(GeniosDataBase geniosData, ILogger<GenioController> logger) //geniosData é o parâmetro que contêm os métodos da classe GeniosDataBase
        {
            _geniosData = geniosData; // _geniosData está recebendo o conteúdo
            _logger = logger;
        }

        private static List<Genio> genios = new List<Genio>
        {
            new Genio { ID = 1, Nome = "Stephen Hawking", AnoNascimento = 1942, AnoObito = 2018, Descricao = "Stephen William Hawking foi um físico teórico, cosmólogo e autor britânico, reconhecido internacionalmente por sua contribuição à ciência, sendo um dos mais renomados cientistas do século.", Contribuicoes = "Descobridor da Radiação Hawking, que mostrou que buracos negros podem emitir radiação. Pioneiro na unificação da relatividade geral com a mecânica quântica, revolucionando a cosmologia moderna."},
            new Genio { ID = 2, Nome = "Pedro Álvares Cabral", AnoNascimento = 1467, AnoObito = 1520, Descricao = "Pedro Álvares Cabral foi um fidalgo, comandante militar, navegador e explorador português, creditado como o descobridor do Brasil. Realizou significativa exploração da costa nordeste da América do Sul, reivindicando-a para Portugal.", Contribuicoes = "Descobridor do Brasil em 1500, liderou a primeira expedição europeia a reivindicar terras na América do Sul para Portugal. Sua exploração ajudou a expandir o império colonial português."},
            new Genio { ID = 3, Nome = "Malala Yousafzai", AnoNascimento = 1997, AnoObito = 2024, Descricao = "é uma ativista paquistanesa. Foi a pessoa mais nova a ser laureada com um prémio Nobel. É conhecida principalmente pela defesa dos direitos humanos das mulheres e do acesso à educação no Paquistão", Contribuicoes = "Defensora global da educação feminina, sobreviveu a um atentado do Talibã e ganhou o Prêmio Nobel da Paz em 2014, tornando-se a mais jovem laureada da história."},
            new Genio { ID = 4, Nome = "Alan Turing", AnoNascimento = 1912, AnoObito = 1954, Descricao = "Alan Mathison Turing foi um matemático, cientista da computação, lógico, criptoanalista, filósofo e biólogo teórico britânico.", Contribuicoes = "Pai da computação moderna, concebeu a \"Máquina de Turing\" e ajudou a decifrar códigos nazistas durante a Segunda Guerra Mundial."},
            new Genio { ID = 5, Nome = "Ptolomeu ", AnoNascimento = 100, AnoObito = 170, Descricao = "Cláudio Ptolemeu, ou apenas Ptolemeu ou Ptolomeu, foi um cientista grego que viveu em Alexandria, uma cidade do Egito. Ele é reconhecido pelos seus trabalhos em matemática, astronomia, geografia e cartografia. Realizou também trabalhos importantes em óptica e teoria musical.", Contribuicoes = "Autor do Almagesto, onde apresentou um modelo geocêntrico do universo, que dominou a astronomia até o Renascimento."},

        };

        [HttpGet("Exibir")]
        public IActionResult ListarTodas()
        {
            return Ok(_geniosData.SelectTodos());
        }

        [HttpGet("Procurar/{ano}", Name = "GetGenio")]
        public IActionResult GetGenio(int ano)
        {
            return Ok(_geniosData.GetGenioAno(ano));
        }

        [HttpPost("Adicionar")]
        public IActionResult Post([FromBody] Genio novoGenio)
        {
            if(novoGenio == null)
            {
                return BadRequest("Preencha os campos corretamente!");
            }

            int id = _geniosData.Post(novoGenio);

            if (id == 0)
            {
                return BadRequest("Não foi possível adicionar o gênio.");
            }

            return Ok(id);
        }

        [HttpPut("Atualizar/{id}")]
        public IActionResult Put(int id, [FromBody] Genio uptadeGenio)
        {
            Genio genioAtt = _geniosData.Uptade(id, uptadeGenio);
            if (genioAtt == null)
            {
                return BadRequest("Preencha os campos corretamente!");
            }

            return Ok(genioAtt);
        }

        [HttpDelete("Deletar/{id}")]
        public IActionResult Delete(int id)
        {
       
            if(!_geniosData.Delete(id))
            {
                return NotFound("ID não existente");
            }
            return Ok("Deletado com sucesso!");
        }
    }
}
