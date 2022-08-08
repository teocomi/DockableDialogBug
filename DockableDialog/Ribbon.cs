using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using DockableDialog.Forms;
using DockableDialog.Properties;
using System;
using System.Reflection;

namespace DockableDialog
{
  public class Ribbon : IExternalApplication
  {
    private UIControlledApplication _uiapp;
    public Result OnStartup(UIControlledApplication a)
    {
      _uiapp = a;

      a.CreateRibbonTab("AEC LABS");

      RibbonPanel AECPanelDebug
        = a.CreateRibbonPanel("AEC LABS", "AEC LABS");

      string path = Assembly.GetExecutingAssembly().Location;

      #region DockableWindow
      PushButtonData pushButtonShowDockableWindow = new PushButtonData("Show DockableWindow", "Show DockableWindow", path, "DockableDialog.ShowDockableWindow");
      pushButtonShowDockableWindow.LargeImage = GetImage(Resources.red.GetHbitmap());
      PushButtonData pushButtonHideDockableWindow = new PushButtonData("Hide DockableWindow", "Hide DockableWindow", path, "DockableDialog.HideDockableWindow");
      pushButtonHideDockableWindow.LargeImage = GetImage(Resources.orange.GetHbitmap());
      //IList<RibbonItem> ribbonpushButtonDockableWindow = AECPanelDebug.AddStackedItems(pushButtonRegisterDockableWindow, pushButtonShowDockableWindow, pushButtonHideDockableWindow);

      RibbonItem ri2 = AECPanelDebug.AddItem(pushButtonShowDockableWindow);
      RibbonItem ri3 = AECPanelDebug.AddItem(pushButtonHideDockableWindow);
      #endregion

      a.ControlledApplication.ApplicationInitialized += ControlledApplication_ApplicationInitialized;

      return Result.Succeeded;
    }

    public Result OnShutdown(
      UIControlledApplication a)
    {
      return Result.Succeeded;
    }

    private System.Windows.Media.Imaging.BitmapSource GetImage(
      IntPtr bm)
    {
      System.Windows.Media.Imaging.BitmapSource bmSource
        = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
          bm,
          IntPtr.Zero,
          System.Windows.Int32Rect.Empty,
          System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());

      return bmSource;
    }

    private void ControlledApplication_ApplicationInitialized(object sender, Autodesk.Revit.DB.Events.ApplicationInitializedEventArgs e)
    {
      MainPage m_MyDockableWindow = null;


      DockablePaneProviderData data = new DockablePaneProviderData();

      MainPage MainDockableWindow = new MainPage();

      m_MyDockableWindow = MainDockableWindow;

      //MainDockableWindow.SetupDockablePane(me);

      data.FrameworkElement = MainDockableWindow as System.Windows.FrameworkElement;

      data.InitialState = new DockablePaneState();

      data.InitialState.DockPosition = DockPosition.Tabbed;

      //DockablePaneId targetPane;
      //if (m_targetGuid == Guid.Empty)
      //    targetPane = null;
      //else targetPane = new DockablePaneId(m_targetGuid);
      //if (m_position == DockPosition.Tabbed)

      data.InitialState.TabBehind = DockablePanes.BuiltInDockablePanes.ProjectBrowser;



      DockablePaneId dpid = new DockablePaneId(new Guid("{D7C963CE-B7CA-426A-8D51-6E8254D21157}"));

      _uiapp.RegisterDockablePane(dpid, "AEC Dockable Window", MainDockableWindow as IDockablePaneProvider);

      //var p1 = DockablePane.PaneIsRegistered(dpid);  //true
      //var p2 = DockablePane.PaneExists(dpid); //false
      //var p3 = _uiapp.GetDockablePane(dpid); //throws "The requested dockable pane has not been created"


    }
  }

  /// <summary>
  /// You can only register a dockable dialog in "Zero doc state"
  /// </summary>
  public class AvailabilityNoOpenDocument
    : IExternalCommandAvailability
  {
    public bool IsCommandAvailable(
      UIApplication a,
      CategorySet b)
    {
      if (a.ActiveUIDocument == null)
      {
        return true;
      }
      return false;
    }
  }


  /// <summary>
  /// Show dockable dialog
  /// </summary>
  [Transaction(TransactionMode.ReadOnly)]
  public class ShowDockableWindow : IExternalCommand
  {
    public Result Execute(
      ExternalCommandData commandData,
      ref string message,
      ElementSet elements)
    {
      DockablePaneId dpid = new DockablePaneId(
        new Guid("{D7C963CE-B7CA-426A-8D51-6E8254D21157}"));

      DockablePane dp = commandData.Application
        .GetDockablePane(dpid);

      dp.Show();

      return Result.Succeeded;
    }
  }

  /// <summary>
  /// Hide dockable dialog
  /// </summary>
  [Transaction(TransactionMode.ReadOnly)]
  public class HideDockableWindow : IExternalCommand
  {
    public Result Execute(
      ExternalCommandData commandData,
      ref string message,
      ElementSet elements)
    {
      DockablePaneId dpid = new DockablePaneId(
        new Guid("{D7C963CE-B7CA-426A-8D51-6E8254D21157}"));

      DockablePane dp = commandData.Application
        .GetDockablePane(dpid);

      dp.Hide();
      return Result.Succeeded;
    }
  }
}
