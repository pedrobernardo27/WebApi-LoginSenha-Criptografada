using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using WebApiLoginRepository.Model;
using WebApiLoginService.Interface;

namespace WebApiLogin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class LoginPessoaController : Controller
    {
        private readonly ILoginPessoaService _loginPessoaService;

        public LoginPessoaController(ILoginPessoaService loginPessoaService)
        {
            _loginPessoaService = loginPessoaService;
        }

        [HttpPost("CriarLogin")]
        public async ValueTask<IActionResult> CriarLogin([FromBody] PessoaRequest login)
        {
            try
            {
                if (String.IsNullOrEmpty(login.Nome))
                    return BadRequest();
                if (String.IsNullOrEmpty(login.Sobrenome))
                    return BadRequest();
                if (login.Idade <= 0)
                    return BadRequest();
                if (String.IsNullOrEmpty(login.Cpf))
                    return BadRequest();
                if (String.IsNullOrEmpty(login.Passoword))
                    return BadRequest();
                if (String.IsNullOrEmpty(login.Username))
                    return BadRequest();

                var result = await _loginPessoaService.InserirLogin(login);
                return result != null ? Ok(result) : BadRequest();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao efetuar a pesquisa. {ex.Message}");
            }
        }

        [HttpPost("AcessoLogin")]
        public async ValueTask<IActionResult> AcessarLogin([FromBody] LoginRequest login)
        {
            try
            {
                if (String.IsNullOrEmpty(login.Passoword))
                    return BadRequest();
                if (String.IsNullOrEmpty(login.Username))
                    return BadRequest();

                var result = await _loginPessoaService.BuscarLogin(login);
                return result != null ? Ok(result) : BadRequest();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao efetuar a pesquisa. {ex.Message}");
            }
        }

        [HttpPost("RecuperarSenha")]
        public async ValueTask<IActionResult> RecuperarSenha([FromBody] recupaSenhaRequest recupaSenha)
        {
            try
            {
                if (String.IsNullOrEmpty(recupaSenha.Nome))
                    return BadRequest();

                var result = await _loginPessoaService.BuscarSenha(recupaSenha);
                return result != null ? Ok(result) : BadRequest();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao efetuar a pesquisa. {ex.Message}");
            }
        }
    }
}
