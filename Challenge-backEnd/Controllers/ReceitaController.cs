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
            // Verificar se j� existe uma receita com a mesma descri��o e m�s
            bool receitaDuplicada = receitas.Any(r =>
                r.Descricao == receita.Descricao &&
                r.Data.Year == receita.Data.Year &&
                r.Data.Month == receita.Data.Month);

            if (receitaDuplicada)
            {
                return BadRequest("J� existe uma receita com a mesma descri��o no mesmo m�s.");
            }
            

            receitas.Add(receita);
            return Ok();
        }

        [HttpGet]
        public void IActionResult()
        {
            foreach (var receita in receitas)
            {
                Console.WriteLine("Descri��o " + receita.Descricao);
                Console.WriteLine("Valor " + receita.Valor);
                Console.WriteLine("Data " + receita.Data);
                
            }
        }

        [HttpGet]
        public void RecuperaReceitaPeloId(int id)
        {
            receitas.FirstOrDefault(receita => receita.Id == id);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaReceita(int id, [FromBody] Receita novaReceita)
        {
            // Localiza a receita existente com base no ID
            Receita receitaExistente = receitas.FirstOrDefault(r => r.Id == id);

            if (receitaExistente == null)
            {
                return NotFound(); // Retorna NotFound caso a receita n�o seja encontrada
            }

            receitaExistente.Descricao = novaReceita.Descricao;
            receitaExistente.Valor = novaReceita.Valor;
            receitaExistente.Data = novaReceita.Data;
            receitaExistente.Categoria = novaReceita.Categoria;

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