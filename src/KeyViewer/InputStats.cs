using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Threading;

namespace KeyViewer
{
    public static class InputStats
    {
        public static HashSet<Key> TrackedKeys = new() { Key.A, Key.S, Key.OemSemicolon, Key.OemQuotes };

        public static Dictionary<Key, bool> KeysDown = new()
        {
            { Key.A, false },
            { Key.S, false },
            { Key.OemSemicolon, false },
            { Key.OemQuotes, false }
        };

        public static int TotalKeys = 0;
        public static int CurrentKPS = 0;
        public static int MaxKPS = 0;

        private static DispatcherTimer frameTimer;

        static InputStats()
        {
            frameTimer = new DispatcherTimer();
            frameTimer.Interval = TimeSpan.FromMilliseconds(16);
            frameTimer.Tick += (_, __) =>
            {
                if (CurrentKPS > MaxKPS) MaxKPS = CurrentKPS;
            };
            frameTimer.Start();
        }

        public static void KeyDown(Key key)
        {
            if (!TrackedKeys.Contains(key)) return;

            if (!KeysDown[key])
            {
                KeysDown[key] = true;
                TotalKeys++;

                CurrentKPS++;

                var timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += (_, __) =>
                {
                    CurrentKPS = Math.Max(CurrentKPS - 1, 0);
                    timer.Stop();
                };
                timer.Start();
            }
        }

        public static void KeyUp(Key key)
        {
            if (!TrackedKeys.Contains(key)) return;
            KeysDown[key] = false;
        }
    }
}