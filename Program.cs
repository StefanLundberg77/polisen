/*

RAPPORTSYSTEM
    1 registrera utryckning
        TYP
        PLATS
        TIDPUNKT
        POLISER(vilka)
    2 rapport
        RAPPORTNR
        DATUM
        POLISSTATION
        BESKRIVNING
    3 personal
        REGISTER
            namn
            tjänstenummer
    4 SAMMANSTÄLLNING
        utryckningar
        rapporter
        personal

Bygg menysystem
1 - regga utryck
2 - generera rapport?
3 - hantera personal
4 - printa listor för utryck, rapport, personal. 
(ska tex man kunna söka händelser via en polis, plats, tid osv?)

Klasser? personal, utryckning, rapport


*/
using System.ComponentModel;
using System.Diagnostics;

class Program
{
    static void Main(string[] args) 
    {   
        
        //Lista med objekt med testpoliser. kanske ska ligga i metoden? Göm i JSON sen
        Roster newOfficer = new Roster();
        newOfficer.Add("Stefan", "Lundberg", 12);
        newOfficer.Add("Erik", "Jansson", 14);
        newOfficer.Add("Lina", "Bengtsson", 10);
        newOfficer.Add("Olle", "Åberg", 4);

        //Lite startvärden
        int reportNr = 0;
        int dispatchNr = 0;
        int badgeNr = 0;
        //För ev. autogenerering av rapport


        DateTime time = DateTime.MinValue;
        
        Dispatch newDisp = new Dispatch();
        
        newDisp.AddDispatch("Brand", "Haninge kyrka", time, "Lina Bengtsson", 2);
        newDisp.AddDispatch("Misshandel", "Södra hamngatan 11", time, "Linn Andersson", 1);
        newDisp.AddDispatch("Stöld", "Kista centrum", time, "Erik Ovarsson", 2);

        Report newRepo = new Report();
        
        newRepo.AddReport(15, "Rövarne shivvade Micke i magen", time, "Rovlanda");
        newRepo.AddReport(16, "Olle i skoga ramlade i backen", time, "Enskede");
        newRepo.AddReport(17, "Inbrott i källare på citrongatan", time, "Alingsås polisstation");
        newRepo.AddReport(18, "Bilbrand på birgittagatn misstanke om mordbrand", time, "Alingsås polisstation");
        
        bool isRunning = true;
        while(isRunning)
        {   
            Console.Clear();
            Console.WriteLine("Välkommen till Rapportsystem-80");
            Console.WriteLine("========= Huvudmeny ==========");
            Console.WriteLine();
            Console.WriteLine("Vad vill du göra?");
            Console.WriteLine();
            Console.WriteLine("1. Hantera utryckning.");
            Console.WriteLine("2. Hantera rapport.");
            Console.WriteLine("3. Hantera personal.");
            Console.WriteLine("4. Sammanställningar.");
            Console.WriteLine("5. Avsluta.");
            Console.Write("Ange val (1-5): ");
            string choice = Console.ReadLine();

            
            if (choice == "1")
            {   
                bool InnerMenu = true;
                while (InnerMenu)
                {   
                    Console.Clear();
                    Console.WriteLine("Utryckningar");
                    Console.WriteLine("=== Meny ===");
                    Console.WriteLine();
                    Console.WriteLine("1. Ny utryckning.");
                    Console.WriteLine("2. Se utryckningsloggen.");
                    Console.WriteLine("3. Tillbaks till huvudmeny.");
                    Console.WriteLine("Ange val:");
                    string input = Console.ReadLine();

                    switch(input)
                    {
                        
                        case "1":
                        {   
                            // lägg till så jag kan få me åäö 
                            Console.WriteLine();

                            Console.WriteLine("Ange brottstyp:");
                            string type = Console.ReadLine();
                                        
                            Console.WriteLine("Ange plats:");
                            string location = Console.ReadLine();
   
                            Console.WriteLine("Ange datum (år/mån/dag):");
                            string date = Console.ReadLine();

                            Console.WriteLine("Ange klockslag (timmar:minuter):");
                            string hour = Console.ReadLine();
                            while (!DateTime.TryParse(date + " " + hour, out time))
                            {
                                Console.WriteLine("Felaktigt datum- eller tidsformat.");
                                Console.WriteLine("Ange datum (åååå/mm/dd):");
                                date = Console.ReadLine();

                                Console.WriteLine("Ange klockslag (timmar:minuter):");
                                hour = Console.ReadLine();
                            }                               
                            string dispatchofficer = "";
                            Console.WriteLine("Tillgängliga poliser");
                            newOfficer.Print();
                            Console.WriteLine("Ange tjänstenummer för att lägga till");
                            string badgeInput = Console.ReadLine();
                            if (int.TryParse(badgeInput, out badgeNr))
                            {   
                                //polis som matchar tjänstenummer
                                Officer officerToAdd = newOfficer.rosterItem.Find(Officer => Officer.BadgeNr == badgeNr);
                                
                                Console.WriteLine($"Vill du lägga till {officerToAdd.FirstName}, {officerToAdd.LastName} J/N?");
                                
                                input = Console.ReadLine().ToUpper();

                                if (input == "J")
                                {
                                    dispatchofficer = $"{officerToAdd.FirstName} {officerToAdd.LastName}";
                                    Console.WriteLine($"{dispatchofficer} tillagd.");
                                }
                                else if (input == "N")
                                {
                                    Console.WriteLine("Åtgärd avbruten.");
                                }
                                else
                                {
                                    Console.WriteLine("Ogiltigt val.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Ogiltigt tjänstenummer. Försök igen.");
                            }
                            //string Dispatchofficer = Console.ReadLine(); // borde synka med roster och lägga till därifrån typ med badgenr och ev. lägga till fler och kanske search-polis inte i registret?
                            
                            dispatchNr++;
                            
                            //Borde detta generera rapport med en mandatory beskrivning?
                            newDisp.AddDispatch(type, location, time, dispatchofficer, dispatchNr);
                            
                            break;
                        }
                        case "2":
                        {   
                            Console.Clear();
                            Console.WriteLine("Utryckningshistorik");
                            Console.WriteLine();
                            newDisp.PrintDispatch();
                            Console.WriteLine();
                            Console.WriteLine("Tryck valfri tangent för att gå tillbaks till meny.");
                            Console.ReadKey();
                            break;
                        }
                        case "3":
                        {
                            Console.WriteLine("Tillbaks till huvudmeny.");
                            Thread.Sleep(1000);
                            InnerMenu = false;
                            break;
                        }
                        default:
                        {
                            Console.WriteLine("Ogiltigt val.");
                            Thread.Sleep(1000);    
                            break;
                        }
                    }    
                }
            }
            else if (choice == "2")
            {   
                while (true)
                {

                    Console.Clear();
                    Console.WriteLine("Rapport");
                    Console.WriteLine("== meny ==");
                    Console.WriteLine("1. Skapa ny rapport.");
                    Console.WriteLine("2. Lista rapporter.");
                    Console.WriteLine("3. Sök rapport.");
                    Console.WriteLine("4. Redigera rapport.");
                    Console.WriteLine("5. Tillbaks till huvudmenyn.");
                    Console.WriteLine("Ange val:");
                    string input = Console.ReadLine();
                    if (input == "1")
                    {
                        Console.Clear();

                        //skapa system för utryckningar som saknar rapport.
                        // ex. utryckningar som saknar rapport listas här
                        //lägg till rapporterad utryckning i listan.
                        Console.WriteLine("Ange polisstation:");
                        string station = Console.ReadLine();
                        Console.WriteLine("Ange beskrivning:");
                        string description = Console.ReadLine();
                        Console.WriteLine("Ange datum");
                        string date = Console.ReadLine();
                        while (!DateTime.TryParse(date, out time))
                        {
                            Console.WriteLine("Felaktigt datum- eller tidsformat.");
                            Console.WriteLine("Ange datum (åååå/mm/dd):");
                            date = Console.ReadLine();
                        }
                        reportNr = dispatchNr + reportNr;
                        //reportNr++;
                        newRepo.AddReport(reportNr, station, time, description);//RAPPORTNR DATUM POLISSTATION  BESKRIVNING

                    }
                    else if (input == "2")
                    {   
                        Console.Clear();
                        Console.WriteLine("Rapportlista:");
                        Console.WriteLine();
                        newRepo.Print();
                        Console.WriteLine();
                        Console.WriteLine("Tryck valfri tangent för att gå tillbaks till meny.");
                        Console.ReadKey();
                        //Thread.Sleep(1000);
                    }
                    else if (input == "5")
                    {
                        break;
                    }
                }

            }
            else if(choice == "3")
            {
                while (true)
                {   
                    
                    Console.Clear();
                    Console.WriteLine("Personalregistret");
                    Console.WriteLine("====== Meny =====");
                    Console.WriteLine();
                    Console.WriteLine("1. Lägga till ny personal.");
                    Console.WriteLine("2. Ta bort personal.");
                    Console.WriteLine("3. Visa Personalregister.");
                    Console.WriteLine("4. Visa Personalarkiv");
                    Console.WriteLine("5. Tillbaks till huvudmenyn.");
                    Console.WriteLine("Ange val:");
                    string input = Console.ReadLine();
                    if(input == "1")
                    {
                        badgeNr++;
                        Console.WriteLine("Ange förnamn:");
                        string firstName = Console.ReadLine();
                        
                        Console.WriteLine("Ange efternamn:");
                        string lastName = Console.ReadLine();
                        
                        //Console.WriteLine("Ange tjänstenummer:"); //Gör om till ngt vettigare nr som genereras med list.Count +1000? eller första boksta = siffra osv.typ
                        newOfficer.Add(firstName, lastName, badgeNr);
                        
                        Console.WriteLine("Tryck valfri tangent för att gå tillbaks till meny.");
                        Console.ReadKey();
                        //Thread.Sleep(1000);
                    }
                    else if(input == "2")
                    {
                        Console.Clear();
                        newOfficer.Print();
                        Console.WriteLine();
                        Console.WriteLine("Ange tjänstenummer på personal som skall strykas från register:");

                        string badgeInput = Console.ReadLine();
                        
                        if (int.TryParse(badgeInput, out badgeNr))
                        {
                            Console.Write($"Är du säker på att du vill arkivera J/N? ");
                            input = Console.ReadLine().ToUpper();

                            if (input == "J")
                            {
                                newOfficer.ArchiveOfficer(badgeNr);
                            }
                            else if (input == "N")
                            {
                                Console.WriteLine("Arkivering avbruten.");
                            }
                            else
                            {
                                Console.WriteLine("Ogiltigt val.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Ogiltigt tjänstenummer. Försök igen.");
                        }
                    }
                    else if(input == "3")
                    {   
                        Console.Clear();
                        Console.WriteLine("Personalregister:");
                        Console.WriteLine();
                        newOfficer.Print();
                        Console.WriteLine();
                        Console.WriteLine("Tryck valfri tangent för att gå tillbaks till meny.");
                        Console.ReadKey();
                        //Thread.Sleep(1000);
                    }
                    else if(input == "4")
                    {   
                        Console.Clear();
                        Console.WriteLine("Personalarkivet:");
                        Console.WriteLine();
                        newOfficer.PrintArchive();
                        Console.WriteLine();
                        Console.WriteLine("Tryck valfri tangent för att gå tillbaks till meny.");
                        Console.ReadKey();
                    }
                    else if(input == "5")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Ogiltigt val.");
                    }
                }
            }
            else if (choice == "4")
            {   
                Console.Clear();
                Console.WriteLine("Sammanställningar.");
                Console.WriteLine();
                Console.WriteLine("Obehandlade utryckningar:");
                newDisp.PrintDispatch();
                Console.WriteLine();
                Console.WriteLine("Rapporter:");
                newRepo.Print();
                Console.WriteLine();
                Console.WriteLine("Peronalregister:");
                newOfficer.Print();
                Console.WriteLine();
                Console.WriteLine("Peronalarkiv");
                newOfficer.PrintArchive();
                Console.ReadKey();
            }
            else if (choice == "5")
            {
                Console.Write("Är du säker på att du vill avsluta J/N?");
                string input = Console.ReadLine().ToUpper();// fixa To Upper
                
                if (input == "J")
                {
                    break;
                }
                else if (input == "N")
                {
                    continue;
                }
                else
                {
                    Console.WriteLine("Ogiltigt val.");
                    Thread.Sleep(1000);
                }
            }

            else
            {
                Console.WriteLine("Ogiltigt val.");
                Thread.Sleep(1000);
            }
        }
    }
}
//EN polis med ett namn och ett nr
public class Officer
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int BadgeNr { get; set; }
    
