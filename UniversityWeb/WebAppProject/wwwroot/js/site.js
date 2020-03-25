var count = 0;

function PostMessages() {
    var xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            console.log('success');
            if (this.responseText != null) {
                //document.getElementById('Output').innerHTML = this.responseText;

            }
        }
    }
    //xhr.open("Get", "/Movies/Index",true);
    var currentPerson = document.getElementById('input').value;
    console.log(currentPerson);
    var tobesendet = {

        'Content': currentPerson
    }

    document.getElementById('input').value = "";

    const jsonToSend = JSON.stringify(tobesendet);
    xhr.open("Post", "https://localhost:44342/api/Chat", true);
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.send(jsonToSend);
    chek = true;
}

window.onload = function Start() {
    count = 0;
    this.console.log('zdrasti');
    setInterval(function () {
        GetMessages();
        console.log('nistaa')

    }, 3000);
}

function GetMessages() {

    var xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            console.log('success');
            if (this.responseText != null) {

                var obj = {};

                obj = JSON.parse(this.responseText);
                var counter = obj.length;
                for (var prop in obj) {

                    var y = obj[prop].userId;
                    var z = obj[prop].content;

                     if(count < counter) {
                        count++;
                         document.getElementById('Output').innerHTML += `<div class="message-candidate center-block">
        <div class="row">
            <div class="col-xs-8 col-md-6">
                <img src="" class="message-photo">
                <h4 class="message-name" id="messagename">${y}</h4>
            </div>
            <div class="col-xs-4 col-md-6 text-right message-date">Date here</div>
        </div>
        <div class="row message-text">
            ${z}
        </div>
    </div>`
                    }
                }
            }
        }
    }
    //xhr.open("Get", "/Movies/Index",true);

    var tobesendet = {
        'UserId': 'BLaaaaaaaaaaa',
        'Content': 'BLAaaaaaaaaaaaaaaaaaaaaaaaaaaaa'
    }

    const jsonToSend = JSON.stringify(tobesendet);
    xhr.open("Get", "https://localhost:44342/api/Chat", true);
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.send();

}

