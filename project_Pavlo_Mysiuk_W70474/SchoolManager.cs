using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using project_Pavlo_Mysiuk_W70474.SchoolManagement;

public class SchoolManager
{

    private List<Student> students = new List<Student>();
    private List<Teacher> teachers = new List<Teacher>();
    private int studentCounter = 1;
    private int teacherCounter = 1;

    private const string StudentsFile = "students.json";
    private const string TeachersFile = "teachers.json";
    private const string StudentGradesFile = "studentGrades.json";
    private const string TeacherSubjectsFile = "teacherSubjects.json";
    private const string AttendanceFile = "attendance.json";

    public SchoolManager()
    {
        LoadData();
    }

    public void SaveData()
    {
        try
        {
            string studentsJson = JsonSerializer.Serialize(students, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(StudentsFile, studentsJson);

            string teachersJson = JsonSerializer.Serialize(teachers, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(TeachersFile, teachersJson);

            SaveStudentGrades();
            SaveTeacherSubjects();

            Console.WriteLine("Dane zapisano do plików JSON.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Wystąpił błąd podczas zapisu danych: {ex.Message}");
        }
    }

    private void LoadData()
    {
        try
        {
            if (File.Exists(StudentsFile))
            {
                string studentsJson = File.ReadAllText(StudentsFile);
                students = JsonSerializer.Deserialize<List<Student>>(studentsJson) ?? new List<Student>();
                studentCounter = students.Count + 1;
            }

            if (File.Exists(TeachersFile))
            {
                string teachersJson = File.ReadAllText(TeachersFile);
                teachers = JsonSerializer.Deserialize<List<Teacher>>(teachersJson) ?? new List<Teacher>();
                teacherCounter = teachers.Count + 1;
            }

            LoadStudentGrades();
            LoadTeacherSubjects();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Wystąpił błąd podczas wczytywania danych: {ex.Message}");
        }
    }

    private void SaveStudentGrades()
    {
        try
        {
            var gradesData = students.ToDictionary(
                student => student.Id,
                student => student.Grades
            );

            string gradesJson = JsonSerializer.Serialize(gradesData, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(StudentGradesFile, gradesJson);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Wystąpił błąd podczas zapisu ocen: {ex.Message}");
        }
    }

    private void LoadStudentGrades()
    {
        try
        {
            if (File.Exists(StudentGradesFile))
            {
                string gradesJson = File.ReadAllText(StudentGradesFile);
                var gradesData = JsonSerializer.Deserialize<Dictionary<string, List<Grade>>>(gradesJson);

                if (gradesData != null)
                {
                    foreach (var student in students)
                    {
                        if (gradesData.TryGetValue(student.Id, out var grades))
                        {
                            student.Grades = grades;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Wystąpił błąd podczas wczytywania ocen: {ex.Message}");
        }
    }

    private void SaveTeacherSubjects()
    {
        try
        {
            var subjectsData = teachers.ToDictionary(
                teacher => teacher.Id,
                teacher => teacher.Subjects
            );

            string subjectsJson = JsonSerializer.Serialize(subjectsData, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(TeacherSubjectsFile, subjectsJson);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Wystąpił błąd podczas zapisu przedmiotów nauczycieli: {ex.Message}");
        }
    }

    private void LoadTeacherSubjects()
    {
        try
        {
            if (File.Exists(TeacherSubjectsFile))
            {
                string subjectsJson = File.ReadAllText(TeacherSubjectsFile);
                var subjectsData = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(subjectsJson);

                if (subjectsData != null)
                {
                    foreach (var teacher in teachers)
                    {
                        if (subjectsData.TryGetValue(teacher.Id, out var subjects))
                        {
                            teacher.Subjects = subjects;
                        }
                    }
                }
            }
        }


        catch (Exception ex)
        {
            Console.WriteLine($"Wystąpił błąd podczas wczytywania przedmiotów nauczycieli: {ex.Message}");
        }
    }



    public void AddStudent(string firstName, string lastName, string className)
    {
        var student = new Student(studentCounter.ToString(), firstName, lastName, className);
        students.Add(student);
        studentCounter++;
        SaveData();
        Console.WriteLine($"Dodano ucznia: {student.Id} - {firstName} {lastName}, klasa {className}");
    }

    public void AddTeacher(string firstName, string lastName)
    {
        var teacher = new Teacher(teacherCounter.ToString(), firstName, lastName);
        teachers.Add(teacher);
        teacherCounter++;
        SaveData();
        Console.WriteLine($"Dodano nauczyciela: {teacher.Id} - {firstName} {lastName}");
    }

    public void AddGradeToStudent(string studentId, string subject, double grade)
    {
        var student = students.FirstOrDefault(s => s.Id == studentId);
        if (student != null)
        {
            student.AddGrade(subject, grade);
            SaveData();
            Console.WriteLine($"Dodano ocenę {grade} z przedmiotu {subject} uczniowi {student.FirstName} {student.LastName}");
        }
        else
        {
            Console.WriteLine("Nie znaleziono ucznia o podanym ID.");
        }
    }

    public void RegisterAttendance(string studentId, DateTime date, bool isPresent)
    {
        var student = students.FirstOrDefault(s => s.Id == studentId);
        if (student != null)
        {
            student.RegisterAttendance(date, isPresent);
            SaveData();
            Console.WriteLine($"Zarejestrowano obecność dla ucznia {student.FirstName} {student.LastName}");
        }
        else
        {
            Console.WriteLine("Nie znaleziono ucznia o podanym ID.");
        }
    }

    public double GetStudentAverageGrade(string studentId)
    {
        var student = students.FirstOrDefault(s => s.Id == studentId);
        if (student != null)
        {
            return student.GetAverageGrade();
        }
        Console.WriteLine("Nie znaleziono ucznia o podanym ID.");
        return 0.0;
    }

    public int GetStudentAttendanceCount(string studentId)
    {
        var student = students.FirstOrDefault(s => s.Id == studentId);
        if (student != null)
        {

            return student.Attendances.Count(a => a.IsPresent);
        }
        Console.WriteLine("Nie znaleziono ucznia o podanym ID.");
        return 0;
    }

    public bool RemoveStudent(string studentId)
    {
        var student = students.FirstOrDefault(s => s.Id == studentId);
        if (student != null)
        {
            students.Remove(student);
            SaveData();
            return true;
        }
        return false;
    }

    public bool RemoveTeacher(string teacherId)
    {
        var teacher = teachers.FirstOrDefault(t => t.Id == teacherId);
        if (teacher != null)
        {
            teachers.Remove(teacher);
            SaveData();
            return true;
        }
        return false;
    }

    public bool ChangeStudentClass(string studentId, string newClassName)
    {
        var student = students.FirstOrDefault(s => s.Id == studentId);
        if (student != null)
        {
            student.ClassName = newClassName;
            SaveData();
            return true;
        }
        return false;
    }

    public bool ChangeTeacherSubject(string teacherId, string newSubject)
    {
        var teacher = teachers.FirstOrDefault(t => t.Id == teacherId);
        if (teacher != null)
        {
            teacher.AddSubject(newSubject);

            SaveData();
            return true;
        }
        return false;
    }
    public void DisplayStudents()
    {
        if (students.Count == 0)
        {
            Console.WriteLine("Brak uczniów w systemie.");
            return;
        }

        Console.WriteLine("Lista uczniów:");
        foreach (var student in students)
        {
            Console.WriteLine($"ID: {student.Id}, Imię: {student.FirstName}, Nazwisko: {student.LastName}, Klasa: {student.ClassName}");
        }
    }
    public void DisplayTeachers()
    {
        if (teachers.Count == 0)
        {
            Console.WriteLine("Brak nauczycieli w systemie.");
            return;
        }

        Console.WriteLine("Lista nauczycielów:");
        foreach (var teacher in teachers)
        {
            Console.WriteLine($"ID: {teacher.Id}, Imię: {teacher.FirstName}, Nazwisko: {teacher.LastName}, Przedmioty: {string.Join(", ", teacher.Subjects)}");
        }
    }
    public void DisplayStudentGrades(string studentId)
    {
        var student = students.FirstOrDefault(s => s.Id == studentId);
        if (student == null)
        {
            Console.WriteLine("Nie znaleziono ucznia o podanym ID.");
            return;
        }

        Console.WriteLine($"Oceny ucznia {student.FirstName} {student.LastName} (ID: {student.Id}):");
        if (student.Grades.Count == 0)
        {
            Console.WriteLine("Brak ocen.");
            return;
        }

        var groupedGrades = student.Grades.GroupBy(g => g.Subject);
        foreach (var group in groupedGrades)
        {
            Console.WriteLine($"Przedmiot: {group.Key}");
            foreach (var grade in group)
            {
                Console.WriteLine($" - Ocena: {grade.Value}");
            }
        }
    }
    public void AddSubjectToTeacher(string teacherId, string subject)
    {
        var teacher = teachers.FirstOrDefault(t => t.Id == teacherId);
        if (teacher != null)
        {
            teacher.AddSubject(subject);
            SaveData();
        }
        else
        {
            Console.WriteLine("Nie znaleziono nauczyciela o podanym ID.");
        }


    }
    public bool RemoveSubjectFromTeacher(string teacherId, string subject)
    {
        var teacher = teachers.FirstOrDefault(t => t.Id == teacherId);
        if (teacher != null && teacher.Subjects.Contains(subject))
        {
            teacher.Subjects.Remove(subject);
            SaveData();
            return true;
        }
        return false;
    }

}
