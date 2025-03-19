using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using SimpleRESTApi.Models;

namespace SimpleRESTApi.Data
{
    public class CategoryADO : Icategory
    {
        private readonly IConfiguration _configuration;
        private string connStr = string.Empty;

        public CategoryADO(IConfiguration configuration)
        {
            _configuration = configuration;
            connStr = _configuration.GetConnectionString("DefaultConnection");
        }

        public Category AddCategory(Category category)
        {
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                string strSql = @"INSERT INTO Categories (CategoryName)
                                VALUES (@CategoryName); 
                                select SCOPE_IDENTITY()";
                                
                SqlCommand cmd = new SqlCommand(strSql, conn);

                try
                {
                    cmd.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                    conn.Open();
                    int CategoryId = Convert.ToInt32(cmd.ExecuteScalar());
                    category.CategoryId=CategoryId;
                    // int rowAffected = cmd.ExecuteNonQuery();
                    // if(rowAffected==0)
                    // {
                    //     throw new Exception("INSERT failed");
                    // }
                    return category;
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);

                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
                
            }
        }

        public void DeleteCategory(int CategoryId)
        {
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                string strsql = @"DELETE FROM Categories WHERE CategoryId = @CategoryId";
                SqlCommand cmd = new SqlCommand(strsql,conn);
                try
                {
                    cmd.Parameters.AddWithValue("@CategoryId", CategoryId);
                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    if(result==0)
                    {
                        throw new Exception("category not found");
                    }
                    // int rowAffected = cmd.ExecuteNonQuery();
                    // if(rowAffected==0)
                    // {
                    //     throw new Exception("INSERT failed");
                    // }                    
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }

        }

        public IEnumerable<Category> GetCategories()
        {               
            List<Category> categories = new List<Category>();
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                string strSql = @"SELECT * FROM Categories ORDER BY CategoryName";
                SqlCommand cmd = new SqlCommand(strSql,conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if(dr.HasRows)
                {
                    while(dr.Read())
                    {
                        Category category= new();
                        category.CategoryId = Convert.ToInt32(dr["CategoryId"]);
                        category.CategoryName = dr["CategoryName"].ToString();
                        categories.Add(category);
                    }
                }
                dr.Close();
                cmd.Dispose();
                conn.Close();
            }
            return categories;
        }

        public Category GetCategoryById(int CategoryId)
        {
            Category category = new();
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                string strSql = @"SELECT * FROM Categories WHERE CategoryId = @CategoryId";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@CategoryId", CategoryId);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if(dr.HasRows)
                {
                    dr.Read();
                    category.CategoryId = Convert.ToInt32(dr["CategoryId"]);
                    category.CategoryName = dr["CategoryName"].ToString();
                }
                else
                {
                    throw new Exception("category not found");

                }

            }
            return category;
        }

        public Category UpdateCategory(Category category)
        {
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                string strsql = @"UPDATE Categories SET CategoryName = @CategoryName
                                WHERE CategoryId=@CategoryId";
                SqlCommand cmd = new SqlCommand(strsql,conn);
                try
                {
                    cmd.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                    cmd.Parameters.AddWithValue("@CategoryId", category.CategoryId);
                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    if(result==0)
                    {
                        throw new Exception("category not found");
                    }
                    return category;
                    // int rowAffected = cmd.ExecuteNonQuery();
                    // if(rowAffected==0)
                    // {
                    //     throw new Exception("INSERT failed");
                    // }                    
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
                
            }
        }
    }
}