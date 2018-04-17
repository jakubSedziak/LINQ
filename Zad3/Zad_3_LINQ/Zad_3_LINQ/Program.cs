using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zad_3_LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            DataClasses1DataContext dat1 = new DataClasses1DataContext();
           int zmienna = Narzedziowa.getNoCategoryDeclar(dat1.Products.ToList<Product>()).Count();
            //zmienna = dat1.Products.Count();
            Console.WriteLine(zmienna);
            Console.WriteLine(dat1.Products.ToList<Product>().toString());

            List<MyProduct> zad5 = new List<MyProduct>();
            foreach(Product prod in dat1.Products)
            {
                MyProduct temp = new MyProduct();
                temp.ProductID = prod.ProductID;
                temp.Name = prod.Name;
                temp.ProductNumber = prod.ProductNumber;
                zad5.Add(temp);
            }
            MyProduct.GetNRecentlyReviewedProductsMYPROD(zad5, 2);
            Console.WriteLine();
        }
    }
}
