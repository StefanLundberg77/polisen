public class Roster
{
    public List<Officer> roster { get; set; }
    public List<Officer> archive { get; set; } //private? på dessa?
        
    public Roster()
    {
        roster = new List<Officer>();
        archive = new List<Officer>();
    }
    public void AddToRoster(string firstName, string lastName, int badgeNr)
    {    
        Officer newItem = new Officer(firstName, lastName, badgeNr);
        roster.Add(newItem); 
    }
    public void PrintRoster()
    {
        for(int i = 0; i < roster.Count; i++)
        {
            Console.Write($"Namn: {roster[i].LastName, -5}, ");
            Console.Write($"{roster[i].FirstName, - 30}");
            Console.WriteLine($"Tjänstenummer: {roster[i].BadgeNr, - 30}");
        }
    }
    public void AddToArchive(string firstName, string lastName, int badgeNr)
    {    
        Officer toArchive = new Officer(firstName, lastName, badgeNr); // Skapa objekt
        archive.Add(toArchive); // Lägg till objektet i lista;
    }
    public void PrintArchive()
    {
        for(int i = 0; i < archive.Count; i++)
        {
            Console.Write($"Namn: {archive[i].LastName, -5}, ");
            Console.Write($"{archive[i].FirstName, - 30}");
            Console.WriteLine($"Tjänstenummer: {archive[i].BadgeNr, - 30}");
        }
    }
    public void ArchiveOfficer(int badgeNr)
    {    
        Officer itemToArchive = roster.Find(o => o.BadgeNr == badgeNr); 
        
        if (itemToArchive != null)
        {
            archive.Add(itemToArchive);

            roster.Remove(itemToArchive);

            Console.WriteLine($"Personal: '{itemToArchive.LastName}, {itemToArchive.FirstName}' är arkiverad.");
        }
        else
        {
            Console.WriteLine("Personal ej funnen i registret. Försök igen!");
        }
    }
}
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