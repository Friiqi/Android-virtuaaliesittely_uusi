Edit for anyone not knowing what this is:
School project, mobile app for Savonia, created with Unity3d (odd choice for a mobile app, i know). Original idea was to make an app that would use AR and object recognition, 
but that was bit too much for my current skill levels and the image recognition seemed to have serious limitations, so in order to be able to create a working app 

within the timelimit, it was decided to just make an app that can read QR code and show videos and PDF files. 

Customer wanted an easy way for getting more information about objects in our schools 3d-printing lab, and to make it more like an interactive "tour".



Hei sinä onneton joka joudut katsomaan tämän projektin koodia!

skriptit ja muut ovat nimetty päin puita.
buttonController-skripti tekee varmaan 80% koko ohjelman toiminnoista.
VScannerButton-skripti hoitaa QR-koodin lukemisen.
CloudRecoEventHandler hoitaa pilven kautta tapahtuvan kuvatunnistuksen.
CloudUpLoading skripti on mahdollista pilveen latausta varten (ei implementoitu tuota skriptiä pidemmälle tässä ohjelmistossa, ei ollenkaan käytössä.)
tyhjä imagetarget on kuvatunnistusta varten (mikäli haluat esim lisätä tunnistettavaan kuvaan jonkin 3d-efektin niin se tulisi tähän).



Pääasiassa buttonController-skriptissä oleva switch-case hoitaa UI elementtien näyttämistä/piilotusta sekä joissain tilanteissa syöte-stringin muokkauksen kutsua (urlForming(inputUrlString)).

hyvin monet public-elementit vaativat että niihin (skriptiin) on dragatty unityn inspectorissa oikea elementti. Varsinkin UI elementit. (esim UICamera->mainCanvas ja katsotte unityn Inspectorissa
niin näette että esim jokainen nappi on dragatty skriptiin että ne voidaan näyttää/piilottaa helpommin (setActive(true/false)) jne. yleensä public stringit/booleanit eivät vaadi tälläista,
ne ovat publiccina että niihin pääsee käsiksi muista skriptoista.
nyrkkisääntönä että mikäli jokin on määritetty publiciksi (esim public CloudRecoEventHandler cloudRecoEventHandler;) ja saman skriptin Start() funktiosta löytyy seuraava rivi
cloudRecoEventHandler = GameObject.Find("Cloud Recognition").GetComponent<CloudRecoEventHandler>();
niin silloin se ei vaadi että siihen dragataan inspectorissa oikea elementti. Joissain se on pakko tehdä draggaamalla (esim element ei ole aktiivisena heti alussa) tai ne vaan on tehty
siten syystä x tai muuten vaan.

Tämä projekti oli käytännössä ensimmäinen kerta koskaan kun rakentelin tälläistä ja käytin unityä enemmälti joten se on hyvin sotkuinen ja epäselvä. mutta toimii. en tiedä miksi.

Ikävä kyllä näissä skriptoissa kummittelee joitakin turhia (käyttämättömiä/vanhoja) funktioita ja elementtejä. Mutta ei saa tehdä asioita liian helpoksi.

.apk tiedostot ovat pääasiassa turhia poislukien viimeisintä versiota, muut ovat jotain testausta varten buildattuja paketteja ja jääneet kummittelemaan tuonne.

Nauttikaa!


Reino P.

