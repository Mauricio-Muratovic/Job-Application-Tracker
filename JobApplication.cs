// -------------------------------------------------------------
// JobApplication.cs
// EN jobbansökan (ett "objekt"). Håller data + enkla metoder.
// -------------------------------------------------------------
using System;

public class JobApplication
{
    // Företagets namn ("Volvo", "Spotify", ...)
    public string CompanyName { get; set; } = string.Empty;

    // Jobbtitel ("Junior .NET Developer", ...)
    public string PositionTitle { get; set; } = string.Empty;

    // Status för ansökan (enum)
    public Status Status { get; set; } = Status.Applied;

    // När ansökan skickades
    public DateTime ApplicationDate { get; set; }

    // När svar mottogs (kan vara null om inget svar än)
    public DateTime? ResponseDate { get; set; }

    // Önskad lön i kronor
    public int SalaryExpectation { get; set; }

    // Returnerar antal dagar sedan ansökan skickades
    public int GetDaysSinceApplied()
    {
        // .Date för att slippa tid på dagen
        return (DateTime.Now.Date - ApplicationDate.Date).Days;
    }

    // Returnerar en kort textsammanfattning (används i utskrifter)
    public string GetSummary()
    {
        string responseText = ResponseDate.HasValue
            ? $"Svar: {ResponseDate.Value:yyyy-MM-dd}"
            : "Inget svar ännu";

        return $"{CompanyName} - {PositionTitle} | Status: {Status} | Skickad: {ApplicationDate:yyyy-MM-dd} | {responseText} | Lön: {SalaryExpectation} kr";
    }
}
