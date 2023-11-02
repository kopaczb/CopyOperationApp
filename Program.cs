using System;
using NXOpen;
using NXOpen.CAM;
using System.Collections.Generic;

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

        // Inicjalizacja listy na operacje
        List<CAMObject> objectsToBeMoved = new List<CAMObject>();

        // Sprawdź, czy grupa "Program Root" istnieje
        if (programRoot != null)
        {
            // Iteruj przez wszystkie grupy w grupie "Program Root"
            foreach (NCGroup group in programRoot.GetMembers())
            {
                theSession.ListingWindow.WriteLine("Group Type: " + group.GetType().ToString());

                // Iteruj przez operacje w grupie i dodaj do listy
                foreach (CAMObject operation in group.GetMembers())
                {
                    theSession.ListingWindow.WriteLine("Operation Type: " + operation.GetType().ToString());
                    objectsToBeMoved.Add(operation);
                }
            }
        }
        else
        {
            theSession.ListingWindow.WriteLine("Grupa 'Program Root' nie istnieje.");
        }

        theSession.ListingWindow.Close();

        // Przeksztalcam liste na tablice, jesli to konieczne
        CAMObject[] objectsArray = objectsToBeMoved.ToArray();
		
		NXOpen.CAM.Tool tool1 = ((NXOpen.CAM.Tool)workPart.CAMSetup.CAMGroupCollection.FindObject("10_R0.0_FL32_Z3_AL"));
		workPart.CAMSetup.MoveObjects(NXOpen.CAM.CAMSetup.View.MachineTool, objectsArray, tool1, NXOpen.CAM.CAMSetup.Paste.Inside);
		
		workPart.CAMSetup.GenerateToolPath(objectsArray);
    }
}
