# Modul 122

## Ziele und Anforderungen ##

### Einleitung ###

Dieses Skript bietet eine benutzerfreundliche Lösung zur Verwaltung von Benutzerkonten. Es ermöglicht die einfache
Erstellung neuer Benutzer entweder manuell durch die direkte Eingabe von Benutzernamen und Passwörtern oder durch das
Einlesen von Benutzerdaten aus einer externen Datei. Das Skript führt Sicherheitsüberprüfungen für Passwörter durch und
bietet die Möglichkeit, erstellte Benutzerdaten für spätere Verwendung in einer Datei zu speichern. Damit bietet es eine
effiziente Möglichkeit zur Verwaltung von Benutzern.

### Zweck des Skriptes ###

Dieses Skript dient als Benutzerverwaltungswerkzeug. Es ermöglicht dem Benutzer entweder die manuelle Erstellung eines
Benutzers oder das Lesen von Benutzerinformationen aus einer Datei (entweder einer .txt- oder .csv-Datei).

### Ziele ###

* Das Skript soll, bis zum Zeitpunkt der Abgabe (21.05.2024), ohne unerwartete Fehlermeldungen laufen.
* Das Skript soll, bis zum Zeitpunkt der Abgabe (21.05.2024), sowohl in Bash wie auch in PowerShell vertreten sein und
  funktionieren (Nutzer können manuell erstellt werden).
* Das Skript soll, bis zum Zeitpunkt der Abgabe (21.05.2024), sowohl in Bash als auch in PowerShell, Lokale Benutzer aus
  Dateien heraus erstellen.

### Anforderungen ###

* Das Script soll das Erstellen von Lokalen Nutzern vereinfachen (Keine sub-Menü suche).
* Das Script muss mit „Unerwarteten“ Eingaben (Ungültigen Pfade, Antworten die nicht zur verfügung stehen, etc.) umgehen
  können.
* Das Script soll die Erstellten Nutzer in einer Datei (txt.) speichern können.

## Ablaufdiagramm ##

### PAP / Flussdiagramm / Struktogramm ###

<br>
<img src="Praxisarbeit_LB1/02_Marjan/LB1_122_PS.drawio.png" width="300">
<br>

<br>
<img src="Praxisarbeit_LB1/01_Leonid/LB1_122_BS.drawio.png" width="300">
<br>

### Kommentar / Beschreibung ###

### Ausführen ###

* Überprüfen, ob das Skript als Administrator ausgeführt wird.
* Die Passwortprüffunktion wird eingeführt.
* Bestimmt den Pfad für den Speicherort des Skripts.
* Auswahl: Manuell(m) oder Datei(f).
* Überprüfen, ob m oder m, wenn ungültig, wiederholen.

### Pfad 1 (m): ###

* Interaktion: Benutzernamen eingeben.
* Überprüfen, ob der Benutzername leer ist.
* Überprüfen, ob der Benutzer bereits existiert.
* Unerwartete Fehler abfangen.
* Interaktion: Passwort eingeben.
* Überprüfen, ob das Passwort leer ist.
* Validiert das Passwort mit einer Funktion.
* Benutzer erstellen.
* Erstellten Benutzer anzeigen.
* Interaktiv: Möchte der Benutzer die Daten speichern?

### Pfad 2 (f): ###

* Interaktion: Pfad zu CSV oder TXT eingeben.
* Dateierweiterung überprüfen, wenn weder CSV noch TXT = ungültig.
* Liste mit Benutzern und Passwörtern erstellen.
* Passwörter werden validiert.
* Benutzer werden aus Anzeigezwecken zur Tabelle hinzugefügt.
* Überprüfen, ob der Benutzer bereits existiert.
* Unerwarteten Fehler abfangen.
* Passwörter validieren -> erfüllen nicht die Anforderungen.
* Benutzer in Tabelle anzeigen.

### Hauptpfad: ###

* Interaktiv: Nach Wiederholung fragen.
* Ende.

## Skript/Programm (Realisierung) ##

### Technologie ###

* GIT

Windows Version:

* PowerShell
* Windows 11 x64

Linux Version:

* Bash
* Ubuntu 22.04.3 LTS

### Ein- und Ausgabe ###

Die Eingabe erfolgt entweder über das CLI oder ein GUI.
Die Ausgabe erfolgt im CLI wie auch in einem .txt- oder .csv-Datei.

### Kontrollstrukturen ###

#### Bedingte Anweisungen (if-else): ####

* Überprüfen, ob das Skript als Administrator ausgeführt wird.
* Überprüfen, ob der Benutzer bereits existiert.
* Überprüfen, ob die Dateierweiterung gültig ist (CSV oder TXT).
* Überprüfen, ob das Passwort den Anforderungen entspricht.
* Überprüfen, ob eine Eingabe leer ist.
* Unerwartete Fehler abfangen und darauf reagieren.

#### Schleifen (while-Schleife): ####

