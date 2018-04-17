using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zad_3_LINQ
{
    public class dostawca_produkt
    {
       public string produkt { get; set; }
      public  string dostawca { get; set; }
        public dostawca_produkt()
        {
            produkt = "";
            dostawca = "";
        }
    }
    public static class Narzedziowa 
    {
        static DataClasses1DataContext bata = new DataClasses1DataContext();
        public static List<Product> GetProductsByName(string namePart) {
            
                var ProductByName = from nazwa in bata.Products
                                    where nazwa.Name.Contains(namePart)
                                    select nazwa;
            return ProductByName.ToList<Product>();
        }
        public static List<Product> GetProductsByVendorName(string vendorName) {
            var ProductByVendor = from produkt in bata.Products
                                  join provendor in bata.ProductVendors
                                  on produkt.ProductID equals provendor.ProductID
                                  join vendors in bata.Vendors
                                  on provendor.BusinessEntityID equals vendors.BusinessEntityID
                                  where vendors.Name.Equals(vendorName)
                                  select produkt;
            return ProductByVendor.ToList<Product>();
        }
        public static List<string> GetProductNamesByVendorName(string vendorName) {
            var ProductByVendor = from produkt in bata.Products
                                  join provendor in bata.ProductVendors
                                  on produkt.ProductID equals provendor.ProductID
                                  join vendors in bata.Vendors
                                  on provendor.BusinessEntityID equals vendors.BusinessEntityID
                                  where vendors.Name.Equals(vendorName)
                                  select produkt.Name;
            return ProductByVendor.ToList<string>();
        }
        public static string GetProductVendorByProductName(string productName) {
            var VendorByProduct = from produkt in bata.Products
                                  join provendor in bata.ProductVendors
                                  on produkt.ProductID equals provendor.ProductID
                                  join vendors in bata.Vendors
                                  on provendor.BusinessEntityID equals vendors.BusinessEntityID
                                  where produkt.Name.Equals(productName)
                                  select vendors.Name;
            string dostawcy = "";
            foreach (string d in VendorByProduct)
                dostawcy += " " + d;
            return dostawcy;
        }
        public static List<Product> GetProductsWithNRecentReviews(int howManyReviews) {
            var reviews = bata.ProductReviews
                .GroupBy(p => new { p.ProductID })
                .Select(g => new { g.Key.ProductID, ile = g.Count() });

            var RecentOpinionProducts = (from produkt in bata.Products
                                         join reviev in reviews
                                         on produkt.ProductID equals reviev.ProductID
                                         where reviev.ile.Equals(howManyReviews)
                                         select produkt);
            return RecentOpinionProducts.ToList<Product>();
        }
        public static List<Product> GetNRecentlyReviewedProducts(int howManyProducts) {
            

            var RecentlyRevievedProducts = (from produkt in bata.Products
                                         join reviev in bata.ProductReviews
                                         on produkt.ProductID equals reviev.ProductID
                                         orderby reviev.ReviewDate
                                         select produkt).Take(howManyProducts);

            return RecentlyRevievedProducts.ToList<Product>();
        }
        public static List<Product> GetNProductsFromCategory(string categoryName, int n) {
            var ProductsFromCategory = (from produkt in bata.Products
                                       join prodsub in bata.ProductSubcategories
                                       on produkt.ProductSubcategoryID equals prodsub.ProductSubcategoryID
                                       join category in bata.ProductCategories
                                       on prodsub.ProductCategoryID equals category.ProductCategoryID
                                       where category.Name.Equals(categoryName)
                                       orderby category.Name,produkt.Name                   //polecenie na wikampie jest bez sensu bo przeciez 1 kategorii nie posortuje :D ale ja tu tylko koduje :D
                                       select produkt).Take(n);
            return ProductsFromCategory.ToList<Product>();
        }
        public static int GetTotalStandardCostByCategory(ProductCategory category) {

            var StandardCostByCategory = from produkt in bata.Products
                                         join prodsub in bata.ProductSubcategories
                                         on produkt.ProductSubcategoryID equals prodsub.ProductSubcategoryID
                                         join cat in bata.ProductCategories
                                         on prodsub.ProductCategoryID equals cat.ProductCategoryID
                                         where cat.Equals(category)
                                         select produkt.StandardCost;
            int overallcost = 0;
            foreach (int i in StandardCostByCategory)
                overallcost += i;                                           
 //           overallcost /= StandardCostByCategory.Count();
            return overallcost;
        }
        public static List<Product> getNoCategoryDeclar (this List<Product> pro)    //nie wiem skad brac te nazwy rozszerzen :< kom2 : mozliwe ze zamiast List<Product> trzeba dac to datacontext1
        {
            var WithCategory = from prod in bata.Products
                             join prodsub in bata.ProductSubcategories
                             on prod.ProductSubcategoryID equals prodsub.ProductSubcategoryID
                             join cat in bata.ProductCategories
                             on prodsub.ProductCategoryID equals cat.ProductCategoryID
                             select prod;

            var NoCategory = (from prod in bata.Products
                              select prod).Except(WithCategory);    //mozna tez zrobic where !(to co wyzej).contains(prod.ProductID);
            return NoCategory.ToList<Product>();
        }
        public static List<Product> ProduktyJakWSklepie(this List<Product> pro,int rozmiar, int numerStrony)
        {
            List<Product> temp = new List<Product>();
           
            int zacznij = rozmiar * numerStrony;
            int skoncz = zacznij + rozmiar;
            if (zacznij > pro.Count())
            {
                int ile = pro.Count() / rozmiar;
                zacznij = ile*rozmiar;    //
                skoncz = zacznij + (pro.Count() % rozmiar);
            }
            if (skoncz >pro.Count())
            {
                skoncz = pro.Count();
            }
            for(int i = zacznij; i<skoncz ;i++)
            {
                temp.Add(pro[i]);
            }
            return temp;
           
        }
        public static string toString(this List<Product> cos)
        {
            string s_out = "";
            var VendorByProduct = (from produkt in bata.Products
                                   join provendor in bata.ProductVendors
                                   on produkt.ProductID equals provendor.ProductID
                                   join vendors in bata.Vendors
                                   on provendor.BusinessEntityID equals vendors.BusinessEntityID
                                   select new dostawca_produkt
                                   {
                                       produkt = produkt.Name,
                                       dostawca = vendors.Name
                                   });
            foreach(dostawca_produkt dp in VendorByProduct)
            {
                s_out += dp.produkt + " - ";
                s_out += dp.dostawca + "\n\r";
            }
            return s_out;
        }
    }
}
