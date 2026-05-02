using CommunityToolkit.Mvvm.ComponentModel;
using MauiApp1.Constants;

namespace MauiApp1.ViewModels;

public partial class CategoryButtonsSelectionAreaViewModel : ObservableObject
{
    [ObservableProperty] public partial List<CategoryButtonViewModel> CategoryButtons { get; set; }
    public CategoryButtonViewModel SelectedButtonViewModel { get; set; }

    public CategoryButtonsSelectionAreaViewModel()
    {
        CategoryButtons = new()
        {
            new CategoryButtonViewModel("All", CategoryButtonType.All, ArtCategory.None),
            new CategoryButtonViewModel("New", CategoryButtonType.New, ArtCategory.None),
            new CategoryButtonViewModel("Category 1", CategoryButtonType.SpecificArtCategory, ArtCategory.One),
            new CategoryButtonViewModel("Category 2", CategoryButtonType.SpecificArtCategory, ArtCategory.Two),
            new CategoryButtonViewModel("Category 3", CategoryButtonType.SpecificArtCategory, ArtCategory.Three),
        };

        SelectedButtonViewModel = CategoryButtons[0];
    }
}