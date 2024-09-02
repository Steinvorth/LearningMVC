using BlogCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BlogCore.AccesoDatos.Data.Repository.IRepository
{
    public interface I_ItemRepository : IRepository<Category>
    {
        void Update(Item item);
    }
}
