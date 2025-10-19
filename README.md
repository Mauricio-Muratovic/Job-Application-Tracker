# Job-Application-Tracker
# 💼 Job Application Tracker                                

Namn: Mauricio Muratovic
Datum: 2025-10-19
Kurs: OOP Grund – Introduktion till Systemutveckling

Ett konsolprogram i **C#** som hjälper användaren att hålla koll på sina jobbansökningar.  
Projektet är gjort som en del av kursen **OOP Grund – Introduktion till Systemutveckling**.

---

## 🧠 Projektbeskrivning
Programmet låter användaren:
- Lägga till nya jobbansökningar  
- Visa, ta bort och uppdatera ansökningar  
- Filtrera och sortera data med **LINQ**  
- Visa statistik (antal, genomsnittlig svarstid, per status osv.)  

Programmet är uppbyggt enligt **objektorienterade principer (OOP)** med klasserna:
- `JobApplication` – representerar en ansökan  
- `JobManager` – hanterar alla ansökningar och LINQ-logiken  
- `Program` – innehåller menysystemet  

1️⃣ Hur hjälpte LINQ dig att skriva renare kod?
LINQ gjorde att koden blev mycket enklare att läsa och förstå.  
Istället för att skriva flera loopar och if-satser kunde jag använda korta LINQ-rader som “Where” och “OrderBy” för att filtrera och sortera.  
Det blev både snabbare att skriva och mer organiserat.  

2️⃣ Vilken del av projektet var mest utmanande, och hur löste du den?
Det svåraste var att få menyn och logiken att fungera utan att det blev rörigt.  
I början hade jag allt i Program.cs, men jag delade upp det i klasser för att få bättre struktur.  
Jag använde också små metoder för varje del (t.ex. AddJob, ShowStatistics) så koden blev lättare att testa och förstå.  

3️⃣ Vad lärde jag mig?
Jag lärde mig mycket om hur man använder klasser och objekt i praktiken och hur LINQ kan förenkla hantering av listor.  
Jag lärde mig också hur man använder Git och GitHub med branches och pull requests, vilket känns som ett professionellt arbetssätt.






---

## ⚙️ Hur man kör programmet
1. Klona repot från GitHub eller Ladda ner:  
  https://github.com/Mauricio-Muratovic/Job-Application-Tracker.git
   
