using BlogCore.AccesoDatos.Data.Repository.IRepository;
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
        }

        //Here go the repositories
        public ICategoryRepository CategoryRepo { get; private set; }

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
