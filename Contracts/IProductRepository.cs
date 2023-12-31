﻿using NSTask.Models;
namespace NSTask.Contracts
   

{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Task<Product> Add(Product product);
        Task<Product> Update(Product product);
        Task<Product> Find(int id);
        Task<Product> Remove(int id);
        Task<bool> IsExists(int id);
    }

}
