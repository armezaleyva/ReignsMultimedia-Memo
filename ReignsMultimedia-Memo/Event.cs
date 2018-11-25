using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReignsMultimedia_Memo {
    class Event {
        // TO DO: Personaje asociado con el evento
        string Text { get; set; }

        string RightReactionText { get; set; }
        List<int> RightReactionEffect { get; set; }
        Event RightReactionSequence { get; set; } 

        string LeftReaction { get; set; }
        List<int> LeftReactionEffect { get; set; }
        Event LeftReactionSequence { get; set; }
    }
}
