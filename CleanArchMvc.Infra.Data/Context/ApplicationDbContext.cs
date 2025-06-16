using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Infra.Data.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CleanArchMvc.Infra.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //para executar o comando migration e criar toda a base do projeto é so abrir o gerenciador de pacotes e digitar o comando add-migration inicial, selecionado o projeto como padrão Infra.data
        //apos a crição do arquivo migration, execute o comando update-database para criar as tabelas
        //comando para popular a tabela add-migration SeedProducts, será criado uma classe na pasta migrations contendo os metodos Up e o Down para ser implementado com comando sqls e para executar basta digitar update-database
        //obs update-database sempre ira executar o ultimo arquivo gerado pelo migrations

        //o caminho da connectionstring do banco de dados
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
