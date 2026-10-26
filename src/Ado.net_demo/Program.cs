using Ado.net_demo;

var connectionString = "server = .\\SQLEXPRESS; Database= database name; User Id= user name; Password: password; Trust Server Certificate = true"; ;

//var sql = "insert int students ([name], cgpa, dateofbirth, isactive) values('azad', 3.5, '2000-02-10', 1)";

var name = "Azad";
var cgpa = 3.5;
var dateofbirth = new DateTime(2000, 1, 1);
var isActive = true;

//wrong way
//var sql = $"insert into students ([name], cgpa, dateofbirth, isactive) values({name}, {cgpa}, {dateofbirth}, {isActive})";

//right way
var sql = $"insert into students ([name], cgpa, dateofbirth, isactive) values(@name, @cgpa, @dob, @isActive)";

Dictionary<string, object> parameters = new Dictionary<string, object>()
{
    {"@name", name },
    {"@cgpa", cgpa },
    {"@dob", dateofbirth },
    {"@isActive", isActive }
};

var deleteSql = "delete from students where name 'Azad'";

SqlUtility sqlUtility = new SqlUtility(connectionString);
sqlUtility.ExecuteSql(sql, parameters);

var query = "select * from students";
List<Dictionary<string, object>> students = sqlUtility.ExecuteQuery(query);

foreach(var student in students)
{
    foreach(var kvp in student)
    {
        Console.Write($"{kvp.Key}: {kvp.Value}");
    }
    Console.WriteLine();
}
