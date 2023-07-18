using Challenge_backEnd.Models;
using Microsoft.AspNetCore.Mvc;

namespace Challenge_backEnd.Controllers
{
    public class DespesasController : Controller
    {
        List<Despesa> despesas = new List<Despesa>();

        [HttpPost]
        public IActionResult AdicionarDespesa([FromBody] Despesa despesa)
        {
            bool despesaDuplicada = despesas.Any(d =>
                d.Descricao == despesa.Descricao &&
                d.Data.Year == despesa.Data.Year &&
                d.Data.Month == despesa.Data.Month);

            if (despesaDuplicada)
            {
                return BadRequest("Já existe uma despesa com a mesma descrição no mesmo mês.");
            }
            if (despesa.Categoria == null)
            {
                despesa.Categoria = 0;
            }

            despesas.Add(despesa);
            return Ok();
        }


        [HttpGet]
        public void IActionResult()
        {
            foreach (var despesa in despesas)
            {
                Console.WriteLine("Descrição: " + despesa.Descricao);
                Console.WriteLine("Valor: " + despesa.Valor);
                Console.WriteLine("Data: " + despesa.Data);
                Console.WriteLine("Categoria " + despesa.Categoria);
            }
        }

        [HttpGet]
        public void RecuperaDespesaPeloId(int id)
        {
            despesas.FirstOrDefault(despesa => despesa.Id == id);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaDespesa(int id, [FromBody] Despesa novaDespesa)
        {
            // Localiza a despesa existente com base no ID
            Despesa despesaExistente = despesas.FirstOrDefault(d => d.Id == id);

            if (despesaExistente == null)
            {
                return NotFound(); // Retorna NotFound caso a despesa não seja encontrada
            }

            despesaExistente.Descricao = novaDespesa.Descricao;
            despesaExistente.Valor = novaDespesa.Valor;
            despesaExistente.Data = novaDespesa.Data;

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaDespesa(int id)
        {
            // Localiza a despesa existente com base no ID
            Despesa despesaExistente = despesas.FirstOrDefault(d => d.Id == id);

            if (despesaExistente == null)
            {
                return NotFound();
            }

            despesas.Remove(despesaExistente);
            return Ok();
        }

    }
}
