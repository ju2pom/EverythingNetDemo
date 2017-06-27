using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using EverythingNet.Core;

namespace WinFinder
{
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      this.QueryCategories = new ObservableCollection<IQueryViewModel> {new NameViewModel(), new DateViewModel()};
      this.DataContext = this;
      this.Results = new ObservableCollection<string>();

      this.InitializeComponent();
    }

    public IEnumerable<IQueryViewModel> QueryCategories { get; }

    public IQueryViewModel SelectedQueryCategory { get; set; }

    public string SearchText { get; set; }

    public ObservableCollection<string> Results { get; }

    private void SearchButton_OnClick(object sender, RoutedEventArgs e)
    {
      if (!EverythingState.IsStarted())
      {
        EverythingState.StartService(true, EverythingState.StartMode.Service);
      }

      var everything = new Everything();
      var nameQueryable = this.SelectedQueryCategory.GetQueryable(everything);

      this.Results.Clear();
      foreach (var result in nameQueryable)
      {
        this.Results.Add(result);
      }
    }
  }
}
