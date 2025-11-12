using Antivirus.Services;
using Antivirus.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antivirus.ViewModel
{
    internal partial class MainWindowViewModel : ObservableObject
    {

        #region Methods
        [RelayCommand]
        public void GetFileInfo()
        {
            WindowService.Instance.Show<FileAllInfoView, FileAllInfoViewModel>();
        }
        #endregion
    }
}
