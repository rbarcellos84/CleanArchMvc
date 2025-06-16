using CleanArchMvc.Domain.Validation;

namespace CleanArchMvc.Domain.Entities
{
    //sealed garante que a classe não pode ser herdada
    public sealed class Category : Entity
    {
        //definições
        public string Name { get; private set; }

        //propriedade de navegação
        public ICollection<Product> Products { get; set; }

        //construtores
        public Category(int id, string name)
        {
            DomainExceptionValidation.When(id <= 0, "Invalid id.");
            ValidateDomain(name);

            Id = id;
            Name = name;
        }
        public Category(string name)
        {
            ValidateDomain(name);
        }

        //metodos
        public void Update(string name)
        {
            ValidateDomain(name);
            Name = name;
        }

        private void ValidateDomain(string name)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Invalid name. Name is required.");
            DomainExceptionValidation.When(name.Length < 3, "Invalid name, too short, minimun 3 charecters.");

            Name = name;
        }
    }
}
