using WebApiLoginRepository.Model;
using Microsoft.EntityFrameworkCore;
using WebApiLoginRepository.Context;
using WebApiLoginRepository.Interface;

namespace WebApiLoginRepository.Repository
{
    public class LoginPessoaRepository : ILoginPessoaRepository
    {
        public async ValueTask<Pessoa> Inserir(Pessoa pessoa)
        {
            try
            {
                using (var db = new EfExercicioModelContext())
                {
                    db.Pessoa.Add(pessoa);
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro{ex.Message}");
            }

            return pessoa;
        }

        public async ValueTask<Pessoa> VerificarLogin(Pessoa login)
        {
            var pessoaVerificada = new Pessoa();

            using (var db = new EfExercicioModelContext())
            {
                pessoaVerificada = await db.Pessoa.FirstOrDefaultAsync(x => x.Username == login.Username);
            }           

            return pessoaVerificada;
        }

        public async ValueTask<Pessoa> BuscarPorSenha(Login login)
        {
            var pessoa = new Pessoa();

            using (var db = new EfExercicioModelContext())
            {
                pessoa = await db.Pessoa.FirstOrDefaultAsync(x => x.Passoword == login.Passoword &&
                x.Username == login.Username);
            }

            return pessoa;
        }

        public async ValueTask<Pessoa> BuscarSenhaPorNome(string nome)
        {
            try
            {
                var pessoa = new Pessoa();

                using (var db = new EfExercicioModelContext())
                {
                    pessoa = await db.Pessoa.FirstOrDefaultAsync(x => x.Nome == nome);
                }

                return pessoa;
            }
            catch (Exception ex)
            {

                throw new Exception($"erro{ex.Message}");
            }
            
        }
    }
}
