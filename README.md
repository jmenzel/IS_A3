#Fishing for Numbers
Ein einfaches Zahlenspiel

Ziel:
Die Spieler ziehen jeweils abwechselnd eine Zahl, um so nah wie möglich
an die vorgegebene Ziel-Zahl heran zu kommen.

Hintergrund:
Das Spiel entstand im Studienmodul "Intelligente Systeme". Vorgabe war es, eine
gegnerische KI mit dem Minimax/Negamax-Algorithmus zu entwickeln, der nicht geschlagen
werden kann. (https://de.wikipedia.org/wiki/Minimax-Algorithmus)

Spieler:
Fishing for Numbers kann gegen einen Menschen oder gegen einen von drei 
Computergegnern gespielt werden.

Elli-AI ist der einfachste Gegner. Sie wählt ihre Zahl rein zufällig.

Randy-AI geht etwas strategischer vor. Er nimmt solange die höchst mögliche Zahl, bis er die Zielzahl erreicht hat. Danach nimmt er die kleinste.

NegaMax-AI generiert die Spielzüge bis zu einer bestimmten Tiefe und bewertet diese. Basierend darauf wird eine Zahl gezogen. Beginnt NegaMax-AI das Spiel,
kann er (bis jetzt) nicht geschlagen werden
