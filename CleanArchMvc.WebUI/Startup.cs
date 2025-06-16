using CleanArchMvc.Domain.Account;
using CleanArchMvc.Infra.IoC;

namespace CleanArchMvc.WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Este método é chamado pelo runtime. Use este método para adicionar serviços ao contêiner.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(); // Adiciona suporte a controladores e visualizações MVC
            services.AddInfrastructure(Configuration); // Registra os serviços de infraestrutura, incluindo repositórios e serviços de autenticação
        }

        // Este método é chamado pelo runtime. Use este método para configurar o pipeline de requisição HTTP.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ISeedUserRoleInitial seedUserRoleInitial)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); // Exibe a página de erro detalhada durante o desenvolvimento
            }
            else
            {
                app.UseExceptionHandler("/Home/Error"); // Redireciona para a página de erro genérica em produção
                app.UseHsts(); // Habilita o HTTP Strict Transport Security (HSTS) para aumentar a segurança
            }

            app.UseHttpsRedirection(); // Redireciona requisições HTTP para HTTPS
            app.UseStaticFiles(); // Permite servir arquivos estáticos (CSS, JS, imagens, etc.)

            app.UseRouting();

            seedUserRoleInitial.SeedRoles(); // Inicializa os papéis de usuário e administrador
            seedUserRoleInitial.SeedUsers(); // Inicializa os usuários com os papéis definidos

            app.UseAuthentication(); // Adiciona autenticação ao pipeline de requisição
            app.UseAuthorization();

            //definição do roteamento padrão quasndo a aplicação for iniciada
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"); // Define a rota padrão para o controlador Home e a ação Index
            });
        }
    }
}