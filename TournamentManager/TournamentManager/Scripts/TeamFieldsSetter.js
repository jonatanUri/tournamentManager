var TeamSizeInput = document.getElementById("TeamSize");
var TeamNamesBox = document.getElementsByName("TeamNamesBox")[0];
TeamSizeInput.onchange = SetTeamNameBoxes;
TeamSizeInput.onkeydown = function () { setTimeout(SetTeamNameBoxes, 10)};
function SetTeamNameBoxes () {
    TeamNamesBox.innerHTML = "";
    for (var i = 0; i < TeamSizeInput.value; i++) {
        TeamNamesBox.innerHTML += '<input class="form-control text-box single-line valid" data-val="true" data-val-required="A következő mező megadása kötelező: TeamSize." name="TeamNames[]" value="Team ' + (i + 1) + '" aria-describedby="TeamSize-error" aria-invalid="false" autocomplete="off"><br>'
    }
}
