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

            despesas.Add(despesa);
            return Ok();
        }


        [HttpGet]
        public void IActionResult()
        {
            foreach(var despesa in despesas)
            {
                Console.WriteLine("Descrição: " + despesa.Descricao);
                Console.WriteLine("Valor: " + despesa.Valor);
                Console.WriteLine("Data: " + despesa.Data);
            }
        }

        [HttpGet]
        public void RecuperaDespesaPeloId(int id)
        {
            despesas.FirstOrDefault(despesa => despesa.Id == id);
        }


    }
}
