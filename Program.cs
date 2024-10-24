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
        
        //Lite startvärden
        bool reportStatus = false;
        int reportNr = 0;
        int dispatchNr = 0;
        int badgeNr = 0;
        DateTime time = DateTime.MinValue;
        //Lite objekt för testning
        Roster newOfficer = new Roster();
        newOfficer.AddToRoster("Stefan", "Lundberg", 12);
        newOfficer.AddToRoster("Erik", "Jansson", 14);
        newOfficer.AddToRoster("Lina", "Bengtsson", 10);
        newOfficer.AddToRoster("Olle", "Åberg", 4);
        Dispatch newDisp = new Dispatch();
        newDisp.AddDispatch("Brand", "Haninge kyrka", time, "Lina Bengtsson", 11, false);
        newDisp.AddDispatch("Misshandel", "Södra hamngatan 11", time, "Linn Andersson", 1, false);
        newDisp.AddDispatch("Stöld", "Kista centrum", time, "Erik Ovarsson", 2, true);
        Report newRep = new Report();
        newRep.AddReport(15, "Rövarne shivvade Micke i magen", time, "Rovlanda");
        newRep.AddReport(16, "Olle i skoga ramlade i backen", time, "Enskede");
        newRep.AddReport(17, "Inbrott i källare på citrongatan", time, "Alingsås polisstation");
        newRep.AddReport(18, "Bilbrand på birgittagatn misstanke om mordbrand", time, "Alingsås polisstation");

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
            string? choice = Console.ReadLine();
            
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
                    string? input = Console.ReadLine();

                    switch(input)
                    {
                        // gör metod för regga?
                        case "1":
                        {    
                            Console.WriteLine();

                            Console.WriteLine("Ange brottstyp:");
                            string? type = Console.ReadLine();
                                        
                            Console.WriteLine("Ange plats:");
                            string? location = Console.ReadLine();
   
                            Console.WriteLine("Ange datum (åååå/mm/dd):");
                            string? date = Console.ReadLine();

                            Console.WriteLine("Ange klockslag (hh:mm):");
                            string? hour = Console.ReadLine();

                            while (!DateTime.TryParse(date + " " + hour, out time))
                            {
                                Console.WriteLine("Felaktigt datum- eller tidsformat.");
                                Console.WriteLine("Ange datum (åååå/mm/dd):");
                                date = Console.ReadLine();

                                Console.WriteLine("Ange klockslag (hh:mm):");
                                hour = Console.ReadLine();
                            }

                            string? dispatchofficer = "";
                            Console.WriteLine("Tillgängliga poliser");
                            newOfficer.PrintRoster();
                            Console.WriteLine("Ange tjänstenummer för att lägga till");
                            string? badgeInput = Console.ReadLine();
                            if (int.TryParse(badgeInput, out badgeNr))
                            {   
                                
                                // söker i lista polis som matchar tjänstenummer
                                Officer officerToAdd = newOfficer.roster.Find(Officer => Officer.BadgeNr == badgeNr);
                                
                                Console.WriteLine($"Vill du lägga till {officerToAdd.FirstName}, {officerToAdd.LastName} J/N?");// lägga till ytterliggare polis??
                                
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
                            
                            // Skapa en lista för att hålla flera poliser för dispatch
                            List<Officer> dispatchOfficers = new List<Officer>();
                            bool continueAdding = true;

                            while (continueAdding)
                            {
                                Console.WriteLine("Tillgängliga poliser");
                                newOfficer.PrintRoster(); // Visar alla poliser i roster

                                Console.WriteLine("Ange tjänstenummer för att lägga till en polis:");
                                badgeInput = Console.ReadLine();

                                if (int.TryParse(badgeInput, out badgeNr))
                                {
                                    // Hitta polisen med det angivna tjänstenumret
                                    Officer officerToAdd = newOfficer.roster.Find(officer => officer.BadgeNr == badgeNr);

                                    if (officerToAdd != null)
                                    {
                                        Console.WriteLine($"Vill du lägga till {officerToAdd.FirstName} {officerToAdd.LastName}? (J/N)");
                                        input = Console.ReadLine().ToUpper();

                                        if (input == "J")
                                        {
                                            dispatchOfficers.Add(officerToAdd); // Lägg till polisen i dispatchlistan
                                            Console.WriteLine($"{officerToAdd.FirstName} {officerToAdd.LastName} tillagd.");
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
                                        Console.WriteLine("Ingen polis med detta tjänstenummer hittades.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Ogiltigt tjänstenummer. Försök igen.");
                                }

                                // Fråga om användaren vill lägga till ytterligare en polis
                                Console.WriteLine("Vill du lägga till ytterligare en polis? (J/N)");
                                string continueInput = Console.ReadLine().ToUpper();

                                if (continueInput != "J")
                                {
                                    continueAdding = false;
                                }
                            }

                            // Sätt ihop namnen på alla tillagda poliser som en sträng för dispatch
                            string dispatchOfficer = string.Join(", ", dispatchOfficers.Select(o => $"{o.FirstName} {o.LastName}"));

                            // Om inga poliser lades till
                            if (dispatchOfficers.Count == 0)
                            {
                                Console.WriteLine("Inga poliser har lagts till.");
                            }
                            Console.WriteLine("Följande poliser har lagts till:");
                            foreach (var officer in dispatchOfficers)
                            {
                                Console.WriteLine($"{officer.FirstName} {officer.LastName}, Tjänstenummer: {officer.BadgeNr}");
                            }
                            Console.WriteLine();
                            
                            // Skapa dispatch och skicka med all data
                            dispatchNr++;
                            bool ReportStatus = false; 
                            //Borde detta generera rapport med en mandatory beskrivning?
                            newDisp.AddDispatch(type, location, time, dispatchOfficer, dispatchNr, reportStatus);
                            Console.WriteLine("Tryck valfri tangent för att gå tillbaks till meny.");
                            Console.ReadKey();
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

                        //skapa system för utryckningar saknar rapport.
                        //tex. utryckningar som saknar rapport listas här
                        //lägg till rapporterad utryckning i listan.
                        newDisp.PrintDispatch();
                        Console.WriteLine("Lägg till orapporterad utryckning");
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
                        reportNr = dispatchNr + reportNr + 100;
                        //reportNr++;
                        newRep.AddReport(reportNr, station, time, description);//RAPPORTNR DATUM POLISSTATION  BESKRIVNING

                    }
                    else if (input == "2")
                    {   
                        Console.Clear();
                        Console.WriteLine("Rapportlista:");
                        Console.WriteLine();
                        newRep.PrintReport();
                        Console.WriteLine();
                        Console.WriteLine("Tryck valfri tangent för att gå tillbaks till meny.");
                        Console.ReadKey();
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
                        
                        newOfficer.AddToRoster(firstName, lastName, badgeNr);
                        
                        Console.WriteLine("Tryck valfri tangent för att gå tillbaks till meny.");
                        Console.ReadKey();
                    }
                    else if(input == "2")
                    {
                        Console.Clear();
                        newOfficer.PrintRoster();
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
                        newOfficer.PrintRoster();
                        Console.WriteLine();
                        Console.WriteLine("Tryck valfri tangent för att gå tillbaks till meny.");
                        Console.ReadKey();
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
                newRep.PrintReport();
                Console.WriteLine();
                Console.WriteLine("Peronalregister:");
                newOfficer.PrintRoster();
                Console.WriteLine();
                Console.WriteLine("Peronalarkiv:");
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