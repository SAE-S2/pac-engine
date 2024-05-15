using pac_engine.Core;
using System;

namespace pac_engine.Core
{
    class DialogueManager
    {
        private Tuple<string, string>[][] dialogues = {
        // Dialogue début de jeu (0)
        new Tuple<string, string>[] {
            Tuple.Create("Halte-là Général !", "Hauts-Dirigeants"),
            Tuple.Create("Que veulent les hauts-dirigeants de l’empire à un général qui vient de démissionner ?", "Pac-Bot"),
            Tuple.Create("Tout simplement vous faire changer d’avis avant que vous ne partiez.", "Hauts-Dirigeants"),
            Tuple.Create("C’est un honneur que vous vous déplaciez pour moi mais vous n’arriverez pas à changer ma décision. Nous avons suffisamment repoussé l’humanité et nos frères robots sont hors de danger. Je me suis lassé de cette guerre et nous n’avons pas besoin d’éradiquer les derniers humains.", "Pac-Bot"),
            Tuple.Create("Il semble en effet que vous ayez fait votre choix, et nous devons donc faire le notre. Gardes ! Arrêtez l’ex-général Pac-Bot et jetez le en prison.", "Hauts-Dirigeants")
        },

        // Dialogue arrivée en prison (1)
        new Tuple<string, string>[] {
            Tuple.Create("Hé toi, Pac-Bot ! Écoutes bien. Tu es enfermé ici pour une raison, et il n'y a qu'une façon de t’en sortir. Récolter assez de pièces pour payer la croisière. Mais méfie-toi, si tu te fais attraper, c'est retour direct en prison pour toi.", "Voix off")
        },

        // Dialogue shop et première rencontre (2)
        new Tuple<string, string>[] {
            Tuple.Create("Salut, ancien général, tu te souviens de moi ? Je suis l'ingénieur. Je suis prêt à t'aider si tu m'apportes des boulons.", "Ingénieur"),
        },

        // Dialogue restart level et première fois (3)
        new Tuple<string, string>[] {
            Tuple.Create("Psst, général !", "Garde allié"),
            Tuple.Create("Que me veux-tu le garde ? Et au cas où tu ne serais pas au courant, je ne suis plus général.", "Pac-Bot"),
            Tuple.Create("Je sais, mais pour moi vous resterez toujours mon supérieur. Vous comptez vous échapper n’est-ce pas ? Laissez-moi vous aider, par contre je ne pourrais pas prendre trop de risques.", "Garde allié")
        },

        // Dialogue fin de jeu (4)
        new Tuple<string, string>[] {
            Tuple.Create("Félicitations Pac-Bot ! Tu as réussi à atteindre le paquebot, enfuis toi vite à bord et pars à la découverte de nouvelles aventures.", "Voix off")
        },

        // Dialogue shop et non première rencontre (5)
        new Tuple<string, string>[] {
            Tuple.Create("Salut, Ingénieur ! J'ai quelques boulons, qu'est-ce que tu as à me proposer ?", "Pac-Bot"),
            Tuple.Create("Voici toutes les améliorations que je te propose. Laquelle t’aiderais à partir ?", "Ingénieur")
        },

        // Dialogue restart level et non première fois (6)
        new Tuple<string, string>[] {
            Tuple.Create("Ah, vous vous êtes fait attraper Général !", "Garde allié"),
            Tuple.Create("Les hauts-dirigeants ont renforcé la surveillance de la prison, je vais devoir augmenter votre contribution, disons que c’est ma prime de risque. Ça vous coûtera <X> pièces. Ça vous dit ?", "Garde allié")
        }
    };

        public void DisplayDialogue(int dialogueIndex, bool isFirstTime)
        {
            if (!isFirstTime && dialogueIndex == 2) //Changement de dialogue si ce n'est pas la première fois
            {
                dialogueIndex = 5;
            }
            if (!isFirstTime && dialogueIndex == 3)
            {
                dialogueIndex = 6;
            }
            int lineIndex = 0;
            while (lineIndex < dialogues[dialogueIndex].Length)
            {
                Tuple<string, string> dialogueLine = dialogues[dialogueIndex][lineIndex];
                Console.WriteLine("Personnage : " + dialogueLine.Item2);
                Console.WriteLine("Dialogue : " + dialogueLine.Item1);
                Console.ReadKey();
                lineIndex++;
            }
        }
    }
}