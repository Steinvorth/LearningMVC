using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Models;
using ProjectoCursoWeb_BlogCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BlogCore.AccesoDatos.Data.Repository
{
    public class ItemRepository : Repository<Item>, I_ItemRepository
    {
        private readonly ApplicationDbContext _db;

        public ItemRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Item item)
        {
            var dbObj = _db.Item.FirstOrDefault(s => s.Id == item.Id);
            dbObj.Name = item.Name;
            dbObj.Description = item.Description;
            dbObj.CategoryId = item.CategoryId;
            dbObj.ImageURL = item.ImageURL;

            //_db.SaveChanges();
        }
    }
}
