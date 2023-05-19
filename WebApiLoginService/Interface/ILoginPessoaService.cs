using WebApiLoginRepository.Model;

namespace WebApiLoginService.Interface
{
    public interface ILoginPessoaService
    {
        ValueTask<string> BuscarLogin(LoginRequest login);
        ValueTask<string> InserirLogin(PessoaRequest login);
        ValueTask<string> BuscarSenha(recupaSenhaRequest recupaSenha);
    }
}
