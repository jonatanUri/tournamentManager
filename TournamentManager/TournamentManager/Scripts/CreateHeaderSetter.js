var NameInput = document.getElementById("Name");
var HeaderText = document.getElementById("header-text");

function SetHeaderTextToNameInput() {
    HeaderText.innerText = NameInput.value;
    if (NameInput.value == "") {
        HeaderText.innerText = "New tournament";
    }
}

NameInput.onkeydown = function () { setTimeout(SetHeaderTextToNameInput, 5) };

