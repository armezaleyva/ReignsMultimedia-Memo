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

        enum GameState { Intro, Menu, Gameplay, Minigame, Gameover };
        GameState gameState = GameState.Intro;

        List<Event> events = new List<Event>();

        public MainWindow() {
            InitializeComponent();

            Event event1 = new Event("test", "right", new List<int> { 0, 0, 0, 0 },
                    null, "left", new List<int> { 0, 0, 0, 0 }, null);
            Event event2 = new Event("test 2", "right 2", new List<int> { 0, 0, 0, 0 },
                    null, "left 2", new List<int> { 0, 0, 0, 0 }, null);
            Event event3 = new Event("test 3", "right 3", new List<int> { 0, 0, 0, 0 },
                    null, "left 3", new List<int> { 0, 0, 0, 0 }, null);

            

            events.Add(event1);
            events.Add(event2);
            events.Add(event3);

            stopwatch = new Stopwatch();
            stopwatch.Start();
            previousTime = stopwatch.Elapsed;
            
            panelBase.Children.Add(new IntroLogo());
                

            ThreadStart threadStart = new ThreadStart(Update);
            Thread thread = new Thread(threadStart);
            thread.Start();
        }

        
        double introFadeInAnimationDuration = 3.00;
        double introWaitDuration = 2.00;
        double introFadeOutAnimationDuration = 3.00;

        double fadeIn = 0.0000;
        double fadeOut = 1.000;

        double fadeInTimer = 0.0000;
        double waitTimer = 0.0000;
        double fadeOutTimer = 0.0000;

        void Update() {
            while (true) {
                if (gameState == GameState.Intro) {
                    Dispatcher.Invoke(AnimateIntro);
                    //Dispatcher.Invoke(new Action(() => AnimateIntro()));
                }

                
                if (gameState == GameState.Gameplay) {
                    Dispatcher.Invoke(
                    () => {
                        var panelGameplay = (Gameplay)panelBase.Children[0];
                        panelGameplay.lblEvent.Text = events[0].Text;

                        var random = new Random();
                        var randomIndex = random.Next(events.Count());
                        var selectedEvent = events[randomIndex];
                        events.RemoveAt(randomIndex);
                    });
                }

            }
        }

        void AnimateIntro() {
            var currentTime = stopwatch.Elapsed;
            var deltaTime = currentTime - previousTime;

            if (fadeInTimer < introFadeInAnimationDuration) {
                fadeInTimer += deltaTime.TotalSeconds;
                fadeIn += deltaTime.TotalSeconds / introFadeInAnimationDuration;
                panelBase.Opacity = fadeIn;
            }
            else {
                if (waitTimer >= introWaitDuration) {
                    if (fadeOutTimer <= introFadeOutAnimationDuration) {
                        fadeOutTimer += deltaTime.TotalSeconds;
                        fadeOut -= deltaTime.TotalSeconds / introFadeOutAnimationDuration;
                        panelBase.Opacity = fadeOut;
                    } else {
                        panelBase.Children.Clear();
                        gameState = GameState.Menu;
                        panelBase.Children.Add(new Gameplay());
                        panelBase.Opacity = 1;
                        gameState = GameState.Gameplay;
                    }
                }
                else {
                    waitTimer += deltaTime.TotalSeconds;
                }
            }

            previousTime = currentTime;
        }

        void AnimateEventIn() {

        }

        void AnimateEventOut() {

        }
    }
}
