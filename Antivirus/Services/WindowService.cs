using Antivirus.UserControls;
using Antivirus.ViewModel;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
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

        // Реализация Singleton
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
            var viewInstance = new TView();
            var viewModelInstance = new TViewModel();
            viewInstance.DataContext = viewModelInstance;
            CurrentView = viewInstance;
            ViewChanged?.Invoke(viewInstance);
        }

        /// <summary>
        /// Отображает UserControl, используя предварительно инициализированный ViewModel.
        /// </summary>
        /// <typeparam name="TView">Тип UserControl (View).</typeparam>
        /// <typeparam name="TViewModel">Тип ViewModel.</typeparam>
        /// <param name="viewModelInstance">Заполненный ViewModel для использования.</param>
        public void Show<TView, TViewModel>(TViewModel viewModelInstance)
            where TView : UserControl, new()
            where TViewModel : ObservableObject
        {
            var viewInstance = new TView();
            viewInstance.DataContext = viewModelInstance;
            CurrentView = viewInstance;
            ViewChanged?.Invoke(viewInstance);
        }
        public void ShowWindow<TView, TViewModel>()
            where TView : Window, new()
            where TViewModel : ObservableObject, new()
        {
            var viewInstance = new TView();
            var viewModelInstance = new TViewModel();
            viewInstance.DataContext = viewModelInstance;
            viewInstance.Show();
        }
        #endregion
    }
}