using WebApiLoginService.Service;
using WebApiLoginService.Interface;
using WebApiLoginRepository.Interface;
using WebApiLoginRepository.Repository;

namespace WebApiLogin.DependencyInjections
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddDatabaseExtensions(this IServiceCollection services)
        {
            services.AddScoped<ILoginPessoaService, LoginPessoaService>();

            services.AddTransient<ILoginPessoaRepository, LoginPessoaRepository>();

            return services;
        }
    }
}
