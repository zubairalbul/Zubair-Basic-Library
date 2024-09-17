using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Diagnostics.Metrics;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace BasicLibrary
{
    internal class Program
    {
        static int currentUser;
        static List<(int IDS, string BName, string BAuthor, int Qnt, int BorrowedCopies, Decimal Price, string BookCategory, int Period)> Books = new List<(int IDS, string BName, string BAuthor, int Qnt, int BorrowedCopies, decimal Price, string BookCategory, int Period)>();//Books List
        static List<(int Id, string Name, string Email, string Password)> UserReg = new List<(int Id, string Name, string Email, string Password)>();//User List
        static List<(int AdminID, string AdminName, string Email, string Password)> AdminReg = new List<(int AdminID, string AdminName, string Email, string Password)>();//Admin List
        static List<(string Email, string Password)> MasterReg = new List<(string Email, string Password)>();// Master List
        static List<(int CataID, string CataName, int NoBooks)> Catagory = new List<(int CataID, string CataName, int NoBooks)>();//Category List
        static List<(string User, string BookN, int Quantity)> BookName = new List<(string user, string BookN, int Quantity)>();//view All Books
        static List<(string User, string Bookk, int Qnt)> Users = new List<(string User, string Bookk, int Qnt)>();// User List With Borrowed Books
        static List<(int UID, int BID, DateTime BorrowDates, DateTime ReturnDate, string ActualReturnDate, int rating, bool IsReturened)> Borrowing = new List<(int UID, int BID, DateTime BorrowDates, DateTime ReturnDate, string ActualReturnDate, int rating, bool IsReturened)>();
        static List<(string Bname2, int BQNT)> Reccomanded = new List<(string Bname2, int BQNT)>();// Recommendations
        static string filePath = "C:\\Users\\Codeline User\\Desktop\\Zubair\\Lib.txt";//Books File
        static string Categories = "C:\\Users\\Codeline User\\Desktop\\Zubair\\Categories.txt";//Categories File
        static string UserFile = "C:\\Users\\Codeline User\\Desktop\\Zubair\\LibUser.txt";// User Registration Files
        static string AdminFile = "C:\\Users\\Codeline User\\Desktop\\Zubair\\LibAdmin.txt";// Admin Registration File
        static string MasterFile = "C:\\Users\\Codeline User\\Desktop\\Zubair\\LibMaster.txt";// Master Registration 
      
        static string UserLists = "C:\\Users\\Codeline User\\Desktop\\Zubair\\UserList.txt";// Users List
        static string BorrowingFile = "C:\\Users\\Codeline User\\Desktop\\Zubair\\BorrowingFile.txt";//Borrowing File
        static void Main(string[] args)
        {
            ReadBorrowing();
            ReadAdmins();
            UserReader();
            //ReadBooks();
            MasterReader();
            //UsersBorrowLists();
            LoadBooksFromFile();
            Console.Clear();
            bool EnterFlag = false;
            
                do
                {
                    try
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
                    }
                    catch (Exception ex) { Console.WriteLine("Invalid Input"); }
                } while (EnterFlag != true);
                Console.Clear();
            
            
        }
        static void LogIn()
        {
            Console.Clear();
            Console.WriteLine("Enter Your Email (Example For Testing User: george.brown@testmail.com \t Admin:eve.davis@example.com");
            string email = Console.ReadLine();
            Console.WriteLine("Enter Your Password: User Hint: Pass7069 \t Admin Pass Hint: AdminPass5552");
            string password = Console.ReadLine();
            bool flagu = false;
            bool flagM = false;
            bool flagA = false;

            for (int i = 0; i < MasterReg.Count; i++)
            {
                if (email == MasterReg[i].Email.Trim() && password == MasterReg[i].Password.Trim())
                {

                    flagM = true;
                    flagA = false;
                    flagu = false;
                    break;
                }
            }
            for (int i = 0; i < UserReg.Count; i++)
            {
                if (email == UserReg[i].Email.Trim() && password == UserReg[i].Password.Trim())
                {

                    flagA = false;
                    flagM = false;
                    flagu = true;
                    currentUser = UserReg[i].Id;
                    break;
                }
            }
            for (int i = 0; i < AdminReg.Count; i++)
            {
                if (email == AdminReg[i].Email.Trim() && password == AdminReg[i].Password.Trim())
                {

                    flagA = true;
                    flagM = false;
                    flagu = false;
                    break;
                }
            }


            if (!flagu && !flagA && !flagM)
            {
                try
                {
                    Console.WriteLine("Invalid User. Do You want to Register as User? \n 1. Yes. \n 2. No. ");

                    int Reg = int.Parse(Console.ReadLine());

                    if (Reg == 1)
                    {
                        AddUser();
                    }
                    else if (Reg == 2)
                    {
                        Console.WriteLine("Thank you for your time");
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter 1 or 2.");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            }
            else if (flagM == true)
            {
                MasterMenu();
            }
            else if (flagA == true)
            {
                AdminMenu();
            }
            else if (flagu == true)
            {
                UserMenu();
            }

        } //check the Input to execute the proper menu
        static void MasterMenu()
        {
            bool ExFlag = false;
            do
            {
                Console.WriteLine("Please Select Opration Want To Perform");
                Console.WriteLine(" 1- Add New Admin. \n 2- Add New User. \n 3- Show Statistics Of Library. \n 4- Show Users List. \n 5- Show Admins List \n 6- Exit");

                try
                {
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
                            Console.WriteLine("Available Books: \n ");
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
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }

                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                Console.Clear();
            } while (ExFlag != true);
        }// Master Menu Options

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

        }// make it as a resource for admin appending 
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

                try
                {
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
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }

                Console.WriteLine("press any key to continue");
                var cont = Console.ReadKey();

                Console.Clear();
            } while (ExitFlag != true);
        }//Admin options menu
        static void UserMenu()
        {
            //for(int i = 0; i <Borrowing.Count; i++) {
            //    Console.WriteLine($"{Borrowing[i]}");
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

                try
                {
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
                            BorrowBook();
                            
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
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            } while (ExitFlag != true);
        }//User options Menu


        static void AddnNewBook()
        {
            int BorroededCopes = 0;

            try
            {
                Console.WriteLine("Enter Book Name");
                string name = Console.ReadLine();

                bool BookExist = false;
                for (int i = 0; i < Books.Count; i++)
                {
                    if (Books[i].BName.Contains(name))
                    {
                        BookExist = true;
                        break; // Exit the loop early if a match is found
                    }
                }

                if (!BookExist)
                {
                    Console.WriteLine("Enter Book Author");
                    string author = Console.ReadLine();

                    //Console.WriteLine("Enter Book ID");
                    //int ID = int.Parse(Console.ReadLine());

                    Console.WriteLine("Enter Number of Book Copies");
                    int Qnt = int.Parse(Console.ReadLine());

                    Console.WriteLine("Borrowed Copies By Default 0");
                    int Borr = int.Parse(Console.ReadLine());
                    int n = BorroededCopes + Borr;

                    Console.WriteLine("Price of the Book: ");
                    Decimal BookPrice = Decimal.Parse(Console.ReadLine());
                    bool CategoryExist = false;
                    Console.WriteLine("Which Category: ");
                    string BooksCategory = Console.ReadLine();

                    for (int i = 0; i < Books.Count; i++)
                    {
                        if (Books[i].BookCategory.Trim().ToLower().Contains(BooksCategory.ToLower().ToLower()))
                        {
                            CategoryExist = true; break;
                        }
                        
                        
                    }
                   if(!CategoryExist) {
                    
                        Console.WriteLine("Category You Entered Is not Exist: 1. ReEnter. \n 2. Add It As A New Category.");
                        int CateChoice = int.Parse(Console.ReadLine());

                        if (CateChoice != 1)
                        {

                            //Console.WriteLine("Enter the Category Want To Add");
                            //string NewCategory = Console.ReadLine();
                            //Catagory.Add(())
                        }

                        Console.WriteLine("Invalid Input");
                        CategoryExist = false;
                    }

                    Console.WriteLine("Enter the number of days the book can be borrowed: ");
                    int BorrowingPeriod = int.Parse(Console.ReadLine());

                    int IDCounter;
                    if (Books.Count > 0)
                    {
                        IDCounter = Books[Books.Count - 1].IDS + 1;
                    }
                    else
                    {
                        IDCounter = 1;
                    }

                    Books.Add((IDCounter, name, author, Qnt, n, BookPrice, BooksCategory, BorrowingPeriod));

                    Console.WriteLine("Book Added Successfully");
                }
                else
                {
                    Console.WriteLine("Book Already Exists");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        






    }// adding book menu
        static void ViewAllBooks()
        {
            StringBuilder sb = new StringBuilder();

            int BookNumber = 0;
            sb.AppendLine($"|{"User ID:",-8}|{"Book Name: ",-30}|{"Book Author: ",-30}|{"Book Available Copies:",-25}|{"Price:",-11}|{"Category:",-20}|{"Must be returned in:",-30}");
            for (int i = 0; i < Books.Count; i++)
            {
                BookNumber = i + 1;
                sb.AppendLine($"|{Books[i].IDS,-8}|{Books[i].BName,-30}|{Books[i].BAuthor,-30}|{Books[i].Qnt,-25}|{Books[i].BorrowedCopies,-11}|{Books[i].BookCategory,-20}|{Books[i].Period,-30}");



            }
            Console.WriteLine(sb.ToString());
            sb.Clear();
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
            try
            {
                Console.Clear();

                string h = SearchForBook();

                if (h != null) // Check if a book was found
                {
                    Console.WriteLine("Do You want to Borrow it? \n 1. Yes \n 2. No");

                    int UserChoice = int.Parse(Console.ReadLine());

                    if (UserChoice == 1)
                    {
                        // ... (rest of your borrowing logic)
                    }
                    else if (UserChoice == 2 || UserChoice != 1)
                    {
                        Console.Clear();
                        Console.WriteLine("Thank you For Your Time");
                    }
                }
                else
                {
                    Console.WriteLine("Book not found.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        static string SearchForBook()
        {
            try
            {
                ViewAllBooks();
                Console.WriteLine("Enter the book name you want");
                string name = Console.ReadLine();

                bool flag = false;

                for (int i = 0; i < Books.Count; i++)
                {
                    if (Books[i].BName.Trim().ToLower().Contains(name.Trim().ToLower()) )
                    {
                        Console.WriteLine("Book Name: " + Books[i].BName + " Book Author is: " + Books[i].BAuthor + " Book ID: " + Books[i].IDS);
                        flag = true;
                        return name;
                    }
                }

                if (!flag)
                {
                    Console.WriteLine("Book not found");
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
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
                            if (parts.Length == 8)
                            {
                                Books.Add((int.Parse(parts[0]), parts[1], parts[2], int.Parse(parts[3]), int.Parse(parts[4]), decimal.Parse(parts[5]), parts[6], int.Parse(parts[7])));
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
                        writer.WriteLine($"{book.IDS}|{book.BName}|{book.BAuthor}|{book.Qnt}|{book.BorrowedCopies}|{book.Price}|{book.BookCategory}|{book.Period}");
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
            try
            {
                bool flag = false;
                int index = -1; // Use a variable to store the index
                List<(int ID, string Name, string AuthName, int Period)> FoundBooks = new List<(int ID, string Name, string AuthName, int Period)>();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"|{"Book ID:",-8}|{"Book Name: ",-30}|{"Book Author: ",-30}|{"Borrow Period",5}");
                ViewAllBooks();
                Console.Write("Please Enter The Book Name To Search: ");
                string bookName = Console.ReadLine();
                for (int i = 0; i < Books.Count; i++)
                {
                    if (Books[i].BName.Trim().ToLower().Contains(bookName.Trim().ToLower()))
                    {
                        FoundBooks.Add((Books[i].IDS, Books[i].BName, Books[i].BAuthor, Books[i].Period));
                        sb.AppendLine($"|{Books[i].IDS,-8}|{Books[i].BName,-30}|{Books[i].BAuthor,-30}|{Books[i].Period,5}");
                        flag = true;
                    }
                }
                Console.WriteLine(sb.ToString());
                int SelectedBook;
                Console.WriteLine("Enter the ID of the book you want:");
                while ((!int.TryParse(Console.ReadLine(), out SelectedBook))||(SelectedBook < 0))
                {
                    Console.WriteLine("Invalid input, please try again:");
                }
                for (int i = 0; i < Books.Count; i++)
                {
                    if (Books[i].IDS == SelectedBook)
                    {

                        index = i; // Store the index of the found book
                        break;
                    }
                }
                Console.WriteLine("Book Name: " + Books[index].BName + " Book ID: " + Books[index].IDS);
                Console.WriteLine("Borrow? \n 1. Yes \n 2. No");

                int choice = int.Parse(Console.ReadLine());

                if (choice == 1)
                {


                    if (Books[index].BorrowedCopies < Books[index].Qnt) // Check if enough copies are available
                    {
                        Books[index] = (Books[index].IDS, Books[index].BName, Books[index].BAuthor, Books[index].Qnt - 1, Books[index].BorrowedCopies + 1, Books[index].Price, Books[index].BookCategory, Books[index].Period);
                        SaveBooksToFile();
                        //Borrowing = new List<(int UID, int BID, DateTime BorrowDates, DateTime ReturnDate, DateTime ActualReturnDate, int rating, bool IsReturened)>();
                        Borrowing.Add((currentUser, Books[index].IDS, DateTime.Now, DateTime.Now.AddDays(Books[index].Period), "N/A", 0, false));

                        WriteBorrowing();

                        Console.WriteLine("Borrowing Was Succesfully Done.");
                    }
                    else
                    {
                        Console.WriteLine("Insufficient copies available. Only {0} copies are available.", Books[index].Qnt);
                    }
                }
                else if (choice == 2)
                {
                    Console.WriteLine("Thank You For Your Time");
                }
                if (!flag)
                {
                    Console.WriteLine("Book not found.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }//need to be edit
        //static void Borrowedbooks()
        //{
        //    try
        //    {
        //        using (StreamWriter writer4 = new StreamWriter(BorrowedBook, true))
        //        {
        //            foreach (var Bbook in BookName)
        //            {
        //                writer4.WriteLine($"{Bbook.User}|{Bbook.BookN}|{Bbook.Quantity}");
        //            }
        //        }
        //        Console.WriteLine("Books saved to file successfully.");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error saving to file: {ex.Message}");
        //    }
        //}
        //static void ReadBooks()
        //{
        //    try
        //    {
        //        if (File.Exists(BorrowedBook))
        //        {
        //            using (StreamReader reader = new StreamReader(BorrowedBook))
        //            {
        //                string line;
        //                while ((line = reader.ReadLine()) != null)
        //                {
        //                    var parts = line.Split('|');
        //                    if (parts.Length == 3)
        //                    {
        //                        BookName.Add((parts[0], parts[1], int.Parse(parts[2])));
        //                    }

        //                }
        //            }


        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error loading from file: {ex.Message}");
        //    }
        //}
        static void ReturnBook()
        {
            ReadBorrowing();
            StringBuilder sb11 = new StringBuilder();
            sb11.AppendLine($"{"Book ID:",-8}|{"Borrow Date: ",-20}|{"Book Returned by:",-20}|{"Days left for returning:",-20}");
            try
            {
                for (int i = 0; i < Borrowing.Count; i++)
                {
                    if (currentUser == Borrowing[i].UID && Borrowing[i].IsReturened != true)
                    {
                        
                        sb11.AppendLine($"{Borrowing[i].BID,-8}|{Borrowing[i].BorrowDates,-20}|{Borrowing[i].ReturnDate,-20}|{(Borrowing[i].ReturnDate-DateTime.Now  ).Days,-20}");
                    }
                }
                Console.WriteLine(sb11.ToString());
                Console.WriteLine("Enter the ID of the book You want to return: ");
                int Read=int.Parse(Console.ReadLine());
                        for (int i = 0; i < Books.Count; i++)
                {
                    if (Read == Books[i].IDS)
                    {
                       
                        Console.WriteLine("Return? \n 1. Yes \n 2. No");

                        int CS = int.Parse(Console.ReadLine());
                        

                        if (CS == 1)
                        {
                            Console.WriteLine("Please Rate the Book from 1-5:");
                            int rating1 = int.Parse(Console.ReadLine());
                            Books[i] = (Books[i].IDS, Books[i].BName, Books[i].BAuthor, (Books[i].Qnt), (Books[i].BorrowedCopies - 1), Books[i].Price, Books[i].BookCategory, Books[i].Period);
                            for (int i2 = 0; i2 < Borrowing.Count; i2++)
                            {
                                if (Read == Borrowing[i].BID&& currentUser == Borrowing[i].UID&& Borrowing[i].IsReturened==false)
                                {
                                    Borrowing[i] = (Borrowing[i].UID, Borrowing[i].BID, Borrowing[i].BorrowDates, Borrowing[i].ReturnDate, DateTime.Now.ToString(), rating1, true);
                                }
                            }
                            Console.WriteLine("Returning Book Was Succesfully Done.");
                        }
                        else
                        {
                            Console.WriteLine("Thank You For Your Time");
                        }

                        break; // Exit the loop after finding the book
                    }
                }

            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        static void WriteBorrowing()
        {
            try
            {
                using (StreamWriter writer5 = new StreamWriter(BorrowingFile, true))
                {
                    foreach (var Cbook in Borrowing)
                    {
                        
                        writer5.WriteLine($"{Cbook.UID}|{Cbook.BID}|{Cbook.BorrowDates}|{Cbook.ReturnDate}|{Cbook.ActualReturnDate}|{Cbook.rating}|{Cbook.IsReturened}");
                    }
                }
                Console.WriteLine("Books Borrowing Details saved to file successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to file: {ex.Message}");
            }
        }
        static void ReadBorrowing()
        {
            try
            {
                if (File.Exists(BorrowingFile))
                {
                    using (StreamReader reader = new StreamReader(BorrowingFile))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            var parts = line.Split('|');
                            if (parts.Length == 7)
                            {
                                Borrowing.Add((int.Parse(parts[0]), int.Parse(parts[1]), DateTime.Parse(parts[2]), DateTime.Parse(parts[3]), (parts[4]), int.Parse(parts[5]), bool.Parse(parts[6])));
                            }
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Borrowing file not found.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Error parsing data in borrowing file.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reading borrowing data: {ex.Message}");
            }
        }
        static void AppendBorrowing()
        {
            StringBuilder sb8 = new StringBuilder();
            int BowrroedNumber = 0;
            sb8.AppendLine($"|{"User ID:",-8}||{"Book ID: ", -8}||{"Borrow Date: ",-20}||{"Book Returned by:",-20}||{"ARD:",-20}||{"Book Rating:",-12}||{"Status:",-10}|");
          
            for (int i = 0; i < BookName.Count; i++)
            {
                BowrroedNumber = i + 1;
 
                sb8.AppendLine($"|{Borrowing[i].UID,-8}||{Borrowing[i].BID,-8}||{Borrowing[i].BorrowDates,-20}||{Borrowing[i].ReturnDate,-20}||{Borrowing[i].ActualReturnDate,-20}||{Borrowing[i].rating,-12}||{Borrowing[i].IsReturened,-10}|");
                Console.WriteLine(sb8.ToString());
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
                        writer1.WriteLine($"{admin.AdminID}|{admin.AdminName}|{admin.Email}|{admin.Password}");
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
            try
            {
                Console.WriteLine("Enter Admin Name: ");
                string AdminN = Console.ReadLine();

                Console.WriteLine("Enter Admin Email (e.g., example@email.com): ");
                string emailAdmin = Console.ReadLine();

                Console.WriteLine("Enter Admin Password (at least 8 characters, including uppercase, lowercase, numbers, and symbols): ");
                string AdminPass = Console.ReadLine();

                // Validate email format
                if (!IsValidEmail(emailAdmin))
                {
                    Console.WriteLine("Invalid email format. Please enter a valid email address.");
                    return;
                }

                // Validate password format (implement your desired validation logic)
                if (!IsValidPassword(AdminPass))
                {
                    Console.WriteLine("Invalid password format. Please ensure your password is at least 8 characters long and contains uppercase, lowercase, numbers, and symbols.");
                    return;
                }

                int IDCounter;
                if (AdminReg.Count > 0)
                {
                    IDCounter = AdminReg[AdminReg.Count - 1].AdminID + 1;
                }
                else
                {
                    IDCounter = 1;
                }

                AdminReg.Add((IDCounter, AdminN, emailAdmin, AdminPass));

                Console.WriteLine("Admin Added Successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        static bool IsValidEmail(string email)
        {
            // Regular expression for email validation
            const string emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailRegex);
        }
        static bool IsValidPassword(string password)
        {
            // Implement your desired password validation logic here
            // For example, check for minimum length, complexity, etc.
            return password.Length >= 8; // Simple example: minimum 8 characters
        }
        static void ReadAdmins()
        {
            try
            {
                if (File.Exists(AdminFile))
                {
                    using (StreamReader reader1 = new StreamReader(AdminFile))
                    {
                        string line;
                        while ((line = reader1.ReadLine()) != null)
                        {
                            var parts = line.Split('|');
                            if (parts.Length == 4)
                            {
                                AdminReg.Add((int.Parse(parts[0]), parts[1], parts[2], parts[3]));
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
                sb5.Append(" |Admin Email:").Append(AdminReg[i].Email).Append("\t|Admin Password:").Append(AdminReg[i].Password).Append("\t|Admin Name:").Append(AdminReg[i].AdminName);
                sb5.AppendLine();
                sb5.AppendLine();
                Console.WriteLine(sb5.ToString());
                sb5.Clear();

            }
        }


        static void AddUser()
        {
            try
            {
                string UserName;
                string emailUser;
                bool UserExist = false;
                Console.WriteLine("Enter Your Name");
                UserName = Console.ReadLine();
                do
                {
                    

                    Console.WriteLine("Enter User Email");
                    emailUser = Console.ReadLine();

                    // Validate email format
                    if (!IsValidEmail(emailUser))
                    {
                        Console.WriteLine("Invalid email format. Please enter a valid email address.");

                    }

                    
                    for (int i = 0; i < UserReg.Count; i++)
                    {
                        if (UserReg[i].Email.Contains(emailUser))
                        {
                            UserExist = true;
                            Console.WriteLine("Email Already Exist");
                            break;
                        }
                    }
                } while (!IsValidEmail(emailUser)||(UserExist));

                if (!UserExist)
                {

                    string UserPass;

                    int IDCounter;
                    if (UserReg.Count > 0)
                    {
                        IDCounter = UserReg[UserReg.Count - 1].Id + 1;
                    }
                    else
                    {
                        IDCounter = 1;
                    }

                    do
                    {
                        Console.WriteLine("Enter User Password");
                        UserPass = Console.ReadLine();
                        // Validate password format (implement your desired validation logic)
                        if (!IsValidPassword(UserPass))
                        {
                            Console.WriteLine("Invalid password format. Please ensure your password is at least 8 characters long and contains uppercase, lowercase, numbers, and symbols.");

                        }



                        
                    }while (!IsValidPassword(UserPass));
                    
                    UserReg.Add((IDCounter, UserName, emailUser, UserPass));

                    Console.WriteLine("User Added Successfully");
                }
                else
                {
                    Console.WriteLine("User Already Exists");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
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
        //static void UsersBorrowLists()
        //{
        //    try
        //    {
        //        if (File.Exists(BorrowedBook))
        //        {
        //            using (StreamReader reader4 = new StreamReader(BorrowedBook))
        //            {
        //                string line;
        //                while ((line = reader4.ReadLine()) != null)
        //                {
        //                    var parts = line.Split('|');
        //                    if (parts.Length == 3)
        //                    {
        //                        Users.Add((parts[0], parts[1], int.Parse(parts[2])));
        //                    }

        //                }
        //            }

        //            Console.WriteLine("User loaded from file successfully.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error loading from file: {ex.Message}");
        //    }
        //}
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

                //SearchWithBorrow2();

            }
            else
            {
                Console.Clear();
                Console.WriteLine("Thank you for Time");
            }
        }


        //static string SearchForBook2()
        //{
        //    try
        //    {
        //        // LoadBooksFromFile(); // Ensure books are loaded before searching
        //        ViewAllBooks();

        //        Console.WriteLine("Enter the book name you want");
        //        string name = Console.ReadLine();

        //        bool flag = false;

        //        for (int i = 0; i < Books.Count; i++)
        //        {
        //            if (Books[i].BName.Trim().Contains(name.Trim()) )
        //            {
        //                Console.WriteLine("Book Name: " + Books[i].BName + " Book Author is: " + Books[i].BAuthor + " Book ID: " + Books[i].IDS);
        //                flag = true;
        //                return name;
        //            }
        //        }

        //        if (!flag)
        //        {
        //            Console.WriteLine("Book not found");
        //        }
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"An error occurred: {ex.Message}");
        //        return null;
        //    }
        //}
        //static void SearchWithBorrow2()
        //{
        //    try
        //    {
        //        //Console.Clear(); // Uncomment if necessary

        //        string h = SearchForBook2();

        //        if (h != null)
        //        {
        //            Console.WriteLine("Do You want to Borrow it? \n 1. Yes \n 2. No");

        //            int UserChoice = int.Parse(Console.ReadLine());

        //            if (UserChoice == 1)
        //            {
        //                for (int i = 0; i < Books.Count; i++)
        //                {
        //                    if (h == Books[i].BName)
        //                    {
        //                        // Check if the book is available
        //                        if (Books[i].Qnt > 0)
        //                        {
        //                            Books[i] = (Books[i].IDS, Books[i].BName, Books[i].BAuthor, Books[i].Qnt - 1, Books[i].BorrowedCopies + 1, Books[i].Price, Books[i].BookCategory, Books[i].Period);
        //                            SaveBooksToFile();

        //                            Borrowing.Add(Borrowing[i]);
        //                            WriteBorrowing();

        //                            Console.WriteLine("Borrowing Was Succesfully Done.");
        //                        }
        //                        else
        //                        {
        //                            Console.WriteLine("Book is not available for borrowing.");
        //                        }
        //                    }
        //                }
        //            }
        //            else if (UserChoice == 2 || UserChoice != 1)
        //            {
        //                Console.Clear();
        //                Console.WriteLine("Thank you For Your Time");
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine("Book not found.");
        //        }
        //    }
        //    catch (FormatException)
        //    {
        //        Console.WriteLine("Invalid input. Please enter a valid number.");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"An error occurred: {ex.Message}");
        //    }
        //}
        static void Riddle()
        {
            try
            {
                Console.WriteLine("Solve the riddle to enter:");
                int IqCounter = 1;
                bool GG = false;

                do
                {
                    Console.WriteLine("It cannot be seen, cannot be felt,\r\nCannot be heard, cannot be smelt.\r\nIt lies behind stars and under hills,\r\nAnd empty holes it fills.\r\nIt comes out first and follows after,\r\nEnds life, kills laughter. \nAnswer: ");

                    string Answer = Console.ReadLine();

                    if (Answer.Trim().ToLower() == "dark") // Case-insensitive comparison
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

                        if (IqCounter >= 4)
                        {
                            Console.WriteLine("It's heartbreaking to get know that this much stupid you are :(");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        }
                    }
                } while (GG == false);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
        
