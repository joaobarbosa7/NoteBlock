using NoteBlock.Domain.Models;
using NoteBlock.Infrastructure;

//Menu

var exit = false;

while (!exit)
{
    Console.Clear();
    Console.WriteLine("------- Menu -------");
    Console.WriteLine(" 1 - Manage Users");
    Console.WriteLine(" 2 - Manage Notes");
    Console.WriteLine(" 3 - Manage Reminders");
    Console.WriteLine(" 4 - Manage Categories");
    Console.WriteLine(" 0 - Exit");

    string[] validOptions = { "1", "2", "3", "4", "0" };
    var option = ReadValidInput(validOptions);


    switch (option)
    {
        case "0":
            exit = true;
            break;
        case "1":
            ManageUsers();
            WaitByClick();
            break;
        case "2":
            ManageNotes();
            WaitByClick();
            break;
        case "3":
            ManageReminders();
            WaitByClick();
            break;
        case "4":
            ManageCategories();
            WaitByClick();
            break;
        default:
            break;
    }
}

static void WaitByClick()
{
    Console.WriteLine("Press any key to continue: ");
    Console.ReadLine();
}

static void ManageUsers()
{
    Console.Clear();
    Console.WriteLine("------- Manage Users -------");
    Console.WriteLine();
    Console.WriteLine("All Admin Users:");
    ShowAllAdminUsersAsync();
    Console.WriteLine();
    Console.WriteLine("All Common Users:");
    ShowAllCommonUsersAsync();
    Console.WriteLine();

    Console.WriteLine("------- Menu -------");
    Console.WriteLine(" C - Create new User");
    Console.WriteLine(" U - Update User");
    Console.WriteLine(" D - Delete User");

    var validOptions = CommonUsersIdListAsync().Result;
    validOptions.Add("c");
    validOptions.Add("u");
    validOptions.Add("d");
    var option = ReadValidInput(validOptions.ToArray());

    switch (option)
    {
        case "c":
            CreateUserAsync();
            break;

        case "u":
            UpdateCommonUserAsync();
            break;

        case "d":
            DeleteCommonUserAsync();
            break;

        default:
            break;
    }
}

static async Task<List<string>> CommonUsersIdListAsync()
{
    List<string> idList = new List<string>();

    using (var uow = new UnitOfWork())
    {
        var listOfCommonUsers = await uow.CommonUserRepository.FindAllAsync();

        foreach (var commonUser in listOfCommonUsers)
        {
            idList.Add(commonUser.Id.ToString());
        }
    }
    
    return idList;
}

static async void ShowAllCommonUsersAsync()
{
    using (var uow = new UnitOfWork())
    {
        var listOfCommonUsers = await uow.CommonUserRepository.FindAllAsync();

        foreach (var commonUser in listOfCommonUsers)
        {
            Console.WriteLine($"Id: {commonUser.Id} || Name: {commonUser.Name} || E-mail: {commonUser.Email}");
        }
    }
}

static async void ShowAllAdminUsersAsync()
{
    using (var uow = new UnitOfWork())
    {
        var listOfAdminUsers = await uow.AdminUserRepository.FindAllAsync();

        foreach (var adminUser in listOfAdminUsers)
        {
            Console.WriteLine($"Id: {adminUser.Id} || Name: {adminUser.Name} || E-mail: {adminUser.Email} || Employee Number: {adminUser.EmployeeNumber}");
        }
    }
}

static async void CreateUserAsync()
{
    using (var uow = new UnitOfWork())
    {
        var exit = false;
        bool isAdmin = false;

        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("------- Create new User -------");
            Console.WriteLine(" 1 - Create new Common User");
            Console.WriteLine(" 2 - Create new Admin User");

            string[] validOptions = { "1", "2"};
            var option = ReadValidInput(validOptions);

            switch (option)
            {
                case "1":
                    isAdmin = false;
                    exit = true;
                    break;

                case "2":
                    isAdmin = true;
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Invalid Option. Please choose a valid option.");
                    Console.WriteLine("Press any key to continue: ");
                    Console.ReadLine();
                    break;
            }
        }

        var name = ReadNonEmptyInput("Enter User Name: ");
        var email = ReadNonEmptyInput("Enter User Email: ");
        var password = ReadNonEmptyInput("Enter User Password: ");

        if (isAdmin)
        {
            var employeeNumber = ReadNonEmptyInput("Enter Employee Number: ");
            var user = new AdminUser { Name = name, Email = email, Password = password, EmployeeNumber = employeeNumber };
            uow.AdminUserRepository.Create(user);
        }
        else
        {
            var user = new CommonUser { Name = name, Email = email, Password = password};
            uow.CommonUserRepository.Create(user);
        }

        await uow.SaveAsync();

        Console.WriteLine("\nUser Created!");
    }
}

