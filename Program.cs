using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Text;

namespace BasicLibrary
{
    internal class Program
    {
        static List<(string BName, string BAuthor, int ID, int Qnt)> Books = new List<(string BName, string BAuthor, int ID, int Qnt)>();
        static List<(string Email, string Password)>UserReg = new List<(string Email, string Password)>();
        static List<(string Email, string Password)> AdminReg = new List<(string Email, string Password)>();
       // static List<(string Bname2, int BId, int BQNT)> Borrowed =new List<(string Bname2, int BId, int BQNT)> ();
        static string filePath = "C:\\Users\\Codeline User\\Desktop\\Zubair\\Lib.txt";
        static string UserFile = "C:\\Users\\Codeline User\\Desktop\\Zubair\\LibUser.txt";
        static string AdminFile = "C:\\Users\\Codeline User\\Desktop\\Zubair\\LibAdmin.txt";

        static void Main(string[] args)
        {
            AddAdmin();
           LoadBooksFromFile();
           bool EnterFlag=false;
            SaveBooksToFile();
            do
            {
                Console.WriteLine("Please Select an option: ");
                Console.WriteLine("1. Log In. \n 2. Exit ");
                int Choice=int.Parse(Console.ReadLine());
                switch (Choice)
                {
                    case 1:
                        
                        LogIn();
                        AdminRegestration();
                        break;
                   
                    default:
                        Console.WriteLine("Please Select an valid Option");
                        break;

                }
                
            } while (EnterFlag != true);  
            Console.Clear();
        }
        static void MasterMenu()
        {
            Console.WriteLine("Please Select Opration Want To Perform");
            Console.WriteLine("1- Add New Admin. \n 2- Add New User. \n 3- Show Statistics Of Library. \n 4- Show Users List. \n 5- Show Admins List");
            int MChoice=0;
            switch (MChoice)
            { 
            
                case 1:
                    AddAdmin();
                    AdminRegestration();
                    break;
            }


        }// edit
        static void LogIn()
        {
            Console.Clear();
            Console.WriteLine("Enter Your Email");
            string email = Console.ReadLine();
            Console.WriteLine("Enter Your Password");
            string password = Console.ReadLine();

            for (int i = 0; i < AdminFile.Length; i++)
            {
                if (email == AdminReg[i].Email && password == AdminReg[i].Password)
                    AdminMenu();

                else if (email == UserReg[i].Email && password == UserReg[i].Password) { UserMenu(); }
                UserMenu();
            }
        }
        static void Master()
                {
                    StringBuilder sb1 = new StringBuilder();

                    int AdminNumber = 0;

                    for (int i = 0; i < Books.Count; i++)
                    {
                        AdminNumber = i + 1;
                        sb1.Append("Admin ").Append(AdminNumber).Append(" Email  : ").Append(AdminReg[i].Email);
                        sb1.AppendLine();
                        sb1.Append("Admin ").Append(AdminNumber).Append(" Pass : ").Append(AdminReg[i].Password);
                        sb1.AppendLine();
                        sb1.AppendLine();
                        sb1.AppendLine();
                        Console.WriteLine(sb1.ToString());
                        sb1.Clear();

                    }

                }
        static void AdminMenu()
        {
            
           
                    bool ExitFlag = false;
                    do
                    {
                        Console.WriteLine("Welcome to Library");
                        Console.WriteLine("\n Select the Option :");
                        Console.WriteLine("\n 1- Add New Book");
                        Console.WriteLine("\n 2- Display All Books");
                        Console.WriteLine("\n 3- Search for Book by Name");
                        Console.WriteLine("\n 4- Save and Exit");

                        int choice = int.Parse(Console.ReadLine());

                        switch (choice)
                        {
                            case 1:
                            AddAdmin();

                            break;

                            case 2:
                                ViewAllBooks();
                                break;

                            case 3:
                                SearchForBook();
                                break;

                            case 4:
                                SaveBooksToFile();
                                ExitFlag = true;
                                break;
                            case 5:
                            AddnNewBook();
                            SaveBooksToFile();
                            break;
                        default:
                                Console.WriteLine("Sorry your choice was wrong");
                                break;



                        }

                        Console.WriteLine("press any key to continue");
                        string cont = Console.ReadLine();

                        Console.Clear();

                    } while (ExitFlag != true);
                
                
            }
        
