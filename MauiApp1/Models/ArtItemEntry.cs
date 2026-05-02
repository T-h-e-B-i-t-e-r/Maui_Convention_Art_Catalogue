using System.Globalization;
using MauiApp1.Constants;

namespace MauiApp1.Models;

public class ArtItemEntry
{
    public string Name { get; set; }
    public string SpriteFilename { get; set; }
    public string Date { get; set; }
    public float Price { get; set; }
    public ArtItemSize Size { get; set; }
    public ArtCategory Categories { get; set; }

    public DateTime DateTime => DateTime.Parse(Date, new CultureInfo("en-US"));
}