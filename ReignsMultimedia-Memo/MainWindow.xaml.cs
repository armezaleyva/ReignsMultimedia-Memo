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
        enum DecisionMade { Right, Left };

        List<Event> events = new List<Event>();

        public MainWindow() {
            InitializeComponent();
            panelBase.Focus();

            //List - Sequence Events
            SequenceEvent sequenceEvent20_1 = new SequenceEvent("Luis Mercado", new BitmapImage(new Uri(
                   "/Assets/Characters/Luis.png", UriKind.Relative)),
                   "“Ok, ¿qué propones?”",
                   "“Diles que tienen que tomar fotos en la Miravalle de noche...solos...”", new List<int> { -20, 20, -20, -20 }, null,
                   "“Nah, era cura”", new List<int> { 0, -10, 0, 0 }, null);
            SequenceEvent sequenceEvent12_2 = new SequenceEvent("Bet´´in", new BitmapImage(new Uri(
                    "/Assets/Characters/Betin.png", UriKind.Relative)),
                    "Varios vértices andan volando y tiene mordidas de perro",
                    "“!MAL!”", new List<int> { 0, 0, 0, 30 }, null,
                    "“!MAL!”", new List<int> { 0, 0, 0, 30 }, null);
            SequenceEvent sequenceEvent12_1 = new SequenceEvent("Betín", new BitmapImage(new Uri(
                    "/Assets/Characters/Betin.png", UriKind.Relative)),
                    "“¡NO! No sé porque está sin cabeza mi personaje”",
                    "“¡¿Qué es esta cochinada?!”", new List<int> { -20, 0, 0, 20 }, null,
                    "“Mmm, mínimo vamos a ver si la geometría la hiciste bien”", new List<int> { 0, 0, 0, 10 }, sequenceEvent12_2);
            SequenceEvent sequenceEvent10_1 = new SequenceEvent("Betito", new BitmapImage(new Uri(
                    "/Assets/Characters/Betito.png", UriKind.Relative)),
                    "“Pues hice lo que pud...”",
                    "“No, no, está todo mal. Cero”", new List<int> { -20, 0, 0, 20 }, null,
                    "“No, no, está todo mal. Cero”", new List<int> { -20, 0, 0, 20 }, null);
            SequenceEvent sequenceEvent1_2 = new SequenceEvent("Alex", new BitmapImage(new Uri(
                    "/Assets/Characters/Alex.png", UriKind.Relative)),
                    "“¿...Cómo hago un te equis te...?”",
                    "“¡¿QUÉ?!”", new List<int> { 0, 0, 0, 30 }, null,
                    "“¿¡QUÉ!?”", new List<int> { 0, 0, 0, 30 }, null);
            SequenceEvent sequenceEvent1_1 = new SequenceEvent("Alex", new BitmapImage(new Uri(
                    "/Assets/Characters/Alex.png", UriKind.Relative)),
                    "“¿Cómo se hace eso?”",
                    "“¿Qué cosa?”", new List<int> { 0, 0, 0, 0 }, sequenceEvent1_2,
                    "“¡YA ALEX! déjame en paz, estás reprobado”", new List<int> { -20, 0, 0, -20 }, null);
            //List - Events
            Event event1 = new Event("Alex", new BitmapImage(new Uri(
                    "/Assets/Characters/Alex.png", UriKind.Relative)),
                    "“Oiga profe no me deja subir la práctica al portal...”",
                    "“Súbela a tu drive y pon el link en un .txt”", new List<int> { 10, 0, 0, 10 }, sequenceEvent1_1,
                    "“Pues ni modo, tienes cero”", new List<int> { -20, 0, 0, -20 }, null);
            Event event2 = new Event("Alex", new BitmapImage(new Uri(
                    "/Assets/Characters/Alex.png", UriKind.Relative)),
                    "“Oye, Memo. Debo tu materia ¿cómo me voy a inscribir yo?”",
                    "“Ughh, deja te ayudo…”", new List<int> { 0, 0, -20, 20 }, null,
                    "“Ni modo, llévala después”", new List<int> { 10, 0, 10, -10 }, null);
            Event event3 = new Event("Sebas", new BitmapImage(new Uri(
                    "/Assets/Characters/Sebas.png", UriKind.Relative)),
                    "Sebas se metió en problemas con un maestro",
                    "Arreglo el problema con el maestro y Sebas", new List<int> { -10, 10, 0, 0 }, null,
                    "Regaño al Sebas", new List<int> { -20, 0, 0, 0 }, null);
            Event event4 = new Event("Sebas", new BitmapImage(new Uri(
                    "/Assets/Characters/Sebas.png", UriKind.Relative)),
                    "“Profe,  ¿puedo tocar su cabeza ?”",
                    "“...”", new List<int> { 20, 0, 0, 20 }, null,
                    "“¡NO!”", new List<int> { -10, 0, 0, 10 }, null);
            Event event5 = new Event("Sofía", new BitmapImage(new Uri(
                    "/Assets/Characters/Sofia.png", UriKind.Relative)),
                    "“Memo ¿Me puedes poner las horas de servicio?”",
                    "“Sí, no hay problema”", new List<int> { 20, 0, -10, -10 }, null,
                    "“Primero tienes que hacer un video para viridis”", new List<int> { 10, 0, 20, -20 }, null);
            Event event6 = new Event("Pablo", new BitmapImage(new Uri(
                    "/Assets/Characters/Pablo.png", UriKind.Relative)),
                    "“Necesito dormir. Mínimo 6 horas, Memo…”",
                    "“¿QUÉ...?!!!”", new List<int> { -10, 0, 0, 20 }, null,
                    "“Bueno, entrega tu trabajo en la tarde…”", new List<int> { 10, 0, 0, +20 }, null);
            Event event7 = new Event("Eduardo", new BitmapImage(new Uri(
                    "/Assets/Characters/Eduardo.png", UriKind.Relative)),
                    "Quiere presentar el corto de “La Galleta” para el Creative Jam",
                    "Lo dejo presentar el corto", new List<int> { 20, 0, -20, 0 }, null,
                    "No lo dejo presentar el corto", new List<int> { -10, +10, 0, -20 }, null);
            Event event8 = new Event("Sestier", new BitmapImage(new Uri(
                    "/Assets/Characters/Sestier.png", UriKind.Relative)),
                    "Necesita aprobación para el taller de 3D los sábados. Los del D1 no quieren.",
                    "Apruebo el taller y manda correo a los del D1", new List<int> { 20, 0, -10, 0 }, null,
                    "Rechazo el taller", new List<int> { -10, 0, 10, -10 }, null);
            Event event9 = new Event("Andy", new BitmapImage(new Uri(
                    "/Assets/Characters/Andy.png", UriKind.Relative)),
                    "Llega 20 minutos tarde",
                    "“Tienes falta”", new List<int> { -10, 0, 0, -10 }, null,
                    "No le digo nada", new List<int> { 10, 0, 0, 10 }, null);
            Event event10 = new Event("Betito", new BitmapImage(new Uri(
                    "/Assets/Characters/Betito.png", UriKind.Relative)),
                    "“Profe, aquí está mi entrega de la revista”",
                    "“Pues, está decente para alguien de primero”", new List<int> { +10, 0, 0, 10 }, null,
                    "“¡¿Qué es esta cochinada?!”", new List<int> { -10, 0, 0, 20 }, sequenceEvent10_1);
            Event event11 = new Event("Betín", new BitmapImage(new Uri(
                    "/Assets/Characters/Betin.png", UriKind.Relative)),
                    "“Memo, si no me pones 10 no voy a participar en el MUDAM”",
                    "“Pues podemos llegar a un acuerdo…”", new List<int> { 20, -10, -10, 10 }, null,
                    "“No llores. Siempre estás llorando”", new List<int> { -10, 0, 0, -10 }, null);
            Event event12 = new Event("Betín", new BitmapImage(new Uri(
                    "/Assets/Characters/Betin.png", UriKind.Relative)),
                    "“Que onda, Memo ¿Listo para mi proyecto?”",
                    "“Sin verlo ya sé que tienes 0”", new List<int> { -10, -0, 0, -20 }, null,
                    "“A verlo, pues…”", new List<int> { 0, 0, 0, 10 }, sequenceEvent12_1);
            Event event13 = new Event("Rubén Alemán", new BitmapImage(new Uri(
                    "/Assets/Characters/Ruben.png", UriKind.Relative)),
                    "“Memo, no quiero venir de traje a la exposición”",
                    "“Tienes que venir de traje, no importa nada”", new List<int> { -10, 20, 0, -10 }, null,
                    "“No hay problema, yo hablo con el maestro”", new List<int> { 20, -20, 0, 0 }, null);
            Event event14 = new Event("Minneth", new BitmapImage(new Uri(
                    "/Assets/Characters/Minneth.png", UriKind.Relative)),
                    "“Ese teléfono parece carpintero, porque hace rín, porque hace rín...”",
                    "“¿Qué son niños o qué? ¡Falta a todos!”", new List<int> { -10, 0, 0, 10 }, null,
                    "Espero pacientemente a que dejen de cantar", new List<int> { 0, 0, 0, 20 }, null);
            Event event15 = new Event("Rafa", new BitmapImage(new Uri(
                    "/Assets/Characters/Rafa.png", UriKind.Relative)),
                    "“Memo, no traigo avances de mi proyecto final…”",
                    "“No hay problema, entrégalo más tarde”", new List<int> { 10, 0, 0, 20 }, null,
                    "“Ni modo, cero”", new List<int> { -10, 0, 0, -20 }, null);
            Event event16 = new Event("Erika", new BitmapImage(new Uri(
                    "/Assets/Characters/Erika.png", UriKind.Relative)),
                    "“Memo, no soporto tu clase y me quiero suicidar”",
                    "“Ven a mi oficina para ponerte al corriente a las 7am”", new List<int> { 10, 10, 0, 10 }, null,
                    "“Pídele ayuda a tus compañeros”", new List<int> { -20, 0, 0, -20 }, null);
            Event event17 = new Event("Erika", new BitmapImage(new Uri(
                    "/Assets/Characters/Erika.png", UriKind.Relative)),
                    "“Memo, necesitamos el salón de fotografía y no nos lo quieren prestar”",
                    "“Yo les abro el salón”", new List<int> { 0, -10, -10, 0 }, null,
                    "“Pues tienen que conseguir otro lugar”", new List<int> { -10, 0, 0, -10 }, null);
            Event event18 = new Event("Andrea", new BitmapImage(new Uri(
                    "/Assets/Characters/Andrea.png", UriKind.Relative)),
                    "Andrea pregunta como hacer mirror a un objeto en maya",
                    "Le enseño como", new List<int> { 10, 0, 0, 10 }, null,
                    "“¡YA DEBERÍAS DE SABER ESO!”", new List<int> { -10, 0, 0, -10 }, null);
            Event event19 = new Event("Luis Mercado", new BitmapImage(new Uri(
                    "/Assets/Characters/Luis.png", UriKind.Relative)),
                    "“El Sebas cometió una tontería y me tiene harto”",
                    "Hablo con el Sebas", new List<int> { 0, 20, 0, 10 }, null,
                    "“No es para tanto, Luis”", new List<int> { 0, -20, 0, -20 }, null);
            Event event20 = new Event("Luis Mercado", new BitmapImage(new Uri(
                    "/Assets/Characters/Luis.png", UriKind.Relative)),
                    "“Memo, los de tercero no están haciendo sus entregas, los quiero matar”",
                    "“Hazles el paro Luis”", new List<int> { +20, -20, 0, 0 }, null,
                    "“Yo te ayudo a matarlos”", new List<int> { -20, 20, -10, -20 }, sequenceEvent20_1);
            Event event21 = new Event("Iván", new BitmapImage(new Uri(
                    "/Assets/Characters/Ivan.png", UriKind.Relative)),
                    "Se siente mal su esposa y no podrá dar clases",
                    "“Pues ni modo, ven cuando puedas”", new List<int> { 0, 10, -20, 0 }, null,
                    "“No, has faltado un montón”", new List<int> { 0, -10, 10, 0 }, null);
            Event event22 = new Event("Calificación Docente", new BitmapImage(new Uri(
                    "/Assets/Characters/Docente.png", UriKind.Relative)),
                    "Muchos alumnos te calificaron mal",
                    "“Hablaré con ellos para resolver el problema”", new List<int> { 20, 0, 10, 10 }, null,
                    "“Ni modo, ellos por flojos”", new List<int> { -10, 0, 0, -10 }, null);
            Event event23 = new Event("Calificación Docente", new BitmapImage(new Uri(
                    "/Assets/Characters/Docente.png", UriKind.Relative)),
                    "Los alumnos se quejaron mucho de Patiño en la evaluación pasada y administración quiere que hables con él",
                    "“Ughhh...Okay…”", new List<int> { 0, -20, 20, 10 }, null,
                    "“Oh, pero mira la hora…”", new List<int> { 0, 0, -20, -10 }, null);
            Event event24 = new Event("Lalo Núñez", new BitmapImage(new Uri(
                    "/Assets/Characters/Lalo.png", UriKind.Relative)),
                    "“Mañana hay junta de academia a las 8 de la mañana en sábado”",
                    "“No hay problema, iré”", new List<int> { 0, 10, 20, 20 }, null,
                    "(Mentir) “No hay problema, iré”", new List<int> { 0, -10, -20, -20 }, null);
            Event event25 = new Event("Lalo Núñez", new BitmapImage(new Uri(
                    "/Assets/Characters/Lalo.png", UriKind.Relative)),
                    "“Mañana hay junta de academia a las 8 de la mañana en sábado”",
                    "“...¿Ahora qué hicieron…?”", new List<int> { 0, 0, 10, 20 }, null,
                    "“Me acabo de acordar que estoy ocupado…”", new List<int> { 0, 0, -20, -20 }, null);
            Event event26 = new Event("Molle", new BitmapImage(new Uri(
                    "/Assets/Characters/Molle.png", UriKind.Relative)),
                    "“Oye, Memo. Estos alumnos reprobaron el semestre ¿Qué onda?”",
                    "“Pues ni modo, que se vayan a extraordinario”", new List<int> { 0, 20, 10, 0 }, null,
                    "“Ponles 7 a todos”", new List<int> { 20, -10, -10, -20 }, null);
            Event event27 = new Event("Celina Gastélum", new BitmapImage(new Uri(
                    "/Assets/Characters/Celina.png", UriKind.Relative)),
                    "“¡MEMO! El sitio de la ULSA no carga”",
                    "“Dale actualizar…”", new List<int> { 0, 20, 20, 20 }, null,
                    "“Ahorita no puedo ver”", new List<int> { 0, -20, -10, -20 }, null);
            Event event28 = new Event("Celina Gastélum", new BitmapImage(new Uri(
                    "/Assets/Characters/Celina.png", UriKind.Relative)),
                    "“Memo, los niños de primero necesitan tutorías de programación”",
                    "“Deja consigo un tutor”", new List<int> { 20, 20, 0, 0 }, null,
                    "“Pues, necesitan estudiar más”", new List<int> { 0, -20, 0, -10 }, null);
            Event event29 = new Event("Claudia", new BitmapImage(new Uri(
                    "/Assets/Characters/Claudia.png", UriKind.Relative)),
                    "“¿Qué es esa madre, Memo?”",
                    "“Ugh”", new List<int> { 0, 0, 0, 10 }, null,
                    "“Ugh”", new List<int> { 0, 0, 0, 10 }, null);

            /*
            Event event0 = new Event("Anon", new BitmapImage(new Uri(
                    "/Assets/Characters/Anon.png", UriKind.Relative)),
                    "“DescriptionText”",
                    "“RespuestaTexto1”", new List<int> { 0, 0, 0, 0 }, null,
                    "“RespuestaTexto1”", new List<int> { 0, 0, 0, 0 }, null);*/

            events.Add(event1);
            events.Add(event2);
            events.Add(event3);
            events.Add(event4);
            events.Add(event5);
            events.Add(event6);
            events.Add(event7);
            events.Add(event8);
            events.Add(event9);
            events.Add(event10);
            events.Add(event11);
            events.Add(event12);
            events.Add(event13);
            events.Add(event14);
            events.Add(event15);
            events.Add(event16);
            events.Add(event17);
            events.Add(event18);
            events.Add(event19);
            events.Add(event20);
            events.Add(event21);
            events.Add(event22);
            events.Add(event23);
            events.Add(event24);
            events.Add(event25);
            events.Add(event26);
            events.Add(event27);
            events.Add(event28);
            events.Add(event29);

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
        double margin = 60.00;

        double fadeInTimer = 0.0000;
        double waitTimer = 0.0000;
        double fadeOutTimer = 0.0000;

        bool initializingGameplayWindow = true;
        bool transitioningToEvent = true;
        bool awaitingRightConfirmation = false;
        bool awaitingLeftConfirmation = false;

        Event currentEvent = null;

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
            if (currentEvent == null) {
                var random = new Random();
                var randomIndex = random.Next(events.Count());
                currentEvent = events[randomIndex];
                events.RemoveAt(randomIndex);
                Stats.currentWeek += 1;
            }

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

        void RightPrompt(List<int> eventEffects, DecisionMade decisionMade) {
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

        void LeftPrompt(List<int> eventEffects, DecisionMade decisionMade) {
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

                }
                else {
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

        void TriggerEventEffects(List<int> eventEffects, DecisionMade decisionMade) {
            Stats.estudiantes += eventEffects[0];
            Stats.maestros += eventEffects[1];
            Stats.administracion += eventEffects[2];
            Stats.estres += eventEffects[3];

            if (decisionMade == DecisionMade.Right) {
                if (currentEvent.RightReactionSequence == null) {
                    currentEvent = null;
                } else {
                    currentEvent = currentEvent.RightReactionSequence;
                }
            } else {
                if (currentEvent.LeftReactionSequence == null) {
                    currentEvent = null;
                }
                else {
                    currentEvent = currentEvent.LeftReactionSequence;
                }
            }

            transitioningToEvent = true;
        }

        private void PanelBase_KeyDown(object sender, KeyEventArgs e) {
            if (gameState == GameState.Gameplay) {
                if (!transitioningToEvent) {
                    DecisionMade decisionMade;
                    if (e.Key == Key.D || e.Key == Key.Right) {
                        var eventEffects = currentEvent.RightReactionEffects;
                        decisionMade = DecisionMade.Right;
                        if (awaitingRightConfirmation) {
                            TriggerEventEffects(eventEffects, decisionMade);
                        } else {
                            RightPrompt(eventEffects, decisionMade);
                        }
                    }
                    else if (e.Key == Key.A || e.Key == Key.Left) {
                        var eventEffects = currentEvent.LeftReactionEffects;
                        decisionMade = DecisionMade.Left;
                        if (awaitingLeftConfirmation) {
                            TriggerEventEffects(eventEffects, decisionMade);
                        } else {
                            LeftPrompt(eventEffects, decisionMade);
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
            margin = 60;
        }
    }
}
