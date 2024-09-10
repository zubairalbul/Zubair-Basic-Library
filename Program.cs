using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Text;

namespace BasicLibrary
{
    internal class Program
    {
        static string currentUser;
        static List<(int IDS, string BName, string BAuthor, int ID, int Qnt)> Books = new List<(int IDS, string BName, string BAuthor, int ID, int Qnt)>();
        static List<(int Id, string Name, string Email, string Password)> UserReg = new List<(int Id, string Name, string Email, string Password)>();
        static List<(string Email, string Password)> AdminReg = new List<(string Email, string Password)>();
        static List<(string Email, string Password)> MasterReg = new List<(string Email, string Password)>();
        static List<(string User, string BookN, int Quantity)> BookName = new List<(string user, string BookN, int Quantity)>();
        static List<(string User, string Bookk, int Qnt)> Users = new List<(string User, string Bookk, int Qnt)>();
        static List<(string Bname2, int BQNT)> Reccomanded = new List<(string Bname2, int BQNT)>();
        static string filePath = "C:\\Users\\Codeline User\\Desktop\\Zubair\\Lib.txt";
        static string UserFile = "C:\\Users\\Codeline User\\Desktop\\Zubair\\LibUser.txt";
        static string AdminFile = "C:\\Users\\Codeline User\\Desktop\\Zubair\\LibAdmin.txt";
        static string MasterFile = "C:\\Users\\Codeline User\\Desktop\\Zubair\\LibMaster.txt";
        static string BorrowedBook = "C:\\Users\\Codeline User\\Desktop\\Zubair\\LibBorrowed.txt";
        static string UserLists = "C:\\Users\\Codeline User\\Desktop\\Zubair\\UserList.txt";

        static void Main(string[] args)
        {
            ReadAdmins();
            UserReader();
            ReadBooks();
            MasterReader();
            UsersBorrowLists();
            LoadBooksFromFile();
            Console.Clear();
            bool EnterFlag = false;
            do
            {
                Console.WriteLine("Please Select an option: ");
                Console.WriteLine(" 1. Log In. \n 2. Registor as User. \n 3. Riddle. \n 4. Exit. ");
                int Choice = int.Parse(Console.ReadLine());
                switch (Choice)
                {
                    case 1:

                        LogIn();
                        AdminRegestration();

                        break;
                    case 2:
                        AddUser();
                        UserRegestration();
                        break;
                    case 3:
                        Riddle();
                        break;
                    case 4:
                        return;
                        

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
                Console.WriteLine(" 1- Add New Admin. \n 2- Add New User. \n 3- Show Statistics Of Library. \n 4- Show Users List. \n 5- Show Admins List \n 6- Exit");
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

                        Console.Clear();
                        Console.WriteLine("Aveliable Books: \n ");
                        ViewAllBooks();
                        Console.WriteLine("Borrowed Books: \n ");
                        ViewAllBorrowedBooks();

                        break;
                    case 4:
                        
                        UsersListsAppend();
                        
                        
                        break;

                    case 5:
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
                Console.Clear();
            } while (ExFlag != true);

        }// Master Menu Options
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

        } //check the Input to execute the proper menu
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

        }// make it as a resource for adminn appending 
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
                var cont = Console.ReadKey();

