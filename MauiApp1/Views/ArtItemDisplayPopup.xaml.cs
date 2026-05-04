using MauiApp1.ViewModels;

namespace MauiApp1.Views;

public partial class ArtItemDisplayPopup : ContentView, IQueryAttributable
{
    private ArtItemDisplayPopupViewModel _viewModel;
    
    public ArtItemDisplayPopup(ArtItemDisplayPopupViewModel viewModel)
    {
        InitializeComponent();
        BackgroundColor = Colors.Transparent;
        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("artItemEntryViewModel", out var artItemEntryViewModel))
        {
            if (artItemEntryViewModel is ArtItemEntryViewModel item)
            {
                _viewModel.ArtItemEntryViewModel = item;
            }
        }
    }
}