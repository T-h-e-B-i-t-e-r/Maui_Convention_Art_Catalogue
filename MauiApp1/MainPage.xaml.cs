using MauiApp1.ViewModels;

namespace MauiApp1;

public partial class MainPage : ContentPage
{
    public CategoryButtonsSelectionAreaViewModel CategoryButtonsSelectionAreaViewModel
    {
        get;
        set;
    }

    public MainPage()
    {
        InitializeComponent();
        CategoryButtonsSelectionAreaViewModel = new();
        BindingContext = this;
    }

    private void OnCategoryButtonClicked(object sender, SelectionChangedEventArgs e)
    {
        SetCategoryButtonState(e.PreviousSelection, e.CurrentSelection);
    }

    private void SetCategoryButtonState(IEnumerable<object> previousSelectedItems,
        IEnumerable<object> currentSelectedItems)
    {
        var previous = previousSelectedItems.FirstOrDefault() as CategoryButtonViewModel;
        var current = currentSelectedItems.FirstOrDefault() as CategoryButtonViewModel;

        if (previous == current)
        {
            return;
        }

        previous?.IsSelected = false;
        current?.IsSelected = true;
    }
}