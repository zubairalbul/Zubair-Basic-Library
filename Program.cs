using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Text;

namespace BasicLibrary
{
    internal class Program
    {
        static string currentUser;
        static List<(string BName, string BAuthor, int ID, int Qnt)> Books = new List<(string BName, string BAuthor, int ID, int Qnt)>();
        static List<(string Email, string Password)> UserReg = new List<(string Email, string Password)>();
        static List<(string Email, string Password)> AdminReg = new List<(string Email, string Password)>();
        static List<(string Email, string Password)> MasterReg = new List<(string Email, string Password)>();
        static List<(string User, string BookN, int Quantity)> BookName = new List<(string user, string BookN, int Quantity)>();
        static List<(string User, string Bookk, int Qnt)> Users = new List<(string User, string Bookk, int Qnt)>();
        //static List<(string Bname2, int BId, int BQNT)> Borrowed =new List<(string Bname2, int BId, int BQNT)> ();
        static string filePath = "C:\\Users\\Codeline User\\Desktop\\Zubair\\Lib.txt";
        static string UserFile = "C:\\Users\\Codeline User\\Desktop\\Zubair\\LibUser.txt";
        static string AdminFile = "C:\\Users\\Codeline User\\Desktop\\Zubair\\LibAdmin.txt";
        static string MasterFile = "C:\\Users\\Codeline User\\Desktop\\Zubair\\LibMaster.txt";
        static string BorrowedBook = "C:\\Users\\Codeline User\\Desktop\\Zubair\\LibBorrowed.txt";
        static string UserLists = "C:\\Users\\Codeline User\\Desktop\\Zubair\\UserList.txt";

