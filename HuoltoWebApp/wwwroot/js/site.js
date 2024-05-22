// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    var imageModal = $('#imageModal');
    if (imageModal.length) {
        imageModal.on('show.bs.modal', function (event) {
            var button = $(event.relatedTarget); // Button that triggered the modal
            var imageUrl = button.data('bs-image-url'); // Extract info from data-* attributes
            console.log("Ladattava kuva URL: " + imageUrl); // Tarkista, saadaanko oikea URL
            var modal = $(this);
            modal.find('#modalImage').attr('src', imageUrl);
        });
    }
});

let capturedImages = [];

function openCameraModal() {
    var modal = document.querySelector('#cameraModal');
    modal.style.display = "block";
    startCamera();
}

function closeCameraModal() {
    var modal = document.querySelector('#cameraModal');
    modal.style.display = "none";
    stopCamera();
}

function startCamera() {
    navigator.mediaDevices.getUserMedia({ video: true })
        .then(function (stream) {
            var video = document.querySelector('#videoElement');
            video.srcObject = stream;
            video.onloadedmetadata = function (e) {
                video.play();
            };
        })
        .catch(function (err) {
            console.log(err.name + ": " + err.message);
        });
}

function stopCamera() {
    var video = document.querySelector('#videoElement');
    var stream = video.srcObject;
    var tracks = stream.getTracks();

    tracks.forEach(function (track) {
        track.stop();
    });

    video.srcObject = null;
}

function captureImage() {
    var canvas = document.querySelector('#canvasElement');
    var video = document.querySelector('#videoElement');
    var context = canvas.getContext('2d');
    // Aseta canvasin koko videon koon mukaan
    canvas.width = video.videoWidth;
    canvas.height = video.videoHeight;

    context.drawImage(video, 0, 0, canvas.width, canvas.height);
    canvas.style.display = "block";
}

function saveCapturedImage() {
    var canvas = document.querySelector('#canvasElement');
    canvas.toBlob(function (blob) {
        // Luodaan uusi File-objekti, jotta se voidaan lisätä FormDataan
        var file = new File([blob], `capturedImage${capturedImages.length}.jpg`, { type: 'image/jpeg' });
        capturedImages.push(file);

        var li = document.createElement('li');
        li.textContent = "Captured Image " + capturedImages.length;
        document.querySelector('#capturedImagesList').appendChild(li);
    });

    closeCameraModal();
}

function handleFormSubmit(event) {
    event.preventDefault();

    var formData = new FormData(event.target);

    capturedImages.forEach((image, index) => {
        formData.append('CapturedImages', image);
    });

    fetch(event.target.action, {
        method: event.target.method,
        body: formData
    }).then(response => {
        if (response.ok) {
            window.location.href = './Index';
        } else {
            // Handle error
            console.error('Failed to submit the form.');
        }
    }).catch(error => {
        console.error('Error:', error);
    });
}

document.addEventListener('DOMContentLoaded', function () {
    var openModalButton = document.querySelector('#openModal');
    if (openModalButton) {
        openModalButton.addEventListener('click', openCameraModal);
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

    var autoForm = document.querySelector('#autoForm');
    if (autoForm) {
        autoForm.addEventListener('submit', handleFormSubmit);
    }
});