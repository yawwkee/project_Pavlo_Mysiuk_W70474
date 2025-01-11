using project_Pavlo_Mysiuk_W70474.SchoolManagement;

class Program
{
    static void Main(string[] args)
    {
        var schoolManager = new SchoolManager();

        bool running = true;
        //
        while (running)
        {
            Console.Clear();
            Console.WriteLine("Szkoła - Zarządzanie");
            Console.WriteLine("1. Dodaj ucznia");
            Console.WriteLine("2. Dodaj nauczyciela");
            Console.WriteLine("3. Dodaj ocenę");
            Console.WriteLine("4. Rejestruj obecność");
            Console.WriteLine("5. Wyświetl średnią ocen ucznia");
            Console.WriteLine("6. Wyświetl liczbę obecności ucznia");
            Console.WriteLine("7. Usuń ucznia");
            Console.WriteLine("8. Usuń nauczyciela");
            Console.WriteLine("9. Zmień klasę ucznia");
            Console.WriteLine("10. Dodaj przedmiot nauczycielowi");
            Console.WriteLine("11. Usuń przedmiot nauczyciela");
            Console.WriteLine("12. Wyświetl listę uczniów");
            Console.WriteLine("13. Wyświetl listę nauczycieli");
            Console.WriteLine("14. Wyświetl oceny ucznia");
            Console.WriteLine("0. Wyjście");
            Console.Write("Wybierz opcję: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddStudent(schoolManager);
                    break;
                case "2":
                    AddTeacher(schoolManager);
                    break;
                case "3":
                    AddGrade(schoolManager);
                    break;
                case "4":
                    RegisterAttendance(schoolManager);
                    break;
                case "5":
                    ShowStudentAverage(schoolManager);
                    break;
                case "6":
                    ShowStudentAttendance(schoolManager);
                    break;
                case "7":
                    RemoveStudent(schoolManager);
                    break;
                case "8":
                    RemoveTeacher(schoolManager);
                    break;
                case "9":
                    ChangeStudentClass(schoolManager);
                    break;
                case "10":
                    AddSubjectToTeacher(schoolManager);
                    break;
                case "11":
                    RemoveSubjectFromTeacher(schoolManager);
                    break;

                case "12":
                    schoolManager.DisplayStudents();
                    break;
                case "13":
                    schoolManager.DisplayTeachers();
                    break;
                case "14":
                    Console.Write("Podaj ID ucznia: ");
                    string studentId = Console.ReadLine();
                    schoolManager.DisplayStudentGrades(studentId);
                    break;

                case "0":
                    schoolManager.SaveData();
                    running = false;
                    break;

                default:
                    Console.WriteLine("Nieprawidłowy wybór!");
                    break;

            }

            Console.WriteLine("\nNaciśnij dowolny klawisz, aby kontynuować...");
            Console.ReadKey();
        }
    }

    static void AddStudent(SchoolManager manager)
    {
        Console.Write("Imię ucznia: ");
        string firstName = Console.ReadLine();
        Console.Write("Nazwisko ucznia: ");
        string lastName = Console.ReadLine();
        Console.Write("Klasa: ");
        string className = Console.ReadLine();
        manager.AddStudent(firstName, lastName, className);
    }

    static void AddTeacher(SchoolManager manager)
    {
        Console.Write("Imię nauczyciela: ");
        string firstName = Console.ReadLine();
        Console.Write("Nazwisko nauczyciela: ");
        string lastName = Console.ReadLine();
        manager.AddTeacher(firstName, lastName);
    }

    static void AddGrade(SchoolManager manager)
    {
        Console.Write("ID ucznia: ");
        string studentId = Console.ReadLine();
        Console.Write("Przedmiot: ");
        string subject = Console.ReadLine();
        Console.Write("Ocena: ");
        double grade = Convert.ToDouble(Console.ReadLine());
        manager.AddGradeToStudent(studentId, subject, grade);
    }

    static void RegisterAttendance(SchoolManager manager)
    {
        Console.Write("ID ucznia: ");
        string studentId = Console.ReadLine();
        Console.Write("Data (YYYY-MM-DD): ");
        DateTime date = DateTime.Parse(Console.ReadLine());
        Console.Write("Czy obecny? (t/n): ");
        bool isPresent = Console.ReadLine().ToLower() == "t";
        manager.RegisterAttendance(studentId, date, isPresent);
    }

    static void ShowStudentAverage(SchoolManager manager)
    {
        Console.Write("ID ucznia: ");
        string studentId = Console.ReadLine();
        double avg = manager.GetStudentAverageGrade(studentId);
        Console.WriteLine($"Średnia ocen: {avg}");
    }

    static void ShowStudentAttendance(SchoolManager manager)
    {
        Console.Write("ID ucznia: ");
        string studentId = Console.ReadLine();
        int attendanceCount = manager.GetStudentAttendanceCount(studentId);
        Console.WriteLine($"Liczba obecności: {attendanceCount}");
    }

    static void RemoveStudent(SchoolManager manager)
    {
        Console.Write("ID ucznia do usunięcia: ");
        string studentId = Console.ReadLine();
        if (manager.RemoveStudent(studentId))
            Console.WriteLine("Uczeń usunięty.");
        else
            Console.WriteLine("Nie znaleziono ucznia.");
    }

    static void RemoveTeacher(SchoolManager manager)
    {
        Console.Write("ID nauczyciela do usunięcia: ");
        string teacherId = Console.ReadLine();
        if (manager.RemoveTeacher(teacherId))
            Console.WriteLine("Nauczyciel usunięty.");
        else
            Console.WriteLine("Nie znaleziono nauczyciela.");
    }

    static void ChangeStudentClass(SchoolManager manager)
    {
        Console.Write("ID ucznia: ");
        string studentId = Console.ReadLine();
        Console.Write("Nowa klasa: ");
        string newClass = Console.ReadLine();
        if (manager.ChangeStudentClass(studentId, newClass))
            Console.WriteLine("Klasa ucznia zmieniona.");
        else
            Console.WriteLine("Nie znaleziono ucznia.");
    }

    static void RemoveSubjectFromTeacher(SchoolManager manager)
    {
        Console.Write("ID nauczyciela: ");
        string teacherId = Console.ReadLine();
        Console.Write("Przedmiot do usunięcia: ");
        string subjectToRemove = Console.ReadLine();

        if (manager.RemoveSubjectFromTeacher(teacherId, subjectToRemove))
            Console.WriteLine("Przedmiot nauczyciela usunięty.");
        else
            Console.WriteLine("Nie znaleziono nauczyciela lub przedmiotu.");
    }

    static void ShowStudentGrades(SchoolManager manager)
    {
        Console.Write("ID ucznia: ");
        string studentId = Console.ReadLine();
        manager.DisplayStudentGrades(studentId);
    }
    static void AddSubjectToTeacher(SchoolManager manager)
    {
        Console.Write("ID nauczyciela: ");
        string teacherId = Console.ReadLine();
        Console.Write("Przedmiot: ");
        string subject = Console.ReadLine();

        manager.AddSubjectToTeacher(teacherId, subject);
    }
}

