using Antivirus.UserControls;
using Antivirus.ViewModel;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Antivirus.Services
{
        public partial class WindowService : ObservableObject
        {
            #region ObservableProperty
            [ObservableProperty]
            private UserControl? _currentView;
            #endregion

            private static readonly Lazy<WindowService> _instance = new(() => new WindowService());

            public static WindowService Instance => _instance.Value;

            #region Events
            public event Action<UserControl>? ViewChanged;
            #endregion

            #region Methods
            public void Show<TView, TViewModel>()
                where TView : UserControl, new()
                 where TViewModel : ObservableObject, new()
            {
                var viewType = typeof(TView);
                var viewModelType = typeof(TViewModel);

                // Создаем экземпляры View и ViewModel
                var viewInstance = new TView();
                var viewModelInstance = new TViewModel();

                // Связываем View и ViewModel
                viewInstance.DataContext = viewModelInstance;

                // Устанавливаем текущее представление
                CurrentView = viewInstance;

                // Генерируем событие ViewChanged
                ViewChanged?.Invoke(viewInstance);
            }
            public void ShowWindow<TView, TViewModel>()
                 where TView : Window, new()
                 where TViewModel : ObservableObject, new()
            {
                var viewType = typeof(TView);
                var viewModelType = typeof(TViewModel);

                // Создаем экземпляры View и ViewModel
                var viewInstance = new TView();
                var viewModelInstance = new TViewModel();

                // Связываем View и ViewModel
                viewInstance.DataContext = viewModelInstance;

                // Отображаем окно
                viewInstance.Show();
            }
            #endregion
        }
    }
