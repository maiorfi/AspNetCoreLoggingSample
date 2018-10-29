### Per il logging su Azure:

- Installare il package "Microsoft.Extensions.Logging.AzureAppServices"
- Verificare la configurazione del appsettings.json (una volta pubblicata con il profilo Release, l'applicazione gira usando un Hosting Environment che per default è "Production" )
- Dopo ogni modifica al appsettings.json occorre riavviare il servizio (viene riavviato il container dell'applicazione)
- Per visualizzare i log via powershell:
	+ Eseguire "Add-AzureAccount" per registrare interattivamente un account azure
	+ Esegure "Get-AzureWebSiteLog -Name "nome-applicazione" -Tail" per visualizzarli in tempo reale (con il supporto dei codici colore console)
