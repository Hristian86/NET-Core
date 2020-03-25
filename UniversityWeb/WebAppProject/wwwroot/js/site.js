

function hello() {
    var xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            console.log('success');
           // alert(this.responseText);
            document.getElementById('Output').innerHTML = this.responseText;
        }



    }
    xhr.open("Get", "/Movies/Index",true);
    xhr.send();
}