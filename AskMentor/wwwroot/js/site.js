"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connection.start().then(function () {
    console.log("connection success")
}).catch(function (err) {
    console.log("connection err:::", err)
    return;
});



