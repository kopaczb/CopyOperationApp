using System;
using NXOpen;
using NXOpen.CAM;

public class Program
{
    public static void Main(string[] args)
    {
        Session theSession = Session.GetSession();
        Part workPart = theSession.Parts.Work;

        // Pobierz ustawienia CAM
        CAMSetup camSetup = workPart.CAMSetup;

        // Pobierz grupę "Program Root"
        NCGroup programRoot = camSetup.GetRoot(CAMSetup.View.ProgramOrder);

        theSession.ListingWindow.Open();

        // Sprawdź, czy grupa "Program Root" istnieje
        if (programRoot != null)
        {
            // Iteruj przez wszystkie grupy w grupie "Program Root"
            foreach (NCGroup group in programRoot.GetMembers())
            {
                theSession.ListingWindow.WriteLine("Group Type: " + group.GetType().ToString());

                // Iteruj przez operacje w grupie
                foreach (CAMObject operation in group.GetMembers())
                {
                    theSession.ListingWindow.WriteLine("Operation Type: " + operation.GetType().ToString());
                }
            }
        }
        else
        {
            theSession.ListingWindow.WriteLine("Grupa 'Program Root' nie istnieje.");
        }

        theSession.ListingWindow.Close();
    }
}
