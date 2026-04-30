using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiApp1.ViewModels;

public partial class CategoryButtonViewModel(string title) : ObservableObject
{
    public string Title { get; set; } = title;

    [ObservableProperty]
    public partial bool IsSelected { get; set; }

    public override string ToString()
    {
        return Title;
    }
}