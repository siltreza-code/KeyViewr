using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace KeyViewer
{
    public partial class StatsWidget : UserControl
    {
        private bool dragging = false;
        private Point offset;

        public StatsWidget()
        {
            InitializeComponent();

            this.MouseDown += Widget_MouseDown;
            this.MouseMove += Widget_MouseMove;
            this.MouseUp += Widget_MouseUp;

            DispatcherTimer refreshTimer = new DispatcherTimer();
            refreshTimer.Interval = TimeSpan.FromMilliseconds(16);
            refreshTimer.Tick += (_, __) => UpdateStats();
            refreshTimer.Start();
        }

        private void Widget_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dragging = true;
            offset = e.GetPosition(this);
            this.CaptureMouse();
        }

        private void Widget_MouseMove(object sender, MouseEventArgs e)
        {
            if (!dragging) return;

            var parent = this.Parent as Canvas;
            if (parent == null) return;

            var pos = e.GetPosition(parent);
            Canvas.SetLeft(this, pos.X - offset.X);
            Canvas.SetTop(this, pos.Y - offset.Y);
        }

        private void Widget_MouseUp(object sender, MouseButtonEventArgs e)
        {
            dragging = false;
            this.ReleaseMouseCapture();
        }

        public void UpdateStats()
        {
            KPSBlock.Text = $"KPS: {InputStats.CurrentKPS}";
            MaxBlock.Text = $"MAX: {InputStats.MaxKPS}";
            TotalBlock.Text = $"TOTAL: {InputStats.TotalKeys}";
        }
    }
}