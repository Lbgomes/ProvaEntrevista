using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoEntrevista2_1.Domains;
using ProjetoEntrevista2_1.Repositories;

namespace ProjetoEntrevista2_1.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioPjController : ControllerBase
    {

        UsuarioPJRepository UsuarioPJRepository = new UsuarioPJRepository();

        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(UsuarioPJRepository.Listar());
        }

        [HttpGet("{cnpj}")]
        public IActionResult BuscarPorId(int cnpj)
        {
            UsuarioPJDomains UsuarioPJDomains = UsuarioPJRepository.BuscarPorCnpj(cnpj);
            if (UsuarioPJDomains == null)
                return NotFound();
            return Ok(UsuarioPJDomains);
        }

        [HttpPost]
        public IActionResult Cadastrar(UsuarioPJDomains UsuarioPFDomains)
        {
            UsuarioPJRepository.Cadastrar(UsuarioPFDomains);
            return Ok();
        }

        [HttpPut("{cnpj}")]
        public IActionResult Atualizar(int cnpj, UsuarioPJDomains UsuarioPJ)
        {
            UsuarioPJ.NumeroCnpj = cnpj;
            UsuarioPJRepository.Alterar(UsuarioPJ);
            return Ok();
        }

        [HttpDelete("{cnpj}")]
        public IActionResult Deletar(int cnpj)
        {
            UsuarioPJRepository.Deletar(cnpj);
            return Ok();
        }

        [HttpGet("buscar/{nome}")]
        public IActionResult BuscarPorNome(string nome)
        {
            try
            {
                List<UsuarioPJDomains> UsuariosPJ = UsuarioPJRepository.BuscarPorNome(nome);
                // caso nenhum funcionario seja encontrado com aquele nome, retorno sem conteudo
                if (UsuariosPJ.Count == 0)
                    return NoContent();
                // caso contrario, apresento a lista de funcionarios com determinado nome
                return Ok(UsuariosPJ);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "Ocorreu um erro." + ex.Message });
            }
        }

    }
}
