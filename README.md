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
![image](https://user-images.githubusercontent.com/79452149/172478737-1577598a-c2d1-424a-a1eb-76a2c280cc71.png)

Voor alle viewmodels maakt de app gebruik van een BaseViewModel

Deze Viewmodel implementeert 3 interfaces

 - INavigationAware: wanneer een navigatie actie gebeurt wordt de vm op de hoogte gebracht

- IPageLifecycleAware: prism interface om bepaalde code uit te voeren wanneer een pagina Appeared of Dissapeared.
- INotifyPropertyChanged: wanneer een property van waarde verandert word de vm op de hoogte gebracht door een propertychangedevent

In de BaseViewModel maken we gebruik van een protected property(1)(alleen overgeërfde viewmodels kunnen deze property aanspreken). De waarde wordt in de constructor(2) bepaald. Deze service komt van prism zelf.

1:

![image](https://user-images.githubusercontent.com/79452149/172478799-ab8c2a3b-8f0d-48b9-9a4f-63ecb5c573fe.png)

2:

![image](https://user-images.githubusercontent.com/79452149/172478836-4f59178e-9523-4f9f-b975-1b6ce911fc17.png)
Dan hebben nog een property genaamd Mainstate(3) deze property binding we op alle views om aan te geven dat de pagina een spinner moet tonen of niet (4) ![](RackMultipart20220607-1-d3wwm7_html_d7de6e7a66bf62a5.png)

![image](https://user-images.githubusercontent.com/79452149/172478853-f4e9297e-62a0-4b2b-9ec1-9f803b2b6486.png)
Deze boolean gebruiken we om na te gaan of dat er een internetconnectie aanwezig is. (5)

5:
![image](https://user-images.githubusercontent.com/79452149/172478883-a570a968-b3b1-4b73-83c4-1a5f7378a3fb.png)
De waarde hiervan bepalen we door een eventhandler te plaatsen op de Connectivity helper van xamarin zelf. Elke keer wanner dat mijn internet connectie wordt aangepast ga ik met behulp van een event(6) nagaan of de HasNoInternetConnection true of false is(7)

6:

![image](https://user-images.githubusercontent.com/79452149/172478900-cef3d9ed-4e7d-4682-a133-0e849ea80717.png)
![image](https://user-images.githubusercontent.com/79452149/172478968-f621fbd9-4d7c-46cf-94a8-f61147adc84d.png)

Ten slot van de BaseViewModel hebben we nog de functies die we van de interfaces implementeren (8)

8:
![image](https://user-images.githubusercontent.com/79452149/172479003-f4010bd1-32a1-4813-bc13-17fa709f7393.png)
Deze functies zijn virtual, kort om wil dat zeggen dat we in een overgeërfd viewmodel deze kunnen overriden. Ook voorkomt dit dat we in elke viewmodel een aparte implementatie van deze functie kunnen gebruiken.

Belangrijk om te weten in de app maak ik gebruik van Dependency Injection. (9)

Deze types in onderstaande afbeelding worden bij het opstarten van de applicatie in de container van de applicatie geregistreerd. Dit wil zeggen dat we vanuit de constructor types kunnen resolven zonder deze opnieuw te moeten aanmaken (new). Dus de applicatie heeft weet van deze types, hetzelfde voor navigatie acties en voor dialogs.

Services worden transient geregistreerd dit wil zeggen dat er een nieuwe instantie wordt gemaakt elke keer dat deze word opgeroepen.

Singleton: IIdentityPrincipal wordt singeleton geregistreerd dit wil zeggen dat er één instantie van dit type bestaat over heel de applicatie. Deze klasse gebruiken we om na te gaan of dat de gebruiker is ingelogd of niet. Dus is het logisch dat de gegevens over de hele applicatie hetzelfde zijn.

9:

![image](https://user-images.githubusercontent.com/79452149/172479018-a0afe010-943f-4d22-a967-de19a691a2e4.png)
Tijdens mijn opzoek werk heb ik gelezen dat bij programmeren het belangrijk is om Readability en Reausableity voorzien is. Vandaar deze opzet voor code, maar voor styling heb ik dit ook zoveel mogelijk gedaan. In onderstaande afbeelding maak ik gebruik van resources (11)

11:

![image](https://user-images.githubusercontent.com/79452149/172479036-23635d51-7e0e-4ea1-ad9e-fdbe1ffcc950.png)
Deze resources komen uit resourcedictionaries ![](RackMultipart20220607-1-d3wwm7_html_bc90e570686d2623.png)

Binnen de Theme.xaml worden de 2 andere styles gemerged ![](RackMultipart20220607-1-d3wwm7_html_44127519e7a28824.png)

De thema.xaml wordt in de App.xaml als resource gedefinieerd. Deze kunnen we hierna over alle views heen gebruiken.
![image](https://user-images.githubusercontent.com/79452149/172479049-aac64002-c346-4477-9c46-28242ad050fa.png)
# ![image](https://user-images.githubusercontent.com/79452149/172479075-6e75e1a2-192d-485a-8005-5deccbe2b05b.png)


![image](https://user-images.githubusercontent.com/79452149/172479092-88b18afe-9643-4cc6-b689-98f5c866f93a.png)
![image](https://user-images.githubusercontent.com/79452149/172479151-4ac66085-7b29-454b-b5f7-fe12e21db3a2.png)

Landing page

Bij het openen van de app beland men op de welkom pagina. Dit is in code de
 **Welcomepage.xaml &amp; WelcomePageViewModel.cs**

Op deze pagina hebben we 2 buttons. Deze zijn gebind tegen commands in de viewmodel.

Functie 1
![image](https://user-images.githubusercontent.com/79452149/172479489-0083ee1a-18a4-4bc2-9e5d-43d4066f9eab.png)We gaan altijd controleren ofdat er internet is anders gaan de calls falen.

Mainstate zetten we op loading zodat de spinner kan beginnen draaien.

Dan gaan we met behulp van de locationservice (service voor Geolocation van xamarin) onze locatie (x &amp; y) ophalen en placemark ophalen (onze stad naam, landnaam), deze slaan we dan op in de securestorage met key Locations
![image](https://user-images.githubusercontent.com/79452149/172479508-733d8a40-77e1-44d4-a988-a7aea742ed26.png)
En daarna gaan we navigeren naar de wheatherpage zelf.

# WheatherPage

# SettingsPage:

![image](https://user-images.githubusercontent.com/79452149/172479537-a99adea4-d763-423c-a75b-107b21274670.png)
De settingspagina wordt gebruikt om voorkeuren in te stellen we kunnen eenheden aanpassen en de applicatie compleet in darkmode/lightmode instellen.

Dit is heel simpel opgebouwd. Voor elke style property hebben we een appthemebinding. Hier gaan voor lightmode een waarde aan toekennen en voor DarkMode een waarde aan toekennen

![image](https://user-images.githubusercontent.com/79452149/172479554-739b453e-3d8d-4b5f-9033-af8d251227e5.png)
De toggle zelf triggert een command . Hier gaan we de Applicatie usertheme bepalen alsook de thema in geheugen opslaan om bij het opnieuw starten van de applicatie te kunnen nagaan welke thema we hadden ingesteld.

![image](https://user-images.githubusercontent.com/79452149/172479601-0d3825d1-732c-430e-9057-1d449d55ee26.png)Identiek werkt dit voor unit stelsel
![image](https://user-images.githubusercontent.com/79452149/172479611-1556a566-41e4-4243-8a77-bf34ddf21292.png)Voor de units te tonen binnen de applicatie heb ik een converter geschreven dat via de preferences nagaat welke units zijn ingesteld op basis daarvan gebruiken we een Math functie voor de conversie.

De converter zelf is in de app.xaml gedfinieerd en kunnen we dus over alle views heen gebruiken.
![image](https://user-images.githubusercontent.com/79452149/172479621-417fbff3-4064-44ae-aeb7-bd9c200e64f1.png)
![image](https://user-images.githubusercontent.com/79452149/172479634-96d34f4a-e029-4eba-bd8f-198b3633993c.png)
# YourLocations:

![image](https://user-images.githubusercontent.com/79452149/172479643-55c4e32e-4335-4773-96be-799ae64cb525.png)
Projectstructuur:

![image](https://user-images.githubusercontent.com/79452149/172479658-88a93d7c-17c2-4b02-9348-bb3cf5c4635f.png)
