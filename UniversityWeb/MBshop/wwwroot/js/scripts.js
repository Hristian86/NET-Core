var hide = document.getElementById("hideMessage");

var messageLenght = document.getElementById("message");
var x = "" + messageLenght.innerText;

if (x.length > 0) {

    window.onload = function () {
            this.messageLenght.style.height = '60px';
            this.messageLenght.style.paddingTop = '10px';
            $("#hideMessage").show().delay(2500).fadeOut();
            //hide.style.display = 'none';
    }
    x.innerText = "";
}