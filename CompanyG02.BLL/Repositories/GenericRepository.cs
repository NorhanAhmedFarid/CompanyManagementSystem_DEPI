﻿using CompanyG02.BLL.Interfaces;
using CompanyG02.DAL.Contexts;
using CompanyG02.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyG02.BLL.Repositories
{
    public class GenericRepository<T>:IGenericRepository<T> where T : class
    {
        private readonly CompanyDbContext dbContext;

        public GenericRepository(CompanyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task Add(T item)
         =>  await dbContext.Set<T>().AddAsync(item);
        

        public void Delete(T item)
          =>  dbContext.Set<T>().Remove(item);
        

        public async Task<T> Get(int id)
       =>await dbContext.Set<T>().FindAsync(id);

        public async Task<IEnumerable<T>> GetAll()
        {
            if (typeof(T) == typeof(Employee))
            {

              return (IEnumerable<T>) await dbContext.Employees.Include(e=>e.Department).ToListAsync();
            }
            else
            {
                return await dbContext.Set<T>().ToListAsync();
            }
        }


        public void Update(T employee)
          =>  dbContext.Set<T>().Update(employee);
    }
}
