using System.Windows.Input;
using CommunityToolkit.Maui;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiApp1.ViewModels;

public partial class ArtItemDisplayPopupViewModel : ObservableObject
{
    public ICommand CloseButtonClicked { get; private set; }
    public ICommand FavoriteButtonClicked { get; private set; }

    [ObservableProperty]
    private double _height;
    [ObservableProperty]
    private double _width;
    [ObservableProperty]
    private ArtItemEntryViewModel _artItemEntryViewModel;

    public ArtItemDisplayPopupViewModel(IPopupService popupService, MainPage mainPage)
    {
        var window = Application.Current?.Windows[0];
        _width = window?.Width ?? 100;
        _height = window?.Height ?? 100;

        _artItemEntryViewModel = new(new());
        FavoriteButtonClicked = mainPage.ArtItemDisplayViewModel.FavoriteItemToggleCommand;
        CloseButtonClicked = new Command(() => { popupService.ClosePopupAsync(Shell.Current); });
    }
}