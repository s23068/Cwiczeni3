using Cwiczeni3.Models;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Cwiczeni3.Services
{
    public class IDbServices
    {
        private static String pathToFile = "students.csv";
        public static void Save(List<Students> saveData, bool overwrite)
        {
            var lines = new List<string>();
            IEnumerable<PropertyDescriptor> props = TypeDescriptor.GetProperties(typeof(Students)).OfType<PropertyDescriptor>();
            var header = string.Join(",", props.ToList().Select(x => x.Name));

            var valueLines = saveData.Select(row => string.Join(",", header.Split(',').Select(a => row.GetType().GetProperty(a).GetValue(row, null))));
            lines.AddRange(valueLines);
            if (overwrite == true)
            {
                File.WriteAllLines(pathToFile, lines.ToArray());
            }
            else
            {
                File.AppendAllLines(pathToFile, lines.ToArray());
            }
        }
        public static void Read(List<Students> list)
        {
            var lines = System.IO.File.ReadAllLines(pathToFile);
            foreach (string item in lines)
            {
                var values = item.Split(',');
                list.Add(new Students()
                {
                    Imie = values[0],
                    Nazwisko = values[1],
                    idStudent = values[2],
                    DataUr = values[3],
                    Studia = values[4],
                    Tryb = values[5],
                    Email = values[6],
                    ImieMatki = values[7],
                    ImieOjca = values[8]
                });
            }
        }
        public static void findByIdStudent(List<Students> list, string idStudent)
        {
            var lines = System.IO.File.ReadAllLines(pathToFile);
            foreach (string item in lines)
            {
                if (Regex.IsMatch(item, idStudent))
                {
                    var values = item.Split(',');
                    list.Add(new Students()
                    {
                        Imie = values[0],
                        Nazwisko = values[1],
                        idStudent = values[2],
                        DataUr = values[3],
                        Studia = values[4],
                        Tryb = values[5],
                        Email = values[6],
                        ImieMatki = values[7],
                        ImieOjca = values[8]
                    });
                }
            }
        }
        public static void Delete(List<Students> list, string idStudent)
        {
            var lines = System.IO.File.ReadAllLines(pathToFile);

            foreach (string item in lines)
            {
                if (!Regex.IsMatch(item, idStudent))
                {
                    var values = item.Split(',');
                    list.Add(new Students()
                    {
                        Imie = values[0],
                        Nazwisko = values[1],
                        idStudent = values[2],
                        DataUr = values[3],
                        Studia = values[4],
                        Tryb = values[5],
                        Email = values[6],
                        ImieMatki = values[7],
                        ImieOjca = values[8]
                    });
                }
            }

        }
        public static List<Students> Update(Students updatedStudent, string idStudent)
        {
            List<Students> students = new List<Students>();
            var lines = System.IO.File.ReadAllLines(pathToFile);

            foreach (string item in lines)
            {
                if (!Regex.IsMatch(item, idStudent))
                {
                    var values = item.Split(',');
                    students.Add(new Students()
                    {
                        Imie = values[0],
                        Nazwisko = values[1],
                        idStudent = values[2],
                        DataUr = values[3],
                        Studia = values[4],
                        Tryb = values[5],
                        Email = values[6],
                        ImieMatki = values[7],
                        ImieOjca = values[8]
                    });
                }
                else
                {
                    students.Add(updatedStudent);
                }
            }
            return students;
        }

    }
}
