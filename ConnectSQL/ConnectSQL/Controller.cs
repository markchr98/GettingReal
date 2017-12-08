using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ConnectSQL
{
    class Controller
    {
        private static string connectionString = "Server=EALSQL1.eal.local; Database=DB2017_A17; User Id=USER_A17; Password=SesamLukOp_17;";


        //Using procedures here
        public void CreatePet(string name, string type, string breed, string DOB,string weight, string ownerId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                try
                {

                    connection.Open();
                    SqlCommand CreateOwner = new SqlCommand("InsertOwner", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    CreateOwner.Parameters.Add(new SqlParameter("@Name", name));
                    CreateOwner.Parameters.Add(new SqlParameter("@Type", type));
                    CreateOwner.Parameters.Add(new SqlParameter("@Breed", breed));
                    CreateOwner.Parameters.Add(new SqlParameter("@DOB", DOB));
                    CreateOwner.Parameters.Add(new SqlParameter("@Weight", weight));
                    CreateOwner.Parameters.Add(new SqlParameter("@OwnerId", ownerId));

                    CreateOwner.ExecuteNonQuery();

                    Console.WriteLine("Successfully entered a new pet");
                    Console.WriteLine("Press enter to continue");
                    Console.ReadKey();

                    connection.Close();
                }
                catch (SqlException e)
                {
                    Console.WriteLine("UPS " + e.Message);
                }
            }
        }

        public void ViewPets()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                Console.Clear();
                try
                {
                    connection.Open();
                    SqlCommand viewPets = new SqlCommand("GetPets", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    SqlDataReader reader = viewPets.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string name = reader["PetName"].ToString();
                            string type = reader["PetType"].ToString();
                            string breed = reader["PetBreed"].ToString();
                            string DOB = reader["PetDOB"].ToString();
                            string weight = reader["PetWeight"].ToString();
                            string ownerId = reader["OwnerId"].ToString();

                            Console.WriteLine("name: " + name + ", Type: " + type + ", Breed: " + breed + ", DOB: " + DOB + ", Weight: " + weight + ", OwnerId: " + ownerId);
                        }
                        Console.WriteLine("Press enter to continue");
                        Console.ReadKey();
                    }
                    connection.Close();
                }
                catch (SqlException e)
                {
                    Console.WriteLine("UPS " + e.Message);
                }
            }
        }
        public void CreateOwner(string lastName,string firstName, string phone, string email)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {               
                try
                {
                    connection.Open();
                    SqlCommand CreateOwner = new SqlCommand("InsertOwner", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    CreateOwner.Parameters.Add(new SqlParameter("@LastName", lastName));
                    CreateOwner.Parameters.Add(new SqlParameter("@FirstName", firstName));
                    CreateOwner.Parameters.Add(new SqlParameter("@Phone", phone));
                    CreateOwner.Parameters.Add(new SqlParameter("@Email", email));

                    CreateOwner.ExecuteNonQuery();

                    Console.WriteLine("Successfully entered a new owner");
                    Console.WriteLine("Press enter to continue");
                    Console.ReadKey();

                    connection.Close();
                }
                catch (SqlException e)
                {
                    Console.WriteLine("UPS " + e.Message);
                }
            }
        }

        public List<Owner> ViewOwner()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<Owner> owners = new List<Owner>();
                Console.Clear();
                try
                {
                    
                    connection.Open();
                    SqlCommand viewOwners = new SqlCommand("GetOwners", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    SqlDataReader reader = viewOwners.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            owners.Add(new Owner()
                            {
                                Id = reader["OwnerID"].ToString(),
                                LastName = reader["OwnerLastName"].ToString(),
                                FirstName = reader["OwnerFirstName"].ToString(),
                                Phone = reader["OwnerPhone"].ToString(),
                                Email = reader["OwnerEmail"].ToString()
                            });                            
                        }
                        Console.WriteLine("Press enter to continue");
                        Console.ReadKey();
                    }                    
                    connection.Close();
                }
                catch (SqlException e)
                {
                    Console.WriteLine("UPS " + e.Message);
                }
                return owners;
            }
        }
    }
}
