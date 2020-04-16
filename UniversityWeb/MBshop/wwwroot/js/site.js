var count = 0;
var urlLink = "";


function PostMessages() {
    var xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            //console.log('success');
            if (this.responseText != null) {
                //document.getElementById('Output').innerHTML = this.responseText;
            }
        } else {
            //console.log('Error in post message');
        }
    }
    //xhr.open("Get", "/Movies/Index",true);
    var currentContent = document.getElementById('input').value;
    //console.log(currentContent);

    var tobesendet = {

        'Content': currentContent,
    }

    document.getElementById('input').value = "";

    const jsonToSend = JSON.stringify(tobesendet);

    
    xhr.open("Post", urlLink, true);
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.send(jsonToSend);
    chek = true;
}

function Start() {
    //setting the enviornment
    var urlLinks = document.getElementById("secretEnviornment");
    urlLink = urlLinks.getAttribute('value');


    //reseting count when the page is loadet on user requested -
    //and starting new compare with data lenght
    count = 0;
    //this.console.log('zdrasti');
    GetMessages();
    // Real time message sistem
    //setInterval(function () {
    //    GetMessages();
    //    //console.log('get responce from server')

    //}, 2000);
}

//saving previous lenght number for comparing with new messages from controller as data
var oldLenght = 0;
//DateTime variable for minutes when they are below 10 to concat a zeto before the method result
var DayTimeMinutes = 0;
//var ids = 0;

function GetMessages() {

    var xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            //console.log('success');
            if (this.responseText != null) {

                var obj = {};

                //geting responce from server and parsing it to a object
                obj = JSON.parse(this.responseText);

                var newLenght = obj.length;

                //console.log(obj);

                if (newLenght != oldLenght) {

                    //when somthing is changed 
                    //reloading all messages
                    document.getElementById('Output').innerHTML = "";

                    //chek for changes with saving 
                    //old object lenght and compare to current
                    oldLenght = newLenght;

                    //for (var prop = 0; prop < obj.length; prop++)
                    //var messagesLenght = obj.length;
                    for (var prop = newLenght - 1; newLenght > 0; prop--) {
                        //date output

                        var messageCreatedDate = new Date(obj[prop].dateT);

                        if (messageCreatedDate.getMinutes() == 0) {
                            DayTimeMinutes = '0' + '0';
                        } else if (messageCreatedDate.getMinutes() < 10) {
                            DayTimeMinutes = '0' + messageCreatedDate.getMinutes();
                        } else {
                            DayTimeMinutes = messageCreatedDate.getMinutes();
                        }

                        var addDate = messageCreatedDate.getDate() + '.' + (messageCreatedDate.getMonth() + 1) + '.' + messageCreatedDate.getFullYear() + ' ' + messageCreatedDate.getHours() + ':' + DayTimeMinutes;

                        var userName = obj[prop].userName;
                        var messageContent = obj[prop].content;
                        var deleteMessage = obj[prop].id;
                        var avatar = obj[prop].avatar;

                        //ids = s;
                        if (avatar == null || avatar.length < 1) {
                            avatar = 'https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcTYPN-S2G6m1RHbx5S4JjAGITpWCYYoBnMTa28lW3E-2aSMCHbD&usqp=CAU';
                        }
                        //current loget user
                        var currUser = obj[prop].currentUser;

                        //console.log(currUser);
                        //console.log(userName);
                        //console.log(avatar);

                        //chek for current user ho can delete 
                        //personal messages
                        if (userName == currUser) {
                            var deleteButton = `<p>Delete message</p><p>
<input type="submit" onclick="Delete(${deleteMessage})" value="Delete" class="btn btn-danger " />
</p>`;
                            //changing background collor for current loged user
                            var curUserFont = `background-color:rgba(0,255,255, 0.5);margin-right:35px;`;

                        } else {
                            //background collor for other people messages
                            curUserFont = `background-color:rgba(223, 229, 121, 0.9);margin-left:35px;`;
                            deleteButton = ``;
                        }

                        //parent where is addet the child html 
                        var Parent = document.getElementById('Output');

                        //set and add the html for messages
                        var child = `<div class="message-candidate center-block" style="${curUserFont}">
        <div class="row">
            <div class="col-xs-8 col-md-6">
                <img src="${avatar}" style="width: 70px;
    height: 70px;" class="message-photo">
                <h4 class="message-name" id="messagename">${userName}</h4>
            </div>
            <div class="col-xs-4 col-md-6 text-right message-date" id="AddDel" >${addDate}${deleteButton}</div>
        </div>
        <div class="row message-text">
            ${messageContent}
        </div>
    </div>`;


                        //adding the messages in chatbox
                        let wrapper = document.createElement("div")
                        //appending the child html
                        wrapper.innerHTML = child;

                        //appending in the parent html element
                        Parent.appendChild(wrapper);

                        count++;

                    }
                }
            } else {
                //console.log('Error from the responce');
            }
        }
    }


    

    xhr.open("Get", urlLink, true);
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.send();

}


function Delete(element) {
    var xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            //console.log('success');
            if (this.responseText != null) {
                //document.getElementById('Output').innerHTML = this.responseText;
            }
        } else {
            //console.log('Error when deleting message');
        }
    }

    //xhr.open("Get", "/Movies/Index",true);
    var currentContent = document.getElementById('input').value;
    console.log(currentContent);

    var tobesendet = {
        'Id': element,
        'Content': currentContent,
    }

    document.getElementById('input').value = "";

    const jsonToSend = JSON.stringify(tobesendet);

    
    xhr.open("Delete", urlLink, true);
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.send(jsonToSend);
}