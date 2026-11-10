# HypnoControl – Windows-Downloads

Dieses öffentliche Download-Repository enthält ausschließlich geprüfte Windows-Binärdateien, einen Versionsindex und Prüfsummen. Die private Entwicklerhistorie wurde nicht übernommen.

## Direkt herunterladen

**[HypnoControl Launcher herunterladen](https://raw.githubusercontent.com/heretoholdbeer/HypnoControl-Downloads/main/downloads/HypnoControl-Launcher.exe)**

Aktuelle Anwendung: **v0.24.0-alpha.325**  
**[HypnoControl.exe herunterladen](https://raw.githubusercontent.com/heretoholdbeer/HypnoControl-Downloads/main/downloads/HypnoControl.exe)**

[Launcher SHA-256](downloads/HypnoControl-Launcher.exe.sha256) · [App SHA-256](downloads/HypnoControl.exe.sha256) · [Versionsindex](downloads/current.json)

## Starten und aktualisieren

1. Nur den `HypnoControl-Launcher.exe` herunterladen und auf dem Desktop speichern.
2. Im Launcher `UPDATE` wählen. Der Launcher ruft den öffentlichen Versionsindex ab, lädt die EXE via HTTPS und verifiziert Größe und SHA-256.
3. Sobald die Installation abgeschlossen ist, `APP STARTEN` verwenden. Vorige, vom Launcher installierte Versionen bleiben für ein Rollback erhalten.

Es ist weder GitHub-Anmeldung noch GitHub CLI erforderlich. Der Launcher speichert die Anwendung und vorherige Versionen unter `%LOCALAPPDATA%\HypnoControl\UpdateManager\Versions`. Eigene Bilder, Videos, Sprachdateien und Konfigurationen werden nicht in den Downloadordner kopiert oder durch Updates gelöscht.

## Hinweise

- **Unsignierte Alpha:** Windows kann eine Sicherheits- oder SmartScreen-Warnung anzeigen. Die Prüfsummenprüfung ist kein Ersatz für eine gültige Herausgebersignatur.
- In die EXE eingebettete PowerShell-Skripte und Laufzeitressourcen sind technisch extrahierbar. Die private Git-Historie ist nicht enthalten.
- Die EXE und ihre Ressourcen wurden automatisiert auf zuvor definierte sensible Muster geprüft. Eine vollständige manuelle Sicherheitsgarantie folgt daraus nicht.
- Physische Geräte und externe Audiohardware sind nicht auf jedem Zielsystem getestet.
