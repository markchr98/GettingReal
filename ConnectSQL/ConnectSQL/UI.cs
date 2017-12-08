using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectSQL
{
    class UI
    {
        Controller controller = new Controller();
        public void Run()
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

                                    controller.CreatePet(name,type,breed,DOB,weight,ownerId);
                                    break;
                                case ConsoleKey.D2:
                                    controller.ViewPets();
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
                                    controller.CreateOwner(lastName,firstName,phone,email);
                                    break;
                                case ConsoleKey.D2:
                                    foreach(Owner owner in controller.ViewOwner())
                                    {
                                        Console.WriteLine("name: " + owner.Id + ", Type: " + owner.LastName + ", Breed: " + owner.FirstName + ", Phone: " + owner.Phone + ", Email: " + owner.Email);
                                    }
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
    }
}

