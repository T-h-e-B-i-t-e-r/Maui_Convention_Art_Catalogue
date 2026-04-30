using CommunityToolkit.Mvvm.ComponentModel;
using MauiApp1.Models;

namespace MauiApp1.ViewModels;

public partial class CategoryButtonsViewModel : ObservableObject
{
    [ObservableProperty]
    private List<CategoryButton> _categoryButtons = new();
    
    private CategoryButton _selectedButton;

    private void SelectButton(CategoryButton categoryButton)
    {
        _selectedButton =  categoryButton;
    }

    public CategoryButtonsViewModel()
    {
        _categoryButtons = new()
        {
            new CategoryButton("All"),
            new CategoryButton("Recent"),
            new CategoryButton("Category 1"),
            new CategoryButton("Category 2"),
            new CategoryButton("Category 3"),
        };
    }
}