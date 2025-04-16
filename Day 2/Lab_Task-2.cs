// Apply SOLID design principles on the following code samples for better design

    // 1.
    // a. Based on specifications, we need to create an interface and a TeamLead class to implement it.
    // b. Later another role like Manager, who assigns tasks to TeamLead and will not work on the tasks, is introduced into the system,
    // Apply needed refactoring to for better design and mention the current design smells
#region case 1
    public Interface ILead
    {  
        void CreateSubTask();  
        void AssginTask();  
        void WorkOnTask();
    }
    public class TeamLead : ILead
{
    public void AssignTask()
    {
        // create a task
        Task t = new Task() { Title = "Merge and Deploy", Description = "Task to merge and deploy sharing feature to develop" };

        //Code to assign a task. 
        t.AssignTo(new Developer() { Name = "Developer1" });
    }
    public void CreateSubTask()
    {
        //Code to create a sub task  
    }
    public void WorkOnTask()
    {
        //Code to implement perform assigned task.  
    }
}

#endregion

#region sol 1
// Manager and TeamLead have common functionality but manager is not working on task
//-- modify : use ISP

public interface IRole
    {
       void CreateSubTask();
       void AssginTask(); 
    }
    public class manager : IRole
    {
    }
    public Interface ILead : IRole
    {  
        void CreateSubTask();  
        void AssginTask();  
        void WorkOnTask();
    }
    public class TeamLead : ILead
    {
    public void AssignTask()
    {
        // create a task
        Task t = new Task() { Title = "Merge and Deploy", Description = "Task to merge and deploy sharing feature to develop" };

        //Code to assign a task. 
        t.AssignTo(new Developer() { Name = "Developer1" });
    }
    public void CreateSubTask()
    {
        //Code to create a sub task  
    }
    public void WorkOnTask()
    {
        //Code to implement perform assigned task.  
    }
}

#endregion
// 2. Client need to build an application to manage data using group of SQL files.
// a. we need to develop load text and save text functionalities for group of SQL files in the application directory.
// b. we need a manager class that manages the load and saves the text of group of SQL files along with the SqlFile Class.
#region case 2
public class SqlFile
    {
        public string FilePath { get; set; }
        public string FileText { get; set; }
        public string LoadText()
        {
            /* Code to read text from sql file */
        }
        public string SaveText()
        {
            /* Code to save text into sql file */
        }
    }
    public class SqlFileManager
    {
        public List<SqlFile> lstSqlFiles { get; set}

        public string GetTextFromFiles()
        {
            StringBuilder objStrBuilder = new StringBuilder();
            foreach (var objFile in lstSqlFiles)
            {
                objStrBuilder.Append(objFile.LoadText());
            }
            return objStrBuilder.ToString();
        }
        public void SaveTextIntoFiles()
        {
            foreach (var objFile in lstSqlFiles)
            {
                objFile.SaveText();
            }
        }
    }
#endregion

#region sol 2
// sqlFileManager depend on concrete implementation not abstraction 
//-- modify : use DIP
//-- SQLFILE has many responsiblity + have many logic
//-- use SRP and Interface Segrigation principle 
public interface ILoad
    {
        string LoadText();
    }
    public class Load : ILoad
    {
       public string LoadText()
       {
           /* Code to read text from sql file */
       }
    }
    public interface ISave
    {
        string SaveText();
    }
    public interface Save
    {
       public string SaveText()
       {
           /* Code to save text into sql file */
       }
    }
    public class SqlFile 
    {
        public string FilePath { get; set; }
        public string FileText { get; set; }
    }
public class SqlFileManager
{
    public List<SqlFile> lstSqlFiles { get; set}
    private readonly ILoad loader;
    private readonly ISave saver;
    public SqlFileManager(ILoad loader, ISave saver)
    {
        this.loader = loader;
        this.saver = saver;
        lstSqlFiles = new List<SqlFile>();
    }
    public string GetTextFromFiles()
    {
        StringBuilder objStrBuilder = new StringBuilder();
        foreach (var objFile in lstSqlFiles)
        {
            objFile.FileText = loader.LoadText(objFile.FilePath);
            objStrBuilder.Append(objFile.FileText);
        }
        return objStrBuilder.ToString();
    }
    public void SaveTextIntoFiles()
    {
        foreach (var objFile in lstSqlFiles)
        {
            saver.SaveText();
        }
    }
}
#endregion

// c. New Requirement:
// After some time our leaders might tell us that we may have a few read-only files in the application folder, 
// so we need to restrict the flow whenever it tries to do a save on them.
#region case 3
public class SqlFile
{
    public string FilePath { get; set; }
    public string FileText { get; set; }
    public string LoadText()
    {
        /* Code to read text from sql file */
    }
    public void SaveText()
    {
        /* Code to save text into sql file */
    }
}
public class ReadOnlySqlFile : SqlFile
{
    public string LoadText()
    {
        /* Code to read text from sql file */
    }
    public void SaveText()
    {
        /* Throw an exception when app flow tries to do save. */
        throw new IOException("Can't Save");
    }
}
#endregion

#region sol 3
// Voliate LSP because we are using inheritance and when calling SaveText() method on ReadOnlySqlFile it will throw an exception.
// -- modify use bool isReadOnly
public interface ILoad
{
    string LoadText();
}
public class Load : ILoad
{
    public string LoadText()
    {
        /* Code to read text from sql file */
    }
}
public interface ISave
{
    string SaveText();
}
public interface Save
{
    public string SaveText()
    {
        /* Code to save text into sql file */
    }
}
public class SqlFile
{
    public string FilePath { get; set; }
    public string FileText { get; set; }
    public bool IsReadOnly { get; set; } // New property to check if the file is read-only
}
public class SqlFileManager
{
    public List<SqlFile> lstSqlFiles { get; set}
    private readonly ILoad loader;
    private readonly ISave saver;
    public SqlFileManager(ILoad loader, ISave saver)
    {
        this.loader = loader;
        this.saver = saver;
        lstSqlFiles = new List<SqlFile>();
    }
    public string GetTextFromFiles()
    {
        StringBuilder objStrBuilder = new StringBuilder();
        foreach (var objFile in lstSqlFiles)
        {
            objFile.FileText = loader.LoadText(objFile.FilePath);
            objStrBuilder.Append(objFile.FileText);
        }
        return objStrBuilder.ToString();
    }
    public void SaveTextIntoFiles()
    {
        foreach (var objFile in lstSqlFiles)
        {
            if(objFile.IsReadOnly)
                continue; // Skip saving if the file is read-only
            saver.SaveText();
        }
    }
}
#endregion

// d. To avoid an exception we need to modify "SqlFileManager" by adding one condition to the loop.
public class SqlFileManager
    {
        public List<SqlFile> lstSqlFiles { get; set; }

        public string GetTextFromFiles()
        {
            StringBuilder objStrBuilder = new StringBuilder();
            foreach (var objFile in lstSqlFiles)
            {
                objStrBuilder.Append(objFile.LoadText());
            }
            return objStrBuilder.ToString();
        }
        public void SaveTextIntoFiles()
        {
            foreach (var objFile in lstSqlFiles)
            {
                //Check whether the current file object is read-only or not.If yes, skip calling it's  
                // SaveText() method to skip the exception.  

                if (!objFile is ReadOnlySqlFile)
                    objFile.SaveText();
            }
        }
    }
#region sol 4
// aready make in sol 3 
//-- modify : use bool isReadOnly
#endregion

//e. Apply needed refactoring to for better design and mention the current design smells

