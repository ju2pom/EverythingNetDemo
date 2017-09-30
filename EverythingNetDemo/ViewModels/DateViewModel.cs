using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using EverythingNet.Interfaces;

namespace WinFinder.ViewModels
{
  public class DateViewModel : IQueryViewModel
  {
    public event PropertyChangedEventHandler PropertyChanged;

    public DateViewModel()
    {
      this.From = DateTime.Today.AddDays(-1);
      this.To = DateTime.Today;
    }

    public string Name { get { return "Date"; } }

    public DateTime? From { get; set; }

    public DateTime? To { get; set; }

    public IQueryable GetQueryable(IEverything everything)
    {
      if (this.From.HasValue && this.To.HasValue)
      {
        return everything
          .Search()
          .ModificationDate
          .Between(this.From.Value, this.To.Value);
      }
      else if (this.From.HasValue)
      {
        return everything
          .Search()
          .ModificationDate
          .Equal(this.From.Value);
      }

      return null;
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}