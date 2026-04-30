using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiApp1.ViewModels;

public partial class CategoryButtonsSelectionAreaViewModel : ObservableObject
{
    [ObservableProperty]
    public partial List<CategoryButtonViewModel> CategoryButtons { get; set; }
    public CategoryButtonViewModel SelectedButtonViewModel { get; set; }

    public CategoryButtonsSelectionAreaViewModel()
    {
        CategoryButtons = new()
        {
            new CategoryButtonViewModel("All"),
            new CategoryButtonViewModel("Recent"),
            new CategoryButtonViewModel("Category 1"),
            new CategoryButtonViewModel("Category 2"),
            new CategoryButtonViewModel("Category 3"),
        };
        
        SelectedButtonViewModel = CategoryButtons[0];
    }
}