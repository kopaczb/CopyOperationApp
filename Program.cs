using System;
using NXOpen;
using NXOpen.CAM;

public class Program
{
    public static void Main(string[] args)
    {
        NXOpen.Session theSession = NXOpen.Session.GetSession();
        NXOpen.Part workPart = theSession.Parts.Work;
        NXOpen.Part displayPart = theSession.Parts.Display;
        
        CAMSetup camSetup = workPart.CAMSetup;
        
        theSession.ListingWindow.Open();
        
        foreach (NXOpen.CAM.NCGroup group in camSetup.CAMGroupCollection)
        {
            theSession.ListingWindow.WriteLine("Group Type: " + group.GetType().ToString());

            foreach (NXOpen.CAM.CAMObject operation in group.GetMembers())
            {
                theSession.ListingWindow.WriteLine("Operation Type: " + operation.GetType().ToString());
            }
        }
        theSession.ListingWindow.Close();
    }
}
