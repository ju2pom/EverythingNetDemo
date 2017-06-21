using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EverythingNet.Core;

namespace WinFinder
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      this.DataContext = this;
      this.Results = new ObservableCollection<string>();

      InitializeComponent();
    }

    public string SearchText { get; set; }

    public ObservableCollection<string> Results { get; }

    private void SearchButton_OnClick(object sender, RoutedEventArgs e)
    {
      if (!EverythingState.IsStarted())
      {
        EverythingState.StartService(true, EverythingState.StartMode.Service);
      }

      var everything = new EverythingNet.Core.Everything();
      var nameQueryable = everything.Search().Name(this.SearchText);

      this.Results.Clear();
      foreach (var result in nameQueryable)
      {
        this.Results.Add(result);
      }
    }
  }
}
