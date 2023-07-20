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
        public IActionResult GetAllDespesas()
        {
            return Ok(despesas);
        }

        [HttpGet("{descricao}")]
        public IActionResult BuscarDespesasPorDescricao(string descricao)
        {
            var despesasEncontradas = despesas.Where(d => d.Descricao.Contains(descricao, StringComparison.OrdinalIgnoreCase)).ToList();
            return Ok(despesasEncontradas);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaDespesaPeloId(int id)
        {
            var despesa = despesas.FirstOrDefault(d => d.Id == id);
            if (despesa == null)
            {
                return NotFound();
            }

            return Ok(despesa);
        }

        [HttpGet("Listar{ano}/{mes}")]
        public IActionResult ListarDespesaMes(int ano, int mes)
        {
            var listaDespesas = despesas.Where(d => d.Data.Year == ano && d.Data.Month == mes).ToList();

            if (listaDespesas.Count == 0)
            {
                return NotFound();
            }

            return Ok(listaDespesas);
        }

        [HttpGet("Resumo/{ano}/{mes}")]
        public IActionResult ResumoDespesaMes(int ano, int mes)
        {
            var despesasDoMes = despesas.Where(d => d.Data.Year == ano && d.Data.Month == mes).ToList();
            var receitasDoMes = receitas.Where(r => r.Data.Year == ano && r.Data.Month == mes).ToList();


            decimal valorTotalReceitas = receitasDoMes.Sum(r => r.Valor);

            decimal valorTotalDespesas = despesasDoMes.Sum(d => d.Valor);

            decimal saldoFinal = valorTotalReceitas - valorTotalDespesas;

            // Valor total gasto no mês em cada uma das categorias
            var categoriasGastos = despesasDoMes.GroupBy(d => d.Categoria)
                                              .Select(group => new { Categoria = group.Key, TotalGasto = group.Sum(d => d.Valor) })
                                              .ToList();

            // Constrói o objeto de resumo do mês
            var resumoMes = new
            {
                ValorTotalReceitas = valorTotalReceitas,
                ValorTotalDespesas = valorTotalDespesas,
                SaldoFinal = saldoFinal,
                CategoriasGastos = categoriasGastos
            };

            return Ok(resumoMes);
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
