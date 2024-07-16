## Idee
Unser Spiel NightLeap ist ein mit Unity entwickeltes Jump 'n' Run. Grundkonzept ist das Thema "Schattentheater", kombiniert mit einem interessanten Mechanikwechsel zwischen "Jump" und "Flap".
Um das Spiel wie ein echtes Schattentheater wirken zu lassen, liegt der Fokus auf dem Einsatz von Licht und Beleuchtung anstelle von Texturen. Außerdem soll das Spiel imperfekt wirken, als wären die Formen mit einer Schere selbst ausgeschnitten.

## Ablauf und Steuerung
Das Spiel startet mit einem Startbildschirm (StartScreen) von dem aus das erste (einzige) Level gestartet wird. Der Spieler muss verschiedene Hindernisse überwinden, um den Ausgang des Levels zu erreichen. Dabei wechselt die Bewegungsmechanik zwischen klassischem Jump 'n' Run (WASD + Leer) zu einer von FlappyBird inspirierten Bewegung (WASD + wiederholt Leer). Bei Beenden des Levels wird ein Endbildschirm mit der gemessenen Zeit angezeigt. Das Spiel kann über ESC jederzeit pausiert werden, um z.B. vom letzten Checkpoint neuzustarten.

## Features
- **Mechanikwechsel:** Der Spieler wechselt zeitweise von der Jump 'n' Run-Mechanik zu einer Flappy Bird-Mechanik und sieht sich einem entsprechenden Hinderniskurs gegenüber.
- **Checkpoints:** Vor schwierigen Passagen gibt es Checkpoints, um den Fortschritt des Spielers zu sichern.
- **Bewegliche Plattformen:** Plattformen, die sich sowohl vertikal als auch horizontal bewegen.
- **Tödliche, dynamische Hindernisse:** Gefährliche Sägen, Schnecken und Laser, die den Spieler bei Kontakt töten (Respawn am letzten Checkpoint).
- **Versteckte Falltüren:** Türen im Boden, die bei Berührung des Spielers nach unten klappen.
- **Verborgene Bereiche:** Bestimmte Levelbereiche werden erst sichtbar, wenn der Spieler sie betritt.

## Rechtliches
Folgende Sounds wurden beim Sounddesign verfremdet eingebunden:
- Circular saw.wav by LiezelDippenaar -- https://freesound.org/s/707513/ -- License: Creative Commons 0
- saw_switched_off_2.wav by Ferdinger -- https://freesound.org/s/146234/ -- License: Attribution NonCommercial 4.0

Alle weiteren Sounds sind selbst erstellt worden oder lizenzfrei.

Sprites wurden selbst erstellt.

Shader beim PLayerDeath nach diesem Video: https://www.youtube.com/watch?v=HYWaU97-UC4&t=289s
