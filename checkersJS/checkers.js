function Checkers(board, side, game_id) {

  var self = this; // reference helper  
  // events constants
  this.EVENT_DATA_RECEIVED = "dataReceivedEvent";
  this.BOARD_SIZE = 8;
  this.TURNS = ["white", "black"];

  this.$board = $("#" + board); // game board
  this.currentChecker = null;
  this.currentTurn = self.TURNS[0];

  // pub/sub utils
  var PubSubUtils = {
    subs: [],

    subscribe: function (event, action) {
      if (this.subs[event] == undefined) {
        this.subs[event] = [];
      }
      this.subs[event].push(action);
    },

    publish: function (event, params) {
      var self = this;
      $.each(this.subs[event], function (index, action) {
        self.subs[event][index](params);
      });
    }
  };

  // socket utils
  var SocketUtils = {
    socket: null,

    connect: function () {
      //io.setPath('/client/');
      //socket = new io.Socket('localhost', {
        //rememberTransport: false,
       // port: 9898
      //});
	  socket = io.connect("http://localhost:9898");
      socket.on('message', function (data) {
        var obj = data;
        PubSubUtils.publish(self.EVENT_DATA_RECEIVED, obj);
      });
    },

    send: function (message) {
      message.side = side;
      message.game_id = game_id;
      socket.send(message);
    }
  };

  // game logic

  var game = {  
    moves: { 'br': { 'x': 1, 'y': 1 },
        'bl': { 'x': -1, 'y': 1 },
        'tr': { 'x': 1, 'y': -1 },
        'tl': { 'x': -1, 'y': -1 }
    },

    // checks if jump over move is available 
    // returns position of the checker to remove
    getJumpOverMove: function(startPos, endPos) {
      if (Math.abs(startPos.x - endPos.x) == 2) {
        if (startPos.x > endPos.x && startPos.y > endPos.y) {
          return this.calculatePosition(this.moves.br, 1, endPos);
        }
        else if (startPos.x < endPos.x && startPos.y > endPos.y) {
          return this.calculatePosition(this.moves.bl, 1, endPos);          
        }
        else if (startPos.x < endPos.x && startPos.y < endPos.y) {
          return this.calculatePosition(this.moves.tl, 1, endPos); 
        }
        else if (startPos.x > endPos.x && startPos.y < endPos.y) {
          return this.calculatePosition(this.moves.tr, 1, endPos);
        }
      }
      return null;
    },

    // TODO: add queen
    getPossibleMoves: function (checker) {
      var m = [];
      //alert(side);
      if (checker.isBlack() && side=='black') { 
        m = this.getPossiblePosition(this.moves.br, m, checker);
        m = this.getPossiblePosition(this.moves.tr, m, checker);
    		if(checker.isQueen()){
      		m = this.getPossiblePosition(this.moves.tl, m, checker);
      		m = this.getPossiblePosition(this.moves.bl, m, checker);
    		}
      }
      if(checker.isWhite() && side=='white'){  
        m = this.getPossiblePosition(this.moves.tl, m, checker);
        m = this.getPossiblePosition(this.moves.bl, m, checker);
    		if(checker.isQueen()){
    			m = this.getPossiblePosition(this.moves.br, m, checker);
    			m = this.getPossiblePosition(this.moves.tr, m, checker);
    		}
      }
      return m;
    },

    getPossiblePosition: function(move, moves, checker) {
      var newPos = this.calculatePosition(move, 1, checker.getPosition());
      var s = getSquare(newPos);
      if (s != undefined) { 
        if (!s.hasChecker()) {
          moves.push(newPos);
        }
        else {
          // make sure checker has different color
          if(checker.isWhite() != s.checker.isWhite()) {
            moves.push(this.calculatePosition(move, 2, checker.getPosition()));
          }          
        }    
      }
      return moves;
    },

    // calculates new position
    calculatePosition: function(move, step, position) {
      return {
        "x": move.x * step + position.x, 
        "y": move.y * step + position.y };
    },

    // shows possible moves
    showPossibleMoves: function($checker) {
      var square = $checker.data("checker").square;
      var moves = game.getPossibleMoves(square.checker);
      
      $.each(moves, function (i, v) {
        var s = getSquare(v);
        if (s != null && !s.hasChecker()) {
          s.highlight();    
        }
      });
    },
    
    // sets next turn
    setNextTurn: function() {
      self.currentTurn = (self.currentTurn == self.TURNS[0]) ? self.TURNS[1] : self.TURNS[0];
    }
  };

  // Square constructor
  function Square(x, y) {
    var self = this;

    this.COLORS = {
      "true": "square_black",
      "false": "square_white"
    };

    this.id = "square_" + x + "_" + y;
    this.color = self.COLORS[x % 2 === y % 2];
    this.x = x;
    this.y = y;

    this.position = {
      "x": self.x,
      "y": self.y
    };

    this.checker = null; // checker

    // set jquery object
    this.$square = $('<div id="' + self.id + '" class="square ' + self.color + '"> </div>')
      .data("square", self);

    // checks if square is empty
    this.hasChecker = function () {
      return self.checker != null;
    }

    // removes checker from the square
    this.removeChecker = function () {
      self.checker = null;
    }

    // associates checker with the square
    this.hasOne = function (checker) {
      self.checker = checker;
    }

    this.highlight = function () {
      self.$square.addClass('square_highlighted');
    }

    this.unHighlight = function () {
      self.$square.removeClass('square_highlighted');
    }

    this.isHighlighted = function () {
      return self.$square.hasClass('square_highlighted');
    }

    this.getPosition = function () {
      return self.position;
    }
  }

  // Checker constructor

  function Checker(x, y) {
    var self = this;

    this.COLORS = {
      "true": "black",
      "false": "white"
    };

    // TODO fix it
    this.color = self.COLORS[x < 3];
	this.queen = "not_queen";
    this.square = null; // square object the checker belongs to
    // create jquery object
    this.$checker = $('<div class="checker ' + self.color + '"></div>').data("checker", self)

    // public
    this.belongsTo = function (square) {
      self.square = square;
      // wires up jquery object
      self.$checker.appendTo(square.$square);
    }

    this.isBlack = function () {
      return self.color == "black";
    }
	this.isQueen = function() {
		return self.queen == "queen";
	}
    this.isWhite = function () {
      return self.color == "white";
    }

    this.isActiveTurn = function (turn) {
      return self.color == turn;
    }

    this.getPosition = function () {
      return self.square.getPosition();
    }

    // checkes if selected checker can move
    this.isMovePossible = function (currentChecker, currentTurn) {
      if (self.isActiveTurn(currentTurn) 
        && (currentChecker == null 
        || currentChecker != self)) {
        return true;
      }
      return false;
    }

    // makes move
    this.moveTo = function (square) {
		if(self.isBlack()){
			if(square.x == 7){
				self.queen = "queen";
				//alert('black queen appears');
				//self.$checker = $('<div class="checker ' + self.color + '"></div>').data("checker", self)
				self.$checker.addClass("queen");
			}
		}
		if(self.isWhite()){
			if(square.x == 0){
				self.queen = "queen";
				//alert('white queen appears');
				self.$checker.addClass("queen");
			}
		}
      self.x = square.x;
      self.y = square.y;
      // remove checker from current square
      self.square.removeChecker();
      // set link between square and checker
      self.belongsTo(square);
      square.hasOne(self);
      //
    }
  }

  // private

  // draws game grid
  function drawGrid() {
    for (var i = 0; i < self.BOARD_SIZE; i++) {
      for (var j = 0; j < self.BOARD_SIZE; j++) {
        var square = new Square(i, j);
        square.$square.appendTo(self.$board);
        drawChecker(square);
      }
    }
  }

  // draws checker on the grid
  function drawChecker(square) {
    if (square.y % 2 === square.x % 2 && (square.x < 3 || square.x > 4)) {
      var checker = new Checker(square.x, square.y);
      // wired up checker with square
      checker.belongsTo(square);
      square.hasOne(checker);
    }
  }

  // callback called when client receives message from server
  var processDataCallback = function (obj) {
    if (obj != undefined && obj.message != undefined) {
      if(obj.message[1].game_id!=game_id)
        return;

      var square = getSquare(obj.message[1].n);
      var checker = getChecker(obj.message[1].o);
	  
	  var p = game.getJumpOverMove(checker.getPosition(), square.getPosition());
      
      if (p != null) {
          // square with checker to remove
          var s = getSquare(p);
          s.checker.$checker.remove();
          s.removeChecker();
      }
	  
      checker.moveTo(square);
      //self.setNextTurn();
	  self.currentTurn = (self.currentTurn == self.TURNS[0]) ? self.TURNS[1] : self.TURNS[0];
    }
    if (obj != undefined && obj.dissallow != undefined) {
        alert('This color was occupied by another player');
        location="http://localhost:9897";

    }
    if (obj != undefined && obj.onrestart != undefined) {
        alert('This room occupied by another players, or room has been deleted');
        location="http://localhost:9897";
    }
    if (obj != undefined && obj.endgame != undefined) {
        alert('Player forfeited the game');
        location="http://localhost:9897";
    }
    if (obj != undefined && obj.startgame != undefined){
        drawGrid();
    }
  }

  // register events

  // TODO use publish / subcribe
  $('.checker').live("mouseover", function () {
    var checker = $(this).data("checker");
    if (self.currentChecker == null 
      && checker.isActiveTurn(self.currentTurn)) {
      game.showPossibleMoves($(this));
    }
  });

  $('.checker').live("mouseout", function () {
    if (self.currentChecker == null) {
      $('.square').removeClass('square_highlighted');
    }
  });

  $('.checker').live("click", function (e) {
    var checker = $(this).data("checker");
    if (checker.isMovePossible(self.currentChecker, self.currentTurn)) {
      $('.square').removeClass('square_highlighted');
      if((checker.isWhite() && side=='white') || (checker.isBlack() && side=='black'))
        checker.square.highlight();
      game.showPossibleMoves($(this));
      self.currentChecker = checker;
    }
    else {
      self.currentChecker = null;
      checker.square.unHighlight();
    }
    return false; // stop bubbling
  });

  $('.square').live("click", function () {
    var square = $(this).data('square');

    // TODO use pub/sub for all things here

    if (self.currentChecker != null && square.isHighlighted()) {
      // message
      var m = {
        'n': square.getPosition(),
        'o': self.currentChecker.getPosition()
      };

      if(m.n.x==m.o.x && m.n.y==m.o.y)
        return;

      var p = game.getJumpOverMove(self.currentChecker.getPosition(), square.getPosition());
      
      if (p != null) {
          // square with checker to remove
          var s = getSquare(p);
          s.checker.$checker.remove();
          s.removeChecker();
      }

      SocketUtils.send(m);
    
      self.currentChecker.moveTo(square);
      game.setNextTurn();
      self.currentChecker = null;
      $('.square').removeClass('square_highlighted');
    }
  });

  // helpers

  function getSquare(position) {
    return $("#square_" + position.x + "_" + position.y).data('square');
  }

  function getChecker(position) {
    return getSquare(position).checker;
  }

  //drawGrid();

  // subscribe elements 
  PubSubUtils.subscribe(self.EVENT_DATA_RECEIVED, processDataCallback);
  SocketUtils.connect();
  var mTemp = {
        'n': -1,
        'o': -1
      };
  SocketUtils.send(mTemp);
}

Checkers.init = function (board, side, game_id) {
  //alert(game_id);
  window.checkers = new Checkers(board, side, game_id);
}
