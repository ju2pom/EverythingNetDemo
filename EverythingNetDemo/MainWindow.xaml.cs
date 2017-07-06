using System.Windows;
using WinFinder.ViewModels;

namespace WinFinder
{
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      this.DataContext = new MainViewModel();

      this.InitializeComponent();
    }
  }
}
