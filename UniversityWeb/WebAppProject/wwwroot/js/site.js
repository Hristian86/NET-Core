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

        'Content': currentPerson,
    }

    document.getElementById('input').value = "";

    const jsonToSend = JSON.stringify(tobesendet);
    xhr.open("Post", "https://localhost:44342/api/Chat", true);
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.send(jsonToSend);
    chek = true;
}

function Start() {

    //reseting count when new load page is requested
    //for starting new compare with data lenght
    count = 0;
    this.console.log('zdrasti');
    setInterval(function () {
        GetMessages();
        console.log('nistaa')

    }, 1000);
}

var oldLenght = 0;
var ids = 0;

function GetMessages() {

    var xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            console.log('success');
            if (this.responseText != null) {

                var obj = {};

                //geting responce from server and parsing to object
                obj = JSON.parse(this.responseText);
                
                var counter = obj.length;
                console.log(obj);


                if (counter != oldLenght) {

                    //when somthing is changed 
                    //reloading all messages
                    document.getElementById('Output').innerHTML = "";

                    //chek for changes with saving 
                    //old object lenght and compare to current
                    oldLenght = counter;

                    //for (var prop = 0; prop < obj.length; prop++)
                    var counters = obj.length;
                    for (var prop = counters-1; counter > 0; prop--)
                    {
                        
                        //date output
                        var today = new Date(obj[prop].dateT);
                        var addDate = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();

                        var y = obj[prop].userName;
                        var z = obj[prop].content;
                        var j = addDate;
                        var s = obj[prop].id;
                        ids = s;

                        //current loget user
                        var currUser = obj[prop].currentUser;

                        console.log(currUser);
                        console.log(y);

                        //chek for current user ho can delete 
                        //personal messages
                        if (y == currUser) {
                            var frost = `<p>Delete message</p><p>
<input type="submit" onclick="Delete(${s})" value="Delete" class="btn btn-danger" />
</p>`;
                            //changing background collor for current loged user
                            var curUserFont = `background-color:rgba(223, 229, 121, 0.9);`;
                          
                        } else {
                            //background collor for not current user
                            curUserFont = `background-color:#7c4e2a;color:white`;
                            frost = ``;
                        }
                        console.log
                        //parent where add child html
                        var Parent = document.getElementById('Output');

                        //add html for messages
                        var child = `<div class="message-candidate center-block" style="${curUserFont}">
        <div class="row">
            <div class="col-xs-8 col-md-6">
                <img src="" class="message-photo">
                <h4 class="message-name" id="messagename">${y} = ${s}</h4>
            </div>
            <div class="col-xs-4 col-md-6 text-right message-date" id="AddDel" >${j}${frost}</div>
        </div>
        <div class="row message-text">
            ${z}
        </div>
    </div>`;


                        //adding the messages in chatbox
                        let p = document.createElement("div")
                        p.innerHTML = child;
                        Parent.appendChild(p);

                        count++;

                    }
                }
            }
        }
    }

    var tobesendet = {
        'UserId': 'BLaaaaaaaaaaa',
        'Content': 'BLAaaaaaaaaaaaaaaaaaaaaaaaaaaaa'
    }

    const jsonToSend = JSON.stringify(tobesendet);
    xhr.open("Get", "https://localhost:44342/api/Chat", true);
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.send();

}


function Delete(e) {
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
        'Id' : e,
        'Content': currentPerson,
    }

    document.getElementById('input').value = "";

    const jsonToSend = JSON.stringify(tobesendet);
    xhr.open("Delete", "https://localhost:44342/api/Chat", true);
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.send(jsonToSend);
}