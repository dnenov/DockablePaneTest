using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RevitAddin1
{
    public class RegisterCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            return Execute(commandData.Application);
        }

        public Result Execute(UIApplication uIApplication)
        {
            var data = new DockablePaneProviderData();
            var page = new Pane();
                    
            data.EditorInteraction = new EditorInteraction(EditorInteractionType.KeepAlive);
            data.FrameworkElement = page as FrameworkElement;

            var starte = new DockablePaneState
            {
                DockPosition = DockPosition.Right,
            };

            var dpid = new DockablePaneId(new Guid("39FA492A-6F72-465C-83C9-F7662B89F62C"));
            uIApplication.RegisterDockablePane(dpid, "Architype Learn", page as IDockablePaneProvider);

            return Result.Succeeded;
        }
    }
}
