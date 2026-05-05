using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Maui;
using CommunityToolkit.Mvvm.ComponentModel;
using MauiApp1.Models;

namespace MauiApp1.ViewModels;

public partial class FavoritedItemsCollectionViewPopupViewModel : ObservableObject
{
    public ICommand CloseButtonClicked { get; private set; }
    
    [ObservableProperty]
    private double _height;
    [ObservableProperty]
    private double _width;
    [ObservableProperty]
    private ObservableCollection<ArtItemEntry> _favoritedItems = new();
    [ObservableProperty]
    private int _favoritedItemsCount;
    [ObservableProperty]
    private float _totalPrice;
    
    public FavoritedItemsCollectionViewPopupViewModel(IPopupService popupService)
    {
        var window = Application.Current?.Windows[0];
        // TOOD: figure out why the content view has a weird inconsistent size
        _width = window != null ? window.Width - 106 : 100;
        _height = window != null ? window.Height - 106 : 100;
        
        CloseButtonClicked = new Command(() => { popupService.ClosePopupAsync(Shell.Current); });
    }
}