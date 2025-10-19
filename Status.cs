// -------------------------------------------------------------
// Status.cs
// En enum (uppräknad typ) med fasta statusvärden för ansökan.
// En enum är bra för att slippa felstavningar i strängar.
// -------------------------------------------------------------
public enum Status
{
    Applied,    // Ansökan skickad
    Interview,  // Intervju inbokad / genomförd
    Offer,      // Jobberbjudande
    Rejected    // Avslag
}
