using System;
using System.Data;
using System.Data.SqlClient;

namespace ConnectSQL
{
    class Controller
    {
        private static string connectionString = "Server=EALSQL1.eal.local; Database=DB2017_A17; User Id=USER_A17; Password=SesamLukOp_17;";
        public void Run()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var menuInput = new ConsoleKey();
                string input = "";
                while (menuInput != ConsoleKey.Escape)
                {
                    MainMenu();
                    menuInput = Console.ReadKey().Key;
                    switch (menuInput)
                    {
                        case ConsoleKey.D1:
                            while (menuInput != ConsoleKey.Escape)
                            {
                                PetMenu();
                                menuInput = Console.ReadKey().Key;
                                switch (menuInput)
                                {
                                    case ConsoleKey.D1:
                                        CreatePet(connection);
                                        break;
                                    case ConsoleKey.D2:
                                        ViewPets(connection);
                                        break;
                                    case ConsoleKey.Escape:
                                        break;
                                    default:
                                        Console.WriteLine("That is not a valid option");
                                        break;
                                }
                            }
                            menuInput = new ConsoleKey();

                            break;
                        case ConsoleKey.D2:
                            while (menuInput != ConsoleKey.Escape)
                            {
                                OwnerMenu();
                                menuInput = Console.ReadKey().Key;
                                switch (menuInput)
                                {
                                    case ConsoleKey.D1:
                                        CreateOwner(connection);
                                        break;
                                    case ConsoleKey.D2:
                                        ViewOwner(connection);
                                        break;
                                    case ConsoleKey.Escape:
                                        break;
                                    default:
                                        Console.WriteLine("That is not a valid option");
                                        break;
                                }
                            }
                            menuInput = new ConsoleKey();
                            break;

                        case ConsoleKey.Escape:
                            input = "";
                            Console.Clear();
                            while (input != "yes" && input != "no")
                            {
                                Console.WriteLine("Are you sure you want to exit the program? (Yes/No)");
                                input = Console.ReadLine();
                                Console.Clear();
                                if (input.ToLower() == "no")
                                {
                                    menuInput = new ConsoleKey();
                                }
                                if (input.ToLower() != "no" && input.ToLower() != "yes")
                                {
                                    Console.Clear();
                                    Console.WriteLine("That is not a valid option");
                                    menuInput = new ConsoleKey();
                                }
                            }
                            break;
                        default:
                            Console.WriteLine("That is not a valid option");
                            break;
                    }
                }
            }
        }


        //writeline menus
        public void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Press 1 to manage Pets");
            Console.WriteLine("Press 2 to manage Owners");
            Console.WriteLine("Press ESC to Exit");
        }

        public void PetMenu()
        {
            Console.Clear();
            Console.WriteLine("Press 1 to enter a new Pet");
            Console.WriteLine("Press 2 to view all Pets");
            Console.WriteLine("Press ESC to go back");
        }

        public void OwnerMenu()
        {
            Console.Clear();
            Console.WriteLine("Press 1 to enter a new Owner");
            Console.WriteLine("Press 2 to view all Owners");
            Console.WriteLine("Press ESC to go back");
        }

        //Using procedures here
        public void CreatePet(SqlConnection connection)
        {
            Console.Clear();

            Console.WriteLine("Please enter name");
            string name = Console.ReadLine();
            Console.WriteLine("Please enter type");
            string type = Console.ReadLine();
            Console.WriteLine("Please enter breed");
            string breed = Console.ReadLine();
            Console.WriteLine("Please enter DOB");
            string DOB = Console.ReadLine();
            Console.WriteLine("Please enter weight");
            string weight = Console.ReadLine();
            Console.WriteLine("Please enter owner ID");
            string ownerId = Console.ReadLine();

            Console.Clear();
            try
            {

                connection.Open();
                SqlCommand CreateOwner = new SqlCommand("InsertOwner", connection);
                CreateOwner.CommandType = CommandType.StoredProcedure;

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

        public void ViewPets(SqlConnection connection)
        {
            Console.Clear();
            try
            {
                connection.Open();
                SqlCommand viewPets = new SqlCommand("GetPets", connection);
                viewPets.CommandType = CommandType.StoredProcedure;
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
        public void CreateOwner(SqlConnection connection)
        {
            Console.Clear();

            Console.WriteLine("Please enter lastname");
            string lastName = Console.ReadLine();
            Console.WriteLine("Please enter first name");
            string firstName = Console.ReadLine();
            Console.WriteLine("Please enter phone number");
            string phone = Console.ReadLine();
            Console.WriteLine("Please enter email address");
            string email = Console.ReadLine();

            Console.Clear();
            try
            {
                connection.Open();
                SqlCommand CreateOwner = new SqlCommand("InsertOwner", connection);
                CreateOwner.CommandType = CommandType.StoredProcedure;

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

        public void ViewOwner(SqlConnection connection)
        {
            Console.Clear();
            try
            {
                connection.Open();
                SqlCommand viewOwners = new SqlCommand("GetOwners", connection);
                viewOwners.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = viewOwners.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string id = reader["OwnerID"].ToString();
                        string lastName = reader["OwnerLastName"].ToString();
                        string firstName = reader["OwnerFirstName"].ToString();
                        string phone = reader["OwnerPhone"].ToString();
                        string email = reader["OwnerEmail"].ToString();

                        Console.WriteLine("name: " + id + ", Type: " + lastName + ", Breed: " + firstName + ", DOB: " + phone + ", Weight: " + email);
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
}
