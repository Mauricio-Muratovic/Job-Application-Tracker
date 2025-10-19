// -------------------------------------------------------------
// JobManager.cs
// Ansvarar för ALLA ansökningar:
//  - Lagra i en lista
//  - Lägga till / uppdatera / ta bort
//  - LINQ: filtrera, sortera, statistik
// -------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;

public class JobManager
{
    // Listan som håller alla ansökningar
    public List<JobApplication> Applications { get; } = new();

    // Lägger till en ny ansökan i listan
    public void AddJob(JobApplication app)
    {
        Applications.Add(app);
    }

    // Visar alla (returnerar IEnumerable för enkel utskrift)
    public IEnumerable<JobApplication> ShowAll()
    {
        return Applications;
    }

    // (VG) LINQ: filtrera efter status (Where)
    public IEnumerable<JobApplication> ShowByStatus(Status status)
    {
        return Applications.Where(a => a.Status == status);
    }

    // (VG) LINQ: sortera efter ansökningsdatum (OrderBy/OrderByDescending)
    public IEnumerable<JobApplication> OrderByApplicationDate(bool desc = false)
    {
        return desc
            ? Applications.OrderByDescending(a => a.ApplicationDate)
            : Applications.OrderBy(a => a.ApplicationDate);
    }

    // Uppdatera status (och ev. svardatum) för en ansökan.
    // Här identifierar vi ansökan via Company + Position (enkelt för kursnivå).
    public bool UpdateStatus(string companyName, string positionTitle, Status newStatus, DateTime? responseDate = null)
    {
        var app = Applications.FirstOrDefault(a =>
            a.CompanyName.Equals(companyName, StringComparison.OrdinalIgnoreCase) &&
            a.PositionTitle.Equals(positionTitle, StringComparison.OrdinalIgnoreCase));

        if (app == null) return false;

        app.Status = newStatus;
        if (responseDate.HasValue)
            app.ResponseDate = responseDate.Value;

        return true;
    }

    // Ta bort en ansökan
    public bool Remove(string companyName, string positionTitle)
    {
        var app = Applications.FirstOrDefault(a =>
            a.CompanyName.Equals(companyName, StringComparison.OrdinalIgnoreCase) &&
            a.PositionTitle.Equals(positionTitle, StringComparison.OrdinalIgnoreCase));

        if (app == null) return false;

        Applications.Remove(app);
        return true;
    }

    // (VG) LINQ: statistik (Count, GroupBy, Average, Where)
    public void ShowStatistics(out int total,
                               out Dictionary<Status, int> perStatus,
                               out double? averageResponseDays)
    {
        // Totalt antal
        total = Applications.Count;

        // Antal per status (GroupBy + Count)
        perStatus = Applications
            .GroupBy(a => a.Status)
            .ToDictionary(g => g.Key, g => g.Count());

        // Genomsnittlig svarstid i dagar (bara de som har ResponseDate != null)
        var responded = Applications.Where(a => a.ResponseDate.HasValue);
        averageResponseDays = responded.Any()
            ? responded.Average(a => (a.ResponseDate!.Value - a.ApplicationDate).TotalDays)
            : (double?)null;
    }
}
