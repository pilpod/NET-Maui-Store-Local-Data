using People.Models;
using SQLite;

namespace People;

public class PersonRepository
{
    string _dbPath;

    public string StatusMessage { get; set; }

    private SQLiteConnection conn;


    private void Init()
    {
        if (conn != null) return;

        conn = new SQLiteConnection(_dbPath);
        conn.CreateTable<Person>();

    }

    public PersonRepository(string dbPath)
    {
        _dbPath = dbPath;
    }

    public void AddNewPerson(string name)
    {
        int result = 0;
        try
        {
            Init();

            // basic validation to ensure a name was entered
            if (string.IsNullOrEmpty(name))
                throw new Exception("Valid name required");

            // TODO: Insert the new person into the database
            Person person = new Person() { Name = name };
            result = conn.Insert(person);

            StatusMessage = string.Format("{0} record(s) added (Name: {1})", result, name);
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
        }

    }

    public List<Person> GetAllPeople()
    {
        // TODO: Init then retrieve a list of Person objects from the database into a list
        try
        {
            Init();
            List<Person> peoples = conn.Table<Person>().ToList();
            return peoples;
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }

        return new List<Person>();
    }
}
