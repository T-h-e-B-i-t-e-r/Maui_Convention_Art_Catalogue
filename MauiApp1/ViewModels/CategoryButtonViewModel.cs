using CommunityToolkit.Mvvm.ComponentModel;
using MauiApp1.Constants;

namespace MauiApp1.ViewModels;

public partial class CategoryButtonViewModel(
    string title,
    CategoryButtonType categoryButtonType,
    ArtCategory artCategory) : ObservableObject
{
    public string Title { get; set; } = title;
    public CategoryButtonType CategoryButtonType = categoryButtonType;
    public ArtCategory ArtCategory = artCategory;

    [ObservableProperty] public partial bool IsSelected { get; set; }

    public override string ToString()
    {
        return Title;
    }
}