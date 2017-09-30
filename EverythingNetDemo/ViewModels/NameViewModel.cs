using System.ComponentModel;
using System.Runtime.CompilerServices;
using EverythingNet.Interfaces;

namespace WinFinder.ViewModels
{
  public class NameViewModel : IQueryViewModel
  {
    public event PropertyChangedEventHandler PropertyChanged;

    public string Name { get { return "Name"; } }

    public string Pattern { get; set; }

    public IQueryable GetQueryable(IEverything everything)
    {
      return everything
        .Search()
        .Name
        .Contains(this.Pattern);

    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}