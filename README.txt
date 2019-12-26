Hei sinä onneton joka joudut katsomaan tämän projektin koodia!

skriptit ja muut ovat nimetty päin puita.
buttonController-skripti tekee varmaan 80% koko ohjelman toiminnoista.
VScannerButton-skripti hoitaa QR-koodin lukemisen.
CloudRecoEventHandler hoitaa pilven kautta tapahtuvan kuvatunnistuksen.
CloudUpLoading skripti on mahdollista pilveen latausta varten (ei implementoitu tuota skriptiä pidemmälle tässä ohjelmistossa, ei ollenkaan käytössä.)
tyhjä imagetarget on kuvatunnistusta varten (mikäli haluat esim lisätä tunnistettavaan kuvaan jonkin 3d-efektin niin se tulisi tähän).
PDF renderer lisenssi on "vr@savonia.fi" unitytilillä (mikko pääkkönen teki tilin).
Vuforia-lisenssi (ilmaislisenssi) on myös vr@savonia.fi nimisellä tilillä (sama salasana kuin unity vr@savonia.fi-tilillä), täältä löytyy myös vuforia datasetit (cloud recognition) mitä ohjelma
käyttää (https://developer.vuforia.com/vui/auth/login)

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

Nauttikaa!


Reino P.

