using Northwind.Repository;
using Northwind.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Northwind.DTO;
using Northwind.Entity;

namespace Northwind.WCF
{
    public class ServiceBase<Rep, Entity, DTO> : IService<DTO> 
        where DTO: class
        where Entity : class
        where Rep: RepositoryBase<Entity>
    {
        // Rep: RepositoryBase<Entity>: ServiceBase'in Rep hareket tipi, RepositoryBase tipinde olduğu belirtildiği için, ServiceBase sınıfını kullandığımız yerlerde Rep hareket tipi için ProductRepository, CategoryRepository, SupplierRepository yazılabilir. Cunku bu sınıflar RepositoryBase sınıfından türemiştir.

        //ServiceBase sınıfı RepositoryBase sınıfına talepleri gonderen, ve RepositoryBase sınıfından response'ları alan bir sınıftır. ServiceBase sınıfının hangi RepositoryBase sınıfı (ProductRepository mi?, CategoryRepository mi?...) ile iletişimde olduğunu bilmek gerekir. ayrıca ServiceBase sınıfı client'a DTO nesnesi yollamalı ve client'tan gelen DTO nesnesini RepositoryBase sınıfına gönderirken Entity'e dönüştürmesi gerekir. 

        //ServiceBase'in , hem RepositoryTipi, hem Entity Tipi, hem de DTO tipi argümanlarına ihtiyacı vardır.

        private Rep repository;

        public Rep Repository
        {
            get
            {
                //Generic tip için instance oluşturmak istediğimizde 
                //repository = new Rep()
                //gibi bir işlem yapamıyoruz. Generic tip için instance oluşturmada kullanılacak class'ın Adı Activator ve Method'un adı CreateInstance isimli Generic Method'dur.
                //repository = repository == null ? Activator.CreateInstance<Rep>():repository;
                //CreateInstance<Rep> Rep dışarıdan alınan tiptir ve instance bu tip için üretilecektir
                repository = repository ?? Activator.CreateInstance<Rep>();
                return repository;
            }
            set
            {
                repository = value;
            }
        }

        public bool Adding(DTO dto)
        {
            //throw new NotImplementedException();
            //Repository.Adding(dto);
            return Repository.Adding(dto.Changer<Entity>());
        }

        public bool Deleting(DTO dto)
        {
            //Repository.Deleting(dto);
            return Repository.Deleting(dto.Changer<Entity>());
        }

        public List<DTO> Listing()
        {
            //ServiceBase'inden RepositoryBase'e talep gonderilecektir.
            //throw new NotImplementedException();
            //return Repository.Listing();
            //Service katmanımız Repository'den Entity alır. Öncelikle alınan entity'lerin DTO nesnesie dönüştürülmesi gerekir
            //Bizim DTO-to-Entitiy ve Entitiy-to-DTO çevrimine ihtiyacımız vardır

            //d.Changer<Product>(): d.'nın anlamı, Changer method'una hangi kaynak(source) ustunden ulaştığınızı gösterir. Yani Changer'a ProductDTO üzerinden ulaşıyorsunuz. Başka bir deyişle ProductDTO nesnesini Product nesnesine dönüştürüyorsunuz
            //<Product>(): Changer methodunun dışarıdan istediği argüman tipini gösterir. Changer hangi tipe dönüştürecek ise o tip arguman tip olarak belirtilir.
            //Product prod = : Changer method'unun return tipini gösterir

            //Deneme kodları
            //ProductDTO d = new ProductDTO();
            //Product prod = d.Changer<Product
            //Product p = new Product();
            //ProductDTO pdt = p.Changer<ProductDTO>();
            //Product p = new Product();
            //p.ProductName = "MyAvokado";
            //p.UnitPrice = 19;
            //p.UnitsInStock = 199;
            //ProductDTO dt = p.Changer<ProductDTO>();

            return Repository.Listing().Select(x => x.Changer<DTO>()).ToList();
        }

        public bool Updating(DTO dto)
        {
            //throw new NotImplementedException();
            return Repository.Updating(dto.Changer<Entity>());
        }
    }
}