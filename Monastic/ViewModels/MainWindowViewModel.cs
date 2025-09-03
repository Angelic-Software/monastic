using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.Input;

namespace Monastic.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public static string ButtonText => "Select SDK folder location";
    
    private string _sdkFolder = string.Empty;
    public string SdkFolder
    {
        get => _sdkFolder;
        set => SetProperty(ref _sdkFolder, value);
    }
    
    public ICommand SelectSdkFolderCommand { get; }
    
    public MainWindowViewModel()
    {
        SelectSdkFolderCommand = new RelayCommand<Window>(async window => await PickFolderAsync(window));
    }
    
    private async Task PickFolderAsync(Window window)
    {
        FolderPickerOpenOptions options = new()
        {
            Title = "Select a folder"
        };

        IReadOnlyList<IStorageFolder> folders = await window.StorageProvider.OpenFolderPickerAsync(options);

        if (folders.Count > 0)
        {
            SdkFolder = folders[0].Name;
        }
    }
    
}