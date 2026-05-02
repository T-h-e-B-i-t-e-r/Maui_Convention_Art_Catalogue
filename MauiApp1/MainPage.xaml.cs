using MauiApp1.ViewModels;

namespace MauiApp1;

public partial class MainPage : ContentPage
{
    public CategoryButtonsSelectionAreaViewModel CategoryButtonsSelectionAreaViewModel { get; set; }
    public SortButtonSelectionAreaViewModel SortButtonSelectionAreaViewModel { get; set; }
    public ArtItemDisplayViewModel ArtItemDisplayViewModel { get; set; }

    public MainPage()
    {
        InitializeComponent();
        CategoryButtonsSelectionAreaViewModel = new();
        SortButtonSelectionAreaViewModel = new();
        ArtItemDisplayViewModel = new(CategoryButtonsSelectionAreaViewModel.SelectedButtonViewModel,
            SortButtonSelectionAreaViewModel.SelectedButtonViewModel);
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

        previous?.IsSelected = false;
        current?.IsSelected = true;

        if (current != null)
        {
            ArtItemDisplayViewModel.SetDisplayCategory(current);
        }
    }

    private void OnSortButtonClicked(object sender, TappedEventArgs e)
    {
        var sortButton = e.Parameter as SortButtonViewModel;
        if (sortButton == null)
        {
            return;
        }

        if (SortButtonSelectionAreaViewModel.SelectedButtonViewModel != sortButton)
        {
            SortButtonSelectionAreaViewModel.SelectedButtonViewModel.IsSelected = false;
            sortButton.IsSelected = true;
            SortButtonSelectionAreaViewModel.SelectedButtonViewModel = sortButton;
        }
        else
        {
            sortButton.IsDescending = !sortButton.IsDescending;
        }

        ArtItemDisplayViewModel.UpdateSortAndDisplay(sortButton);
    }
}