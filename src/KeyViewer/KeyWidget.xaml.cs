using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace KeyViewer
{
    public partial class KeyWidget : UserControl
    {
        private bool isDragging = false;
        private Point Offset;

        public KeyWidget()
        {
            InitializeComponent();

            this.MouseDown += Widget_MouseDown;
            this.MouseMove += Widget_MouseMove;
            this.MouseUp += Widget_MouseUp;

            DispatcherTimer refreshTimer = new DispatcherTimer();
            refreshTimer.Interval = TimeSpan.FromMilliseconds(16);
            refreshTimer.Tick += (_, __) => UpdateKeys();
            refreshTimer.Start();
        }

        private void Widget_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isDragging = true;
            Offset = e.GetPosition(this);
            this.CaptureMouse();
        }

        private void Widget_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isDragging) return;

            var parent = this.Parent as Canvas;
            if (parent == null) return;

            var pos = e.GetPosition(parent);
            Canvas.SetLeft(this, pos.X - Offset.X);
            Canvas.SetTop(this, pos.Y - Offset.Y);
        }

        private void Widget_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            this.ReleaseMouseCapture();
        }

        public void UpdateKeys()
        {
            ABlock.Background = InputStats.KeysDown[Key.A] ? Brushes.White : Brushes.Gray;
            SBlock.Background = InputStats.KeysDown[Key.S] ? Brushes.White : Brushes.Gray;
            SemicolonBlock.Background = InputStats.KeysDown[Key.OemSemicolon] ? Brushes.White : Brushes.Gray;
            QuoteBlock.Background = InputStats.KeysDown[Key.OemQuotes] ? Brushes.White : Brushes.Gray;

            AText.Foreground = InputStats.KeysDown[Key.A] ? Brushes.Black : Brushes.White;
            SText.Foreground = InputStats.KeysDown[Key.S] ? Brushes.Black : Brushes.White;
            SemicolonText.Foreground = InputStats.KeysDown[Key.OemSemicolon] ? Brushes.Black : Brushes.White;
            QuoteText.Foreground = InputStats.KeysDown[Key.OemQuotes] ? Brushes.Black : Brushes.White;
        }
    }
}