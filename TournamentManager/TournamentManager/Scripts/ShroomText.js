Array.prototype.random = function () {
    return this[Math.floor(Math.random() * this.length)];
}
String.prototype.random = function () {
    return this[Math.floor(Math.random() * this.length)];
}
HTMLCollection.prototype.random = function () {
    return this[Math.floor(Math.random() * this.length)];
}
HTMLCollection.prototype.forEach = function (func) {
    for (var i = 0; i < this.length; i++) {
        func(this[i], i);
    }
}
HTMLCollection.prototype.findIndex = function (myItem) {
    var indexer;
    this.forEach(function (item, index) {
        if (myItem == item) {
            indexer = index;
            return index
        }
    });
    if (indexer) {
        return indexer;
    }
    return -1;
}

var SPEED = 60; //changes each n ms
var ITERATIONS = 50; //repeat change n times
var CHANGE_RATE = 0.7 //percent to change each iteration
var COUNTER = 0;
var INFINITE = false;
const CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz óüöúőűáéŰÁÉÚŐÓÜÖπΣδə𝔰ㄹΞεʒ⌕⥀⍳Γ";
var shroomTexts = document.getElementsByClassName("shroom-text");
var originalTexts = new Array(shroomTexts.length);

function SetOriginalText(item, index) {
    originalTexts[index] = item.innerText;
}

function RandomizeShroomTexts() {
    shroomTexts.forEach(function (item, index) {
        var newText = "";
        for (var i = 0; i < item.innerText.length; i++) {
            if (Math.random() > CHANGE_RATE) {
                newText += GetSimilarChar(item.innerText[i]);
            } else {
                newText += originalTexts[index][i];
            }
        }
        if (Math.random() > 0.70) {
            item.innerText = newText;
        }
    });
    if (INFINITE) {
        setTimeout(RandomizeShroomTexts, SPEED);
    } else {
        COUNTER++;
        if (COUNTER <= ITERATIONS) {
            /*
            if (COUNTER / ITERATIONS > Math.random()) {
                var element = shroomTexts.random();
                var index = shroomTexts.findIndex(element);
                element.classList.remove("shroom-text");
                element.innerText = originalTexts[index];
                shroomTexts = document.getElementsByClassName("shroom-text");
                shroomTexts.forEach(SetOriginalText);
            }
            */
            setTimeout(RandomizeShroomTexts, SPEED);
        }
        else {
            setTimeout(SetShroomTextsToOriginal, SPEED);
        }
    }
}

function SetShroomTextsToOriginal() {
    shroomTexts.forEach(SetTextToOriginal);
}

function SetTextToOriginal(item, index) {
    item.innerText = originalTexts[index];
}

function Start() {
    COUNTER = 0;
    shroomTexts.forEach(SetOriginalText);
    RandomizeShroomTexts();
}

function GetSimilarChar(char) {
    switch (char) {
        case "Q": case "O": case "o": case "0": case "ö": case "Ö": case "ó": case "Ó": case "ő": case "Ő": case "D": case "c": case "C":
            return "0QqOoöÖóÓőŐDcC⎔⌕⥀".random();
            break;
        case "W": case "w": case "v": case "V": case "A": case "y": case "Y": case "M": case "m": case "n": case "N":
            return "EWwVvAyYMmNn><".random();
            break;
        case "E": case "3": case "e": case "a": case "é":
            return "E3WXəΞΣεʒ".random();
        case "S": case "s": case "5": case "2": case "z": case "Z": case "$":
            return "Ss52zZ".random();
            break;
        case "T": case "F": case "t": case "f":
            return "TtFf".random();
            break;
        case "I": case "i": case "j": case "J": case "1": case "7": case "Í": case "í":
            return "IiJj17Íí⍳".random();
            break;
        case "H": case "h": case "K": case "k": case "X": case "x": case "R":
            return "HhKkkXxRπ".random();
            break;
        case "P": case "p": case "b": case "B": case "8": case "9": case "6": case "q":
            return "PpBb986".random();
            break;
        case "-": case "_":
            return "-_".random();
            break;
        default:
            return CHARS.random();
    }
}

Start();