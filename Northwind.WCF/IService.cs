using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace Northwind.WCF
{
    [ServiceContract]
    public interface IService<DTO> where DTO:class
    {
        //Service katmanı client'tan talebi alıp, Repository katmanına iletir
        //IService interface'i ve içerisinde tanımlı method'lar client ile iletişime geçeceği için Contract(sözleşme) içine dahil eidlmesi 
        //Bu katmanın olmasının sebebi, Client'ların direkt olarak Entity ve Facade'lara ulaşmaması içindir
        //Service katmanına Client tarafından gelen nesneler DTO nesneleridir (Entity nesneleri gelmez)

       //DTO katmanı Entity'lerin aynısı olacaktır, sadece DTO içerisinde serilize edilebilir nesneler barındırılır
       //Client ile Service arasında gidip/gelen nesnelerin serilize edilebilir olması gerekir

        [OperationContract]
        List<DTO> Listing();
        [OperationContract]
        bool Adding(DTO dto);
        [OperationContract]
        bool Updating(DTO dto);
        [OperationContract]
        bool Deleting(DTO dto);
    }
}