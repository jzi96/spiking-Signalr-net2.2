"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/hubs/positions").build();

connection.on("OnPositionUpdated", function (position) {
    var root = document.getElementById('root');
    var id = 'g' + position.grid;
    var basePos = document.getElementById(id);
    if (!basePos) {
        basePos = document.createElement('div',);
        basePos.setAttribute('id', id)
        root.appendChild(basePos);
    }
    //remove everything
    while (basePos.firstChild) {
        basePos.removeChild(basePos.firstChild);
    }

    var head = document.createElement('div');
    head.setAttribute('class', 'head');
    basePos.innerText = position.grid;
    basePos.appendChild(head);
    var listD = document.createElement('div');

    var list = document.createElement('ul');
    var ar = position.values;
    for (var i = 0; i < ar.length; i++) {
        var item = ar[i];
        var val = document.createElement('li');
        val.innerText = item.key + ':' + item.value;
        list.appendChild(val);
    }
    listD.appendChild(list);
    basePos.appendChild(listD);
});

connection.start().then(function () {
}).catch(function (err) {
    return console.error(err.toString());
});