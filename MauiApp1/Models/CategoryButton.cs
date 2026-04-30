namespace MauiApp1.Models;

public class CategoryButton(string title)
{
    public string Title { get; set; } = title;

    public override string ToString()
    {
        return Title;
    }
}