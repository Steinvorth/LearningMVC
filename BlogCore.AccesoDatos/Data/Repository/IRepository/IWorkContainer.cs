using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.AccesoDatos.Data.Repository.IRepository
{
    public interface IWorkContainer : IDisposable //interface that indicates that a class is disposable
    {
        //Here go the repositories
        ICategoryRepository CategoryRepo { get; }
        I_ItemRepository ItemRepo { get; }
        void save(); //method to save changes
    }
}
