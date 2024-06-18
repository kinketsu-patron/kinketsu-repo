/**
 * =============================================================
 * File         :MainWindowViewModel.cs
 * Summary      :MainWindowのビューモデル
 * Author       :kinketsu patron (https://kinketsu-patron.com)
 * Ver          :1.0
 * Date         :2024/06/18
 * =============================================================
 */

// =======================================================
// using
// =======================================================
using Prism.Mvvm;

namespace Pachislot_DataCounter.ViewModels
{
        public class MainWindowViewModel : BindableBase
        {
        private string _title = "Prism Application";
        public string Title
        {
                get { return _title; }
                set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel()
        {

        }
        }
}