    public Officer(string firstName, string lastName, int badgeNr)
    {
        FirstName = firstName;
        LastName = lastName;
        BadgeNr = badgeNr;    
    }
}
//En rapporthub med egenskaper för utryckningar och rapporter
public class ReportHub
{   
    public string Type { get; set; }
    public string Location { get; set; }
    public DateTime Time { get; set; } // fixa datetime
    public string DispatchOfficer { get; set; }
    public string Station { get; set; }
    public int DispatchNr { get; set; } //en egenskap för att framöver fixa ngn funktion för rapportnrgenerering?
    public int ReportNr { get; set;}
    public string Description { get; set; }
    
   /*public ReportHub(string type, string location, DateTime time, string dispatchofficer, string station, int dispatchNr, int reportNr, string description) //kan jag ta bort hela denna?
    {
        Type = type;
        Location = location;
        Time = time; 
        Station = station;
        DispatchOfficer = dispatchofficer;
        DispatchNr = dispatchNr; 
        ReportNr = reportNr;
        Description = description;
        
    }*/
    public ReportHub(string type, string location, DateTime time, string dispatchofficer, int dispatchNr)//utryckningskonstruktor
    {
        Type = type;
        Location = location;
        Time = time;
        DispatchOfficer = dispatchofficer;
        DispatchNr = dispatchNr;//en egenskap för att framöver fixa ngn funktion för rapportnrgenerering?
    }
        // Ska innehålla RAPPORTNR DATUM POLISSTATION  BESKRIVNING
    public ReportHub(int reportNr, string description, DateTime time, string station)//rapportkonstruktor
    {
        ReportNr = reportNr;
        Description = description;
        Time = time;
        Station = station;
    }
}
public class Dispatch
{   
    public List<ReportHub> dispatches { get; set; }

