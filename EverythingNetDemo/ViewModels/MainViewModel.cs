using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Common.MvvM;
using EverythingNet.Core;
using EverythingNet.Interfaces;

namespace WinFinder.ViewModels
{
  public class MainViewModel : INotifyPropertyChanged
  {
    private readonly object collectionLock = new object();
    private readonly IEverything everything;

    private long processingTime;

    public MainViewModel()
    {
      everything = new Everything();
      QueryCategories =
        new ObservableCollection<IQueryViewModel> {new NameViewModel(), new DateViewModel(), new SizeViewModel()};
      Results = new ObservableCollection<ISearchResult>();
      BindingOperations.EnableCollectionSynchronization(Results, collectionLock);
      SearchCommand = new RelayCommand(SearchExecute);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public IEnumerable<IQueryViewModel> QueryCategories { get; }

    public IQueryViewModel SelectedQueryCategory { get; set; }

    public long ResultCount { get; private set; }

    public long ProcessingTime
    {
      get { return processingTime; }
      set
      {
        processingTime = value;
        OnPropertyChanged();
      }
    }

    public ObservableCollection<ISearchResult> Results { get; }

    public ICommand SearchCommand { get; }

    private void SearchExecute(object parameter)
    {
      if (!EverythingState.IsStarted())
        EverythingState.StartService(true, EverythingState.StartMode.Service);

      ProcessingTime = 0;
      var stopwatch = new Stopwatch();
      stopwatch.Start();
      var queryable = SelectedQueryCategory.GetQueryable(everything);

      everything.Reset();
      Results.Clear();
      Task.Factory.StartNew(() =>
      {
        foreach (var result in queryable)
          Results.Add(result);
        stopwatch.Stop();

        ProcessingTime = stopwatch.ElapsedMilliseconds;
        ResultCount = queryable.Count;
        OnPropertyChanged(nameof(ResultCount));
        OnPropertyChanged(nameof(Results));
      });
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}