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
    public bool ReportStatus { get; set; }
    
    public ReportHub(string type, string location, DateTime time, string dispatchOfficer, int dispatchNr, bool reportStatus)//utryckningskonstruktor
    {
        Type = type;
        Location = location;
        Time = time;
        DispatchOfficer = dispatchOfficer;
        DispatchNr = dispatchNr;//en egenskap för att framöver fixa ngn funktion för rapportnrgenerering?
        ReportStatus = reportStatus;
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
    //Ska den va static?    
    public void AddDispatch(string type, string location, DateTime time, string dispatchofficer, int dispatchNr, bool reportStatus)
    {
        ReportHub newDisp = new ReportHub(type, location, time, dispatchofficer, dispatchNr, reportStatus); // Skapa objekt
        dispatches.Add(newDisp); // Lägg till objektet i lista;
    }
    public void PrintDispatch()
    {
        for (int i = 0; i < dispatches.Count; i++)
        {
            Console.Write($"Tid: {dispatches[i].Time, - 25}");
            Console.Write($"Typ: {dispatches[i].Type, - 25}");
            Console.Write($"Plats: {dispatches[i].Location, - 25}");
            Console.Write($"Polis: {dispatches[i].DispatchOfficer, - 25}");
            if (dispatches[i].ReportStatus == false)
            {
                Console.WriteLine("Rapportstatus: ofullständig.");
            }
            else
            {
                Console.WriteLine("Rapportstatus: fullständig.");
            }    

        }
    }
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
    public void PrintReport()
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