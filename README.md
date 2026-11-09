# HypnoControl – Windows-Downloads

Hier werden nach manueller Prüfung die Windows-Versionen von HypnoControl bereitgestellt. Das Entwicklungsrepository und seine Git-Historie bleiben privat.

## Download

**[GitHub Releases ansehen](https://github.com/heretoholdbeer/HypnoControl-Downloads/releases)**

Ein Release enthält folgende Dateien:

| Datei | Verwendung |
| --- | --- |
| `HypnoControl-Launcher.exe` | Startet den Update-Manager ohne GitHub-Anmeldung |
| `HypnoControl.exe` | Vollständige Windows-Anwendung |
| `HypnoControl.exe.sha256` | SHA-256-Prüfsumme für die EXE |

Wenn die Release-Seite noch leer ist, ist die öffentliche Veröffentlichung noch nicht abgeschlossen. Ein leeres Repository ist **kein** Zeichen dafür, dass der Launcher schon heruntergeladen werden kann.

## Launcher

1. `HypnoControl-Launcher.exe` herunterladen und unter Windows starten.
2. **UPDATE** lädt die neueste veröffentlichte Version per HTTPS herunter, prüft die von GitHub angegebene SHA-256-Prüfsumme und installiert sie im Benutzerprofil.
3. **ALTE VERSION STARTEN** startet die unmittelbar vorherige, vom Launcher verwaltete Version, sofern verfügbar. **BEENDEN** schließt den Launcher.

Der Launcher benötigt keine GitHub-Anmeldung und keine GitHub-CLI. Eigene Medien, Einstellungen und Benutzerdateien sind nicht Teil der Releases.

## Wichtige Hinweise

- **Alpha-Version / unsigniert:** Die EXE-Dateien sind nicht mit einem Windows-Code-Signing-Zertifikat signiert. Eine SmartScreen-Warnung ist möglich.
- Der aktuelle Programmcontainer enthält eingebettete Skripte und Laufzeitressourcen. Die private Git-Historie wird nicht veröffentlicht, aber in der EXE enthaltene Skripte sind technisch extrahierbar.
- Hardwarefunktionen sowie Live-Synchronisation mit externen Geräten sind nicht vollständig auf dem Zielsystem geprüft.
- Ein Download ist erst dann freigegeben, wenn die manuelle private Build-, Datenschutz- und Release-Prüfung abgeschlossen wurde.
