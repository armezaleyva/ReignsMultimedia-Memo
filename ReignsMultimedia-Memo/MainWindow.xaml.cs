using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Diagnostics;

namespace ReignsMultimedia_Memo {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        Stopwatch stopwatch;
        TimeSpan previousTime;

        public MainWindow() {
            InitializeComponent();
            stopwatch = new Stopwatch();
            stopwatch.Start();
            previousTime = stopwatch.Elapsed;
            
            panelBase.Children.Add(new IntroLogo());
                

            ThreadStart threadStart = new ThreadStart(update);
            Thread thread = new Thread(threadStart);
            thread.Start();
        }

        double fadeIn = 0.0000;
        double fadeOut = 1.000;
        double timer = 0.0000;
        void update() {
            while (true) {
                Dispatcher.Invoke(
                () => {
                    var currentTime = stopwatch.Elapsed;
                    var deltaTime = currentTime - previousTime;
                    
                    
                    if (fadeIn < 1) {
                        fadeIn += 0.00001;
                        panelBase.Opacity = fadeIn;
                    } else {
                        timer += 0.0001;
                        if (timer >= 1) {
                            if (fadeOut > 0) {
                                fadeOut -= 0.00001;
                                panelBase.Opacity = fadeOut;
                            }
                        }
                    }
                    
                    
                    previousTime = currentTime;
                });
            }
        }
    }
}
