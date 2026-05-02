using CommunityToolkit.Mvvm.ComponentModel;
using MauiApp1.Constants;

namespace MauiApp1.ViewModels;

public partial class SortButtonViewModel(string title, SortButtonType sortButtonType) : ObservableObject
{
    public string Title { get; set; } = title;
    public SortButtonType SortButtonType = sortButtonType;
    [ObservableProperty] public partial bool IsSelected { get; set; }
    [ObservableProperty] public partial bool IsDescending { get; set; }

    
    public override string ToString()
    {
        return Title;
    }
}