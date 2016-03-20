var http = require('http');
var url = require('url');
var fs = require('fs');
var sys = require('sys');
var io = require('socket.io').listen(9898);
var conSockets = new Array();
var socketRoom = new Array();
var rooms = new Array();
rooms['white']=new Array();
rooms['black']=new Array();
		
send404 = function(res){
	res.writeHead('404');
	res.write('404');
	res.end();
};

server = http.createServer(function(req, res){
	// your normal server code
	var path = url.parse(req.url).pathname;
	switch (path){
		case '/':
			res.writeHead(200, {'Content-Type': 'text/html'});
			res.write('<h1>Welcome. <a href="/index.html">Start checkers with black</a></h1>');
			res.write('<h1>Welcome. <a href="/white_index.html">Start checkers with white</a></h1>');
			res.end();
			break;
			
		default:
			
				try {
					var swf = path.substr(-4) == '.swf';
					res.writeHead(200, {'Content-Type': swf ? 'application/x-shockwave-flash' : ('text/' + (path.substr(-3) == '.js' ? 'javascript' : 'html'))});
					res.write(fs.readFileSync(__dirname + path, swf ? 'binary' : 'utf8'), swf ? 'binary' : 'utf8');
					res.end();
				} catch(e){ 
					send404(res); 
				}				
				break;
	
			send404(res);
			break;
	}
});

server.listen(9897, 'localhost');
json = JSON.stringify;
// Навешиваем обработчик на подключение нового клиента
io.on('connection', function (socket) {
	console.log('client connected ' + socket.id);
	socket.broadcast.json.send({ announcement: socket.sessionId + ' connected' });
    // Посылаем клиенту сообщение о том, что он успешно подключился и его имя
    // Посылаем всем остальным пользователям, что подключился новый клиент и его имя
    // Навешиваем обработчик на входящее сообщение
    socket.on('message', function (msg) {
    	if(msg.n==-1 && msg.o==-1)
    	{
    		if(rooms[msg.side][msg.game_id]==undefined)
    		{
    			rooms[msg.side][msg.game_id]=socket.id;
    			conSockets[socket.id]=socket;
    			socketRoom[socket.id]=msg.game_id;
				console.log(socket.id + ' entered in "' + msg.game_id + '" as ' + msg.side);
				if(msg.side=='white')
				{
					if(rooms['black'][msg.game_id]!=undefined)
					{
						socket.send({ 'startgame': [socket.sessionId, msg] });
						if(conSockets[rooms['black'][msg.game_id]]!=undefined)
						conSockets[rooms['black'][msg.game_id]].send({ 'startgame': [socket.sessionId, msg] });
					}
				}
				if(msg.side=='black')
				{
					if(rooms['white'][msg.game_id]!=undefined)
					{
						socket.send({ 'startgame': [socket.sessionId, msg] });
						if(conSockets[rooms['white'][msg.game_id]]!=undefined)
						conSockets[rooms['white'][msg.game_id]].send({ 'startgame': [socket.sessionId, msg] });
					}
				}
			}
			else
			{
				socket.send({ 'dissallow': [socket.sessionId, msg] });
			}
    	}
    	else
    	{
    		if(rooms[msg.side][msg.game_id]==socket.id)
    		{
				console.log(msg);
				console.log('message received');
				var msg2 = { 'message': [socket.sessionId, msg] };
				socket.broadcast.send({ 'message': [socket.sessionId, msg] });
			}
			else
				socket.send({ 'onrestart': [socket.sessionId, msg] });
		}
    });
    // При отключении клиента - уведомляем остальных
    socket.on('disconnect', function() {
        console.log(socket.id + ' disconnected');
        if(conSockets[socket.id]!=undefined)
        {
        	conSockets[socket.id]=undefined;

        	if(rooms['white'][socketRoom[socket.id]]==socket.id)
        	{
        		if(conSockets[rooms['black'][socketRoom[socket.id]]]!=undefined)
        		conSockets[rooms['black'][socketRoom[socket.id]]].send({ 'endgame': 1 });
        	}
        	if(rooms['black'][socketRoom[socket.id]]==socket.id)
        	{
        		if(conSockets[rooms['white'][socketRoom[socket.id]]]!=undefined)
        		conSockets[rooms['white'][socketRoom[socket.id]]].send({ 'endgame': 1 });
        	}
        	socket.send({ 'endgame': 1 });
        	if(rooms['white'][socketRoom[socket.id]]!=undefined)
        	{
        		console.log('Deleted white from "' + socketRoom[socket.id] + '" room');
        		rooms['white'][socketRoom[socket.id]]=undefined;
        	}
        	if(rooms['black'][socketRoom[socket.id]]!=undefined)
        	{
        		console.log('Deleted black from "' + socketRoom[socket.id] + '" room');
        		rooms['black'][socketRoom[socket.id]]=undefined;
        	}
        	socketRoom[socket.id]=undefined;

        }
		socket.broadcast.send({ 'announcement': socket.id + ' disconnected' });
    });
});