static async void UpdateCommonUserAsync()
{
    using (var uow = new UnitOfWork())
    {
        Console.Clear();
        Console.WriteLine("------- Select User by ID -------");
        ShowAllCommonUsersAsync();

        var validOptions = CommonUsersIdListAsync().Result;
        var option = ReadValidInput(validOptions.ToArray());

        Console.Clear() ;
        var name = ReadNonEmptyInput("Enter User Name: ");
        var email = ReadNonEmptyInput("Enter User Email: ");
        var password = ReadNonEmptyInput("Enter User Password: ");

        var user = new CommonUser {Id = int.Parse(option), Name = name, Email = email, Password = password };
        uow.CommonUserRepository.Update(user);
        await uow.SaveAsync();

        Console.WriteLine("\nUser Updated!");
    }
}

static async void DeleteCommonUserAsync()
{
    using (var uow = new UnitOfWork())
    {
        Console.Clear();
        Console.WriteLine("------- Select User to Delete by ID -------");
        ShowAllCommonUsersAsync();

        var validOptions = CommonUsersIdListAsync().Result;
        var option = ReadValidInput(validOptions.ToArray());

        var userToDelete = await uow.CommonUserRepository.FindByIdAsync(int.Parse(option));
        uow.CommonUserRepository.Delete(userToDelete);
        await uow.SaveAsync();

        Console.WriteLine("\nUser Deleted!");
    }
}

static void ManageNotes()
{
    Console.Clear();
    Console.WriteLine("------- Manage Notes -------");
    Console.WriteLine();
    Console.WriteLine("All Notes:");
    ShowAllNotesAsync();
    Console.WriteLine();

    Console.WriteLine("------- Menu -------");
    Console.WriteLine(" C - Create new Note");

    string[] validOptions = { "c"};
    var option = ReadValidInput(validOptions.ToArray());

    switch (option)
    {
        case "c":
            CreateNoteAsync();
            break;

        default:
            break;
    }
}

static async void ShowAllNotesAsync()
{
    using (var uow = new UnitOfWork())
    {
        var listOfNotes = await uow.NoteRepository.FindAllWithDependenciesAsync();

        foreach (var note in listOfNotes)
        {
            Console.WriteLine($"Id: {note.Id} || Title: {note.Title} || Content: {note.Content} || User Name: {note.CommonUser.Name} || Creation Date: {note.CreationDate} || Last Modification Date: {note.LastModificationDate} ||  Is Favorite: {note.IsFavorite} || Is Filled: {note.IsFiled}");
            Console.Write("Categories:");
            foreach (var noteCategory in note.NoteCategories)
            {
                Console.Write($" {noteCategory.Category.Name}");
            }
            Console.WriteLine();
        }
    }
}

static async void CreateNoteAsync()
{
    using (var uow = new UnitOfWork())
    {
        Console.Clear();
        Console.WriteLine("------- Enter User Id -------");

        ShowAllCommonUsersAsync();
        Console.WriteLine();
        var validOptions = CommonUsersIdListAsync().Result;
        var option = ReadValidInput(validOptions.ToArray());

        var title = ReadNonEmptyInput("Enter Note Title: ");
        var content = ReadNonEmptyInput("Enter Note Content: ");
        var categoryName = ReadNonEmptyInput("Enter Category Name: ");

        var newCategory = new Category { Name = categoryName };
        var newNote = new Note
        {
            Title = title,
            Content = content,
            NoteCategories = new List<NoteCategory>
            {
                new NoteCategory
                {
                    Category = newCategory
                }
            }
        };

        var user = uow.CommonUserRepository.FindByIdWithDependenciesAsync(int.Parse(option));
        user.Result.Notes.Add(newNote);
        uow.CategoryRepository.Create(newCategory);
        uow.NoteRepository.Create(newNote);


        await uow.SaveAsync();

        Console.WriteLine("\nNote Created!");
    }
}

