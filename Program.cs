using System;
using NXOpen;
using NXOpen.CAM;

public class TestRenameCamObjects
{
    public static void Main(string[] args)
    {
        Session theSession = Session.GetSession();
        Part workPart = theSession.Parts.Work;
        UI theUI = UI.GetUI();
        CAMSetup camSetup = workPart.CAMSetup;

        for (int i = 0; i < theUI.SelectionManager.GetNumSelectedObjects(); i++)
        {
            TaggedObject obj = theUI.SelectionManager.GetSelectedTaggedObject(i);

            if (obj is CAMObject)
            {
                CAMObject camObject = obj as CAMObject;

                if (camSetup.IsGroup(camObject))
                {
                    string theIndex = NXOpenUI.NXInputBox.GetInputString("Nazwa operacji");
                    NCGroup theNcGroup = camObject as NCGroup;

                    int theCounter = 0;

                    foreach (CAMObject theCAMObject in theNcGroup.GetMembers())
                    {
                        string theGroupName = theIndex;
                        string key = theNcGroup.Name.Substring(7, 1);

                        theCAMObject.SetName(key + theCounter.ToString("00") + "____" + theGroupName.Replace(" / ", "_").Replace(" ", "_"));
                        theCounter++;

                        if (theCounter > 999)
                        {
                            break;
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
