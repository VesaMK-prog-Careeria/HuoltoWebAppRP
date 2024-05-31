
// Kuvien lataamiseen liittyvät toiminnot Vesku
$(function () {
    var imageModal = $('#imageModal');  // Modal-ikkuna, joka näyttää kuvan
    if (imageModal.length) {            // Tarkistetaan, onko modal-ikkuna olemassa
        imageModal.on('show.bs.modal', function (event) {       // Kun modal-ikkuna avataan (show.bs.modal)
            var button = $(event.relatedTarget);                // Button, joka avaa modal-ikkunan (event.relatedTarget)
            var imageUrl = button.data('bs-image-url');         // Buttonin data-attribuutti, joka sisältää kuvan URL:n (bs-image-url)
            console.log("Ladattava kuva URL: " + imageUrl);     // Tarkista, saadaanko oikea URL konsoliin (debug)
            var modal = $(this);                                // Modal-ikkuna, joka avataan (this)
            modal.find('#modalImage').attr('src', imageUrl);    // Aseta modal-ikkunan kuvan lähde (src) URL:ksi (imageUrl)
        });
        // Event listener, joka kuuntelee, kun modal-ikkuna piilotetaan (hide.bs.modal)
        imageModal.on('hide.bs.modal', function () {
            console.log('Modal is being hidden');
            // Piilota kuva, kun modal-ikkuna piilotetaan
            $(this).find('#modalImage').attr('src', '');
        });
    }
});

// Kameran käyttöön liittyvät toiminnot Vesku

let capturedImages = [];                                        // Taulukko, johon tallennetaan kameralla otetut kuvat

function openCameraModal() {                                    // Avaa kameran modal-ikkunan
    var modal = document.querySelector('#cameraModal');         // Hae modal-ikkuna dokumentista
    modal.style.display = "block";                              // Aseta modal-ikkunan näyttötilaksi "block"
    startCamera();                                              // Käynnistä kamera funktio
}

function closeCameraModal() {                                   // Sulkee kameran modal-ikkunan
    var modal = document.querySelector('#cameraModal');         // Hae modal-ikkuna dokumentista
    modal.style.display = "none";                               // Aseta modal-ikkunan näyttötilaksi "none" (piilota)
    stopCamera();                                               // Sammuta kamera funktio
}

function startCamera() {                                        // Käynnistää kameran
    navigator.mediaDevices.getUserMedia({ video: true })        // Hae mediaDevices-rajapinnasta käyttäjän media (video) laite (getUserMedia) ja käynnistä kamera
        .then(function (stream) {                               // Jos kamera käynnistyy onnistuneesti
            var video = document.querySelector('#videoElement');// Hae video-elementti dokumentista
            video.srcObject = stream;                           // Aseta videon lähde (srcObject) streamiksi (kameran kuva)
            video.onloadedmetadata = function (e) {             // Kun video on ladattu (onloadedmetadata) suorita seuraavat toiminnot
                video.play();                                   // Käynnistä video (play) automaattisesti
            };
        })
        .catch(function (err) {                                 // Jos kameraa ei voida käynnistää, tulosta virhe konsoliin
            console.log(err.name + ": " + err.message);         // Tulosta virheen nimi ja viesti konsoliin
        });                                                         // Voisi tehdä virheilmoituksen näkyviin käyttäjälle myös (esim. alert)
}

function stopCamera() {                                         // Sammuttaa kameran
    var video = document.querySelector('#videoElement');        // Hae video-elementti dokumentista
    var stream = video.srcObject;                               // Hae videon lähde (srcObject) streamiksi (kameran kuva)
    var tracks = stream.getTracks();                            // Hae streamin raidat (getTracks) ja tallenna ne tracks-muuttujaan (taulukkoon)

    tracks.forEach(function (track) {                           // Käy läpi kaikki raidat (forEach) ja suorita seuraavat toiminnot jokaiselle raidalle
        track.stop();                                           // Sammuta raita (stop) (kamera)
    });
    videoElement.style.display = "none";                        // Piilota video-elementti
    canvasElemnt.style.display = "block";                        // Piilota canvas-elementti
    video.srcObject = null;                                     // Aseta videon lähde (srcObject) tyhjäksi (null)
}

function captureImage() {                                       // Ottaa kuvan kameralla
    var canvas = document.querySelector('#canvasElement');      // Hae canvas-elementti dokumentista (kuva)
    var video = document.querySelector('#videoElement');        // Hae video-elementti dokumentista (kamera)
    var context = canvas.getContext('2d');                      // Hae canvasin konteksti (getContext) 2d-muodossa (kaksiulotteinen)
    // Aseta canvasin koko videon koon mukaan
    canvas.width = video.videoWidth;
    canvas.height = video.videoHeight;

    context.drawImage(video, 0, 0, canvas.width, canvas.height);
    canvas.style.display = "block";
    stopCamera();
}

