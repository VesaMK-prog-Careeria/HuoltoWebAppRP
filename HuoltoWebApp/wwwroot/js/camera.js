let video = document.getElementById("videoElement");
let canvas = document.getElementById("canvasElement");
let captureBtn = document.getElementById("capture");
let saveBtn = document.getElementById("saveImage");
let capturedImagesList = document.getElementById("capturedImagesList");
let cameraModal = document.getElementById("cameraModal");
let closeModal = document.querySelector(".close");

let stream;

// Avaa kamera
document.getElementById("openModal").addEventListener("click", async () => {
    cameraModal.style.display = "block";
    stream = await navigator.mediaDevices.getUserMedia({ video: true });
    video.srcObject = stream;
});

// Sulje kamera
closeModal.onclick = () => {
    cameraModal.style.display = "none";
    if (stream) {
        stream.getTracks().forEach(track => track.stop());
    }
};

// Ota kuva
captureBtn.addEventListener("click", () => {
    canvas.width = video.videoWidth;
    canvas.height = video.videoHeight;
    let context = canvas.getContext("2d");
    context.drawImage(video, 0, 0, canvas.width, canvas.height);
    canvas.style.display = "block";
});

// Tallenna kuva ja liitä formiin
saveBtn.addEventListener("click", () => {
    canvas.toBlob(blob => {
        let file = new File([blob], `kamera-kuva-${Date.now()}.jpg`, { type: "image/jpeg" });

        // Luo piilotettu input ja lisää se lomakkeeseen
        let input = document.createElement("input");
        input.type = "file";
        input.name = "CapturedImages";
        input.files = createFileList([file]);
        input.style.display = "none";

        document.getElementById("capturedImagesList").appendChild(input);

        // Näytä käyttäjälle nimi
        let li = document.createElement("li");
        li.textContent = file.name;
        capturedImagesList.appendChild(li);
    }, "image/jpeg");

    cameraModal.style.display = "none";
    stream.getTracks().forEach(track => track.stop());
});

// Helper: Luo FileList olio
function createFileList(files) {
    let dataTransfer = new DataTransfer();
    files.forEach(file => dataTransfer.items.add(file));
    return dataTransfer.files;
}
