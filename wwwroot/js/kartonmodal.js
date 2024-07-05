var btn = document.getElementById("KartonCreate"); // DUGME NA INDEXU ZA KREIRANJE KARTONA
var close = document.getElementById("closeModal"); // DUGME NA MODELU ZA ZATVARANJE KARTONA
var modalBox = document.getElementById("KartonWrapper"); // MODAL BOX

btn.onclick = function () {
    modalBox.style.display = "block";
}

close.onclick = function () {
    modalBox.style.display = "none";
}


btn.onclick = $('#KartonModal').modal('show');