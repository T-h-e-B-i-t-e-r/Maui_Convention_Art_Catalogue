using System.Windows.Input;
using CommunityToolkit.Maui;
using CommunityToolkit.Mvvm.ComponentModel;
using MauiApp1.Constants;

namespace MauiApp1.ViewModels;

public partial class ArtItemDisplayPopupViewModel : ObservableObject
{
    public ICommand CloseButtonClicked { get; private set; }

    [ObservableProperty]
    private double _height;
    [ObservableProperty]
    private double _width;
    [ObservableProperty]
    private string _spriteFilename;
    [ObservableProperty]
    private string _itemName;
    [ObservableProperty]
    private string _date;
    [ObservableProperty]
    private float _price;
    [ObservableProperty]
    private ArtItemSize _size;

    public ArtItemDisplayPopupViewModel(IPopupService popupService)
    {
        var window = Application.Current?.Windows[0];
        _width = window?.Width ?? 100;
        _height = window?.Height ?? 100;

        CloseButtonClicked = new Command(() =>
        {
            popupService.ClosePopupAsync(Shell.Current);
        }); 
    }
}