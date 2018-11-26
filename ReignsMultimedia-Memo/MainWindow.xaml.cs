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

        public enum GameState { Intro, Menu, Gameplay, Minigame, Gameover };
        public static GameState gameState = GameState.Intro;

        List<Event> events = new List<Event>();

        public MainWindow() {
            InitializeComponent();
            panelBase.Focus();

            Event event1 = new Event("Sebas", new BitmapImage(new Uri(
                    "/Assets/Characters/Sebas.png", UriKind.Relative)),
                    "test", "right", new List<int> { 0, 0, 0, 0 },
                    null, "left", new List<int> { 0, 0, 0, 0 }, null);
            events.Add(event1);

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

        bool initializingGameplayWindow = true;

        void Update() {
            while (true) {
                if (gameState == GameState.Intro) {
                    Dispatcher.Invoke(AnimateIntro);
                    //Dispatcher.Invoke(new Action(() => AnimateIntro()));
                }

                if (gameState == GameState.Gameplay) {
                    Dispatcher.Invoke(
                    () => {
                        if (initializingGameplayWindow) {
                            panelBase.Children.Clear();
                            panelBase.Children.Add(new GameplayWindow());
                            initializingGameplayWindow = false;
                        }
                        var gameplayWindow = (GameplayWindow)panelBase.Children[0];
                        var eventPanel = (EventPanel)gameplayWindow.PanelEvent.Children[0];
                        eventPanel.lblPersonaje.Text = events[0].EventCharacter;
                        eventPanel.imgEvent.Source = events[0].CharacterImage;
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
                        panelBase.Children.Add(new MainMenu());
                        panelBase.Opacity = 1;
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

        private void PanelBase_KeyDown(object sender, KeyEventArgs e) {
            if (gameState == GameState.Gameplay) {
                if (e.Key == Key.A || e.Key == Key.Left) {
                    Application.Current.Shutdown();
                }
            }
        }
    }
}
