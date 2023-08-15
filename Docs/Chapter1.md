### 初始的结构目录

依靠 `WPF UI` 创建的 `MVVM` 式的工程结构如图所示。

![8e09e60b3c99ad5b657bdd79a6316e4b.png](../../../_resources/8e09e60b3c99ad5b657bdd79a6316e4b.png)

### 运行概览

运行结果如下图所示。

`Home` 界面下，点击按钮数字加一。

![e4dacedf08a9ade6ad54413b7e20d100.png](../../../_resources/e4dacedf08a9ade6ad54413b7e20d100.png)

`Data` 界面下，有许多颜色面板。

![dcd859dfbad768387810467996ddb700.png](../../../_resources/dcd859dfbad768387810467996ddb700.png)

`Settings` 界面下，可以切换主题。

![image-20230815100021879](C:\Users\86176\.config\joplin-desktop\image-20230815100021879.png)

### 简单的实现-添加一个自减的按钮

#### 效果

![d5201d0fa05177a122e5030d522ab088.png](../../../_resources/d5201d0fa05177a122e5030d522ab088.png)

第一步，创建 `ViewModel` ，在 `ViewModels/Pages/` 创建 `ExampleViewModel.cs` ，以下代码仿照 `ViewModels/Pages/DashBoardViewModel.cs`

```c#
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
```

第二步，创建 `Page` ，在 `Views/Pages/` 创建 `ExamplePage` 的页面，以下代码仿照 `Views/Page/DashBoard.xaml` 

```xaml
<Page
    x:Class="UiDesktopApp2.Views.Pages.ExamplePage"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:local="clr-namespace:UiDesktopApp2.Views.Pages"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    xmlns:ui="http://schemas.lepo.co/wpfui/2022/xaml"
    Title="ExamplePage"
    d:DataContext="{d:DesignInstance local:ExamplePage,
                                     IsDesignTimeCreatable=False}"
    d:DesignHeight="450"
    d:DesignWidth="800"
    ui:Design.Background="{DynamicResource ApplicationBackgroundBrush}"
    ui:Design.Foreground="{DynamicResource TextFillColorPrimaryBrush}"
    Foreground="{DynamicResource TextFillColorPrimaryBrush}"
    mc:Ignorable="d">

    <Grid VerticalAlignment="Top">
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="Auto" />
            <ColumnDefinition Width="Auto" />
        </Grid.ColumnDefinitions>

        <ui:Button
            Grid.Column="0"
            Command="{Binding ViewModel.ExamIncrementCommand, Mode=OneWay}" 
            Content="Click me!"
            Icon="Fluent24" />  <!-- ExamIncrementCommand  -->
        <TextBlock
            Grid.Column="1"
            Margin="12,0,0,0"
            VerticalAlignment="Center"
            Text="{Binding ViewModel.Exam, Mode=OneWay}" />  <!-- Exam 为ViewModel中设置的 -->
    </Grid>
</Page>
```

第三步，编写对应的后台代码

```c#
using UiDesktopApp2.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace UiDesktopApp2.Views.Pages
{
    /// <summary>
    /// ExamplePage.xaml 的交互逻辑
    /// </summary>
    public partial class ExamplePage : INavigableView<ExampleViewModel> /* 将原先的改完这里继承的 */
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
```

第四步，填加相应的配置，在 `App.xaml.cs` 中

```csharp
services.AddSingleton<ExamplePage>();
services.AddSingleton<ExampleViewModel>();
```

第五步，添加左侧菜单栏

```c#
/* 左侧菜单栏设置 */
[ObservableProperty]
private ObservableCollection<object> _menuItems = new()
{
	new NavigationViewItem()
	{
		Content = "Home",
		Icon = new SymbolIcon { Symbol = SymbolRegular.Home24 },
		TargetPageType = typeof(Views.Pages.DashboardPage)
    },
	new NavigationViewItem()
	{
        Content = "Data",
        Icon = new SymbolIcon { Symbol = SymbolRegular.DataHistogram24 },
        TargetPageType = typeof(Views.Pages.DataPage)
	},
    /* 以下为新添加的部分 */
    new NavigationViewItem()
    {
        Content = "Example",
        Icon = new SymbolIcon {Symbol = SymbolRegular.AccessibilityCheckmark28},
        TargetPageType = typeof(Views.Pages.ExamplePage)
    }
};
```

通过以上的设置就可以完成如最上图的演示效果。