using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;

using EverythingNet.Core;
using System.Windows.Input;
using Common.MvvM;
using System.Runtime.CompilerServices;
using System;

using System.Diagnostics;

namespace WinFinder.ViewModels
{
  public class MainViewModel : INotifyPropertyChanged
  {
    private long processingTime;

    public MainViewModel()
    {
      this.QueryCategories = new ObservableCollection<IQueryViewModel> { new NameViewModel(), new DateViewModel(), new SizeViewModel() };
      this.Results = new ObservableCollection<string>();
      this.SearchCommand = new RelayCommand(this.SearchExecute);
    }
    public event PropertyChangedEventHandler PropertyChanged;

    public IEnumerable<IQueryViewModel> QueryCategories { get; }

    public IQueryViewModel SelectedQueryCategory { get; set; }

    public int ResultCount { get { return this.Results.Count; } }

    public long ProcessingTime
    {
      get { return this.processingTime; }
      set
      {
        this.processingTime = value;
        this.OnPropertyChanged();
      }
    }

    public ObservableCollection<string> Results { get; }

    public ICommand SearchCommand { get; }

    private void SearchExecute(object parameter)
    {
      if (!EverythingState.IsStarted())
      {
        EverythingState.StartService(true, EverythingState.StartMode.Service);
      }

      this.ProcessingTime = 0;
      Stopwatch stopwatch = new Stopwatch();
      stopwatch.Start();
      var everything = new Everything();
      var nameQueryable = this.SelectedQueryCategory.GetQueryable(everything);

      this.Results.Clear();
      foreach (var result in nameQueryable)
      {
        this.Results.Add(result);
      }
      stopwatch.Stop();

      this.ProcessingTime = stopwatch.ElapsedMilliseconds;

      this.OnPropertyChanged(nameof(this.ResultCount));
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
