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
    public class SequenceEvent : Event {
        public SequenceEvent(string eventCharacter, ImageSource characterImage, string text,
                string rightReactionText, List<int> rightReactionEffects, Event rightReactionSequence,
                string leftReactionText, List<int> leftReactionEffects, Event leftReacionSequence)
                : base(eventCharacter, characterImage, text, rightReactionText, rightReactionEffects,
                       rightReactionSequence, leftReactionText, leftReactionEffects, leftReacionSequence) {
            EventCharacter = eventCharacter;
            CharacterImage = characterImage;
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
