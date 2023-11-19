using System.IO;

public class FileUtils
{
    public static string ReadString(string filename)
    {
        string s = "";
        StreamReader sr = new StreamReader(filename);
        s = sr.ReadToEnd();
        sr.Close();
        return s;
    }

    public static void WriteString(string filename, string str)
    {
        StreamWriter sr = new StreamWriter(filename);
        sr.Write(str);
        sr.Close();
    }
}