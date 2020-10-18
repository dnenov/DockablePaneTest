using Autodesk.Revit.UI;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace RevitAddin1
{
    /// <summary>
    /// Interaction logic for Pane.xaml
    /// </summary>
    public partial class Pane : Page, IDisposable, IDockablePaneProvider
    {
        public Pane()
        {
            InitializeComponent();
        }

        public void Dispose()
        {
            this.Dispose();
        }

        public void SetupDockablePane(DockablePaneProviderData data)
        {
            data.FrameworkElement = this as FrameworkElement;
            data.EditorInteraction = new EditorInteraction(EditorInteractionType.KeepAlive);
            
            data.InitialState = new DockablePaneState
            {
                DockPosition = DockPosition.Right,
            };
        }
    }
}