* Wiederholen der Eingabe, wenn eine ungültige Eingabe vorliegt, bis eine gültige Eingabe erhalten wird.

#### Schleifen (for-Schleife): ####

* Durchlaufen der Liste mit Benutzern und Passwörtern, um sie zu validieren und zur Anzeige hinzuzufügen.

## Integration und Sicherheit ##

### Implementierung ### 

Wir verarbeiten CLI-Inputs, Daten aus Dateien und erstellen neue Dateien wie Logs.
Das Skript gibt es für PowerShell und Bash

### Sicherheit ###

Das Skript überprüft die Berechtigungen, mit denen es ausgeführt wurde
Falsche Eingaben werden mit den passenden Fehlermeldungen beantwortet.
Das Skript erstellt automatisch Logs.

### Kompatibilität ###

Das Bash-Skript ist mit Linux kompatibel, solange Bash aktuell ist.
Das PowerShell-Skript ist mit Windows 11 Kompatibel

### Betrieb und Wartung ###

Die Wartung wird bei Bash mit der OOP-Methode vereinfacht, ist jedoch nicht vorgesehen, bei PowerShell wurden keine
Vorkehrungen getroffen, da es nicht vorgesehen ist, das Skript zu warten.

## Programmanwendung ##

### Usecase

<br>
<img src="Praxisarbeit_LB1/01_Leonid/LB1_122_Usecase.drawio.png" width="300">
<br>

### Testfall

<br>
<img src="Praxisarbeit_LB1/01_Leonid/LB1_122_Test_Case.drawio.png" width="300">
<br>

## Präsentation, Dokumentation ##

### Demo-Video ###

