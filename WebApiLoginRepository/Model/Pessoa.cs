using System.ComponentModel.DataAnnotations;

namespace WebApiLoginRepository.Model
{
    public class Pessoa
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public int Idade { get; set; }
        public string Cpf { get; set; }
        public string Passoword { get; set; }
        public string Username { get; set; }
    }
}
