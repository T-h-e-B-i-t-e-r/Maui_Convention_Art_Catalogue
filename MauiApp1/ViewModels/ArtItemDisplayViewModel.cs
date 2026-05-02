using CommunityToolkit.Mvvm.ComponentModel;
using MauiApp1.Constants;
using MauiApp1.Models;

namespace MauiApp1.ViewModels;

public partial class ArtItemDisplayViewModel : ObservableObject
{
    private List<ArtItemEntry> _allArtItems = new();
    private List<ArtItemEntry> _newArtItems = new();
    private Dictionary<ArtCategory, List<ArtItemEntry>> _categoryArtItems = new();
    
    private const string _RECENT_DATE_STRING_LIMIT = "January 01, 2026";
    private DateTime _recentDateTimeLimit = DateTime.Parse(_RECENT_DATE_STRING_LIMIT);

    private CategoryButtonViewModel _currentCategoryButtonViewModel;
    private SortButtonViewModel _currentSortButtonViewModel;

    [ObservableProperty]
    private List<ArtItemEntry> _displayedArtItems = new();
    
    [ObservableProperty]
    private SortButtonType _currentSortButtonType;

    public ArtItemDisplayViewModel(CategoryButtonViewModel currentCategoryButtonViewModel,
        SortButtonViewModel currentSortButtonViewModel)
    {
        InitializeArtItemData();

        _displayedArtItems = _allArtItems;

        _currentCategoryButtonViewModel = currentCategoryButtonViewModel;
        _currentSortButtonViewModel = currentSortButtonViewModel;
        _currentSortButtonType = _currentSortButtonViewModel.SortButtonType;
    }

    private void InitializeArtItemData()
    {
        PopulateAllArtItems();
        
        _newArtItems.Clear();
        _categoryArtItems.Clear();
        
        foreach (var artItem in _allArtItems)
        {
            if (artItem.Categories.HasFlag(ArtCategory.One))
            {
                if (_categoryArtItems.ContainsKey(ArtCategory.One))
                {
                    _categoryArtItems[ArtCategory.One].Add(artItem);    
                }
                else
                {
                    _categoryArtItems.TryAdd(ArtCategory.One, new() { artItem });
                }
            }
            
            if (artItem.Categories.HasFlag(ArtCategory.Two))
            {
                if (_categoryArtItems.ContainsKey(ArtCategory.Two))
                {
                    _categoryArtItems[ArtCategory.Two].Add(artItem);    
                }
                else
                {
                    _categoryArtItems.TryAdd(ArtCategory.Two, new() { artItem });
                }
            }
            
            if (artItem.Categories.HasFlag(ArtCategory.Three))
            {
                if (_categoryArtItems.ContainsKey(ArtCategory.Three))
                {
                    _categoryArtItems[ArtCategory.Three].Add(artItem);    
                }
                else
                {
                    _categoryArtItems.TryAdd(ArtCategory.Three, new() { artItem });
                }
            }

            if (artItem.DateTime >= _recentDateTimeLimit)
            {
                _newArtItems.Add(artItem);
            }
        }
    }

    // TODO: learn dynamic data loading/population
    private void PopulateAllArtItems()
    {
        _allArtItems = new()
        {
            new ArtItemEntry
            {
                Name = "Art Piece 1",
                SpriteFilename = "sample_1_art.png",
                Date = "January 15, 2026",
                Price = 20,
                Size = ArtItemSize.Large,
                Categories = ArtCategory.One,
            },
            new ArtItemEntry
            {
                Name = "Art Piece 2",
                SpriteFilename = "sample_2_art.png",
                Date = "December 01, 2025",
                Price = 10,
                Size = ArtItemSize.Small,
                Categories = ArtCategory.Two,
            },
            new ArtItemEntry
            {
                Name = "Art Piece 3",
                SpriteFilename = "sample_3_art.png",
                Date = "January 15, 2025",
                Price = 15,
                Size = ArtItemSize.Medium,
                Categories = ArtCategory.Three,
            },
            new ArtItemEntry
            {
                Name = "Art Piece 4",
                SpriteFilename = "sample_4_art.png",
                Date = "February 21, 2025",
                Price = 20,
                Size = ArtItemSize.Large,
                Categories = ArtCategory.One | ArtCategory.Two,
            },
            new ArtItemEntry
            {
                Name = "Art Piece 5",
                SpriteFilename = "sample_5_art.png",
                Date = "April 01, 2026",
                Price = 20,
                Size = ArtItemSize.Large,
                Categories = ArtCategory.Two | ArtCategory.Three,
            },
            new ArtItemEntry
            {
                Name = "Art Piece 6",
                SpriteFilename = "sample_6_art.png",
                Date = "March 18, 2026",
                Price = 15,
                Size = ArtItemSize.Medium,
                Categories = ArtCategory.One | ArtCategory.Three,
            },
            new ArtItemEntry
            {
                Name = "Art Piece 7",
                SpriteFilename = "sample_7_art.png",
                Date = "January 01, 2025",
                Price = 20,
                Size = ArtItemSize.Large,
                Categories = ArtCategory.One | ArtCategory.Two | ArtCategory.Three,
            },
        };
    }

