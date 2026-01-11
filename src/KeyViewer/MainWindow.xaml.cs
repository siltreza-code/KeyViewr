using Gma.System.MouseKeyHook;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KeyViewer
{
    public partial class MainWindow : Window
    {
        private KeyWidget keyWidget;
        private StatsWidget statsWidget;

        private IKeyboardMouseEvents globalHook;

        public MainWindow()
        {
            InitializeComponent();

            this.Left = 0;
            this.Top = 0;
            this.Width = SystemParameters.PrimaryScreenWidth;
            this.Height = SystemParameters.PrimaryScreenHeight;

            this.WindowStyle = WindowStyle.None;
            this.AllowsTransparency = true;
            this.Background = System.Windows.Media.Brushes.Transparent;

            this.Topmost = true;
            this.ShowInTaskbar = false;

            keyWidget = new KeyWidget();
            statsWidget = new StatsWidget();

            RootCanvas.Children.Add(keyWidget);
            RootCanvas.Children.Add(statsWidget);

            Canvas.SetLeft(keyWidget, 50);
            Canvas.SetTop(keyWidget, 50);

            Canvas.SetLeft(statsWidget, 300);
            Canvas.SetTop(statsWidget, 50);

            globalHook = Hook.GlobalEvents();
            globalHook.KeyDown += GlobalHook_KeyDown;
            globalHook.KeyUp += GlobalHook_KeyUp;
        }

        private void GlobalHook_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            Key key = KeyInterop.KeyFromVirtualKey(e.KeyValue);
            InputStats.KeyDown(key);

            keyWidget.UpdateKeys();
            statsWidget.UpdateStats();
        }

        private void GlobalHook_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            Key key = KeyInterop.KeyFromVirtualKey(e.KeyValue);
            InputStats.KeyUp(key);

            keyWidget.UpdateKeys();
            statsWidget.UpdateStats();
        }
    }
}
