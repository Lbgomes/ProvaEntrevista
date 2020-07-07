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
    public class UsuarioPfController : ControllerBase
    {

        UsuarioPfRepository UsuarioPfRepository = new UsuarioPfRepository();

        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(UsuarioPfRepository.Listar());
        }

        [HttpGet("{cpf}")]
        public IActionResult BuscarPorId(int cpf)
        {
            UsuarioPFDomains UsuarioPFDomains = UsuarioPfRepository.BuscarPorCpf(cpf);
            if (UsuarioPFDomains == null)
                return NotFound();
            return Ok(UsuarioPFDomains);
        }

        [HttpPost]
        public IActionResult Cadastrar(UsuarioPFDomains UsuarioPFDomains)
        {
            UsuarioPfRepository.Cadastrar(UsuarioPFDomains);
            return Ok();
        }

        [HttpPut("{cpf}")]
        public IActionResult Atualizar(int cpf, UsuarioPFDomains UsuarioPF)
        {
            UsuarioPF.NumeroCpf = cpf;
            UsuarioPfRepository.Alterar(UsuarioPF);
            return Ok();
        }

        [HttpDelete("{cpf}")]
        public IActionResult Deletar(int cpf)
        {
            UsuarioPfRepository.Deletar(cpf);
            return Ok();
        }

        [HttpGet("buscar/{nome}")]
        public IActionResult BuscarPorNome(string nome)
        {
            try
            {
                List<UsuarioPFDomains> UsuariosPF = UsuarioPfRepository.BuscarPorNome(nome);
                // caso nenhum funcionario seja encontrado com aquele nome, retorno sem conteudo
                if (UsuariosPF.Count == 0)
                    return NoContent();
                // caso contrario, apresento a lista de funcionarios com determinado nome
                return Ok(UsuariosPF);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "Ocorreu um erro." + ex.Message });
            }
        }

    }
}
