using Northwind.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Repository
{
    public class RepositoryBase<TT> : IRepository<TT> where TT:class
    {
        //RepositoryBase class'ında context'e  ihtiyacımız var. Bu yuzden Northwind.Repository projemize Northwind.Entity projesini Reference olarak eklememiz gerekir

        //Singleton Pattern mimarisi, uygulamanın tek context ya da tek connection üzerinden işlem yapmasının sağlandığı design pattern(tasarım deseni)dir.
        //Sık  bağlantı açılıp kapatılan uygulamalarda bu işlemler SQL server'a gereksiz yük bindirir. Bunun yerine hazırda context nesnesivar mı bakılır, eğer yoksa yeniden oluşturulu, varsa var olan kullanılır.

        private NorthwindEntities context;

        public NorthwindEntities Context
        {
            get
            {
                //if (context == null)
                //{
                //    context = new NorthwindEntities();
                //}
                return context ?? new NorthwindEntities();
            }
            set
            {
                context = value;
            }
        }

        public bool Adding(TT entity)
        {
            //Set<TT> : Context'in TT tipini algılamasını sağlar
            Context.Set<TT>().Add(entity);
            try
            {
                Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Deleting(TT entity)
        {
            Context.Set<TT>().Remove(entity);
            try
            {
                Context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<TT> Listing()
        {
            //Product entity'si gelirse Product'lar listelenecek, Category Entity'si gelirse Category'ler listelenecek
            return Context.Set<TT>().ToList();
        }

        public bool Updating(TT entity)
        {
            try
            {
                Context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
