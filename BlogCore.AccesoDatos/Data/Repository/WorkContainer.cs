﻿using BlogCore.AccesoDatos.Data.Repository.IRepository;
using ProjectoCursoWeb_BlogCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.AccesoDatos.Data.Repository
{
    public class WorkContainer : IWorkContainer
    {
        private readonly ApplicationDbContext _db;

        public WorkContainer(ApplicationDbContext db)
        {
            _db = db;
            CategoryRepo = new CategoryRepository(_db);
            ItemRepo = new ItemRepository(_db);
            SliderRepo = new SliderRepository(_db);
            UserRepo = new UserRepository(_db);
        }

        //Here go the repositories
        public ICategoryRepository CategoryRepo { get; private set; }
        public I_ItemRepository ItemRepo { get; private set; }
        public ISliderRepository SliderRepo { get; private set; }
        public IUserRepository UserRepo { get; private set; }

        //method to dispose
        public void Dispose()
        {
            _db.Dispose();
        }

        //method to save changes
        public void save()
        {
            _db.SaveChanges();
        }
    }
}
