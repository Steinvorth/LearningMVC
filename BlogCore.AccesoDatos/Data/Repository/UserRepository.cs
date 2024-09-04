using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Models;
using ProjectoCursoWeb_BlogCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.AccesoDatos.Data.Repository
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        private readonly ApplicationDbContext _db;
        public UserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void BlockUser(string id)
        {
            var dbUser = _db.ApplicationUser.FirstOrDefault(u => u.Id == id);
            dbUser.LockoutEnd = DateTime.Now.AddYears(1000);
            _db.SaveChanges();
        }

        public void UnBlockUser(string id)
        {
            var dbUser = _db.ApplicationUser.FirstOrDefault(u => u.Id == id);
            dbUser.LockoutEnd = DateTime.Now;
            _db.SaveChanges();
        }
    }
}
