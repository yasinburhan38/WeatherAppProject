Documentatie wheatherapp

Inleiding:

De wheatherapp is een met PRISM framework, mvvm gemaakt applicatie. Met behulp van een open api ([https://openweathermap.org/api](https://openweathermap.org/api)) haalt de applicatie data ivm weersvoorspelling op, ook wordt gebruik gemaakt van een eigen .net 6 api die gebruikt wordt om de gebruiker te authentiseren. De api maakt gebruik van een in memory sql server. De app maakt ook gebruik v.d Securestorage &amp; Preferences van xamarin. Hiermee slaan we data op in de cache van de applicatie (lokaal)

 De bedoeling van deze wheaterapp is om van uw favoriete locaties weersvoorspelling op te halen en deze in een overzicht weer te geven om ze later op een oogopslag te kunnen herbekijken.

We kunnen op 2 manieren weersvoorspelling op halen.

- Op basis van je huidige locatie
- Op basis van ingevulde locatie

Wel is belangrijk om te weten dat voor deze acties inloggen niet verplicht is, echter kan men locaties bewerken wanneer ingelogd.

Voor de opbouw van dit project heb ik me zoveel mogelijk aan de principes van Object-georiënteerd programmeren proberen te houden. Hier kom ik later in het document op terug.

# Basis opzet

V ![](RackMultipart20220607-1-d3wwm7_html_ef8e49769b248135.png) oor alle viewmodels maakt de app gebruik van een BaseViewModel

Deze Viewmodel implementeert 3 interfaces

 - INavigationAware: wanneer een navigatie actie gebeurt wordt de vm op de hoogte gebracht

- IPageLifecycleAware: prism interface om bepaalde code uit te voeren wanneer een pagina Appeared of Dissapeared.
- INotifyPropertyChanged: wanneer een property van waarde verandert word de vm op de hoogte gebracht door een propertychangedevent

In de BaseViewModel maken we gebruik van een protected property(1)(alleen overgeërfde viewmodels kunnen deze property aanspreken). De waarde wordt in de constructor(2) bepaald. Deze service komt van prism zelf.

1:

![](RackMultipart20220607-1-d3wwm7_html_4c93f7d08bc1dd98.png)

2:

![](RackMultipart20220607-1-d3wwm7_html_dd71e30962905fd8.png)

Dan hebben nog een property genaamd Mainstate(3) deze property binding we op alle views om aan te geven dat de pagina een spinner moet tonen of niet (4) ![](RackMultipart20220607-1-d3wwm7_html_d7de6e7a66bf62a5.png)

![](RackMultipart20220607-1-d3wwm7_html_b263a2d264d86d73.png)

Deze boolean gebruiken we om na te gaan of dat er een internetconnectie aanwezig is. (5)

5:
 ![](RackMultipart20220607-1-d3wwm7_html_5cf0b204a3ae8324.png)

De waarde hiervan bepalen we door een eventhandler te plaatsen op de Connectivity helper van xamarin zelf. Elke keer wanner dat mijn internet connectie wordt aangepast ga ik met behulp van een event(6) nagaan of de HasNoInternetConnection true of false is(7)

6:

![](RackMultipart20220607-1-d3wwm7_html_685dabdb8b1580f.png) 7: ![](RackMultipart20220607-1-d3wwm7_html_b86e3cb84220681b.png)

Ten slot van de BaseViewModel hebben we nog de functies die we van de interfaces implementeren (8)

8:
 ![](RackMultipart20220607-1-d3wwm7_html_8a47f99c7f98d7ab.png)

Deze functies zijn virtual, kort om wil dat zeggen dat we in een overgeërfd viewmodel deze kunnen overriden. Ook voorkomt dit dat we in elke viewmodel een aparte implementatie van deze functie kunnen gebruiken.

Belangrijk om te weten in de app maak ik gebruik van Dependency Injection. (9)

Deze types in onderstaande afbeelding worden bij het opstarten van de applicatie in de container van de applicatie geregistreerd. Dit wil zeggen dat we vanuit de constructor types kunnen resolven zonder deze opnieuw te moeten aanmaken (new). Dus de applicatie heeft weet van deze types, hetzelfde voor navigatie acties en voor dialogs.

Services worden transient geregistreerd dit wil zeggen dat er een nieuwe instantie wordt gemaakt elke keer dat deze word opgeroepen.

Singleton: IIdentityPrincipal wordt singeleton geregistreerd dit wil zeggen dat er één instantie van dit type bestaat over heel de applicatie. Deze klasse gebruiken we om na te gaan of dat de gebruiker is ingelogd of niet. Dus is het logisch dat de gegevens over de hele applicatie hetzelfde zijn.

9:

![](RackMultipart20220607-1-d3wwm7_html_5ec8d467de85f998.png)

Tijdens mijn opzoek werk heb ik gelezen dat bij programmeren het belangrijk is om Readability en Reausableity voorzien is. Vandaar deze opzet voor code, maar voor styling heb ik dit ook zoveel mogelijk gedaan. In onderstaande afbeelding maak ik gebruik van resources (11)

11:

![](RackMultipart20220607-1-d3wwm7_html_59d84c82db753a2c.png)

Deze resources komen uit resourcedictionaries ![](RackMultipart20220607-1-d3wwm7_html_bc90e570686d2623.png)

Binnen de Theme.xaml worden de 2 andere styles gemerged ![](RackMultipart20220607-1-d3wwm7_html_44127519e7a28824.png)

De thema.xaml wordt in de App.xaml als resource gedefinieerd. Deze kunnen we hierna over alle views heen gebruiken.
 ![](RackMultipart20220607-1-d3wwm7_html_c2741c6ef5bad71b.png)

# ![](RackMultipart20220607-1-d3wwm7_html_15713498f9e1375b.png) Landing page

Bij het openen van de app beland men op de welkom pagina. Dit is in code de
 **Welcomepage.xaml &amp; WelcomePageViewModel.cs**

Op deze pagina hebben we 2 buttons. Deze zijn gebind tegen commands in de viewmodel.
 ![](RackMultipart20220607-1-d3wwm7_html_4e804496146a6cac.png)

![](RackMultipart20220607-1-d3wwm7_html_6f49086d2908071d.png)

Functie 1
 ![](RackMultipart20220607-1-d3wwm7_html_ed9b71cb20f1a632.png)

We gaan altijd controleren ofdat er internet is anders gaan de calls falen.

Mainstate zetten we op loading zodat de spinner kan beginnen draaien.

Dan gaan we met behulp van de locationservice (service voor Geolocation van xamarin) onze locatie (x &amp; y) ophalen en placemark ophalen (onze stad naam, landnaam), deze slaan we dan op in de securestorage met key Locations
 ![](RackMultipart20220607-1-d3wwm7_html_f2540eee61c5203c.png)

En daarna gaan we navigeren naar de wheatherpage zelf.

# WheatherPage

# SettingsPage:

![](RackMultipart20220607-1-d3wwm7_html_96eba3d9a1de2758.png)

De settingspagina wordt gebruikt om voorkeuren in te stellen we kunnen eenheden aanpassen en de applicatie compleet in darkmode/lightmode instellen.

Dit is heel simpel opgebouwd. Voor elke style property hebben we een appthemebinding. Hier gaan voor lightmode een waarde aan toekennen en voor DarkMode een waarde aan toekennen

![](RackMultipart20220607-1-d3wwm7_html_c5d41a1d6cf42f15.png)

De toggle zelf triggert een command . Hier gaan we de Applicatie usertheme bepalen alsook de thema in geheugen opslaan om bij het opnieuw starten van de applicatie te kunnen nagaan welke thema we hadden ingesteld.

![](RackMultipart20220607-1-d3wwm7_html_915a0efe2e249e87.png)

Identiek werkt dit voor unit stelsel
 ![](RackMultipart20220607-1-d3wwm7_html_343fb3a8a00ac808.png)

Voor de units te tonen binnen de applicatie heb ik een converter geschreven dat via de preferences nagaat welke units zijn ingesteld op basis daarvan gebruiken we een Math functie voor de conversie.

De converter zelf is in de app.xaml gedfinieerd en kunnen we dus over alle views heen gebruiken.
 ![](RackMultipart20220607-1-d3wwm7_html_12c268b409e07bf8.png)

![](RackMultipart20220607-1-d3wwm7_html_19baea026643ae6a.png)

# YourLocations:

![](RackMultipart20220607-1-d3wwm7_html_bb7e7bdfc1ea097c.png)

Projectstructuur:

![](RackMultipart20220607-1-d3wwm7_html_65bbb6f556ce5126.png)
