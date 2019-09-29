using System;
using System.Windows;
using System.Windows.Threading;

namespace SimpleAsync
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(1000);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        /// <summary>
        /// Keep a visible timer running to show that the main thread is active
        /// </summary>
        void timer_Tick(object sender, EventArgs e)
        {
            TimerTextBlock.Text = DateTime.Now.Ticks.ToString();
        }

        /// <summary>
        /// Warm-up system by ensuring code is loaded/JIT
        /// </summary>
        /// <remarks>
        /// Done empirically, reason is assumed
        /// </remarks>
        private async void WarmUp_Click(object sender, RoutedEventArgs e)
        {
            var display = new Display(this);
            display.DisplayText("--------------------- Warm-up");
            var task = System.Threading.Tasks.Task.Delay(10);
            display.DisplayText("-------------------- Done");
        }

        /// <summary>
        /// Call method that simulates a delay
        /// </summary>
        /// <remarks>
        /// Note that this is a void return method. The NetworkDownload 
        /// illustrates a more realistic example with a Task returning method
        /// </remarks>
        private async void SimplestButton_Click(object sender, RoutedEventArgs e)
        {
            var display = new Display(this);
            display.DisplayText("---------------------");
            display.DisplayText("a. Button clicked!!");
            var simple = new SimpleAsyncClass(display);
            var task = simple.MyAsyncMethod();
            display.DisplayText($"c. After task returned");
            await task;
            display.DisplayText($"f. After await");
        }


        /// <summary>
        /// Illustrates a network operation
        /// </summary>
        /// <remarks>
         /// Note that all operations occur on the same thread
        /// </remarks>
        private async void NetworkDownloadButton_Click(object sender, RoutedEventArgs e)
        {
            var display = new Display(this);
            display.DisplayText("---------------------");
            display.DisplayText($"a. Starting network download {Utils.CurrentThread}");
            var webRead = new AsyncWebReadClass(display);
            var task = webRead.Run();
            string result = await task;
            Console.WriteLine(result);
            display.DisplayText($"{result}\r\n");
        }

        private async void ClearDisplay_Click(object sender, RoutedEventArgs e)
        {
            ResultTextBlock.Text = "";
        }

        public class Display : IDisplay
        {
            private readonly MainWindow _window;
            public Display(MainWindow window)
            {
                _window = window;
            }

            public void DisplayText(string text)
            {
                _window.ResultTextBlock.Text += $"{text}\r\n";
            }
        }
    }
}
