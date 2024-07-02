public class FileHandler
{
    private string _filePath;

    public FileHandler(string filePath)
    {
        _filePath = filePath;
    }

    public void Create()
    {
        if (File.Exists(_filePath)) return; 

        if (!Directory.Exists(Path.GetDirectoryName(_filePath))) Directory.CreateDirectory(Path.GetDirectoryName(_filePath));

        File.Create(_filePath).Close();
    }

    public void WriteLine(string lineToWrite)
    {
        if (!Exists()) Create();

        using (StreamWriter writetext = File.AppendText(_filePath))
        {
            writetext.WriteLine(lineToWrite);
        }
    }

    public bool Exists()
    {
        return File.Exists(_filePath);
    }

    public IEnumerable<String> ReadAllLines()
    {
        if (!Exists()) throw new Exception($"Tried accessing a file that does not exist: {_filePath}");

        foreach (var line in File.ReadAllLines(_filePath))
        {
            yield return line;
        }
    }

    public void DeleteFile()
    {
        try
        {
            File.Delete(_filePath);
        }
        catch (Exception)
        {}
    }
}