    public void SetDisplayCategory(CategoryButtonViewModel newCategoryButtonViewModel)
    {
        List<ArtItemEntry> listToSort;
        
        if (_currentCategoryButtonViewModel == newCategoryButtonViewModel)
        {
            listToSort = new(DisplayedArtItems);
            SortDisplayCategory(listToSort);
            DisplayedArtItems = listToSort;
            return;
        }

        _currentCategoryButtonViewModel = newCategoryButtonViewModel;
        CategoryButtonType categoryButtonType = _currentCategoryButtonViewModel.CategoryButtonType;
        ArtCategory artCategory = _currentCategoryButtonViewModel.ArtCategory;
        
        switch (categoryButtonType)
        {
            case CategoryButtonType.All:
            {
                listToSort = new(_allArtItems);
                SortDisplayCategory(listToSort);
                DisplayedArtItems = listToSort;
                break;
            }
            case CategoryButtonType.New:
            {
                listToSort = new(_newArtItems);
                SortDisplayCategory(listToSort);
                DisplayedArtItems = listToSort;
                break;
            }
            case CategoryButtonType.SpecificArtCategory:
            default:
            {
                listToSort = new(_categoryArtItems[artCategory]);
                SortDisplayCategory(listToSort);
                DisplayedArtItems = listToSort;
                break;
            }
        }
    }

    public void UpdateSortAndDisplay(SortButtonViewModel newSortButtonViewModel)
    {
        _currentSortButtonViewModel = newSortButtonViewModel;
        SetDisplayCategory(_currentCategoryButtonViewModel);
    }
    
    private void SortDisplayCategory(List<ArtItemEntry> listToSort)
    {
        CurrentSortButtonType = _currentSortButtonViewModel.SortButtonType;
        bool isDescending = _currentSortButtonViewModel.IsDescending;

        switch (CurrentSortButtonType)
        {
            case SortButtonType.Name:
            {
                if (!isDescending)
                {
                    listToSort.Sort((x, y) => String.Compare(x.Name, y.Name, StringComparison.CurrentCulture));
                }
                else
                {
                    listToSort.Sort((x, y) => String.Compare(y.Name, x.Name, StringComparison.CurrentCulture));
                }

                break;
            }
            case SortButtonType.Date:
            {
                if (!isDescending)
                {
                    listToSort.Sort((x, y) => x.DateTime.CompareTo(y.DateTime));
                }
                else
                {
                    listToSort.Sort((x, y) => y.DateTime.CompareTo(x.DateTime));
                }

                break;
            }
            case SortButtonType.Size:
            {
                if (!isDescending)
                {
                    listToSort.Sort((x, y) => x.Size.CompareTo(y.Size));
                }
                else
                {
                    listToSort.Sort((x, y) => y.Size.CompareTo(x.Size));
                }

                break;
            }
            case SortButtonType.Price:
            {
                if (!isDescending)
                {
                    listToSort.Sort((x, y) => x.Price.CompareTo(y.Price));
                }
                else
                {
                    listToSort.Sort((x, y) => y.Price.CompareTo(x.Price));
                }

                break;
            }
        }
    }
}