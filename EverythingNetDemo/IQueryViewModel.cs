using System.ComponentModel;
using EverythingNet.Interfaces;

namespace WinFinder
{
  public interface IQueryViewModel : INotifyPropertyChanged
  {
    string Name { get; }
    IQueryable GetQueryable(IEverything everything);
  }
}