        static void UserMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Library");
            bool ExitFlag = false;
            do
            {
                
                Console.WriteLine("\n Please Select An Option :");
                Console.WriteLine("\n 1. Search For Book");
                Console.WriteLine("\n 2. Display Available Books");
                Console.WriteLine("\n 3. Borrow A Book");
                Console.WriteLine("\n 4. Return A Book");
                Console.WriteLine("\n 5. Save and Exit");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        SearchWithBorrow();
                        break;

                    case 2:
                        Console.Clear();
                        ViewAllBooks();
                        break;

                    case 3:
                        Console.Clear();
                        BorrowBook();
                        
                        break;
                    case 4:
                        Console.Clear();
                        ReturnBook();
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Sorry your choice was wrong");
                        break;

                }
                
            }while (ExitFlag != true);
        }
        static void AddnNewBook() 
        { 

                 Console.WriteLine("Enter Book Name");
                 string name = Console.ReadLine();   

                 Console.WriteLine("Enter Book Author");
                 string author= Console.ReadLine();  

                 Console.WriteLine("Enter Book ID");
                 int ID = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter Book Quantity");
                int Qnt = int.Parse(Console.ReadLine());

                Books.Add(( name, author, ID, Qnt ));

                  Console.WriteLine("Book Added Succefully");


        }
        static void ViewAllBooks()
        {
            StringBuilder sb = new StringBuilder();

            int BookNumber = 0;

            for (int i = 0; i < Books.Count; i++)
            {             
                BookNumber = i + 1;
                sb.Append("Book ").Append(BookNumber).Append(" name : ").Append(Books[i].BName);
                sb.AppendLine();
                sb.Append("Book ").Append(BookNumber).Append(" Author : ").Append(Books[i].BAuthor);
                sb.AppendLine();
                sb.Append("Book ").Append(BookNumber).Append(" ID : ").Append(Books[i].ID);
                sb.AppendLine();
                sb.Append("Book ").Append(BookNumber).Append(" Quantity : ").Append(Books[i].Qnt);
                sb.AppendLine();
                sb.AppendLine();
                Console.WriteLine(sb.ToString());
                sb.Clear();

            }
        }
        static void SearchWithBorrow() 
        {
            Console.Clear();
            SearchForBook();
            Console.WriteLine("Do You want to Borrow it? \n 1. Yes \n 2. No");
            int UserChoice = int.Parse(Console.ReadLine());
            if (UserChoice == 1)
            {
                for (int i = 0; i < Books.Count; i++)
                {
                    {
                        Books[i] = (Books[i].BName, Books[i].BAuthor, Books[i].ID, Books[i].Qnt - 1);

                        SaveBooksToFile();
                    }
                }
            }
            else
                Console.WriteLine("Thank you For Your Time");
        }
        static void SearchForBook()
        {
            Console.WriteLine("Enter the book name you want");
            string name = Console.ReadLine();  
            bool flag=false;

            for(int i = 0; i< Books.Count;i++)
            {
                if (Books[i].BName == name)
                {
                    Console.WriteLine("Book Author is : " + Books[i].BAuthor + "Book ID: "+ Books[i].ID);
                    flag = true;
                    break;
                }
            }

            if (flag != true)
            { Console.WriteLine("book not found"); }
        }
        static void LoadBooksFromFile()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            var parts = line.Split('|');
                            if (parts.Length == 4)
                            {
                                Books.Add((parts[0], parts[1], int.Parse(parts[2]), int.Parse(parts[3])));
                            }

                        }
                    }
                   
                    Console.WriteLine("Books loaded from file successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading from file: {ex.Message}");
            }

        }
        static void SaveBooksToFile()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (var book in Books)
                    {
                        writer.WriteLine($"{book.BName}|{book.BAuthor}|{book.ID}|{book.Qnt}");
                    }
                }
                Console.WriteLine("Books saved to file successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to file: {ex.Message}");
            }
        }
        static void BorrowBook() 
        { 
           ViewAllBooks();
            Console.Write("Please Enter The Book Name Want To Borrow: ");
            string BookName=Console.ReadLine();
            for (int i = 0; i < Books.Count; i++)
            {
                if (BookName == Books[i].BName)
                {
                    Console.WriteLine("Book name "+ Books[i].BName +  "\t Book ID: " + Books[i].ID);
                    Console.WriteLine("Borrow? \n 1. Yes \n 2. No");
                    int CS = int.Parse(Console.ReadLine());
                    if (CS == 1)
                    {
                        Books[i] = (Books[i].BName, Books[i].BAuthor, Books[i].ID, Books[i].Qnt - 1);

                        SaveBooksToFile();
                        Console.WriteLine("Borrowing Was Succesfully Done.");
                    }
                    else
                        break;
                }
                else if (BookName != Books[i].BName)
                {
                    Console.WriteLine("The Book Is Not Available Right Now");
                    break;
                }
            }
        }
        static void ReturnBook() 
        {
            Console.Write("Please Enter The Book Name: ");
            string BookName = Console.ReadLine();
            Console.Write("Enter The Book ID: ");
            int BID = int.Parse(Console.ReadLine());
            for (int i = 0; i < Books.Count; i++)
            {
                if (BookName == Books[i].BName && BID == Books[i].ID)
                {
                    Console.WriteLine("Book ID and Name Is Correct");
                    Console.WriteLine("Return? \n 1. Yes \n 2. No");
                    int CS = int.Parse(Console.ReadLine());
                    if (CS == 1)
                    {
                        Books[i] = (Books[i].BName, Books[i].BAuthor, Books[i].ID, Books[i].Qnt + 1);
                        Console.WriteLine("Returning Book Was Succesfully Done.");
                        SaveBooksToFile();
                    }
                    else
                        break;
                }
                else
                {
                    Console.WriteLine("The Book Is Not Available Right Now");
                    break;
                }
            }
        }
       static void AdminRegestration()
        {
            try
            {
                using (StreamWriter writer1 = new StreamWriter(AdminFile, true))
                {
                    foreach (var admin in AdminReg)
                    {
                        writer1.Write($"{admin.Email}|{admin.Password}");
                    }
                }
                Console.WriteLine("Admin user saved to file successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to file: {ex.Message}");
            }
        }
        static void AddAdmin()
        {
            Console.WriteLine("Enter Admin Email");
            string emailAdmin = Console.ReadLine();

            Console.WriteLine("Enter Admin Password");
            string AdminPass = Console.ReadLine();


            AdminReg.Add((emailAdmin, AdminPass));
            AdminRegestration();
            Console.WriteLine("Book Added Succefully");
        }
        static void AddUser ()
        {
            Console.WriteLine("Enter Admin Email");
            string emailUser = Console.ReadLine();

            Console.WriteLine("Enter Admin Password");
            string UserPass = Console.ReadLine();


            UserReg.Add((emailUser, UserPass));

            Console.WriteLine("Book Added Succefully");
        }
        static void UserRegestration()
        {
            try
            {
                using (StreamWriter writer2 = new StreamWriter(UserFile))
                {
                    foreach (var user in UserReg)
                    {
                        writer2.WriteLine($"{user.Email}|{user.Password}");
                    }
                }
                Console.WriteLine("User saved to file successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to file: {ex.Message}");
            }
        }
        

    }
}
        
