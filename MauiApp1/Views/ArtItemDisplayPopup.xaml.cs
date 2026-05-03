using MauiApp1.Models;
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
        if (query.TryGetValue("artItemEntry", out var artItemEntry))
        {
            var item = artItemEntry as ArtItemEntry;
            if (item != null)
            {
                _viewModel.ItemName = item.Name;
                _viewModel.SpriteFilename = item.SpriteFilename;
                _viewModel.Date = item.Date;
                _viewModel.Size = item.Size;
                _viewModel.Price = item.Price;
            }
        }
    }
}