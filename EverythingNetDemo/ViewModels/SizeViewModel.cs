using System.ComponentModel;
using System.Runtime.CompilerServices;
using EverythingNet.Interfaces;

namespace WinFinder.ViewModels
{
  public class SizeViewModel : IQueryViewModel
  {
    public string Name
    {
      get { return "Size"; }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public EverythingNet.Query.Sizes PredefinedSize { get; set; }

    public IQueryable GetQueryable(IEverything everything)
    {
      return everything.Search()
        .Size
        .Equal(this.PredefinedSize);
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
