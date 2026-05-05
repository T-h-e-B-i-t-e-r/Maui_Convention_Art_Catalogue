using MauiApp1.Models;
using MauiApp1.ViewModels;

namespace MauiApp1.Views;

public partial class FavoritedItemsCollectionViewPopup : ContentView, IQueryAttributable
{
    private FavoritedItemsCollectionViewPopupViewModel _viewModel;
    
    public FavoritedItemsCollectionViewPopup(FavoritedItemsCollectionViewPopupViewModel viewModel)
    {
        InitializeComponent();
        BackgroundColor = Colors.Transparent;
        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("artItemEntries", out var artItemEntryViewModels))
        {
            if (artItemEntryViewModels is List<ArtItemEntry> items)
            {
                _viewModel.FavoritedItems = new(items);
                _viewModel.FavoritedItemsCount = items.Count;
                _viewModel.TotalPrice = items.Sum(x => x.Price);
            }
        }
    }
}