static void ManageReminders()
{
    Console.Clear();
    Console.WriteLine("------- Manage Reminders -------");
    Console.WriteLine();
    Console.WriteLine("All Reminders:");
    ShowAllRemindersAsync();
    Console.WriteLine();

    Console.WriteLine("------- Menu -------");
    Console.WriteLine(" C - Create new Reminder");

    string[] validOptions = { "c" };
    var option = ReadValidInput(validOptions.ToArray());

    switch (option)
    {
        case "c":
            CreateReminderAsync();
            break;

        default:
            break;
    }
}

static async void ShowAllRemindersAsync()
{
    using (var uow = new UnitOfWork())
    {
        var listOfReminders = await uow.ReminderRepository.FindAllWithDependenciesAsync();

        foreach (var reminder in listOfReminders)
        {
            Console.WriteLine($"Id: {reminder.Id} || Title: {reminder.Title} || User Name: {reminder.CommonUser.Name} || Creation Date: {reminder.CreationDate} || Last Modification Date: {reminder.LastModificationDate} || Is Expired: {reminder.IsExpired}");
            Console.Write("Categories:");
            foreach (var reminderCategory in reminder.ReminderCategories)
            {
                Console.Write($" {reminderCategory.Category.Name}");
            }
            Console.WriteLine();
        }
    }
}

static async void CreateReminderAsync()
{
    using (var uow = new UnitOfWork())
    {
        Console.Clear();
        Console.WriteLine("------- Enter User Id -------");

        ShowAllCommonUsersAsync();
        Console.WriteLine();
        var validOptions = CommonUsersIdListAsync().Result;
        var option = ReadValidInput(validOptions.ToArray());

        var title = ReadNonEmptyInput("Enter Reminder Title: ");
        var categoryName = ReadNonEmptyInput("Enter Category Name: ");

        var newCategory = new Category { Name = categoryName };
        var newReminder = new Reminder
        {
            Title = title,
            ReminderCategories = new List<ReminderCategory>
            {
                new ReminderCategory
                {
                    Category = newCategory
                }
            }
        };

        var user = uow.CommonUserRepository.FindByIdWithDependenciesAsync(int.Parse(option));
        user.Result.Reminders.Add(newReminder);
        uow.CategoryRepository.Create(newCategory);
        uow.ReminderRepository.Create(newReminder);


        await uow.SaveAsync();

        Console.WriteLine("\nReminder Created!");
    }
}

static void ManageCategories()
{
    Console.Clear();
    Console.WriteLine("------- Manage Categories -------");
    Console.WriteLine();
    Console.WriteLine("All Categories:");
    ShowAllCategoriesAsync();
    Console.WriteLine();


}

static async void ShowAllCategoriesAsync()
{
    using (var uow = new UnitOfWork())
    {
        var listOfCategories = await uow.CategoryRepository.FindAllWithDependenciesAsync();

        foreach (var category in listOfCategories)
        {
            Console.WriteLine($"Id: {category.Id} || Name: {category.Name}");
        }
    }
}

static string ReadNonEmptyInput(string prompt)
{
    string input;
    do
    {
        Console.Write(prompt);
        input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("The field cannot be empty. Please enter valid text.");
        }

    } while (string.IsNullOrWhiteSpace(input));

    return input;
}

static string ReadValidInput(string[] validOptions)
{
    string input;
    bool isValidInput;

    do
    {
        Console.Write("\nSelect Option: ");
        input = Console.ReadLine().ToLower();

        isValidInput = validOptions.Contains(input);

        if (!isValidInput)
        {
            Console.WriteLine("Invalid input. Please enter a valid option.");
        }

    } while (!isValidInput);

    return input;
}