        static void Main(string[] args)
        {


            Console.Clear();
            bool EnterFlag = false;
            do
            {
                Console.WriteLine("Please Select an option: ");
                Console.WriteLine(" 1. Log In. \n 2. Registor as User. \n 3. Exit. ");
                int Choice = int.Parse(Console.ReadLine());
                switch (Choice)
                {
                    case 1:
                        MasterReader();
                        ReadAdmins();
                        UserReader();
                        LogIn();
                        AdminRegestration();

                        break;
                    case 2:
                        AddUser();
                        UserRegestration();
                        break;

                    case 3:
                        Console.WriteLine("Please Select an valid Option");
                        break;

                }

            } while (EnterFlag != true);
            Console.Clear();
        }
        static void MasterMenu()
        {
            bool ExFlag = false;
            do
            {
                Console.WriteLine("Please Select Opration Want To Perform");
                Console.WriteLine("1- Add New Admin. \n 2- Add New User. \n 3- Show Statistics Of Library. \n 4- Show Users List. \n 5- Show Admins List \n 6- Exit");
                int MChoice = int.Parse(Console.ReadLine());
                switch (MChoice)
                {

                    case 1:
                        AddAdmin();
                        AdminRegestration();
                        break;
                    case 2:
                        AddUser();
                        UserRegestration();
                        break;
                    case 3:
                        LoadBooksFromFile();
                        Console.Clear();
                        Console.WriteLine("Aveliable Books: \n ");
                        ViewAllBooks();
                        Console.WriteLine("Borrowed Books: \n ");
                        ReadBooks();
                        ViewAllBorrowedBooks();

                        break;
                    case 4:
                        UsersBorrowLists();
                        UsersInfoAppend();
                        break;

                    case 5:
                        ReadAdmins();
                        AppendAdmins();
                        break;
                    case 6:
                        ExFlag = true;
                        break;
                    default:
                        Console.WriteLine("Invalid Selection");
                        break;

                }
                Console.WriteLine("press any key to continue");
                string conts = Console.ReadLine();
            } while (ExFlag != true);

        }// edit
        static void LogIn()
        {
            Console.Clear();
            Console.WriteLine("Enter Your Email");
            string email = Console.ReadLine();
            Console.WriteLine("Enter Your Password");
            string password = Console.ReadLine();
            bool flagu = false;
            bool flagM = false;
            bool flagA = false;

            for (int i = 0; i < MasterReg.Count; i++)
            {
                if (email == MasterReg[i].Email && password == MasterReg[i].Password)
                {

                    flagM = true;
                    flagA = false;
                    flagu = false;
                    break;
                }
            }
            for (int i = 0; i < UserReg.Count; i++)
            {
                if (email == UserReg[i].Email && password == UserReg[i].Password)
                {

                    flagA = false;
                    flagM = false;
                    flagu = true;
                    currentUser = UserReg[i].Email;
                    break;
                }
            }
            for (int i = 0; i < AdminReg.Count; i++)
            {
                if (email == AdminReg[i].Email && password == AdminReg[i].Password)
                {

                    flagA = true;
                    flagM = false;
                    flagu = false;
                    break;
                }
            }


            if (!flagu && !flagA && !flagM)
            {
                Console.WriteLine("Invalid User Do You want to Registor as User? \n 1. Yes. \n 2. No. ");

                int Reg = int.Parse(Console.ReadLine());
                if (Reg == 1)
                {
                    AddUser();
                }
                else if (Reg == 2)
                {
                    Console.WriteLine("Thank you for your time");
                }
            }
            else if (flagM == true) { MasterMenu(); }
            else if (flagA == true) { AdminMenu(); }
            else if (flagu == true) { UserMenu(); }

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
                        AddnNewBook();
                        SaveBooksToFile();

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
                        SaveBooksToFile();
                        break;

                    case 2:
                        Console.Clear();
                        ViewAllBooks();
                        break;

                    case 3:
                        Console.Clear();
                        //LoadBooksFromFile();
                        SearchWithBorrow();

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

            } while (ExitFlag != true);
        }
        static void AddnNewBook()
        {

            Console.WriteLine("Enter Book Name");
            string name = Console.ReadLine();

            Console.WriteLine("Enter Book Author");
            string author = Console.ReadLine();

            Console.WriteLine("Enter Book ID");
            int ID = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Book Quantity");
            int Qnt = int.Parse(Console.ReadLine());

            Books.Add((name, author, ID, Qnt));

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
        static void ViewAllBorrowedBooks()
        {
            StringBuilder sb2 = new StringBuilder();
            int BowrroedNumber = 0;
            for (int i = 0; i < BookName.Count; i++)
            {
                BowrroedNumber = i + 1;
                sb2.Append("Book ").Append(BowrroedNumber).Append(" name : ").Append(BookName[i].BookN);
                sb2.AppendLine();
                sb2.Append("Book ").Append(BowrroedNumber).Append(" Quantity : ").Append(BookName[i].Quantity);
                sb2.AppendLine();
                sb2.AppendLine();

                Console.WriteLine(sb2.ToString());
            }

        }
        static void SearchWithBorrow()
        {
            int BS;
            Console.Clear();
            
            string h=SearchForBook();
            Console.WriteLine("Do You want to Borrow it? \n 1. Yes \n 2. No");
            int UserChoice = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the quantity: ");
            BS = int.Parse(Console.ReadLine());
            if (UserChoice == 1)
            {
                for (int i = 0; i < Books.Count; i++)
                {
                    if (h == Books[i].BName)
                    {
                        Books[i] = (Books[i].BName, Books[i].BAuthor, Books[i].ID, (Books[i].Qnt - BS));
                        SaveBooksToFile();
                        BookName.Add((currentUser, Books[i].BName, BS));
                        Borrowedbooks();

                    }
                }
            }
            else
                Console.WriteLine("Thank you For Your Time");
        }
        static string SearchForBook()
        {
            LoadBooksFromFile();
            ViewAllBooks();
            Console.WriteLine("Enter the book name you want");
            string name = Console.ReadLine();
            bool flag = false;

            for (int i = 0; i < Books.Count; i++)
            {
                if (Books[i].BName == name)
                {
                    Console.WriteLine("Book Author is : " + Books[i].BAuthor + "Book ID: " + Books[i].ID);
                    flag = true;
                    return name;
                    break;
                }
            }

            if (flag != true)
            { Console.WriteLine("book not found"); } return null;
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
            bool flag = false;
            int Index;
            ViewAllBooks();
            Console.Write("Please Enter The Book Name Want To Borrow: ");
            string BookNames = Console.ReadLine();
            for (int i = 0; i < Books.Count; i++)
            {
                if (BookNames == Books[i].BName)
                {
                    Console.WriteLine("Book name " + Books[i].BName + "\t Book ID: " + Books[i].ID);
                    Console.WriteLine("Borrow? \n 1. Yes \n 2. No");
                    int CS = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the quantity You want");
                    int BS = int.Parse(Console.ReadLine());
                    if (CS == 1)
                    {
                        Books[i] = (Books[i].BName, Books[i].BAuthor, Books[i].ID, Books[i].Qnt - BS);

                        SaveBooksToFile();
                        BookName.Add((currentUser, Books[i].BName, BS));
                        Borrowedbooks();
                        Console.WriteLine("Borrowing Was Succesfully Done.");
                    }
                    else if (CS == 2)
                    {
                        Console.WriteLine("Thank You For Your Time");
                        break;



                    }
                    else if (BookNames != Books[i].BName)
                    {
                        Console.WriteLine("The Book Is Not Available Right Now");
                        flag = !true;
                    }


                }
            }
        }
        static void Borrowedbooks()
        {
            try
            {
                using (StreamWriter writer4 = new StreamWriter(BorrowedBook, true))
                {
                    foreach (var Bbook in BookName)
                    {
                        writer4.WriteLine($"{Bbook.User}|{Bbook.BookN}|{Bbook.Quantity}");
                    }
                }
                Console.WriteLine("Books saved to file successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to file: {ex.Message}");
            }
        }
        static void ReadBooks()
        {
            try
            {
                if (File.Exists(BorrowedBook))
                {
                    using (StreamReader reader = new StreamReader(BorrowedBook))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            var parts = line.Split('|');
                            if (parts.Length == 3)
                            {
                                BookName.Add((parts[0], parts[1],int.Parse(parts[2])));
                            }

                        }
                    }


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading from file: {ex.Message}");
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

            Console.WriteLine("Admin Added Succefully");
        }
        static void ReadAdmins()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    using (StreamReader reader1 = new StreamReader(AdminFile))
                    {
                        string line;
                        while ((line = reader1.ReadLine()) != null)
                        {
                            var parts = line.Split('|');
                            if (parts.Length == 2)
                            {
                                AdminReg.Add((parts[0], parts[1]));
                            }

                        }
                    }

                    Console.WriteLine("Admin loaded from file successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading from file: {ex.Message}");
            }
        }
        static void AppendAdmins()
        {
            StringBuilder sb5 = new StringBuilder();

            int UserNumber = 0;
            for (int i = 0; i < UserReg.Count; i++)
            {
                UserNumber = i + 1;
                sb5.Append("Admin ").Append(UserNumber).Append(" name : ").Append(AdminReg[i].Email);
                sb5.AppendLine();
                sb5.AppendLine();
                Console.WriteLine(sb5.ToString());
                sb5.Clear();

            }
        }
        static void AddUser()
        {
            Console.WriteLine("Enter User Email");
            string emailUser = Console.ReadLine();

            Console.WriteLine("Enter User Password");
            string UserPass = Console.ReadLine();


            UserReg.Add((emailUser, UserPass));

            Console.WriteLine("User Added Succefully");
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
        static void UserReader()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    using (StreamReader reader2 = new StreamReader(UserFile))
                    {
                        string line;
                        while ((line = reader2.ReadLine()) != null)
                        {
                            var parts = line.Split('|');
                            if (parts.Length == 2)
                            {
                                UserReg.Add((parts[0], parts[1]));
                            }

                        }
                    }

                    Console.WriteLine("User loaded from file successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading from file: {ex.Message}");
            }
        }
        static void MasterWriter()
        {
            try
            {
                using (StreamWriter writer2 = new StreamWriter(MasterFile))
                {
                    foreach (var Master in MasterReg)
                    {
                        writer2.WriteLine($"{Master.Email}|{Master.Password}");
                    }
                }
                Console.WriteLine("Master saved to file successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to file: {ex.Message}");
            }
        }
        static void MasterReader()
        {
            try
            {
                if (File.Exists(MasterFile))
                {
                    using (StreamReader reader3 = new StreamReader(MasterFile))
                    {
                        string line;
                        while ((line = reader3.ReadLine()) != null)
                        {
                            var parts = line.Split('|');
                            if (parts.Length == 2)
                            {
                                MasterReg.Add((parts[0], parts[1]));
                            }

                        }
                    }

                    Console.WriteLine("Master loaded from file successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading from file: {ex.Message}");
            }
        }
        static void UsersLists()
        {







        }
        static void UsersBorrowLists ()
        {
            try
            {
                if (File.Exists(BorrowedBook))
                {
                    using (StreamReader reader4 = new StreamReader(BorrowedBook))
                    {
                        string line;
                        while ((line = reader4.ReadLine()) != null)
                        {
                            var parts = line.Split('|');
                            if (parts.Length == 3)
                            {
                                Users.Add((parts[0], parts[1], int.Parse(parts[2])));
                            }

                        }
                    }

                    Console.WriteLine("User loaded from file successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading from file: {ex.Message}");
            }
        }

        static void UsersInfoAppend()
        {
            StringBuilder sb4 = new StringBuilder();

            int BookNumber = 0;

            for (int i = 0; i < Users.Count; i++)
            {
                BookNumber = i + 1;
                sb4.Append("Book ").Append(BookNumber).Append(" name : ").Append(Users[i].User);
                sb4.AppendLine();
                sb4.Append("Book ").Append(BookNumber).Append(" Author : ").Append(Users[i].Bookk);
                sb4.AppendLine();
                sb4.Append("Book ").Append(BookNumber).Append(" ID : ").Append(Users[i].Qnt);
                sb4.AppendLine();
                sb4.Append("Book ").Append(BookNumber).Append(" Quantity : ").Append(Users[i].Qnt);
                sb4.AppendLine();
                sb4.AppendLine();
                Console.WriteLine(sb4.ToString());
                sb4.Clear();

            }
        }
    }
}
        
