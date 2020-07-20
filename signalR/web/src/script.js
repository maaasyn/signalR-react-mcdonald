const sesjaMarcinPos = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJSb2xlIjpbIlBvcyJdLCJkZXZpY2UiOiJwb3MiLCJzZXNzaW9uSWQiOiJzZXNqYU1hcmNpbmthIiwibmJmIjoxNTczMDM1MDE0LCJleHAiOjE3MDU2MjcwMTQsImlhdCI6MTU3MzAzNTAxNH0.cebPMXZPiaLl_75Qe_nYJ5MgOTXPWfSaX8PqAq8p4Z0"
const sesjaMarcinDisplay = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJSb2xlIjpbIkRpc3BsYXkiXSwiZGV2aWNlIjoiZGlzcGxheSIsInNlc3Npb25JZCI6InNlc2phTWFyY2lua2EiLCJuYmYiOjE1NzMwMzUwMTQsImV4cCI6MTcwNTYyNzAxNCwiaWF0IjoxNTczMDM1MDE0fQ.LmWOA4wvJds8M5sMQlIkBb2h7Qq2-DDhCWZ-uhftqGU"
const sesjaDomcialaPos = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJSb2xlIjpbIlBvcyJdLCJkZXZpY2UiOiJwb3MiLCJzZXNzaW9uSWQiOiJzZXNqYURvbWNpYWxhIiwibmJmIjoxNTczMDM1MDE0LCJleHAiOjE3MDU2MjcwMTQsImlhdCI6MTU3MzAzNTAxNH0.qlaIW8xGWyTS_AzpSSPZhXEzBJgz9rB337Km6mgZemY"

//Connections
const connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:8080/chatHub", 
    // .withUrl("https://localhost:8080/chatHub", coffeeHub
    {
        accessTokenFactory: () => inputToken.value
    })
    .configureLogging(signalR.LogLevel.Information)
    .build();


//Buttons
buttonConnectWithHub = document.getElementById('buttonConnectWithHub');
buttonNewOrder = document.getElementById('buttonNewOrder');

//Text input
inputToken = document.getElementById('inputToken');
inputNewOrder = document.getElementById('inputNewOrder');

//P
ordersDisplay = document.getElementById('ordersDisplay');

//EventListenery
buttonConnectWithHub.addEventListener('click', (x) => {
    connection.start();
})

buttonNewOrder.addEventListener('click', (x) => {
    connection.invoke("NewOrderSubmitter", inputNewOrder.value);
})


//SignalR 

connection.on("wpiete", (serverData) => {
    console.log(`wpiete. Info z serwera: ${serverData}`);
})

connection.on("MessageReceiver", (data) => {
    console.log(data);
})



messageInput = document.getElementById('message');
buttonMessageToMyself = document.getElementById('btnMsgToMslf');
buttonObjectJsonSender = document.getElementById('btnObjSendJson');

objekt = {
    name: "stefan",
    surname: "kowalski"
}

buttonMessageToMyself.addEventListener('click', (x) => {
    connection.invoke("NewOrderSubmitter", messageInput.value);
})


buttonObjectJsonSender.addEventListener('click', (x) => {
    connection.invoke("PersonSended", objekt),
        console.log(objekt);
})


// //coffee hub

// const connections = new signalR.HubConnectionBuilder()
//     .withUrl("https://localhost:5001/coffeeHub",
//     {
//         accessTokenFactory: () => "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjpbIlBpZXMiLCJLb3QiXSwiZGV2aWNlIjoicG9zIiwic2Vzc2lvbklkIjoibWFyY2luZWsiLCJuYmYiOjE1NzMwMzUwMTQsImV4cCI6MTcwNTYyNzAxNCwiaWF0IjoxNTczMDM1MDE0fQ.LXYp68qz-jWv2oCcheYYDWyRqmUAG1KZioLCxWRPVWk"
//     })
//     .configureLogging(signalR.LogLevel.Information)
//     .build();

// connections.start();

// connections.on("wpiete", () => {
//     console.log("wpialem sie do kawy");
// })

// const connection = new signalR.HubConnectionBuilder()
//     .withUrl("https://localhost:5001/chatHub", 
//     {
//         accessTokenFactory: () => "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJSb2xlIjpbIlBvcyJdLCJkZXZpY2UiOiJwb3MiLCJzZXNzaW9uSWQiOiJzZXNqYU1hcmNpbmthIiwibmJmIjoxNTczMDM1MDE0LCJleHAiOjE3MDU2MjcwMTQsImlhdCI6MTU3MzAzNTAxNH0.cebPMXZPiaLl_75Qe_nYJ5MgOTXPWfSaX8PqAq8p4Z0"
//     })
//     .configureLogging(signalR.LogLevel.Information)
//     .build();

//connection.start();
