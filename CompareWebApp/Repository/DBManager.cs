using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using CompareWebApp.Models;

namespace CompareWebApp.Repository
{
    public static class DBManager
    {
        // Category Section
        public static List<Category> GetParerntCategories()
        {
            List<Category> parentcategoryList = new List<Category>();
            string queryString = @"SELECT parentcategoryID,parentcategoryName FROM compare.dbo.ParentCategories";
            using (SqlConnection connection = new SqlConnection(
                           ConfigurationManager.ConnectionStrings["MatchConnectionString"].ToString()))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Category category = new Category();
                    category.categoryID = Convert.ToInt32(reader[0]);
                    category.categoryName = Convert.ToString(reader[1]);
                    parentcategoryList.Add(category);
                }

                return parentcategoryList;
            }
        }
        public static List<Category> GetCategories(int parentCategoryID)
        {
            List<Category> categoryList = new List<Category>();
            string queryString = @"SELECT categoryID,categoryName FROM compare.dbo.Categories WHERE parentCategoryID = " + parentCategoryID;
            using (SqlConnection connection = new SqlConnection(
                           ConfigurationManager.ConnectionStrings["MatchConnectionString"].ToString()))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Category category = new Category();
                    category.categoryID = Convert.ToInt32(reader[0]);
                    category.categoryName = Convert.ToString(reader[1]);
                    categoryList.Add(category);
                }

                return categoryList;
            }
        }


        // Product Section 
        public static double GetProductsCount(string categoryName)
        {
            double _productsCount = 0;
            string queryString = @"SELECT COUNT(*) 
                                   FROM ( SELECT p2.productName,MIN(p3.price) price
                                          FROM Compare.dbo.Categories p1,Compare.dbo.Products p2,Compare.dbo.ProductDetails p3
                                          WHERE  p1.categoryID = p2.categoryID 
                                          AND p2.productID = p3.productID
                                          AND p1.categoryName = '" + categoryName + @"'
                                          group by p2.productName ) p4";

            using (SqlConnection connection = new SqlConnection(
                           ConfigurationManager.ConnectionStrings["MatchConnectionString"].ToString()))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);
                _productsCount = Convert.ToDouble(command.ExecuteScalar());
            }
            return _productsCount;
        }
        public static List<Product> GetProductsByCategory(string categoryName, int noOfProductsPerPage, int currentPageIndex)
        {
            List<Product> productList = new List<Product>();
            using (SqlConnection connection = new SqlConnection(
                           ConfigurationManager.ConnectionStrings["MatchConnectionString"].ToString()))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("[Compare].[dbo].[GetProductsByCategory]", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@categoryName", categoryName));
                command.Parameters.Add(new SqlParameter("@noOfProductsPerPage", noOfProductsPerPage));
                command.Parameters.Add(new SqlParameter("@currentPageIndex", currentPageIndex));

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Product product = new Product();
                    product.productName = Convert.ToString(reader[0]);
                    product.price = Convert.ToDecimal(reader[1]);
                    productList.Add(product);
                }

                return productList;

            }
        }

        /*public List<Product> GetAllProducts()
        {
            List<Product> productList = new List<Product>();
            string queryString = @"SELECT TOP 100 vendorName,t1.productName,price  
				                  from compare.dbo.products t1,compare.dbo.productdetails t2,Compare.dbo.Vendors t3 
				                  where t1.productID = t2.productID
				                  AND t2.vendorID = t3.vendorID";

            using (SqlConnection connection = new SqlConnection(
                           ConfigurationManager.ConnectionStrings["MatchConnectionString"].ToString()))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Product product = new Product();
                    product.vendorName = Convert.ToString(reader[0]);
                    product.productName = Convert.ToString(reader[1]);
                    product.price = Convert.ToDecimal(reader[2]);
                    productList.Add(product);
                }

                return productList;

            }
        }*/

        public static int CountVendorsByProduct(string productID)
        {
            int rowCount = 0;
            using (SqlConnection connection = new SqlConnection(
                           ConfigurationManager.ConnectionStrings["MatchConnectionString"].ToString()))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("[Compare].[dbo].[CountVendorsByProduct]", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@productID", productID));

                rowCount = Convert.ToInt32(command.ExecuteScalar());
                return rowCount;

            }
        }
        public static List<Product> GetVendorsByProduct(string productID, string pageSize, string pageNumber)
        {
            List<Product> productList = new List<Product>();
            using (SqlConnection connection = new SqlConnection(
                           ConfigurationManager.ConnectionStrings["MatchConnectionString"].ToString()))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("[Compare].[dbo].[GetVendorsByProduct]", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@productID", productID));
                command.Parameters.Add(new SqlParameter("@pageSize", pageSize));
                command.Parameters.Add(new SqlParameter("@pageNumber", pageNumber));

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Product product = new Product();
                    product.productID = Convert.ToInt32(reader[0]);
                    product.productName = Convert.ToString(reader[1]);
                    product.vendorName = Convert.ToString(reader[2]);
                    product.price = Convert.ToDecimal(reader[3]);
                    product.productAttributes = Convert.ToString(reader[4]);

                    productList.Add(product);
                }

                return productList;


            }
        }

        public static int GetMatchedProductsCount(string strSearch)
        {
            int noOfProducts = 0;
            string queryString = @"select COUNT(*) FROM (
                                  select vendorName,t1.productName,min(price) price 
				                  from compare.dbo.products t1,compare.dbo.productdetails t2,Compare.dbo.Vendors t3 
				                  where t1.productID = t2.productID
				                  AND t2.vendorID = t3.vendorID
				                  AND t1.productName like '%" + strSearch + @"%'
				                  group by vendorName,t1.productName) t1 ";

            using (SqlConnection connection = new SqlConnection(
                           ConfigurationManager.ConnectionStrings["MatchConnectionString"].ToString()))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);
                noOfProducts = Convert.ToInt32(command.ExecuteScalar());
            }
            return noOfProducts;
        }

        public static List<Product> GetProductsByPage(string strSearch, int pageNumber)
        {
            List<Product> productList = new List<Product>();
            using (SqlConnection connection = new SqlConnection(
                           ConfigurationManager.ConnectionStrings["MatchConnectionString"].ToString()))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("[Compare].[dbo].[SearchProduct]", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@searchTerm", strSearch));
                command.Parameters.Add(new SqlParameter("@pageNumber", pageNumber));

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Product product = new Product();
                    product.vendorName = Convert.ToString(reader[0]);
                    product.productName = Convert.ToString(reader[1]);
                    product.price = Convert.ToDecimal(reader[2]);
                    productList.Add(product);
                }

                return productList;


            }
        }


        // Search Section         

        public static List<Category> SearchInCategory(string strSearch)
        {
            List<Category> categoryList = new List<Category>();

            string queryString = @"SELECT DISTINCT categoryID,categoryName 
                                   FROM Compare.dbo.Categories 
                                   WHERE categoryName like '%" + strSearch + @"%'";

            using (SqlConnection connection = new SqlConnection(
                           ConfigurationManager.ConnectionStrings["MatchConnectionString"].ToString()))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Category category = new Category();
                    category.categoryID = Convert.ToInt32(reader[0]);
                    category.categoryName = Convert.ToString(reader[1]);
                    categoryList.Add(category);
                }

                return categoryList;

            }


        }
    }
}