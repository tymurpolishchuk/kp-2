using SQLite;

namespace Lab2.Services;

public static class DatabaseService
{
    public const string DatabaseFilename = "SQLDB.db3";
    private static SQLiteAsyncConnection Database;

    public const SQLite.SQLiteOpenFlags Flags =
        // open the database in read/write mode
        SQLite.SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
        SQLite.SQLiteOpenFlags.Create |
        // enable multi-threaded database access
        SQLite.SQLiteOpenFlags.SharedCache;

    // Update the DatabasePath with the new code
    public static string DatabasePath => System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

    public static async Task Init()
    {
        if (Database is not null)
        {
            Console.WriteLine("Return1");
            return;
        }

        Database = new SQLiteAsyncConnection(Path.Combine(DatabasePath, DatabaseFilename), Flags);
        await Database.CreateTableAsync<ToDoItem>();
        Console.WriteLine("DatabasePath: " + DatabasePath);
        await SaveItemAsync(new ToDoItem
        {
            userId = 1,
            title = "some-title",
            completed = true
        });
        Console.WriteLine("Debug point");
    }

    public static async Task SaveItemAsync(ToDoItem item)
    {
        await Database.InsertAsync(item);
        GetData(item);
    }

    private static void GetData(ToDoItem item)
    {
        Console.WriteLine("User ID: " + item.userId);
        Console.WriteLine("ID: " + item.id);
        Console.WriteLine("Title: " + item.title);
        Console.WriteLine("Completed: " + item.completed);
    }
    
}

public class ToDoItem
{
    public int userId { get; set; }
    public int id { get; set; }
    public string title { get; set; }
    public bool completed { get; set; }
}