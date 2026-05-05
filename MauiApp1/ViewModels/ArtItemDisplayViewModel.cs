using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Constants;
using MauiApp1.Models;

namespace MauiApp1.ViewModels;

public partial class ArtItemDisplayViewModel : ObservableObject
{
    private List<ArtItemEntryViewModel> _allArtItems = new();
    private List<ArtItemEntryViewModel> _newArtItems = new();
    private Dictionary<ArtCategory, List<ArtItemEntryViewModel>> _categoryArtItems = new();
    private HashSet<ArtItemEntryViewModel> _favoritedItems = new();
    
    private const string _RECENT_DATE_STRING_LIMIT = "January 01, 2026";
    private DateTime _recentDateTimeLimit = DateTime.Parse(_RECENT_DATE_STRING_LIMIT);

    private CategoryButtonViewModel _currentCategoryButtonViewModel;
    private SortButtonViewModel _currentSortButtonViewModel;

    [ObservableProperty]
    private List<ArtItemEntryViewModel> _displayedArtItems;
    
    [ObservableProperty]
    private SortButtonType _currentSortButtonType;

    [ObservableProperty]
    private int _favoritedItemsCount;

    public ArtItemDisplayViewModel(CategoryButtonViewModel currentCategoryButtonViewModel,
        SortButtonViewModel currentSortButtonViewModel)
    {
        InitializeArtItemData();

        _displayedArtItems = _allArtItems;

        _currentCategoryButtonViewModel = currentCategoryButtonViewModel;
        _currentSortButtonViewModel = currentSortButtonViewModel;
        _currentSortButtonType = _currentSortButtonViewModel.SortButtonType;
        _favoritedItemsCount = _favoritedItems.Count;
    }

    private void InitializeArtItemData()
    {
        PopulateAllArtItems();
        
        _newArtItems.Clear();
        _categoryArtItems.Clear();
        
        foreach (var artItem in _allArtItems)
        {
            if (artItem.ArtItemEntry.Categories.HasFlag(ArtCategory.One))
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
            
            if (artItem.ArtItemEntry.Categories.HasFlag(ArtCategory.Two))
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
            
            if (artItem.ArtItemEntry.Categories.HasFlag(ArtCategory.Three))
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

            if (artItem.ArtItemEntry.DateTime >= _recentDateTimeLimit)
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
            new ArtItemEntryViewModel(new ArtItemEntry
            {
                Name = "Art Piece 1",
                SpriteFilename = "sample_1_art.png",
                Date = "January 15, 2026",
                Price = 20,
                Size = ArtItemSize.Large,
                Categories = ArtCategory.One,
            }),
            new ArtItemEntryViewModel(new ArtItemEntry {
                Name = "Art Piece 2",
                SpriteFilename = "sample_2_art.png",
                Date = "December 01, 2025",
                Price = 10,
                Size = ArtItemSize.Small,
                Categories = ArtCategory.Two,
            }),
            new ArtItemEntryViewModel(new ArtItemEntry
            {
                Name = "Art Piece 3",
                SpriteFilename = "sample_3_art.png",
                Date = "January 15, 2025",
                Price = 15,
                Size = ArtItemSize.Medium,
                Categories = ArtCategory.Three,
            }),
            new ArtItemEntryViewModel(new ArtItemEntry
            {
                Name = "Art Piece 4",
                SpriteFilename = "sample_4_art.png",
                Date = "February 21, 2025",
                Price = 20,
                Size = ArtItemSize.Large,
                Categories = ArtCategory.One | ArtCategory.Two,
            }),
            new ArtItemEntryViewModel(new ArtItemEntry
            {
                Name = "Art Piece 5",
                SpriteFilename = "sample_5_art.png",
                Date = "April 01, 2026",
                Price = 20,
                Size = ArtItemSize.Large,
                Categories = ArtCategory.Two | ArtCategory.Three,
            }),
            new ArtItemEntryViewModel(new ArtItemEntry
            {
                Name = "Art Piece 6",
                SpriteFilename = "sample_6_art.png",
                Date = "March 18, 2026",
                Price = 15,
                Size = ArtItemSize.Medium,
                Categories = ArtCategory.One | ArtCategory.Three,
            }),
            new ArtItemEntryViewModel(new ArtItemEntry
            {
                Name = "Art Piece 7",
                SpriteFilename = "sample_7_art.png",
                Date = "January 01, 2025",
                Price = 20,
                Size = ArtItemSize.Large,
                Categories = ArtCategory.One | ArtCategory.Two | ArtCategory.Three,
            }),
        };
    }

    public void SetDisplayCategory(CategoryButtonViewModel newCategoryButtonViewModel)
    {
        List<ArtItemEntryViewModel> listToSort;
        
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
    
    private void SortDisplayCategory(List<ArtItemEntryViewModel> listToSort)
    {
        CurrentSortButtonType = _currentSortButtonViewModel.SortButtonType;
        bool isDescending = _currentSortButtonViewModel.IsDescending;

        switch (CurrentSortButtonType)
        {
            case SortButtonType.Name:
            {
                if (!isDescending)
                {
                    listToSort.Sort((x, y) => String.Compare(x.ArtItemEntry.Name, y.ArtItemEntry.Name, StringComparison.CurrentCulture));
                }
                else
                {
                    listToSort.Sort((x, y) => String.Compare(y.ArtItemEntry.Name, x.ArtItemEntry.Name, StringComparison.CurrentCulture));
                }

                break;
            }
            case SortButtonType.Date:
            {
                if (!isDescending)
                {
                    listToSort.Sort((x, y) => x.ArtItemEntry.DateTime.CompareTo(y.ArtItemEntry.DateTime));
                }
                else
                {
                    listToSort.Sort((x, y) => y.ArtItemEntry.DateTime.CompareTo(x.ArtItemEntry.DateTime));
                }

                break;
            }
            case SortButtonType.Size:
            {
                if (!isDescending)
                {
                    listToSort.Sort((x, y) => x.ArtItemEntry.Size.CompareTo(y.ArtItemEntry.Size));
                }
                else
                {
                    listToSort.Sort((x, y) => y.ArtItemEntry.Size.CompareTo(x.ArtItemEntry.Size));
                }

                break;
            }
            case SortButtonType.Price:
            {
                if (!isDescending)
                {
                    listToSort.Sort((x, y) => x.ArtItemEntry.Price.CompareTo(y.ArtItemEntry.Price));
                }
                else
                {
                    listToSort.Sort((x, y) => y.ArtItemEntry.Price.CompareTo(x.ArtItemEntry.Price));
                }

                break;
            }
        }
    }
    
    [RelayCommand]
    private void FavoriteItemToggle(ArtItemEntryViewModel artItemEntryViewModel)
    {
        if (!_favoritedItems.Add(artItemEntryViewModel))
        {
            _favoritedItems.Remove(artItemEntryViewModel);
        }
        
        artItemEntryViewModel.IsFavorite = !artItemEntryViewModel.IsFavorite;

        FavoritedItemsCount = _favoritedItems.Count;
    }

    public List<ArtItemEntry> GetFavoritedItems()
    {
        return _favoritedItems.Select(artItemEntryViewModel => artItemEntryViewModel.ArtItemEntry).ToList();;
    }
}