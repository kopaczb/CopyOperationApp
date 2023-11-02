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

		NXOpen.CAM.NCGroupCollection groups = camSetup.CAMGroupCollection;
		
		foreach (NXOpen.CAM.NCGroup group in groups)
        {
            Type opType = group.GetType();
            NXOpen.UF.UFSession ufsession = NXOpen.UF.UFSession.GetUFSession();
			theSession.ListingWindow.Open();
			theSession.ListingWindow.WriteLine(opType.ToString());
			theSession.ListingWindow.Close();               
        }     
    }
}