    public Dispatch()
    {
        dispatches = new List<ReportHub>();
    }
    
    public void AddDispatch(string type, string location, DateTime time, string dispatchofficer, int dispatchNr)
    {
        ReportHub newDisp = new ReportHub(type, location, time, dispatchofficer, dispatchNr); // Skapa objekt
        dispatches.Add(newDisp); // Lägg till objektet i lista;
    }
    public void PrintDispatch()
    {
        for (int i = 0; i < dispatches.Count; i++)
        {
            //Console.Write($"{dispatches[i].DispatchNr}.");
            Console.Write($"Tid: {dispatches[i].Time, - 25}");
            Console.Write($"Typ: {dispatches[i].Type, - 25}");
            Console.Write($"Plats: {dispatches[i].Location, - 25}");
            Console.WriteLine($"Polis: {dispatches[i].DispatchOfficer, - 25}");

        }
    }

        
        //obehandlade utryckningar som ej rapporterats
    /*public void DispatchOfficer(int badgeNr)
        {
            
            Officer dispatchOfficer = rosterItem.Find(o => o.BadgeNr == badgeNr); 

            if (dispatchOfficer != null)
                {
                    // Lägg till personen i arkivet
                    archiveItem.Add(personToArchive);

                    // Ta bort personen från den aktiva listan
                    rosterItem.Remove(personToArchive);

                    Console.WriteLine($"Personal: '{personToArchive.LastName}, {personToArchive.FirstName}' är arkiverad.");
                }
                else
                {
                    Console.WriteLine("Personal ej funnen i registret. Försök igen!");
        }
                }*/

}    
public class Report
{   
    public List<ReportHub> reports { get; set; }
    public Report()
    {
        reports = new List<ReportHub>();
    }


