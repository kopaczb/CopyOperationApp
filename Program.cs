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
        
        NCGroup geometryRoot = camSetup.GetRoot(CAMSetup.View.Geometry);
        NCGroup methodRoot = camSetup.GetRoot(CAMSetup.View.MachineMethod);
        NCGroup machineRoot = camSetup.GetRoot(CAMSetup.View.MachineTool);
        NCGroup programRoot = camSetup.GetRoot(CAMSetup.View.ProgramOrder);
        
        CAMObject[] geometryRootMembers = geometryRoot.GetMembers();
        CAMObject[] methodRootMembers = methodRoot.GetMembers();
        CAMObject[] machineRootMembers = machineRoot.GetMembers();
        CAMObject[] programRootMembers = programRoot.GetMembers();
        
        theSession.ListingWindow.Open();
        
        theSession.ListingWindow.WriteLine("Geometry Root Members:");
        foreach (CAMObject member in geometryRootMembers)
        {
            theSession.ListingWindow.WriteLine("Operation Type: " + member.GetType().ToString());
        }

        theSession.ListingWindow.WriteLine("Method Root Members:");
        foreach (CAMObject member in methodRootMembers)
        {
            theSession.ListingWindow.WriteLine("Operation Type: " + member.GetType().ToString());
        }

        theSession.ListingWindow.WriteLine("Machine Root Members:");
        foreach (CAMObject member in machineRootMembers)
        {
            theSession.ListingWindow.WriteLine("Operation Type: " + member.GetType().ToString());
        }

        theSession.ListingWindow.WriteLine("Program Root Members:");
        foreach (CAMObject member in programRootMembers)
        {
            theSession.ListingWindow.WriteLine("Operation Type: " + member.GetType().ToString());
        }
        
        theSession.ListingWindow.Close();
    }
}
