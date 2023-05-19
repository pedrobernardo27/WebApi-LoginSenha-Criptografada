using AutoMapper;
using WebApiLoginService.Utilit;
using WebApiLoginRepository.Model;
using WebApiLoginService.Interface;
using WebApiLoginRepository.Interface;
using Microsoft.Extensions.Configuration;

namespace WebApiLoginService.Service
{
    public class LoginPessoaService : ILoginPessoaService
    {
        private readonly IMapper _mapper;
        private IConfiguration _configuration;
        public readonly ILoginPessoaRepository _loginPessoaRepository;

        public LoginPessoaService(ILoginPessoaRepository loginPessoaRepository, IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;
            _configuration = configuration;
            _loginPessoaRepository = loginPessoaRepository;
        }

        public async ValueTask<string> InserirLogin(PessoaRequest loginRequest)
        {
            try
            {
                var mensagem = default(string);
                var encrypt = new EncryptDecrypt();
                loginRequest.Passoword = encrypt.Encrypt(loginRequest.Passoword);
                var novoLogin = _mapper.Map<Pessoa>(loginRequest);
                var verificarLogin = await _loginPessoaRepository.VerificarLogin(novoLogin);

                if(verificarLogin == null)
                {
                    var resultLogin = await _loginPessoaRepository.Inserir(novoLogin);

                    if (resultLogin != null)
                    {
                        mensagem = _configuration["RetornoCadastro:CadastradoComSucesso"];
                    }

                    else
                    {
                        mensagem = _configuration["RetornoCadastro:LoginJaExistente"];
                    }
                }               

                return mensagem;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao afetuar método InserirLogin. {ex.Message}");
            }
        }

        public async ValueTask<string> BuscarLogin(LoginRequest loginResquest)
        {
            try
            {
                var mensagem = "Usuário ou senha inválido.";
                var login = new Login();
                var encrypt = new EncryptDecrypt();

                login.Username = loginResquest.Username;
                login.Passoword = encrypt.Encrypt(loginResquest.Passoword);

                var resultLogin = await _loginPessoaRepository.BuscarPorSenha(login);

                if (resultLogin != null)
                {
                    mensagem = "Acesso Liberado.";
                }

                return mensagem;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao afetuar método BuscarLogin. {ex.Message}");
            }
        }

        public async ValueTask<string> BuscarSenha(recupaSenhaRequest recupaSenhaRequest)
        {
            try
            {
                var encrypt = new EncryptDecrypt();
                var senhaDescriptografada = default(string);

                var resultLogin = await _loginPessoaRepository.BuscarSenhaPorNome(recupaSenhaRequest.Nome);

                if (resultLogin != null)
                {
                    senhaDescriptografada = encrypt.Decrypt(resultLogin.Passoword);
                }

                else
                {
                    senhaDescriptografada = _configuration["RetornoCadastro:UsuarioNaoCadastrado"];
                }

                return senhaDescriptografada;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao afetuar método BuscarSenha. {ex.Message}");
            }
        }
    }
}
