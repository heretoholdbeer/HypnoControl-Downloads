# HypnoControl – Windows-Downloads

Hier werden nach manueller Prüfung die Windows-Versionen von HypnoControl bereitgestellt. Das Entwicklungsrepository und seine Git-Historie bleiben privat.

## Download

**[Launcher für Windows herunterladen (EXE, 32 KB)](https://github.com/heretoholdbeer/HypnoControl-Downloads/raw/refs/heads/main/downloads/HypnoControl-Launcher.exe)**

[SHA-256-Prüfsumme](https://github.com/heretoholdbeer/HypnoControl-Downloads/blob/main/downloads/HypnoControl-Launcher.exe.sha256) · [GitHub Releases ansehen](https://github.com/heretoholdbeer/HypnoControl-Downloads/releases)

**Hinweis:** Der Launcher ist bereits öffentlich verfügbar, der erste öffentliche App-Release aber noch nicht. Daher kann `UPDATE` aktuell noch keine HypnoControl-Version installieren. Der Launcher benötigt später keine GitHub-Anmeldung.

Ein Release enthält folgende Dateien:

| Datei | Verwendung |
| --- | --- |
| `HypnoControl-Launcher.exe` | Startet den Update-Manager ohne GitHub-Anmeldung |
| `HypnoControl.exe` | Vollständige Windows-Anwendung |
| `HypnoControl.exe.sha256` | SHA-256-Prüfsumme für die EXE |

Wenn die Release-Seite noch leer ist, steht die Anwendung selbst noch nicht öffentlich bereit. Der Launcher-Download oben ist davon unabhängig.

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
