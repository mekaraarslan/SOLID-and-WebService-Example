using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Extensions
{
    //Extension method'lar ve bu methodların bulundugu class'lar "static" olmalıdır
    public static class Extension
    {
        //C#'ta bulunan Object class'ının içine yeni bir method ekleyeceğiz.

        //(this Object source): Object tipinin içerisine bu method (Changer) eklenecektir.Bu yazım tarzı ile, halihazırda bulunan bir sınıfın içerisine dışarıdan bir method eklemiş olacağız. Bundan sonra projenin içerisine herhangi bir sınıf eklendiğinde, bu method o sınıfın içinde otomatik olarak varmış gibi olacak (inheritance'dan dolayı). Kısacası bu gösterim extension method gösterimidir

        //T Changer<T>(this Object source): Source elemanı, hangi instance üzerinden . yazarak method'a ulaşıyorsak, o instance'ı temsil eder. Biz changer metod'u ile source elemanını T tipine dönüştüreceğiz. Ve geriye T tipinde eleman döndüreceğiz.

        //Product nesnesinin içindeki property'leri ProductDTO içerisine koyacağız, ya da ProductDTO içerisindeki property'leri Product nesnesinin içerisine koyacağız.

        /// <summary>
        /// Product nesnesinin içindeki property'leri ProductDTO içerisine koyacağız, ya da ProductDTO içerisindeki property'leri Product nesnesinin içerisine koyacağız.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T Changer<T>(this Object source)
        {
            // T tipinde instance oluştur ve oluşan instace'ı yine T tipinde bir değişkene ata
            T target = Activator.CreateInstance<T>();

            Type targetType = target.GetType();
            Type sourceType = source.GetType();

            PropertyInfo[] sourceProperties = sourceType.GetProperties();
            PropertyInfo[] targetProperties = targetType.GetProperties();

            foreach (PropertyInfo pInf in sourceProperties)
            {
                object value = pInf.GetValue(source);

                PropertyInfo targetpInf = targetProperties.FirstOrDefault(x => x.Name == pInf.Name);
                if (targetpInf != null)
                    targetpInf.SetValue(target, value);

                //Abdurrahman'ın önerisi
                //pInf.SetValue(target, pInf.GetValue(source));
            }

            return target;
        }
    }
}
