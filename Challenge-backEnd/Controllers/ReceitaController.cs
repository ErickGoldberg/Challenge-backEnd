using Challenge_backEnd.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Challenge_backEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReceitaController : ControllerBase
    {
        List<Receita> receitas = new List<Receita> { };

        [HttpPost]
        public IActionResult AdicionarReceita([FromBody] Receita receita)
        {
            // Verificar se já existe uma receita com a mesma descrição e mês
            bool receitaDuplicada = receitas.Any(r =>
                r.Descricao == receita.Descricao &&
                r.Data.Year == receita.Data.Year &&
                r.Data.Month == receita.Data.Month);

            if (receitaDuplicada)
            {
                return BadRequest("Já existe uma receita com a mesma descrição no mesmo mês.");
            }
            

            receitas.Add(receita);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAllReceitas()
        {
            return Ok(receitas);
        }

        [HttpGet("{descricao}")]
        public IActionResult BuscarReceitasPelaDescricao(string descricao)
        {
            var receitasEncontradas = receitas.Where(r => r.Descricao.Contains(descricao, StringComparison.OrdinalIgnoreCase)).ToList();
            return Ok(receitasEncontradas);
        }

        [HttpGet]
        public IActionResult RecuperaReceitaPeloId(int id)
        {
            receitas.FirstOrDefault(receita => receita.Id == id);

            if(receitas == null)
            {
                return NotFound();
            }

            return Ok(receitas);
        }

        [HttpGet("listar/{ano}/{mes}")] // Para evitar conflito de rota com o GetAllReceitas()
        public IActionResult ListarReceitasMes(int ano, int mes)
        {
            var receitasFiltradas = receitas.Where(r => r.Data.Year == ano && r.Data.Month == mes).ToList();

            if (receitasFiltradas.Count == 0)
            {
                return NotFound();
            }

            return Ok(receitasFiltradas);
        }


        [HttpPut("{id}")]
        public IActionResult AtualizaReceita(int id, [FromBody] Receita novaReceita)
        {
            // Localiza a receita existente com base no ID
            Receita receitaExistente = receitas.FirstOrDefault(r => r.Id == id);

            if (receitaExistente == null)
            {
                return NotFound(); // Retorna NotFound caso a receita não seja encontrada
            }

            receitaExistente.Descricao = novaReceita.Descricao;
            receitaExistente.Valor = novaReceita.Valor;
            receitaExistente.Data = novaReceita.Data;

            return Ok(); 
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaReceita(int id)
        {
            // Localiza a receita existente com base no ID
            Receita receitaExistente = receitas.FirstOrDefault(r => r.Id == id);

            if (receitaExistente == null)
            {
                return NotFound();
            }

            receitas.Remove(receitaExistente);
            return Ok();
        }


    }
}