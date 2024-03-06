const express = require('express');
const http = require("http");
const path = require("path");
const socketI0 = require("socket.io");

const app = express(); 

const PORT = process.env.PORT || 3000;
const server = http.createServer(app); // http server 
const chatServer = socketI0(server); // socket for server 

// set the public or static folder for app 
app.use(express.static(path.join(__dirname, 'public')));

// connection event handler 
chatServer.on('connection', socket =>{
     console.log("user is connected");
     socket.on('disconnect', () => {
        console.log('user disconnect');
    });
    // chat event handler 
    socket.on('chatEvent', chatMessage => {
        socket.emit('chatEvent', chatMessage);
    });

});

// running and listen server on  port 
server.listen(PORT, () => {
    console.log(`server is running on port ${PORT}`);
})