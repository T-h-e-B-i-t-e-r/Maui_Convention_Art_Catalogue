using CommunityToolkit.Mvvm.ComponentModel;
using MauiApp1.Constants;

namespace MauiApp1.ViewModels;

public partial class SortButtonSelectionAreaViewModel : ObservableObject
{
    [ObservableProperty] public partial List<SortButtonViewModel> SortButtons { get; set; }
    public SortButtonViewModel SelectedButtonViewModel { get; set; }

    public SortButtonSelectionAreaViewModel()
    {
        SortButtons = new()
        {
            new SortButtonViewModel("Name", SortButtonType.Name),
            new SortButtonViewModel("Date", SortButtonType.Date),
            new SortButtonViewModel("Size", SortButtonType.Size),
            new SortButtonViewModel("Price", SortButtonType.Price),
        };

        SelectedButtonViewModel = SortButtons[0];
        SelectedButtonViewModel.IsSelected = true;
    }
}