    // Ska innehålla RAPPORTNR DATUM POLISSTATION  BESKRIVNING
    public void AddReport(int reportNr, string station, DateTime time, string description)
    {
        ReportHub newRep = new ReportHub(reportNr, station , time, description); // Skapa objekt
        reports.Add(newRep); // Lägg till objektet i lista;
    }
        public void Print()
    {
        for (int i = 0; i < reports.Count; i++)
        {   
            Console.Write($"Rapportnummer: {reports[i].ReportNr, - 25}");
            Console.Write($"Polisstation: {reports[i].Station, - 25}");
            Console.WriteLine($"Tidpunkt: {reports[i].Time, - 25}");
            Console.WriteLine($"Beskrivning: {reports[i].Description, - 25}");
            Console.WriteLine();
        }       
    } 
}
public class Roster
{
    public List<Officer> rosterItem { get; set; }
    public List<Officer> archiveItem { get; set; } //private? på dessa?
    
    public Roster()
    {
        rosterItem = new List<Officer>();
        archiveItem = new List<Officer>();
    }
    public void Add(string firstName, string lastName, int badgeNr)
    {    
        Officer newItem = new Officer(firstName, lastName, badgeNr); // Skapa objekt
        rosterItem.Add(newItem); // Lägg till objektet i lista;
    }
    public void Print()
    {
        for(int i = 0; i < rosterItem.Count; i++)
        {
            Console.Write($"Namn: {rosterItem[i].LastName, -5}, ");
            Console.Write($"{rosterItem[i].FirstName, - 30}");
            Console.WriteLine($"Tjänstenummer: {rosterItem[i].BadgeNr, - 30}");
        }
    }
        public void AddToArchive(string firstName, string lastName, int badgeNr)
    {    
        Officer toArchive = new Officer(firstName, lastName, badgeNr); // Skapa objekt
        archiveItem.Add(toArchive); // Lägg till objektet i lista;
    }
    public void PrintArchive()
    {
        for(int i = 0; i < archiveItem.Count; i++)
        {
            Console.Write($"Namn: {archiveItem[i].LastName, -5}, ");
            Console.Write($"{archiveItem[i].FirstName, - 30}");
            Console.WriteLine($"Tjänstenummer: {archiveItem[i].BadgeNr, - 30}");
        }
    }
    public void ArchiveOfficer(int badgeNr)
    {
        
        Officer personToArchive = rosterItem.Find(o => o.BadgeNr == badgeNr); 

        if (personToArchive != null)
            {
                // Lägg till personen i arkivet
                archiveItem.Add(personToArchive);

                // Ta bort personen från den aktiva listan
                rosterItem.Remove(personToArchive);

                Console.WriteLine($"Personal: '{personToArchive.LastName}, {personToArchive.FirstName}' är arkiverad.");
            }
            else
            {
                Console.WriteLine("Personal ej funnen i registret. Försök igen!");
            }
    }

}
        
        //roster.Sort((item1, item2) => item2.LastName.CompareTo(item1.LastName));
/*enum Crimes 
{ 
    Theft, 
    Assault, 
    Burglary, 
    Fraud, 
    Vandalism
};

DateTime newTime;
while (!DateTime.TryParse(date + " " + hour, out newTime))
{
    Console.WriteLine("Felaktigt datum- eller tidsformat.");
    Console.WriteLine("Ange datum (år/mån/dag):");
    date = Console.ReadLine();

    Console.WriteLine("Ange klockslag (timmar:minuter):");
    hour = Console.ReadLine();
}

Crimes currentCrime = Crimes.Fraud;
*/        