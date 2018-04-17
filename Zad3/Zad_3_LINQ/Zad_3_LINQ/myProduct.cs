using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zad_3_LINQ
{
     class MyProduct 
    {
        private static DataClasses1DataContext bata = new DataClasses1DataContext();
        public int ProductID { get; set; }

        public string Name { get; set; }

        public string ProductNumber { get; set; }


        public MyProduct()
        {
            
        }
        public  static List<MyProduct> GetProductsByVendorNameMYPROD( List<MyProduct> produkty,string vendorName)
        {

            var ProductByVendor = from produkt in produkty
                                  join provendor in bata.ProductVendors
                                  on produkt.ProductID equals provendor.ProductID
                                  join vendors in bata.Vendors
                                  on provendor.BusinessEntityID equals vendors.BusinessEntityID
                                  where vendors.Name.Equals(vendorName)
                                  select produkt;
            return ProductByVendor.ToList<MyProduct>();
        }
        public static List<MyProduct> GetProductsWithNRecentReviewsMYPROD(List<MyProduct> produkty, int howManyReviews)
        {
            
            var reviews = bata.ProductReviews
                .GroupBy(p => new { p.ProductID })
                .Select(g => new { g.Key.ProductID, ile = g.Count() });

            var RecentOpinionProducts = (from produkt in produkty
                                         join reviev in reviews
                                         on produkt.ProductID equals reviev.ProductID
                                         where reviev.ile.Equals(howManyReviews)
                                         select produkt);
            return RecentOpinionProducts.ToList<MyProduct>();
        }
        public static List<MyProduct> GetNRecentlyReviewedProductsMYPROD(List<MyProduct> produkty, int howManyProducts)
        {


            var RecentlyRevievedProducts = (from produkt in produkty
                                            join reviev in bata.ProductReviews
                                            on produkt.ProductID equals reviev.ProductID
                                            orderby reviev.ReviewDate
                                            select produkt).Take(howManyProducts);

            return RecentlyRevievedProducts.ToList<MyProduct>();
        }

    }

}
