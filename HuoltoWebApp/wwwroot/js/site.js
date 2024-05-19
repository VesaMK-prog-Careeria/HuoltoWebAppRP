// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    $('#imageModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget); // Button that triggered the modal
        var imageUrl = button.data('image-url'); // Extract info from data-* attributes
        console.log("Ladattava kuva URL: " + imageUrl); // Tarkista, saadaanko oikea URL
        var modal = $(this);
        modal.find('#modalImage').attr('src', imageUrl);
    });
});