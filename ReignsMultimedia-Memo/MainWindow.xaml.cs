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
                    "Sebas se metió en problemas con un maestro",
                    "Arreglo el problema con el maestro y Sebas", new List<int> { -10, 10, 0, 0 }, null,
                    "Regaño al Sebas", new List<int> { -20, 0, 0, 0 }, null);
            Event event2 = new Event("Sofía", new BitmapImage(new Uri(
                    "/Assets/Characters/Sofia.png", UriKind.Relative)),
                    "Memo ¿Me puedes poner las horas de servicio?",
                    "“Sí, no hay problema”", new List<int> { 20, 0, -10, -10 }, null,
                    "“Primero tienes que hacer un video para viridis”", new List<int> { 10, 0, 20, -20 }, null);
            events.Add(event1);
            events.Add(event2);

            stopwatch = new Stopwatch();
            stopwatch.Start();
            previousTime = stopwatch.Elapsed;
            
            panelBase.Children.Add(new IntroLogo());
                

            ThreadStart threadStart = new ThreadStart(Update);
            Thread thread = new Thread(threadStart);
            thread.Start();
        }

        
        double introFadeInAnimationDuration = 2.50;
        double introWaitDuration = 2.00;
        double introFadeOutAnimationDuration = 3.00;
        double indicatorFadeInDuration = 0.60;

        double fadeIn = 0.0000;
        double fadeOut = 1.000;

        double fadeInTimer = 0.0000;
        double waitTimer = 0.0000;
        double fadeOutTimer = 0.0000;

        bool initializingGameplayWindow = true;
        bool transitioningToEvent = true;
        bool awaitingRightConfirmation = false;
        bool awaitingLeftConfirmation = false;

        Event currentEvent;

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
                            panelBase.Focus();
                            initializingGameplayWindow = false;
                        }

                        if (transitioningToEvent) {
                            ResetTimers();
                            NewEvent();
                        }
                        
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

        void NewEvent() {
            var random = new Random();
            var randomIndex = random.Next(events.Count());
            currentEvent = events[randomIndex];
            events.RemoveAt(randomIndex);

            var gameplayWindow = (GameplayWindow)panelBase.Children[0];
            var eventPanel = (EventPanel)gameplayWindow.PanelEvent.Children[0];
            var statsPanel = (StatsPanel)gameplayWindow.PanelStats.Children[0];
            ResetIndicators(statsPanel);
            eventPanel.lblPersonaje.Text = currentEvent.EventCharacter;
            eventPanel.lblEventoDescripcion.Text = currentEvent.Text;
            eventPanel.imgEvent.Source = currentEvent.CharacterImage;
            eventPanel.imgEvent.Margin = new Thickness(60, 0, 60, 80);
            eventPanel.lblRespuesta.Text = "";

            awaitingRightConfirmation = false;
            awaitingLeftConfirmation = false;
            transitioningToEvent = false;
        }

        void RightPrompt(List<int> eventEffects) {
            var gameplayWindow = (GameplayWindow)panelBase.Children[0];
            var eventPanel = (EventPanel)gameplayWindow.PanelEvent.Children[0];
            var statsPanel = (StatsPanel)gameplayWindow.PanelStats.Children[0];

            ThreadStart threadStart = new ThreadStart(new Action(() => FadeIndicators(eventEffects, statsPanel)));
            Thread thread = new Thread(threadStart);
            thread.Start();

            eventPanel.lblRespuesta.Text = currentEvent.RightReactionText;
            eventPanel.imgEvent.Margin = new Thickness(60, 0, 0, 80);
            awaitingRightConfirmation = true;
            awaitingLeftConfirmation = false;
        }

        void LeftPrompt(List<int> eventEffects) {
            var gameplayWindow = (GameplayWindow)panelBase.Children[0];
            var eventPanel = (EventPanel)gameplayWindow.PanelEvent.Children[0];
            var statsPanel = (StatsPanel)gameplayWindow.PanelStats.Children[0];

            ThreadStart threadStart = new ThreadStart(new Action(() => FadeIndicators(eventEffects, statsPanel)));
            Thread thread = new Thread(threadStart);
            thread.Start();

            eventPanel.lblRespuesta.Text = currentEvent.LeftReactionText;
            eventPanel.imgEvent.Margin = new Thickness(0, 0, 60, 80);
            awaitingLeftConfirmation = true;
            awaitingRightConfirmation = false;
        }

        void FadeIndicators(List<int> eventEffects, StatsPanel statsPanel) {
            ResetTimers();
            Dispatcher.Invoke(() => { ResetIndicators(statsPanel); });

            previousTime = stopwatch.Elapsed;
            while (true) {
                var currentTime = stopwatch.Elapsed;
                var deltaTime = currentTime - previousTime;

                if (fadeInTimer < indicatorFadeInDuration) {
                    fadeInTimer += deltaTime.TotalSeconds;
                    fadeIn += deltaTime.TotalSeconds / indicatorFadeInDuration;
                    Dispatcher.Invoke(
                    () => {
                        if (eventEffects[0] != 0) {
                            statsPanel.indicatorAlumnos.Opacity = fadeIn;
                        }
                        if (eventEffects[1] != 0) {
                            statsPanel.indicatorMaestros.Opacity = fadeIn;
                        }
                        if (eventEffects[2] != 0) {
                            statsPanel.indicatorAdministracion.Opacity = fadeIn;
                        }
                        if (eventEffects[3] != 0) {
                            statsPanel.indicatorEstres.Opacity = fadeIn;
                        }
                    });
                    
                } else {
                    break;
                }

                previousTime = currentTime;
            }
        }

        void ResetIndicators(StatsPanel statsPanel) {
            statsPanel.indicatorAlumnos.Opacity = 0;
            statsPanel.indicatorMaestros.Opacity = 0;
            statsPanel.indicatorAdministracion.Opacity = 0;
            statsPanel.indicatorEstres.Opacity = 0;
        }

        void AnimateEventIn() {

        }

        void AnimateEventOut() {

        }

        void TriggerEventEffects(List<int> eventEffects) {
            Stats.estudiantes += eventEffects[0];
            Stats.maestros += eventEffects[1];
            Stats.administracion += eventEffects[2];
            Stats.estres += eventEffects[3];
            transitioningToEvent = true;
        }

        private void PanelBase_KeyDown(object sender, KeyEventArgs e) {
            if (gameState == GameState.Gameplay) {
                if (!transitioningToEvent) {
                    if (e.Key == Key.D || e.Key == Key.Right) {
                        var eventEffects = currentEvent.RightReactionEffects;
                        if (awaitingRightConfirmation) {
                            TriggerEventEffects(eventEffects);
                        } else {
                            RightPrompt(eventEffects);
                        }
                    }
                    else if (e.Key == Key.A || e.Key == Key.Left) {
                        var eventEffects = currentEvent.LeftReactionEffects;
                        if (awaitingLeftConfirmation) {
                            TriggerEventEffects(eventEffects);
                        } else {
                            LeftPrompt(eventEffects);
                        }
                    }
                }
            }
        }

        void ResetTimers() {
            fadeInTimer = 0;
            waitTimer = 0;
            fadeOutTimer = 0;

            fadeIn = 0;
            fadeOut = 1;
        }
    }
}
