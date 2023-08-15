using UiDesktopApp2.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace UiDesktopApp2.Views.Pages
{
    /// <summary>
    /// ExamplePage.xaml 的交互逻辑
    /// </summary>
    public partial class ExamplePage : INavigableView<ExampleViewModel>
    {
        public ExampleViewModel ViewModel { get; }
        public ExamplePage(ExampleViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }
    }
}
