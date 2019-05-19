
// Get the modal
var modal = document.getElementById('autorize-modal');

// Get the button that opens the modal
var btn = document.getElementById("subscribeNewsLetter");

// Get the <span> element that closes the modal
var span = document.getElementsByClassName("close")[0];

// When the user clicks the button, open the modal
$('#subscribeNewsLetter').bind('click', handleButtonClick);


// When the user clicks on <span> (x), close the modal
$('.closeme').bind('click', handlecloseClick);

// When the user clicks anywhere outside of the modal, close it
window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = "none";
    }
}
function handleButtonClick() {
    modal.style.display = "block";
}
function handlecloseClick() {
    modal.style.display = "none";
}