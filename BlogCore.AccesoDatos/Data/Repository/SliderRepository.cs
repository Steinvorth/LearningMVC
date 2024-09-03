using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Models;
using Microsoft.EntityFrameworkCore;
using ProjectoCursoWeb_BlogCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.AccesoDatos.Data.Repository
{
    public class SliderRepository : Repository<Slider>, ISliderRepository
    {
        private readonly ApplicationDbContext _db;
        public SliderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Slider slider)
        {
            var dbObj = _db.Slider.FirstOrDefault(s => s.Id == slider.Id);
            dbObj.Name = slider.Name;
            dbObj.State = slider.State;
            dbObj.ImageURL = slider.ImageURL;

            //_db.SaveChanges();
        }
    }
}
