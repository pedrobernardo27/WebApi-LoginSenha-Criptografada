using WebApiLoginRepository.Model;

namespace WebApiLoginRepository.Interface
{
    public interface ILoginPessoaRepository
    {
        ValueTask<Pessoa> BuscarPorSenha(Login login);
        ValueTask<Pessoa> VerificarLogin(Pessoa login);
        ValueTask<Pessoa> Inserir(Pessoa login);
        ValueTask<Pessoa> BuscarSenhaPorNome(string nome);
    }
}
