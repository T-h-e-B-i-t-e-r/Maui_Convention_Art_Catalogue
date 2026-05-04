using CommunityToolkit.Mvvm.ComponentModel;
using MauiApp1.Models;

namespace MauiApp1.ViewModels;

public partial class ArtItemEntryViewModel(ArtItemEntry artItemEntry) : ObservableObject
{
    [ObservableProperty] private bool _isFavorite;

    [ObservableProperty] 
    private ArtItemEntry _artItemEntry = artItemEntry;
}