Das Demo-Video von dem PowerShell-Skript und von der C#-App befindet
sich [hier](https://git.gibb.ch/urs.dummermuth/inf-122-23n-sg2/-/blob/main/Praxisarbeit_LB1/01_Leonid/LB1_UserManager_Bash.mp4?ref_type=heads).

Das Demo-Video von dem Bash-Skript befindet
sich [hier](https://git.gibb.ch/urs.dummermuth/inf-122-23n-sg2/-/blob/main/Praxisarbeit_LB1/02_Marjan/LB1_UserManager_PowerShell_C%23.mp4?ref_type=heads).

## Journal ##

### Tag 1 ###

**Positive Entwicklungen:**

- Erfolgreiches Brainstorming zum Thema des Skripts.
- Implementierung der Grundfunktionalität des Skripts zur Erfassung und Speicherung von Benutzerdaten.
- Klar definierte Aufgabenverteilung innerhalb des Teams.
- Erstellung eines einfachen GUIs zur Testung der Benutzeroberfläche.

**Herausforderungen:**

- Es bestanden Unklarheiten über die genauen Anforderungen des Skripts.
- Das GUI war sehr simpel und bot nur grundlegende Funktionen.

**Weitere Schritte:**

- Ein Thema für das Skript finden und festlegen.

### Tag 2 ###

**Positive Entwicklungen:**

- Leonid hat erste wichtige Funktionen im Skript erfolgreich implementiert.
- Marjan integrierte Feedback und führte eine Passwort-Verschlüsselung sowie eine Testfunktion für Passwörter ein.
- Odysseus und Simeon konzentrierten sich auf die Themenfindung und das Klären von Rahmenbedingungen.

**Herausforderungen:**

- Leonid stiess auf Schwierigkeiten bei der Implementierung von Funktionen zur Massenerstellung von Benutzern aus einer
  Datei.
- Marjan fand die Arbeit mit dem GUI herausfordernd, was auf mangelnde Erfahrung mit XAML zurückzuführen ist.

**Weitere Schritte:**

- Es stehen Besprechungen an, um Scripts für Bash und PowerShell zu entwickeln und zu erarbeiten.
- Simeon plant, die Dokumentation fertigzustellen.

### Tag 3 ###

**Positive Entwicklungen:**

- Leonid und Marjan haben erfolgreich das Feature implementiert, Benutzer aus einer Datei zu erstellen, und haben
  zusätzlich manuelle Eingabemethoden verbessert. Leonid hat ausserdem den Code in eine objektorientierte
  Programmierung (OOP) umgeschrieben, um die Wartbarkeit und Erweiterbarkeit des Skripts zu verbessern.
- Ein Vergleich zwischen PowerShell- und Bash-Skripten führte zu wichtigen Erkenntnissen, die das Team für die
  Weiterentwicklung nutzen konnte.
- Odysseus arbeitete an Diagrammen, um die Scripts zu überprüfen und zu validieren.
- Simeon konzentrierte sich auf die Fertigstellung und das Testen der Dokumentation.

**Herausforderungen:**

- Keine grösseren Probleme wurden berichtet, was auf einen erfolgreichen Tag hinweist.

**Weitere Schritte:**

- Simeon plant, die Dokumentation zu vervollständigen.
- Odysseus wird die Überprüfung der Scripts mithilfe von Diagrammen fortsetzen, um die Implementierung weiter zu
  validieren.

### Tag 4 ###

**Positive Entwicklungen:**

- Leonid und Marjan haben weitere Optimierungen an der Feature-Liste vorgenommen, basierend auf Vergleichen mit anderen
  Skripten. Beide bestätigten durch intensive Tests die Robustheit des Skripts, welches nun als fertig betrachtet wird
  und bereit für weitere Optimierungen sowie Bug-Fixing ist.
- Simeon setzte seine Arbeit an der Dokumentation fort und nähert sich dem Abschluss dieser wichtigen Aufgabe.

**Herausforderungen:**

- Sowohl Leonid als auch Marjan merkten an, dass ein umfassendes Bug-Fixing und die Kommentierung des Codes noch
  ausstehen. Dies bleibt ein wichtiger Schritt, um die Qualität und Wartbarkeit des Skripts zu sichern.

**Weitere Schritte:**

- Das Team plant, das Skript umfassend auf Bugs zu überprüfen und den Code zu optimieren.
- Simeon wird die Dokumentation fertigstellen, was für das Verständnis und die Nutzung des Skripts essenziell ist.

### Tag 5 ###

**Positive Entwicklungen:**

- Leonid hat das Ablaufdiagramm erfolgreich ergänzt, ein Video des Skripts erstellt und sowohl Code-Kommentierung als
  auch Bug-Fixing durchgeführt. Das Skript ist damit umfassend dokumentiert und optimiert.
- Marjan arbeitete an den Flussdiagrammen, die die Funktionalitäten der Skripte visualisieren und hat in stetiger
  Abstimmung mit Leonid sichergestellt, dass die Diagramme korrekt und hilfreich sind. Die Flussdiagramme bieten nun
  eine klare Übersicht und unterstützen die weitere Entwicklung.

**Herausforderungen:**

- Marjan berichtet, dass die Erstellung der Flussdiagramme viel Zeit in Anspruch genommen hat und dadurch andere
  Aufgaben verzögert wurden. Zudem sind Fortschritte bei der Code-Kommentierung noch ausständig.

## Reflexion ##

### Ergebnis ###

- Das Team hat über fünf Tage hinweg intensiv an der Entwicklung eines Skripts gearbeitet, das Benutzerdaten effektiv
  erfassen und verarbeiten kann. Durch eine klar definierte Aufgabenverteilung und kontinuierliche Optimierung konnten
  wichtige Funktionen wie die Passwort-Verschlüsselung, Benutzererstellung aus Dateien und das Schreiben in
  objektorientierter Programmierung realisiert werden. Zusätzlich wurde das Skript durch Ablauf- und Flussdiagramme
  dokumentiert und mittels eines Videos visuell präsentiert, wodurch die Funktionalität und Robustheit des Skripts
  bestätigt wurde.

### Zusammenarbeit ###

- Die Teammitglieder zeigten eine ausgezeichnete Zusammenarbeit, bei der jeder Einzelne spezifische Rollen übernahm, um
  das Projekt voranzutreiben. Die regelmässige Kommunikation und Abstimmung, besonders zwischen Leonid und Marjan bei
  der Erstellung und Überprüfung der Diagramme, spielten eine entscheidende Rolle für den reibungslosen Ablauf und die
  erfolgreiche Umsetzung der geplanten Features. Simeon und Odysseus trugen wesentlich zur Dokumentation und Validierung
  der Scripts bei, was die Qualität und Nachvollziehbarkeit des Projekts sicherstellte.

### Fazit ###

- Effektive Teamarbeit und klare Kommunikation: Das Projekt verdeutlicht die Bedeutung einer gut koordinierten
  Zusammenarbeit und stetigen Kommunikation innerhalb des Teams. Diese Elemente waren entscheidend für den Erfolg und
  die effiziente Bewältigung technischer Herausforderungen.


- Systematische Problembehandlung: Die systematische und kooperative Herangehensweise an Probleme ermöglichte es dem
  Team, Herausforderungen erfolgreich zu meistern und das Skript kontinuierlich zu verbessern.


- Technische Entwicklung und Lerngewinn: Durch die Arbeit an komplexen Aufgaben, wie der Implementierung von
  Benutzererstellung und Passwort-Verschlüsselung sowie der Umstellung auf objektorientierte Programmierung, sammelte
  das Team wertvolle technische Erfahrungen.


- Dokumentation und Transparenz: Die Erstellung von Ablauf- und Flussdiagrammen sowie die umfassende Dokumentation und
  das Testen des Skripts trugen wesentlich zur Qualitätssicherung und zur Nachvollziehbarkeit des Projekts bei.

  
- Nachhaltige Produktentwicklung: Die iterative Verbesserung, das regelmässige Testing und die finale Optimierung des
  Codes führten zu einem robusten und zuverlässigen Endprodukt, das den gestellten Anforderungen entspricht.