                Console.Clear();

            } while (ExitFlag != true);


        }//Admin options menu
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
                        Recomandation();
                        break;

                    case 3:
                        Console.Clear();

                        SearchWithBorrow();

                        break;
                    case 4:
                        Console.Clear();
                        ReturnBook();
                        break;
                        case 5:
                        return;
                       

                    default:
                        Console.Clear();
                        Console.WriteLine("Sorry your choice was wrong");
                        break;

                }

            } while (ExitFlag != true);
        }//User options Menu
        static void AddnNewBook()
        {

            Console.WriteLine("Enter Book Name");
            string name = Console.ReadLine();
            bool BookExist = false;
            for (int i = 0; Books.Count > i; i++) 
            {
                if (Books[i].BName.Contains(name))
                {
                    BookExist = true;

                }
            }
            if (!BookExist)
            {
                 Console.WriteLine("Enter Book Author");
            string author = Console.ReadLine();
             Console.WriteLine("Enter Book ID");
            int ID = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Number of Book Copies");
            int Qnt = int.Parse(Console.ReadLine());

                int IDCounter;
                if (Books.Count > 0)
                {
                    IDCounter = Books[Books.Count - 1].IDS + 1;
                }
                else { IDCounter = 1; }

                Books.Add((IDCounter, name, author, ID, Qnt));

            Console.WriteLine("Book Added Succefully");
            }
            else Console.WriteLine("Book Already Exist");






        }// adding book menu
        static void ViewAllBooks()
        {
            StringBuilder sb = new StringBuilder();

            int BookNumber = 0;

            for (int i = 0; i < Books.Count; i++)
            {
                BookNumber = i + 1;
                sb.Append("Book ID: "+Books[i].IDS);
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
        }// appending the books 
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
           // int BS;
            Console.Clear();

            string h = SearchForBook();
            Console.WriteLine("Do You want to Borrow it? \n 1. Yes \n 2. No");
            int UserChoice = int.Parse(Console.ReadLine());

            if (UserChoice == 1)
            {
               // Console.WriteLine("Enter the quantity: ");
                //BS = int.Parse(Console.ReadLine());

                for (int i = 0; i < Books.Count; i++)
                {
                    if (h == Books[i].BName)
                    {
                        Books[i] = (Books[i].IDS, Books[i].BName, Books[i].BAuthor, Books[i].ID, (Books[i].Qnt - 1));
                        SaveBooksToFile();
                        BookName.Add((currentUser, Books[i].BName, Books[i].Qnt));
                        Borrowedbooks();

                    }
                }
            }
            else if (UserChoice == 2 || UserChoice != 1)
            {
                Console.Clear();
                Console.WriteLine("Thank you For Your Time");
            }
        }
        static void SearchWithBorrow2()
        {
           
            //Console.Clear();

            string h = SearchForBook2();
            Console.WriteLine("Do You want to Borrow it? \n 1. Yes \n 2. No");
            int UserChoice = int.Parse(Console.ReadLine());

            if (UserChoice == 1)
            {
                

                for (int i = 0; i < Books.Count; i++)
                {
                    if (h == Books[i].BName)
                    {
                        Books[i] = (Books[i].IDS, Books[i].BName, Books[i].BAuthor, Books[i].ID, (Books[i].Qnt - 1));
                        SaveBooksToFile();
                        BookName.Add((currentUser, Books[i].BName, Books[i].Qnt));
                        Borrowedbooks();

                    }
                }
            }
            else if (UserChoice == 2 || UserChoice != 1)
            {
                Console.Clear();
                Console.WriteLine("Thank you For Your Time");
            }
        }
        static string SearchForBook()
        {

            Console.WriteLine("Enter the book name you want");
            string name = Console.ReadLine();
            ViewAllBooks();

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
            { Console.WriteLine("book not found"); }
            return null;
        }
        static string SearchForBook2()
        {
            // LoadBooksFromFile();
            // ViewAllBooks();
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
            { Console.WriteLine("book not found"); }
            return null;
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
                            if (parts.Length == 5)
                            {
                                Books.Add((int.Parse(parts[0]), parts[1], parts[2], int.Parse(parts[3]), int.Parse(parts[4])));
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
                        writer.WriteLine($"{book.IDS}|{book.BName}|{book.BAuthor}|{book.ID}|{book.Qnt}");
                    }
                }
                // Console.WriteLine("Books saved to file successfully.");
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
                        Books[i] = (Books[i].IDS, Books[i].BName, Books[i].BAuthor, Books[i].ID, (Books[i].Qnt - BS));

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
                                BookName.Add((parts[0], parts[1], int.Parse(parts[2])));
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
                        Books[i] = (Books[i].IDS, Books[i].BName, Books[i].BAuthor, Books[i].ID, (Books[i].Qnt - 1));
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
                using (StreamWriter writer1 = new StreamWriter(AdminFile))
                {
                    foreach (var admin in AdminReg)
                    {
                        writer1.WriteLine($"{admin.Email}|{admin.Password}");
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
                sb5.Append("Admin").Append(" Email: ").Append(AdminReg[i].Email);
                sb5.AppendLine();
                sb5.AppendLine();
                Console.WriteLine(sb5.ToString());
                sb5.Clear();

            }
        }
        static void AddUser()
        {
            Console.WriteLine("Enter Your Name");
            string UserName=Console.ReadLine();

            Console.WriteLine("Enter User Email");
            string emailUser = Console.ReadLine();

            bool UserExist = false;
            for (int i = 0; UserReg.Count > i; i++) 
            {
                if (UserReg[i].Email.Contains(emailUser))
                {
                    UserExist = true;

                }
            }
            if (!UserExist)
            {
                Console.WriteLine("Enter User Password");
                string UserPass = Console.ReadLine();
                int IDCounter;
                if (UserReg.Count > 0)
                {
                    IDCounter = UserReg[UserReg.Count - 1].Id + 1;
                }
                else { IDCounter = 1; }

                UserReg.Add((IDCounter, UserName, emailUser, UserPass));

                Console.WriteLine("User Added Succefully");
            }
            else Console.WriteLine("User Exist");
        }
        static void UserRegestration()
        {
            try
            {
                using (StreamWriter writer2 = new StreamWriter(UserFile))
                {
                    foreach (var user in UserReg)
                    {
                        writer2.WriteLine($"{user.Id}|{user.Name}|{user.Email}|{user.Password}");
                    }
                }
                //Console.WriteLine("User saved to file successfully.");
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
                if (File.Exists(UserFile))
                {
                    using (StreamReader reader2 = new StreamReader(UserFile))
                    {
                        string line;
                        while ((line = reader2.ReadLine()) != null)
                        {
                            var parts = line.Split('|');
                            if (parts.Length == 4)
                            {
                                UserReg.Add((int.Parse(parts[0]), parts[1], parts[2], parts[3]));
                            }

                        }
                    }

                    //Console.WriteLine("User loaded from file successfully.");
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
        static void UsersListsAppend()// User Details Appending Function.
        {
            StringBuilder sb7 = new StringBuilder();

            int BookNumber = 0;

            for (int i = 0; i < UserReg .Count; i++)
            {
                BookNumber = i + 1;
                sb7.Append("User ID: ").Append(UserReg[i].Id);
                sb7.AppendLine();
                sb7.Append("User Name: ").Append(UserReg[i].Name);
                sb7.AppendLine();
                sb7.Append("User Email: ").Append(UserReg[i].Email);
                sb7.AppendLine();
                sb7.Append("User Password:  ").Append(UserReg[i].Password);
                sb7.AppendLine();
                sb7.AppendLine();
                Console.WriteLine(sb7.ToString());
                sb7.Clear();

            }






        }
        static void UsersBorrowLists()
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
                sb4.Append("User ").Append("Email: ").Append(Users[i].User);
                sb4.AppendLine();

                sb4.Append("Borrowed Book").Append(" Name: ").Append(Users[i].Bookk);
                sb4.AppendLine();
                sb4.Append("Borrowed Book ").Append("Quantity: ").Append(Users[i].Qnt);
                sb4.AppendLine();
                sb4.AppendLine();
                Console.WriteLine(sb4.ToString());
                sb4.Clear();

            }
        }
        static void Recomandation()
        {

            int index1;
            StringBuilder Quant1 = new StringBuilder();
            for (int i = 0; i < BookName.Count; i++)
            {
                Reccomanded.Add((BookName[i].BookN, BookName[i].Quantity));

            }
            Reccomanded.Sort((t1, t2) => t2.BQNT.CompareTo(t1.BQNT));
            for (int i = 0; i < Reccomanded.Count; i++)
            {
                Quant1.AppendLine("Book Name: " + Reccomanded[i].Bname2 + " \nQuantity of Borrowed Books By People: " + Reccomanded[i].BQNT);
                Quant1.AppendLine();

                if (i >= 2) break;

            }
            Console.WriteLine(Quant1.ToString());
            Console.WriteLine("The Most Borrowed Books Until Now Do You Want To Borrow any?. \n 1. Yes. \n 2. No");
            int Select = int.Parse(Console.ReadLine());
            if (Select == 1)
            {

                SearchWithBorrow2();

            }
            else
            {
                Console.Clear();
                Console.WriteLine("Thank you for Time");
            }
        }
        static void Riddle()
        {
            Console.WriteLine("Solve the riddle to enter:)");
            int IqCounter = 1;
            bool GG = false;
            do
            {
                Console.WriteLine("It cannot be seen, cannot be felt,\r\nCannot be heard, cannot be smelt.\r\nIt lies behind stars and under hills,\r\nAnd empty holes it fills.\r\nIt comes out first and follows after,\r\nEnds life, kills laughter. \nAnswer: ");
                string Answer = Console.ReadLine();
                if (Answer == "Dark" || Answer == "dark")
                {
                    Console.WriteLine("You are smart");
                    Console.Clear();
                    MasterMenu();
                    GG = true;
                }
                else
                {
                    if (IqCounter <= 2)
                    {
                        Console.WriteLine($"try again to test you IQ level (number of tries left:{3 - IqCounter}):");
                    }
                    IqCounter++;
                    if (IqCounter >= 4) { Console.WriteLine("its heart breaking to get know that this much stupid you are :( ");
                        Console.ReadKey();
                        Console.Clear();
                        break; }

                }
            } while (GG == false);
        }
    }
}
        
