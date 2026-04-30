using MauiApp1.ViewModels;

namespace MauiApp1;

public partial class MainPage : ContentPage
{
    public CategoryButtonsViewModel CategoryButtonsViewModel
    {
        get;
        set;
    }

    public MainPage()
    {
        InitializeComponent();
        CategoryButtonsViewModel = new();
        BindingContext = this;
    }
}