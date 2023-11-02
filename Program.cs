using System;
using System.IO;
using System.Windows.Forms;
using NXOpen;
using NXOpen.UF;
using NXOpen.Utilities;
using NXOpen.CAM;

public class TestRenameCamObjects
{
	static NXOpen.Session theSession = NXOpen.Session.GetSession();
    static NXOpen.Part workPart = theSession.Parts.Work;
    static NXOpen.Part displayPart = theSession.Parts.Display;
    static UFSession theUFSession = UFSession.GetUFSession();
    static NXOpen.UI theUI = NXOpen.UI.GetUI();
    static string TempPath = Environment.GetEnvironmentVariable("TMP");
    static string UGRelease = null;
    static string UGFullRelease = null;

	static NXOpen.Selection selection = theUI.SelectionManager;
	static CAMSetup camSetup = workPart.CAMSetup;
			
	public static void Main(string[] args)
    {
		theSession.LogFile.WriteLine("Executing ... " + System.Reflection.Assembly.GetExecutingAssembly().Location);

        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");

        UGRelease = theSession.GetEnvironmentVariableValue("UGII_VERSION");
        UGFullRelease = theSession.GetEnvironmentVariableValue("UGII_FULL_VERSION");

        System.Windows.Forms.Application.EnableVisualStyles();

        Session.UndoMarkId MyUndoMark = theSession.SetUndoMark(Session.MarkVisibility.Visible, "TestRenameCamObjects");
		

		
		for (int i = 0; i <= theUI.SelectionManager.GetNumSelectedObjects() - 1; i++)
        {	
			if (selection.GetNumSelectedObjects() == 1)
			{
				TaggedObject obj = selection.GetSelectedTaggedObject(0);
				
			    if (obj is NXOpen.CAM.CAMObject)
				{				
					NXOpen.CAM.CAMObject camObject = obj as NXOpen.CAM.CAMObject;
					
					if (camSetup.IsGroup(camObject))
					{
						string thePartName = System.IO.Path.GetFileNameWithoutExtension(theSession.Parts.Work.FullPath);
						string theIndex = NXOpenUI.NXInputBox.GetInputString("Nazwa operacji");
						NXOpen.CAM.NCGroup theNcGroup = obj as NXOpen.CAM.NCGroup;
						int theCounter = 0;
					
						foreach (NXOpen.CAM.CAMObject theCAMObject in theNcGroup.GetMembers())
						{
							string theGroupName = theIndex;
							string key = theNcGroup.Name.Substring(7, 1);

							theCAMObject.SetName(key + theCounter.ToString("00") + "____" + theGroupName.Replace(" / ", "_").Replace(" ", "_"));
							theCounter ++;
							if (theCounter > 999)
							{
								break;
							}
						}	
					}	
				}
			}			
        }
    }	
	
	public int GetUnloadOption(string arg)
    {
        return (int)Session.LibraryUnloadOption.Immediately;
    }
}