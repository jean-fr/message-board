var app = {
	init: function() {
		app.bindEvents();
		app.loadMessages();
	},
	bindEvents: function() {
		$(document).on('click', '#send-btn', app.sendMessage);
	},
	sendMessage: function(ev) {
		ev.preventDefault();

		var messageText = $('#message-field').val();
		if (!messageText || (messageText && $.trim(messageText) === '')) {
			alert('enter message first');
			return;
		}

		$.ajax({
			url: '/api/message/send',
			data: JSON.stringify({ Content: messageText }),
			type: 'POST',
			dataType: 'json',
			contentType: 'application/json; charset=utf-8'
		})
			.done(function(data) {
				if (data) {				 		 
					$('#message-list').append('<li> ' + data.Content + ' </li > ');
				}
			})
			.fail(function (error) {
				alert('Error: ' + error.responseText);
			});
	},
	loadMessages: function() {
		$.ajax({
			url: '/api/message/list',
			type: 'GET',
			dataType: 'json'
		})
			.done(function(data) {
				if (data) {
					var $list = $('#message-list');
					$list.remove('li');
					for (var i = 0; i < data.length; i++) {					 
						$list.append('<li> ' + data[i] + ' </li > ');
					}
				}
			})
			.fail(function() {
				alert('Error');
			});
	}
};

app.init();
