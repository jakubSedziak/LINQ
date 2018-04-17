using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zad_3_LINQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zad_3_LINQ.Tests
{
    [TestClass()]
    public class NarzedziowaTests
    {
        [TestMethod()]
        public void GetProductsByNameTest()
        {   
            Assert.AreEqual(Narzedziowa.GetProductsByName("Blade").Count(), 1);
        }

        [TestMethod()]
        public void GetProductsByVendorNameTest()
        {
            Assert.AreEqual(Narzedziowa.GetProductsByVendorName("Ready Rentals").Count(), 16);
        }

        [TestMethod()]
        public void GetProductNamesByVendorNameTest()
        {
            Assert.AreEqual(Narzedziowa.GetProductNamesByVendorName("Ready Rentals").Count(), 16);
        }

        [TestMethod()]
        public void GetProductVendorByProductNameTest()
        {
            Assert.AreEqual(Narzedziowa.GetProductVendorByProductName("Bearing Ball"), " Wood Fitness");
        }

        [TestMethod()]
        public void GetProductsWithNRecentReviewsTest()
        {
            Assert.AreEqual(Narzedziowa.GetProductsWithNRecentReviews(2).Count(), 1);
        }

        [TestMethod()]
        public void GetNRecentlyReviewedProductsTest()
        {
            Assert.AreEqual(Narzedziowa.GetNRecentlyReviewedProducts(2).Count(), 2);
        }

        [TestMethod()]
        public void GetNProductsFromCategoryTest()
        {
            Assert.AreEqual(Narzedziowa.GetNProductsFromCategory("Bikes", 2).Count(), 2);
        }

        [TestMethod()]
        public void GetTotalStandardCostByCategoryTest()
        {
            DataClasses1DataContext bata = new DataClasses1DataContext();
            var kategorie = from b in bata.ProductCategories
                            select b;
            ProductCategory cat = kategorie.First();
            Assert.AreEqual(Narzedziowa.GetTotalStandardCostByCategory(cat), 92040);
        }
    }
}