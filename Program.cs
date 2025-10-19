// -------------------------------------------------------------
// Program.cs
// Menysystem i while-loop + switch.
// Sköter all input/output och anropar JobManager för logiken.
// -------------------------------------------------------------
using System;
using System.Globalization;
using System.Linq;

internal class Program
{
    private static readonly JobManager manager = new JobManager();

    private static void Main()
    {
        // (Frivilligt) lite testdata så du ser något direkt
        SeedDemoData();

        while (true)
        {
            Console.WriteLine("\n--- Job Application Tracker ---");
            Console.WriteLine("1) Lägg till ny ansökan");
            Console.WriteLine("2) Visa alla ansökningar");
            Console.WriteLine("3) Filtrera ansökningar efter status (LINQ) (VG)");
            Console.WriteLine("4) Sortera ansökningar efter datum (OrderBy) (VG)");
            Console.WriteLine("5) Visa statistik (Count, GroupBy, Average) (VG)");
            Console.WriteLine("6) Uppdatera status på en ansökan");
            Console.WriteLine("7) Ta bort en ansökan");
            Console.WriteLine("0) Avsluta");
            Console.Write("Val: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddJobFlow();
                    break;
                case "2":
                    PrintList(manager.ShowAll());
                    break;
                case "3":
                    FilterByStatusFlow();
                    break;
                case "4":
                    SortByDateFlow();
                    break;
                case "5":
                    ShowStatsFlow();
                    break;
                case "6":
                    UpdateStatusFlow();
                    break;
                case "7":
                    RemoveFlow();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Ogiltigt val.");
                    break;
            }
        }
    }

    // ---------------- Hjälpmetoder för menyflöden ----------------

    private static void AddJobFlow()
    {
        Console.Write("Företag: ");
        string company = Console.ReadLine() ?? string.Empty;

        Console.Write("Jobbtitel: ");
        string title = Console.ReadLine() ?? string.Empty;

        Status status = ReadStatus("Status (Applied, Interview, Offer, Rejected): ");

        DateTime appDate = ReadDate("Ansökningsdatum (yyyy-MM-dd): ");

        Console.Write("Svar mottaget? (j/n): ");
        bool hasResponse = (Console.ReadLine() ?? "").Trim().ToLower() == "j";
        DateTime? respDate = null;
        if (hasResponse)
        {
            respDate = ReadDate("Svardatum (yyyy-MM-dd): ");
        }

        int salary = ReadInt("Önskad lön (kr): ");

        var app = new JobApplication
        {
            CompanyName = company,
            PositionTitle = title,
            Status = status,
            ApplicationDate = appDate,
            ResponseDate = respDate,
            SalaryExpectation = salary
        };

        manager.AddJob(app);
        Console.WriteLine("Ansökan tillagd!");
    }

    private static void FilterByStatusFlow()
    {
        Status status = ReadStatus("Visa status (Applied, Interview, Offer, Rejected): ");
        var list = manager.ShowByStatus(status);
        PrintList(list);
    }

    private static void SortByDateFlow()
    {
        Console.Write("Sortera nyast först? (j/n): ");
        bool desc = (Console.ReadLine() ?? "").Trim().ToLower() == "j";

        var list = manager.OrderByApplicationDate(desc);
        PrintList(list);
    }

    private static void ShowStatsFlow()
    {
        manager.ShowStatistics(out int total, out var perStatus, out double? avgDays);

        Console.WriteLine($"Totalt antal ansökningar: {total}");
        foreach (var kv in perStatus.OrderBy(k => k.Key))
        {
            Console.WriteLine($" - {kv.Key}: {kv.Value}");
        }

        if (avgDays.HasValue)
            Console.WriteLine($"Genomsnittlig svarstid (dagar): {avgDays.Value:F1}");
        else
            Console.WriteLine("Genomsnittlig svarstid: ingen har svarat ännu.");
    }

    private static void UpdateStatusFlow()
    {
        Console.Write("Företag (att uppdatera): ");
        string company = Console.ReadLine() ?? string.Empty;

        Console.Write("Jobbtitel (att uppdatera): ");
        string title = Console.ReadLine() ?? string.Empty;

        Status newStatus = ReadStatus("Ny status (Applied, Interview, Offer, Rejected): ");

        Console.Write("Vill du ange svardatum nu? (j/n): ");
        DateTime? resp = null;
        if ((Console.ReadLine() ?? "").Trim().ToLower() == "j")
        {
            resp = ReadDate("Svardatum (yyyy-MM-dd): ");
        }

        bool ok = manager.UpdateStatus(company, title, newStatus, resp);
        Console.WriteLine(ok ? "Uppdaterat." : "Hittade ingen ansökan som matchar.");
    }

    private static void RemoveFlow()
    {
        Console.Write("Företag (att ta bort): ");
        string company = Console.ReadLine() ?? string.Empty;

        Console.Write("Jobbtitel (att ta bort): ");
        string title = Console.ReadLine() ?? string.Empty;

        bool ok = manager.Remove(company, title);
        Console.WriteLine(ok ? "Borttagen." : "Hittade ingen ansökan som matchar.");
    }

    // ---------------- In-/utdatahjälp ----------------

    private static Status ReadStatus(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string s = (Console.ReadLine() ?? "").Trim();

            if (Enum.TryParse<Status>(s, ignoreCase: true, out var status))
                return status;

            Console.WriteLine("Ogiltig status. Tillåtna: Applied, Interview, Offer, Rejected.");
        }
    }

    private static DateTime ReadDate(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string s = (Console.ReadLine() ?? "").Trim();

            // Strikt format: yyyy-MM-dd (enkelt och tydligt)
            if (DateTime.TryParseExact(s, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out DateTime dt))
                return dt;

            Console.WriteLine("Ogiltigt datum. Använd format yyyy-MM-dd, t.ex. 2025-10-19.");
        }
    }

    private static int ReadInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string s = (Console.ReadLine() ?? "").Trim();

            if (int.TryParse(s, out int value))
                return value;

            Console.WriteLine("Ogiltigt heltal, försök igen.");
        }
    }

    private static void PrintList(System.Collections.Generic.IEnumerable<JobApplication> list)
    {
        bool any = false;
        foreach (var a in list)
        {
            any = true;
            Console.WriteLine(a.GetSummary());
        }

        if (!any)
            Console.WriteLine("(Inget att visa)");
    }

    // ---------------- Demo-data (valfritt) ----------------
    private static void SeedDemoData()
    {
        manager.AddJob(new JobApplication
        {
            CompanyName = "Volvo",
            PositionTitle = "Backend Developer",
            Status = Status.Applied,
            ApplicationDate = DateTime.Now.Date.AddDays(-10),
            SalaryExpectation = 38000
        });

        manager.AddJob(new JobApplication
        {
            CompanyName = "Spotify",
            PositionTitle = "Junior .NET Developer",
            Status = Status.Interview,
            ApplicationDate = DateTime.Now.Date.AddDays(-20),
            ResponseDate = DateTime.Now.Date.AddDays(-12),
            SalaryExpectation = 40000
        });

        manager.AddJob(new JobApplication
        {
            CompanyName = "IKEA",
            PositionTitle = "Fullstack .NET",
            Status = Status.Rejected,
            ApplicationDate = DateTime.Now.Date.AddDays(-30),
            ResponseDate = DateTime.Now.Date.AddDays(-25),
            SalaryExpectation = 37000
        });
    }
}
