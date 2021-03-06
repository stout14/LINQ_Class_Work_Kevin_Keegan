﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Demo_LINQ_ClassOfProducts
{

    //
    // demo adapted from MSDN demo
    // https://code.msdn.microsoft.com/SQL-Ordering-Operators-050af19e/sourcecode?fileId=23914&pathId=1978010539
    //
    class Program
    {
        static void Main(string[] args)
        {

            Console.WindowWidth = 150;
            
            //
            // write all data files to Data folder
            //
            GenerateDataFiles.InitializeXmlFile();

            List<Product> productList = ReadAllProductsFromXml();

            OrderByCatagory(productList);

            OrderByCatagoryAnoymous(productList);

            OrderByUnits(productList);

            OrderByPrice(productList);

            FindExpensive(productList);

            OrderByTotalValue(productList);

            OrderByName(productList);

            AveragePricePerCategory(productList);

            FullList(productList);

        }

        //
        // Write the following methods
        //

        // OrderByUnits(): List the names and units of all products with less than 10 units in stock. Order by units.
        private static void OrderByUnits(List<Product> products)
        {
            string TAB = "   ";

            Console.Clear();
            Console.WriteLine(TAB + "List all beverages with less than 10 units and sort by the unit price.");
            Console.WriteLine();

            //
            // lambda syntax
            //
            var sortedProducts = products.Where(p => p.UnitsInStock <= 10).OrderByDescending(p => p.UnitsInStock);

            Console.WriteLine(TAB + "Category".PadRight(15) + "Product Name".PadRight(35) + "Unit Price".PadRight(20) + "Number Units".PadLeft(3));
            Console.WriteLine(TAB + "--------".PadRight(15) + "------------".PadRight(35) + "----------".PadRight(20) + "----------".PadLeft(3));

            foreach (Product product in sortedProducts)
            {
                Console.WriteLine(TAB + product.Category.PadRight(15) + product.ProductName.PadRight(35) + product.UnitPrice.ToString("C2").PadRight(20) + product.UnitsInStock.ToString().PadLeft(3));
            }

            Console.WriteLine();
            Console.WriteLine(TAB + "Press any key to continue.");
            Console.ReadKey();
        }


        // OrderByPrice(): List all products with a unit price less than $10. Order by price.
        private static void OrderByPrice(List<Product> products)
        {
            string TAB = "   ";

            Console.Clear();
            Console.WriteLine(TAB + "List all beverages less than $10 and sort by the unit price.");
            Console.WriteLine();

            //
            // lambda syntax
            //
            var sortedProducts = products.Where(p => p.UnitPrice <= 10).OrderByDescending(p => p.UnitPrice);

            Console.WriteLine(TAB + "Category".PadRight(15) + "Product Name".PadRight(35) + "Unit Price".PadLeft(10));
            Console.WriteLine(TAB + "--------".PadRight(15) + "------------".PadRight(35) + "----------".PadLeft(10));

            foreach (Product product in sortedProducts)
            {
                Console.WriteLine(TAB + product.Category.PadRight(15) + product.ProductName.PadRight(35) + product.UnitPrice.ToString("C2").PadLeft(10));
            }

            Console.WriteLine();
            Console.WriteLine(TAB + "Press any key to continue.");
            Console.ReadKey();
        }

        // FindExpensive(): List the most expensive Seafood. Consider there may be more than one.
        private static void FindExpensive(List<Product> products)
        {
            string TAB = "   ";

            Console.Clear();
            Console.WriteLine(TAB + "List the most expensive Seafood. Consider there may be more than one");
            Console.WriteLine();

            var query = from product in products
                        group product by product.Category into cat                        
                        select new
                        {
                            cat = cat.Key,                            
                            heighestPrice = cat.Max(x => x.UnitPrice)
                        };
    
            Console.WriteLine(TAB + "Category".PadRight(15) + "Unit Price".PadLeft(10));
            Console.WriteLine(TAB + "--------".PadRight(15) + "----------".PadLeft(10)); 

            foreach (var product in query)
	            { 
                    if (product.cat == "Seafood")
	                {
                        Console.WriteLine(TAB + product.cat.PadRight(15) + TAB + product.heighestPrice.ToString("c2"));
	                }

	            };        	

            Console.WriteLine();
            Console.WriteLine(TAB + "Press any key to continue.");
            Console.ReadKey();
        }

        // OrderByTotalValue(): List all condiments with total value in stock (UnitPrice * UnitsInStock). Sort by total value.
        private static void OrderByTotalValue(List<Product> products)
        {
            string TAB = "   ";

            Console.Clear();
            Console.WriteLine(TAB + "List all condiments with total value in stock (UnitPrice * UnitsInStock). Sort by total value.");
            Console.WriteLine();

            //
            // lambda syntax
            //
            var sortedProducts = products.Where(p => p.Category == "Condiments").OrderByDescending(p => p.UnitPrice * p.UnitsInStock);

            Console.WriteLine(TAB + "Category".PadRight(15) + "Product Name".PadRight(35) + "Total Price".PadLeft(10));
            Console.WriteLine(TAB + "--------".PadRight(15) + "------------".PadRight(35) + "----------".PadLeft(10));

            foreach (Product product in sortedProducts)
            {
                Console.WriteLine(TAB + product.Category.PadRight(15) + product.ProductName.PadRight(35) + (product.UnitPrice * product.UnitsInStock).ToString("c2").PadLeft(10));
            }

            Console.WriteLine();
            Console.WriteLine(TAB + "Press any key to continue.");
            Console.ReadKey();
        }

        // OrderByName(): List all products with names that start with "S" and calculate the average of the units in stock.
        private static void OrderByName(List<Product> products)
        {
            string TAB = "   ";

            Console.Clear();
            Console.WriteLine(TAB + "List all products with names that start with \"S\" and calculate the average of the units in stock.");
            Console.WriteLine();

            //
            // lambda syntax
            //
            var sortedProducts = products.Where(p => p.ProductName.StartsWith("S")).OrderByDescending(p => p.Category);

            Console.WriteLine(TAB + "Category".PadRight(15) + "Product Name".PadRight(35) + "Units In Stock".PadLeft(10));
            Console.WriteLine(TAB + "--------".PadRight(15) + "------------".PadRight(35) + "--------------".PadLeft(10));

            int totalUnitsInStock = 0;
            int totalNumberOfProduct = 0;
            foreach (Product product in sortedProducts)
	        {                
                totalUnitsInStock = product.UnitsInStock + totalUnitsInStock;
                totalNumberOfProduct = totalNumberOfProduct + 1;                 
	        }

            foreach (Product product in sortedProducts)
            {
                Console.WriteLine(TAB + product.Category.PadRight(15) + product.ProductName.PadRight(35) + product.UnitsInStock.ToString().PadLeft(10));
            }

            Console.WriteLine(); 
            Console.WriteLine(TAB + "Average units in stock = " + (totalUnitsInStock / totalNumberOfProduct));

            Console.WriteLine();
            Console.WriteLine(TAB + "Press any key to continue.");
            Console.ReadKey();
        }

        // Query: Student Choice - Minimum of one per team member

        // Average price per category
        private static void AveragePricePerCategory(List<Product> products)
        {
            string TAB = "   ";

            Console.Clear();
            Console.WriteLine(TAB + "Calculate the average price for every category.");
            Console.WriteLine();

            var query = from product in products
            group product by product.Category into cat                        
            select new
            {
                cat = cat.Key,                            
                averagePrice = cat.Average(x => x.UnitPrice)
            };

            Console.WriteLine(TAB + "Category".PadRight(15) + "Average Price".PadLeft(10));
            Console.WriteLine(TAB + "--------".PadRight(15) + "----------".PadLeft(10)); 

            foreach (var product in query)
	            { 
                        Console.WriteLine(TAB + product.cat.PadRight(15) + TAB + product.averagePrice.ToString("c2")); 
	            }; 

            Console.WriteLine();
            Console.WriteLine(TAB + "Press any key to continue.");
            Console.ReadKey();
        }

        // Anonymous Type printing out the full list      
        private static void FullList(List<Product> products)
        {
            string TAB = "   ";

            Console.Clear();
            Console.WriteLine(TAB + "Anonymous return type printing out full list");
            Console.WriteLine();

            var query = from product in products
                        select new
                        {
                            ProductID = product.ProductID,
                            Category = product.Category,
                            ProductName = product.ProductName,
                            UnitPrice = product.UnitPrice,
                            UnitsInStock = product.UnitsInStock
                        };

            Console.WriteLine(TAB + "ProductID".PadRight(15) + "Category".PadRight(15) + "Product Name".PadRight(35) + "Unit Price".PadRight(15) + "Units In Stock".PadLeft(10));
            Console.WriteLine(TAB + "---------".PadRight(15) + "--------".PadRight(15) + "------------".PadRight(35) + "----------".PadRight(15) + "--------------".PadLeft(10));

            foreach (var product in query)
	        {
                Console.WriteLine(TAB + product.ProductID.ToString().PadRight(15) + product.Category.PadRight(15) + product.ProductName.PadRight(35) + product.UnitPrice.ToString("c2").PadRight(10) + product.UnitsInStock.ToString().PadLeft(10));
	        }

            Console.WriteLine();
            Console.WriteLine(TAB + "Scroll up to see table headings");
            Console.WriteLine();
            Console.WriteLine(TAB + "Press any key to continue.");
            Console.ReadKey();

        }


        /// <summary>
        /// read all products from an XML file and return as a list of Product
        /// in descending order by price
        /// </summary>
        /// <returns>List of Product</returns>
        private static List<Product> ReadAllProductsFromXml()
        {
            string dataPath = @"Data\Products.xml";
            List<Product> products;

            try
            {
                StreamReader streamReader = new StreamReader(dataPath);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Product>), new XmlRootAttribute("Products"));

                using (streamReader)
                {
                    products = (List<Product>)xmlSerializer.Deserialize(streamReader);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return products;
        }


        private static void OrderByCatagory(List<Product> products)
        {
            string TAB = "   ";

            Console.Clear();
            Console.WriteLine(TAB + "List all beverages and sort by the unit price.");
            Console.WriteLine();


            //
            // lambda syntax
            //
            var sortedProducts = products.Where(p => p.Category == "Beverages").OrderByDescending(p => p.UnitPrice);

            Console.WriteLine(TAB + "Category".PadRight(15) + "Product Name".PadRight(25) + "Unit Price".PadLeft(10));
            Console.WriteLine(TAB + "--------".PadRight(15) + "------------".PadRight(25) + "----------".PadLeft(10));

            foreach (Product product in sortedProducts)
            {
                Console.WriteLine(TAB + product.Category.PadRight(15) + product.ProductName.PadRight(25) + product.UnitPrice.ToString("C2").PadLeft(10));
            }

            Console.WriteLine();
            Console.WriteLine(TAB + "Press any key to continue.");
            Console.ReadKey();
        }

        private static void OrderByCatagoryAnoymous(List<Product> products)
        {
            string TAB = "   ";

            Console.Clear();
            Console.WriteLine(TAB + "List all beverages that cost more the $15 and sort by the unit price.");
            Console.WriteLine();

            //
            // query syntax
            //
            var sortedProducts =
                from product in products
                where product.Category == "Beverages" &&
                    product.UnitPrice > 15
                orderby product.UnitPrice descending
                select new
                {
                    Name = product.ProductName,
                    Price = product.UnitPrice
                };

            //
            // lambda syntax
            //
            //var sortedProducts = products.Where(p => p.Category == "Beverages" && p.UnitPrice > 15).OrderByDescending(p => p.UnitPrice).Select(p => new
            //{
            //    Name = p.ProductName,
            //    Price = p.UnitPrice
            //});


            decimal average = products.Average(p => p.UnitPrice);

            Console.WriteLine(TAB + "Product Name".PadRight(20) + "Product Price".PadLeft(15));
            Console.WriteLine(TAB + "------------".PadRight(20) + "-------------".PadLeft(15));

            foreach (var product in sortedProducts)
            {
                Console.WriteLine(TAB + product.Name.PadRight(20) + product.Price.ToString("C2").PadLeft(15));
            }

            Console.WriteLine();
            Console.WriteLine(TAB + "Average Price:".PadRight(20) + average.ToString("C2").PadLeft(15));

            Console.WriteLine();
            Console.WriteLine(TAB + "Press any key to continue.");
            Console.ReadKey();
        }
    }
}
