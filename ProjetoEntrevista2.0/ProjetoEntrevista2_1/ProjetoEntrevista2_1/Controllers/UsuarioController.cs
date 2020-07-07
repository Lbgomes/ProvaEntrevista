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
    public class UsuarioController : ControllerBase
    {
        UsuarioRepository UsuarioRepository = new UsuarioRepository();

        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(UsuarioRepository.Listar());
        }

        [HttpGet("{documento}")]
        public IActionResult BuscarPorDocumento(int documento)
        {
            UsuarioDomain UsuarioDomain = UsuarioRepository.BuscarPorDocumento(documento);
            if (UsuarioDomain == null)
                return NotFound();
            return Ok(UsuarioDomain);
        }

        [HttpPost]
        public IActionResult Cadastrar(UsuarioDomain UsuarioDomain)
        {
            UsuarioRepository.Cadastrar(UsuarioDomain);
            return Ok();
        }

        [HttpPut("{documento}")]
        public IActionResult Atualizar(int documento, UsuarioDomain Usuario)
        {
            Usuario.NumeroDocumento = documento;
            UsuarioRepository.Alterar(Usuario);
            return Ok();
        }

        [HttpDelete("{documento}")]
        public IActionResult Deletar(int documento)
        {
            UsuarioRepository.Deletar(documento);
            return Ok();
        }

        [HttpGet("buscar/{nome}")]
        public IActionResult BuscarPorNome(string nome)
        {
            try
            {
                List<UsuarioDomain> Usuarios = UsuarioRepository.BuscarPorNome(nome);
                // caso nenhum funcionario seja encontrado com aquele nome, retorno sem conteudo
                if (Usuarios.Count == 0)
                    return NoContent();
                // caso contrario, apresento a lista de funcionarios com determinado nome
                return Ok(Usuarios);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "Ocorreu um erro." + ex.Message });
            }
        }

    }
}
