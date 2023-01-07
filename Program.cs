using System.Runtime.Serialization.Formatters.Binary;

namespace FinalTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"C:\\Users\\Admin\\Desktop\\Students.dat";   //путь к базе студентов
            string dirPath = @"C:\\Users\\Admin\\Desktop\\Students\";   //путь создания каталога

            DeserializeToTXT(filePath, dirPath);
        }
        private static void DeserializeToTXT(string filePath, string dirPath)
        {
            var students = GetDeserializedInfo(filePath);

            if(!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);
            foreach (var student in students)
            {
                if (!File.Exists(dirPath + student.Group + ".txt"))
                {
                    using (var writer = File.CreateText(dirPath + student.Group + ".txt"))
                    {
                       writer.Write($"{student.Name}\t{student.DateOfBirth}\n"); 
                    }
                }
                else
                {
                    using (StreamWriter writer = File.AppendText(dirPath + student.Group + ".txt"))
                    {
                        writer.Write($"{student.Name}\t{student.DateOfBirth}\n");
                    }
                }
            }
        }

        private static List<Student> GetDeserializedInfo(string filePath)
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                List<Student> students = new List<Student>();
                if (File.Exists(filePath))
                {
                    using (var fs = new FileStream(@"C:\\Users\\Admin\\Downloads\\Students.dat", FileMode.Open))
                    {
                        students = (((Student[])formatter.Deserialize(fs))).ToList();
                    }
                    return students;
                }
                else
                {
                    Console.WriteLine($"Отсутствует файл: {filePath}!");
                    return new List<Student>();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<Student>();
            }
        }
    }
}