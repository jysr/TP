var http = require('http');
var url = require('url');
var fs = require('fs');
var io = require('socket.io');
var sys = require('sys');
		
send404 = function(res){
	res.writeHead(404);
	res.write('404');
	res.end();
};
		
var server = http.createServer(function(req, res){
	// your normal server code
	var path = url.parse(req.url).pathname;
	switch (path){
		case '/':
			res.writeHead(200, {'Content-Type': 'text/html'});
			res.write('<h1>Welcome. Try the <a href="/index.html">checkers</a> example.</h1>');
			res.end();
			break;
			
		default:
			if (/\.(js|html|swf)$/.test(path)){
				try {
					var swf = path.substr(-4) == '.swf';
					res.writeHead(200, {'Content-Type': swf ? 'application/x-shockwave-flash' : ('text/' + (path.substr(-3) == '.js' ? 'javascript' : 'html'))});
					res.write(fs.readFileSync(__dirname + path, swf ? 'binary' : 'utf8'), swf ? 'binary' : 'utf8');
					res.end();
				} catch(e){ 
					send404(res); 
				}				
				break;
			}
	
			send404(res);
			break;
	}
});

server.listen(9898, 'localhost');
		
var buffer = [], json = JSON.stringify;
console.log('start listening...');
io.listen(server, {
	onClientConnect: function(client){
		console.log('client connected');
		client.send(json({ buffer: buffer }));
		client.broadcast(json({ announcement: client.sessionId + ' connected' }));
	},
	
	onClientDisconnect: function(client){
		console.log('disconnected');
		client.broadcast(json({ announcement: client.sessionId + ' disconnected' }));
	},
	
	onClientMessage: function(message, client){
		console.log('test');
		var msg = { message: [client.sessionId, message] };
    //sys.puts(msg);
		client.broadcast(json(msg));
	}
	
});

