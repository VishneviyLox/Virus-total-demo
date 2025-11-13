using Antivirus.Services;
using Antivirus.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;

namespace Antivirus.ViewModel
{
    internal partial class MainWindowViewModel : ObservableObject
    {

        #region Methods
        [RelayCommand]
        public async Task GetFileInfo()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Выберите файл для сканирования";

            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {
                string filePath = openFileDialog.FileName;
                var fileInfoViewModel = new FileAllInfoViewModel();
                await fileInfoViewModel.LoadFileAnalysisAsync(filePath);
                WindowService.Instance.Show<FileAllInfoView, FileAllInfoViewModel>(fileInfoViewModel);
            }
        }
        #endregion
    }
}
