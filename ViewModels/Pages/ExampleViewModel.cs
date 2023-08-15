using System.Diagnostics.Metrics;

namespace UiDesktopApp2.ViewModels.Pages
{
    public partial class ExampleViewModel: ObservableObject
    {
        [ObservableProperty]
        private int _exam = 100;

        [RelayCommand]
        private void OnExamIncrement()
        {
            Exam--;
        }
    }
}
