using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace FirstMauiApp.ViewModel;

public partial class MainViewModel : ObservableObject
{
    IConnectivity connectivity;

    public MainViewModel(IConnectivity connectivity)
    {
        Items = [];
        this.connectivity = connectivity;
    }

    [ObservableProperty]
    ObservableCollection<string> items;

    [ObservableProperty]
    string? text;

    [RelayCommand]
    async Task Add()
    {
        if (string.IsNullOrWhiteSpace(Text))
            return;

        if (connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            await Shell.Current.DisplayAlert("Uh Oh!", "No internet connection", "Ok");
            return;
        }

        Items.Add(Text);

        Text = string.Empty;
    }

    [RelayCommand]
    void Delete(string item)
    {
        if (Items.Contains(item))
        {
            Items.Remove(item);
        }
    }

    [RelayCommand]
    async Task Tap(string item)
    {
        await Shell.Current.GoToAsync($"{nameof(DetailPage)}?Text={item}");
    }
}
