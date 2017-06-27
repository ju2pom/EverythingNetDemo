using System.ComponentModel;
using System.Runtime.CompilerServices;
using EverythingNet.Interfaces;

namespace WinFinder
{
  public class NameViewModel : IQueryViewModel
  {
    public event PropertyChangedEventHandler PropertyChanged;

    public string Name { get { return "Name"; } }

    public string StartWith { get; set; }

    public string EndWith { get; set; }

    public IQueryable GetQueryable(IEverything everything)
    {
      return everything
        .Search()
        .Name()
        .StartWith(this.StartWith)
        .And
        .Name()
        .EndWith(this.EndWith);

    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}