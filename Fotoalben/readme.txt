Einsatz von sed
===============

Sollte es irgendwann nötig sein, in diesem Verzeichnis
den Inhalt von 'albumname.txt' zu bearbeiten, so bietet
sich unter Linux ein 'sed-Befehl' auf der Kommandozeile
an. Denn mit Emacs oder gedit lässt sich die Änderung
nicht korrekt abspeichern. Einen bestehenden Inhalt
'Meine Fische' kann man mit

sed --in-place 's/.*/Meine Hunde/' albumname.txt

in meine 'Meine Hunde' abändern.
