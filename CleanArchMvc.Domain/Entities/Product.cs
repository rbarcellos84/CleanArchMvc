﻿using CleanArchMvc.Domain.Validation;

namespace CleanArchMvc.Domain.Entities
{
    public class Product : Entity
    {
        //definições
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }
        public string Image { get; private set; }

        //propriedade de navegação
        public int CategoryId { get; private set; }
        public Category Category { get; private set; }

        //construtor
        public Product(string name, string description, decimal price, int stock, string image)
        {
            ValidateDomain(name, description, price, stock, image);
        }

        public Product(int id, string name, string description, decimal price, int stock, string image)
        {
            ValidateDomain(name, description, price, stock, image);
            DomainExceptionValidation.When(id < 0, "Invalid Id value.");
            Id = id;
        }

        //metodo
        public void Update(string name, string description, decimal price, int stock, string image, int categoryId)
        {
            ValidateDomain(name, description, price, stock, image);
            CategoryId = categoryId;
        }

        private void ValidateDomain(string name, string description, decimal price, int stock, string image)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Invalid name. Name is required.");
            DomainExceptionValidation.When(name.Length < 3, "Invalid name, too short, minimun 3 charecters.");

            DomainExceptionValidation.When(string.IsNullOrEmpty(description), "Invalid description. Description is required.");
            DomainExceptionValidation.When(description.Length < 3, "Invalid description, too short, minimun 3 charecters.");

            DomainExceptionValidation.When(price < 0, "Invalid price value.");
            DomainExceptionValidation.When(stock < 0, "Invalid stock value.");

            DomainExceptionValidation.When(string.IsNullOrEmpty(description), "Invalid name. Name is required.");
            DomainExceptionValidation.When(image?.Length > 250, "Invalid image name, too long, maximum 250 charecters.");

            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            Image = image;
        }
    }
}
