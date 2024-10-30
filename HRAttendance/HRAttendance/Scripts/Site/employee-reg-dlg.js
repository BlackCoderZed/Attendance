var EmployeeRegDlg = function () {
    var baseDlg = new BaseDlg('EmployeeRegDlg');
    var IsUpdateDlg = false;
    var ActiveTabId = null;
    this.Show = function (param) {
        IsUpdateDlg = param.isUpdate;

        togo = commonUtil.AppRootDir + "/Employee/Register";
        uiBlocker.showUIBlocker("Loading...");

        $.ajax({
            url: togo,
            type: 'GET',
            data: param,
            success: function (response) {
                uiBlocker.hideUIBlocker();
                if (commonUtil.HandleResponseError(response) === false) {
                    baseDlg.AddDiv();
                    $($('#' + baseDlg.GetDivID())).html(response.View);

                    Initialize();
                    $('#EmployeeRegisterDlg').modal('show');
                }
            },
            error: function () {
                uiBlocker.hideUIBlocker();
            }
        });
    }

    var Initialize = function () {
        ActiveTabId = $('#employeeTab .nav-link.active').attr('id');

        DisableInput();



        $('button[data-bs-toggle="tab"]').on('shown.bs.tab', function (event) {
            // Get the ID of the currently active tab
            ActiveTabId = $(event.target).attr('id');
            let activeTabText = $(event.target).text();
        });

        $('#EmployeeRegisterDlg').on('hidden.bs.modal', function () {
            baseDlg.RemoveDiv();

            employeeListView.Reload();
        });

        $('#EmployeeRegisterDlg #UserID').blur(function () {
            // check user id is available -> if !Update
        });

        $('#EmployeeRegisterDlg #btnEmpRegister').on('click', function () {
            CreateOrUpdate();
        });

        $('#EmployeeRegisterDlg #btnReset').on('click', function () {
            bootbox.alert('Comming soon...');
        });
    }

    var DisableInput = function () {
        if (IsUpdateDlg) {
            $('#EmployeeRegisterDlg #UserID').prop('readonly', true);
            $('#EmployeeRegisterDlg #Password').prop('readonly', true);
            $('#EmployeeRegisterDlg #Password').hide();
        } else {
            $('#EmployeeRegisterDlg #Gender_Male').prop('checked', true);
        }
    }

    //var ChangeSelections = function () {
    //    if (!IsUpdateDlg) {
    //        return;
    //    }

    //    if ($('#EmployeeRegisterDlg #Gender').val() === "0") {
    //        $('#EmployeeRegisterDlg #Gender_Male').prop('checked', true);
    //    } else {
    //        $('#EmployeeRegisterDlg #Gender_Female').prop('checked', true);
    //    }
    //}

    var CreateOrUpdate = function () {
        if (ValidateInput() === false) {
            return;
        }

        var model = CreateModel();
        togo = commonUtil.AppRootDir + "/Employee/Register";
        uiBlocker.showUIBlocker("Loading...");

        $.ajax({
            url: togo,
            type: 'POST',
            contentType: "application/json; charset=utf8",
            data: JSON.stringify(model),
            success: function (response) {
                if (commonUtil.HandleResponseError(response) === false) {
                    bootbox.alert(response.Message);
                    $('#EmployeeRegisterDlg').modal('hide');
                }

                uiBlocker.hideUIBlocker();

            },
            error: function () {
                uiBlocker.hideUIBlocker();
                bootbox.alert('error');
            }
        });
    }

    var CreateModel = function () {
        var model = {
            ID: $('#EmployeeRegisterDlg #ID').val(),
            Name: $('#EmployeeRegisterDlg #Name').val(),
            NRC: $('#EmployeeRegisterDlg #NRC').val(),
            Phone: $('#EmployeeRegisterDlg #Phone').val(),
            Email: $('#EmployeeRegisterDlg #Email').val(),
            Address: $('#EmployeeRegisterDlg #Address').val(),
            UserID: $('#EmployeeRegisterDlg #UserID').val(),
            Password: $('#EmployeeRegisterDlg #Password').val(),
            Gender: $("input[name='Gender']:checked").val(),
            IsUpdate: $('#EmployeeRegisterDlg #IsUpdate').val(),
            UserIDVal: $('#EmployeeRegisterDlg #UserIDVal').val(),
            IsManagementPermission: $('#EmployeeRegisterDlg #IsManagementPermission').prop('checked')
        }

        return model;
    }

    var ValidateInput = function () {
        var showAccountMessage = false;
        var showGeneralMessage = false;
        var accountMessage = 'Please fills the account tab';
        var generalMessage = 'Please fills the general tab';
        var isValid = false;

        // active tab
        if (ActiveTabId === 'general-tab') {
            showAccountMessage = true;

            if (ValidateGeneralTab(showGeneralMessage, generalMessage) === false) {
                return false;
            }

            if (ValidateAccountTab(showAccountMessage, accountMessage) === false) {
                return false;
            }
        }

        if (ActiveTabId === 'account-tab') {
            showGeneralMessage = true;

            if (ValidateAccountTab(showAccountMessage, accountMessage) === false) {
                return false;
            }

            if (ValidateGeneralTab(showGeneralMessage, generalMessage) === false) {
                return false;
            }
        }

    }

    var ValidateGeneralTab = function (show, message) {
        if (!commonUtil.ValidateRequiredInput('#EmployeeRegisterDlg #Name')) {
            ShowValidationMessage(show, message);
            return false;
        }

        if (!commonUtil.ValidateRequiredInput('#EmployeeRegisterDlg #NRC')) {
            ShowValidationMessage(show, message);
            return false;
        }

        if (!commonUtil.ValidateRequiredInput('#EmployeeRegisterDlg #Phone')) {
            ShowValidationMessage(show, message);
            return false;
        }

        if (!commonUtil.ValidateRequiredInput('#EmployeeRegisterDlg #Email')) {
            ShowValidationMessage(show, message);
            return false;
        }

        if (!commonUtil.ValidateRequiredInput('#EmployeeRegisterDlg #Address')) {
            ShowValidationMessage(show, message);
            return false;
        }
    }

    var ValidateAccountTab = function (show, message) {
        if (!commonUtil.ValidateRequiredInput('#EmployeeRegisterDlg #UserID')) {
            ShowValidationMessage(show, message);
            return false;
        }

        if (IsUpdateDlg === true) {
            return true;
        }

        if (!commonUtil.ValidateRequiredInput('#EmployeeRegisterDlg #Password')) {
            ShowValidationMessage(show, message);
            return false;
        }
    }

    var ShowValidationMessage = function (isShow, message) {
        if (isShow) {
            bootbox.alert(message);
        }
    }
}

var employeeRegDlg = new EmployeeRegDlg();