function saveCapturedImage() {                                  // Tallentaa otetun kuvan
    var canvas = document.querySelector('#canvasElement');      // Hae Canvas-elementti dokumentista (kuva)
    canvas.toBlob(function (blob) {                             // canvas.toBlob on JS metodi joka luo blob-objektin canvas elementistä.
                                                                // Blob (Binary Large Object) voi sisältää suuria määriä dataa esim. kuvat
        // Luodaan uusi File-objekti, jotta se voidaan lisätä FormDataan
        var file = new File([blob], `capturedImage${capturedImages.length}.jpg`, { type: 'image/jpeg' });
        capturedImages.push(file);

        var li = document.createElement('li');
        li.textContent = "Captured Image " + capturedImages.length;
        document.querySelector('#capturedImagesList').appendChild(li);
    });

    closeCameraModal();
}

function handleFormSubmit(event) {                              // Käsittelee lomakkeen lähetyksen (submit)
    event.preventDefault();                                     // Estä lomakkeen oletustoiminnot (preventDefault) (ei lataa sivua uudelleen)

    var formData = new FormData(event.target);                  // Luo uusi FormData-objekti (lomakkeen tiedot) ja tallenna se formData-muuttujaan

    capturedImages.forEach((image, index) => {                  // Käy läpi kaikki kuvat (forEach) ja suorita seuraavat toiminnot jokaiselle kuvalle
        formData.append('CapturedImages', image);               // Lisää FormDataan (lomakkeen tiedot) kuva (CapturedImages) tiedostona (image)
    });

    fetch(event.target.action, {                                // fetch-metodi, joka lähettää lomakkeen tiedot palvelimelle
        method: event.target.method,                            // Lähetyksen metodi (POST)
        body: formData                                          // Lähetyksen tiedot (formData)
    }).then(response => {                                       // Kun vastaus saadaan (then)
        if (response.ok) {                                      // Jos vastaus on ok (200)
            window.location.href = './Index';                   // Uudelleenohjaa käyttäjä Index-sivulle
        } else {
            // Handle error
            console.error('Failed to submit the form.');
        }
    }).catch(error => {
        console.error('Error:', error);
    });
}

// addEventListener-metodi, joka kuuntelee, kun sivu on latautunut ja suorittaa annetut toiminnot
document.addEventListener('DOMContentLoaded', function () {                 // Kun sivu on latautunut (DOMContentLoaded)
    var openModalButton = document.querySelector('#openModal');             // Hae openModal-button dokumentista
    if (openModalButton) {                                                  // Jos openModal-button on olemassa
        openModalButton.addEventListener('click', openCameraModal);         // Lisää tapahtumankuuntelija (click) openCameraModal-funktiolle
    }

    var closeModalButton = document.querySelector('.close');
    if (closeModalButton) {
        closeModalButton.addEventListener('click', closeCameraModal);
    }

    var captureButton = document.querySelector('#capture');
    if (captureButton) {
        captureButton.addEventListener('click', captureImage);
    }

    var saveImageButton = document.querySelector('#saveImage');
    if (saveImageButton) {
        saveImageButton.addEventListener('click', saveCapturedImage);
    }

    var autoForm = document.querySelector('#autoForm');                     // Hae autoForm dokumentista (lomake)
    if (autoForm) {
        autoForm.addEventListener('submit', handleFormSubmit);              // Lisää tapahtumankuuntelija (submit) handleFormSubmit-funktiolle (lomakkeen lähetys)
    }
});

// Päivämäärän lataaminen automaattisesti pvm kenttiin Vesku
document.addEventListener("DOMContentLoaded", function () {                 // Kun sivu on latautunut (DOMContentLoaded)
    // Hanki nykyinen päivämäärä
    var today = new Date();

    // Muotoile päivämäärä yyyy-mm-dd-muotoon
    var day = ("0" + today.getDate()).slice(-2);                            // Lisää etunollat, jos päivä tai kuukausi on yksinumeroinen
    var month = ("0" + (today.getMonth() + 1)).slice(-2);
    var year = today.getFullYear();                                         // Hae vuosi

    var formattedDate = year + "-" + month + "-" + day;                     // Muodosta päivämäärä yyyy-mm-dd-muotoon

    // Hanki kaikki date-tyyppiset input-kentät
    var dateInputs = document.querySelectorAll('input[type="date"]');       

    // Aseta päivämäärä kaikkiin date-tyyppisiin input-kenttiin
    dateInputs.forEach(function (input) {
        input.value = formattedDate;
    });
});