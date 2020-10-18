#region Namespaces
using System;
using System.Collections.Generic;
using System.Reflection;
using Archilizer_Bulk.Helpers;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.UI;
#endregion

namespace RevitAddin1
{
    class App : IExternalApplication
    {
        private static UIControlledApplication MyApplication { get; set; } //Set the Revit Application so we can access it 
        private static Assembly assembly; //Set the Assembly so we can access it later
        // TO DO - change the uri
        //private static string helpFile = "file:///C:/ProgramData/Autodesk/ApplicationPlugins/Archilizer_Bulk.bundle/Content/Help/Tiny%20Tools%20_%20AutoCAD%20_%20Autodesk%20App%20Store.html";
        //private static string helpFile = "file:///C:/ProgramData/Autodesk/ApplicationPlugins/Archilizer_Warchart.bundle/Content/Help/Warchart%20_%20Revit%20_%20Autodesk%20App%20Store.html";
        private static string assemblyVersion;

        /// <summary>
        /// Add the Ribbon associated with this Plugin
        /// Each Ribbon can contain more than 1 command
        /// </summary>
        /// <param name="application">The Revit Application</param>
        static void AddRibbonPanel(UIControlledApplication application)
        {
            String tabName = "Architype";  //Create a custom ribbon panel
            try
            {
                application.CreateRibbonTab(tabName);
            }
            catch (Exception) { }

            RibbonPanel ribbonPanel = application.CreateRibbonPanel(tabName, "Side Panel");

            string thisAssemblyPath = Assembly.GetExecutingAssembly().Location; //Get dll assembly path
            assembly = Assembly.GetExecutingAssembly(); //Set the Assembly
            assemblyVersion = $"v{assembly.GetName().Version.ToString()}";
            assemblyVersion = assemblyVersion.Substring(0, assemblyVersion.Length - 2);

            CreatePushButton(ribbonPanel, String.Format("Toggle"), thisAssemblyPath, "RevitAddin1.CommandToggle",
                "Toggle Architype Sidebar", "RevitAddin1.Resources.icon_Rename.png");
        }
        /// <summary>
        /// Create a push button and adds it to the panel
        /// </summary>
        /// <param name="ribbonPanel">The panel that the pushbutton will be added to</param>
        /// <param name="name">Name of the pushbutton</param>
        /// <param name="path">Aassembly path</param>
        /// <param name="command">Command that will execute after pushing the button</param>
        /// <param name="tooltip">Tooltip to be displayed</param>
        /// <param name="icon">Icon associated with the pushbutton</param>
        private static void CreatePushButton(RibbonPanel ribbonPanel, string name, string path, string command, string tooltip, string icon)
        {
            BitmapIcons bitmapIcons = new BitmapIcons(assembly, icon, MyApplication);
            //ContextualHelp ch = new ContextualHelp(ContextualHelpType.Url, @helpFile);

            PushButtonData pbData = new PushButtonData(
                name,
                name,
                path,
                command);

            PushButton pb = ribbonPanel.AddItem(pbData) as PushButton;

            pb.ToolTip = $"{tooltip}{Environment.NewLine}{Environment.NewLine}{assemblyVersion}";
            //RibbonToolTip tip = new RibbonToolTip();

            var largeImage = bitmapIcons.LargeBitmap();
            var smallImage = bitmapIcons.SmallBitmap();
            pb.LargeImage = largeImage;
            pb.Image = smallImage;
            //pb.SetContextualHelp(ch);
        }

        public Result OnStartup(UIControlledApplication a)
        {
            MyApplication = a;  //Initialize MyApplication that will be used by the class
            a.ControlledApplication.ApplicationInitialized += DockablePaneRegister;
            AddRibbonPanel(a);
            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication a)
        {
            a.ControlledApplication.ApplicationInitialized -= DockablePaneRegister;
            return Result.Succeeded;
        }

        private void DockablePaneRegister(object sender, ApplicationInitializedEventArgs e)
        {
            var regCommand = new RegisterCommand();
            regCommand.Execute(new UIApplication(sender as Application));
        }
    }
}
