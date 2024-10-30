var UIBlocker = function () {
	this.showUIBlocker = function (msg) {
		$.blockUI({
			message: msg
		});
	}

	this.hideUIBlocker = function () {
		$.unblockUI();
	}
}

var uiBlocker = new UIBlocker();

var CommonUtil = function () {

	this.AppRootDir = null;
	
	this.SetCookie = function (name, value, days) {
		var expires = "";
		if (days) {
			var date = new Date();
			date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
			expires = "; expires=" + date.toUTCString();
		}
		document.cookie = name + "=" + (value || "") + expires + "; path=/";
	};


	this.GetCookie = function (name) {
		var nameEQ = name + "=";
		var ca = document.cookie.split(';');
		for (var i = 0; i < ca.length; i++) {
			var c = ca[i];
			while (c.charAt(0) == ' ') c = c.substring(1, c.length);
			if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
		}
		return null;
	};

	function EraseCookie(name) {
		document.cookie = name + '=; Path=/; Expires=Thu, 01 Jan 1970 00:00:01 GMT;';
	}

	this.ValidateRequiredInput = function (inputField) {
		var value = $(inputField).val();
		value = $.trim(value);
		if (value === null || value === '' || value === undefined) {
			$(inputField).focus();
			return false;
		}

		return true;
	}

	this.ValidateNumberInputRange = function (inputField) {
		var depth = parseInt($(inputField).val());
		var min = $(id).attr("min");
		var max = $(id).attr("max");

		if (depth < min || depth > max) {
			$(inputField).focus();
			return false;
		}
		return true;
	}

	this.HandleResponseError = function (data) {
		if (data.Success === true) {
			return false;
		}

		if (data.RedirectToLogin === true) {
			window.location = this.AppRootDir + "/Account/Login";
		} else {
			bootbox.alert(data.Message);
		}
	}

	this.HandleGridviewResponseError = function (data) {
		if (data.Success === true) {
			return false;
		}

		if (data.RedirectToLogin === true) {
			window.location = this.AppRootDir + "/Account/Login";
		}
	}

	this.HandleDeleteResponseError = function (data) {
		if (data.Success === true) {
			bootbox.alert(data.Message);
			return false;
		}

		if (data.RedirectToLogin === true) {
			window.location = this.AppRootDir + "/Account/Login";
		} else {
			bootbox.alert(data.Message);
		}
	}
}

var commonUtil = new CommonUtil();

var BaseDlg = function (name) {
	this.name = name;

	this.GetName = function () {
		return name;
	};

	this.GetDivID = function () {
		return name + 'Holder';
	};

	this.AddDiv = function () {
		$("<div id='" + name + 'Holder' + "'></div>").appendTo('#DialogContainer');
	}

	this.RemoveDiv = function () {
		$('#' + name + 'Holder').remove();
	};

	this.GetContentID = function () {
		return name + 'Content';
	};

	this.SetContentID = function () {
		var contentDiv = $('#' + name + 'Holder .modal-content');
		contentDiv.attr('id', name + 'Content');
	};
}

CommonUtil.ValidateForTextInputRequired = function (id, trim) {
	// trim set by default to true
	if (trim === undefined) {
		trim = true;
	}

	var value = $(id).val();
	if (trim === true) {
		value = $.trim(value);
	}

	if (value === '') {
		$(id).focus();
		return false;
	}
	return true;
};