using Northwind.DTO;
using Northwind.Entity;
using Northwind.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Northwind.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ProductService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ProductService.svc or ProductService.svc.cs at the Solution Explorer and start debugging.
    public class ProductService : ServiceBase<ProductRepository, Product, ProductDTO > , IProductService
    {
        //ProductService isimli bir Servis oluşturunca, arka planda IProductService isimli bir interface oluştur. ProductService servisimiz IProductService isimli interface'den türetirsek yani hiyerarşiyi olduğu gibi bırakırsak, IService ve ServiseBase içindeki method'lar kullanılamayacak. Oysaki biz IService ve ServiceBase içindeki method'ların (Listing, Deleting, Updating, Adding) tüm Service'ler (Product, Category, Supplier için yazılacak) tekrar tekrar yazılmaması için oluşturmuştuk. 

    }
}
