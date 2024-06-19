using System;

using System;

namespace pac_interface
{
    public class DialogueManager
    {
        public int dialogueIndex;
        public bool isFirstTime;
        private Tuple<string, string>[][] dialogues;

        public DialogueManager(int index, bool firstTime)
        {
            dialogueIndex = index;
            isFirstTime = firstTime;

            dialogues = new Tuple<string, string>[][] {
                // Dialogue début de jeu (0)

                new Tuple<string, string>[] {
                    Tuple.Create("Halte-là Général !", "Dirigeant.png"),
                    Tuple.Create("Que veulent les hauts-dirigeants de l’empire à un général qui vient de démissionner ?", "Pac-Bot_dialogue1.png"),
                    Tuple.Create("Tout simplement vous faire changer d’avis avant que vous ne partiez.", "Dirigeant.png"),
                    Tuple.Create("C’est un honneur que vous vous déplaciez pour moi mais vous n’arriverez pas à changer ma décision.", "Pac-Bot_dialogue1.png"),
                    Tuple.Create("Nous avons suffisamment repoussé l’humanité et nos frères robots sont hors de danger. Je me suis lassé de cette guerre et nous n’avons pas besoin d’éradiquer les derniers humains.", "Pac-Bot_dialogue1.png"),
                    Tuple.Create("Il semble en effet que vous ayez fait votre choix, et nous devons donc faire le notre. Gardes ! Arrêtez l’ex-général Pac-Bot et jetez le en prison.", "Dirigeant.png"),
                    Tuple.Create("","Voix off")
                },

                // Dialogue arrivée en prison (1)
                new Tuple<string, string>[] {
                    Tuple.Create("Hé toi, Pac-Bot ! Écoutes bien. Tu es enfermé ici pour une raison, et il n'y a qu'une façon de t’en sortir.", "Voix off"),
                    Tuple.Create("Récolter assez de pièces pour payer la croisière. Mais méfie-toi, si tu te fais attraper, c'est retour direct en prison pour toi.", "Voix off"),
                    Tuple.Create("","Voix off")
                },

                // Dialogue ingénieur et première rencontre (2)
                new Tuple<string, string>[] {
                    Tuple.Create("Salut, ancien général, tu te souviens de moi ? Je suis l'ingénieur. Je suis prêt à t'aider si tu m'apportes des boulons.", "Engineer1.png"),
                    Tuple.Create("","Voix off")
                },

                // Dialogue guarde et première fois (3)
                new Tuple<string, string>[] {
                    Tuple.Create("Psst, général !", "Guard1.png"),
                    Tuple.Create("Que me veux-tu le garde ? Et au cas où tu ne serais pas au courant, je ne suis plus général.", "Pac-Bot_dialogue1.png"),
                    Tuple.Create("Je sais, mais pour moi vous resterez toujours mon supérieur. Vous comptez vous échapper n’est-ce pas ? Laissez-moi vous aider, par contre je ne pourrais pas prendre trop de risques.", "Guard1.png"),
                    Tuple.Create("","Voix off")
                },

                // Dialogue fin de jeu (4)
                new Tuple<string, string>[] {
                    Tuple.Create("Félicitations Pac-Bot ! Tu as réussi à atteindre le paquebot, enfuis toi vite à bord et pars à la découverte de nouvelles aventures.", "Voix off"),
                    Tuple.Create("","Voix off")
                },

                // Dialogue ingénieur et non première rencontre (5)
                new Tuple<string, string>[] {
                    Tuple.Create("Salut, Ingénieur ! J'ai quelques boulons, qu'est-ce que tu as à me proposer ?", "Pac-Bot_dialogue1.png"),
                    Tuple.Create("Voici toutes les améliorations que je te propose. Laquelle t’aiderais à partir ?", "Engineer1.png"),
                    Tuple.Create("","Voix off")
                },

                // Dialogue guarde et non première fois (6)
                new Tuple<string, string>[] {
                    Tuple.Create("Ah, vous vous êtes fait attraper Général !", "Guard1.png"),
                    Tuple.Create("Les hauts-dirigeants ont renforcé la surveillance de la prison, Prenez garde !", "Guard1.png"),
                    Tuple.Create("","Voix off")
                }
            };
        }

        public Tuple<string, string> GetDialogueLine(int index, bool isFirstTime, int lineIndex)
        {
            if (!isFirstTime && index == 2)
            {
                index = 5;
            }
            if (!isFirstTime && index == 3)
            {
                index = 6;
            }
            return dialogues[index][lineIndex];
        }

        public int GetDialogueLength(int index, bool isFirstTime)
        {
            if (!isFirstTime && index == 2)
            {
                index = 5;
            }
            if (!isFirstTime && index == 3)
            {
                index = 6;
            }

            return dialogues[index].Length-1;
        }
    }
}