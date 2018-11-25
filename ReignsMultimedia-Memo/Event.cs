using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReignsMultimedia_Memo {
    public class Event {
        // TO DO: Personaje asociado con el evento
        public string Text { get; set; }

        string RightReactionText { get; set; }
        List<int> RightReactionEffects { get; set; }
        Event RightReactionSequence { get; set; } 

        string LeftReactionText { get; set; }
        List<int> LeftReactionEffects { get; set; }
        Event LeftReactionSequence { get; set; }

        public Event(string text, string rightReactionText, List<int> rightReactionEffects, Event rightReactionSequence,
              string leftReactionText, List<int> leftReactionEffects, Event leftReacionSequence) {
            Text = text;
            RightReactionText = rightReactionText;
            RightReactionEffects = rightReactionEffects;
            RightReactionSequence = rightReactionSequence;
            LeftReactionText = leftReactionText;
            LeftReactionEffects = leftReactionEffects;
            LeftReactionSequence = LeftReactionSequence;
        }
    }
}
