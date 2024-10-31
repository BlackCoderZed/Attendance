var AttendanceView = function () {
    this.Initialize = function () {

        GetCheckInOut();

        GetAttendanceInfoList();

        $('#attendance #checkIn').on('change', function () {
            if ($(this).is(':checked')) {
                // check in
                SetCheckInOut(0);
            } else {
                GetCheckInOut();
            }

            GetAttendanceInfoList();
        });

        $('#attendance #checkOut').on('change', function () {
            if ($(this).is(':checked')) {
                // check out
                SetCheckInOut(1);
            } else {
                GetCheckInOut();
            }
            GetAttendanceInfoList();
        });
    }

    var SetCheckInOut = function (status) {
        var model = {
            CheckInOutStatus: status
        };

        togo = commonUtil.AppRootDir + "/Attendance/CheckInOut";
        uiBlocker.showUIBlocker("Loading...");

        $.ajax({
            url: togo,
            type: 'POST',
            contentType: "application/json; charset=utf8",
            data: JSON.stringify(model),
            success: function (response) {
                uiBlocker.hideUIBlocker();

                if (commonUtil.HandleResponseError(response) === false) {
                    //bootbox.alert(response.Message);
                } 

                GetCheckInOut();
            },
            error: function () {
                uiBlocker.hideUIBlocker();
                bootbox.alert('error');
            }
        });
    }

    var GetCheckInOut = function () {
        togo = commonUtil.AppRootDir + "/Attendance/GetCheckInOutInfo";
        uiBlocker.showUIBlocker("Loading...");

        $.ajax({
            url: togo,
            type: 'GET',
            success: function (response) {
                uiBlocker.hideUIBlocker();

                //if (commonUtil.HandleResponseError(response) === false) {
                //    bootbox.alert(response.Message);
                //}

                HandleCheckBoxOnOff(response.IsCheckIn, response.IsCheckOut);

            },
            error: function () {
                uiBlocker.hideUIBlocker();
                bootbox.alert('error');
            }
        });
    }

    var GetAttendanceInfoList = function () {
        togo = commonUtil.AppRootDir + "/Attendance/GetEmpAttendanceInfoList";
        uiBlocker.showUIBlocker("Loading...");

        $.ajax({
            url: togo,
            type: 'GET',
            success: function (response) {
                uiBlocker.hideUIBlocker();

                if (commonUtil.HandleGridviewResponseError(response) === false) {
                    ShowEmpAttendanceList(response.data);
                }
            },
            error: function () {
                uiBlocker.hideUIBlocker();
            }
        });
    }

    var ShowEmpAttendanceList = function (datas) {
        if (datas === null) {
            return;
        }

        $('#employee-attendance').html('');

        $.each(datas, function (index, data) {
            const card = `
                <div class="col-lg-6 col-md-6">
                    <div class="card mb-4  emp-att">
                        <div class="card-body">
                            <h6 class="card-title">Date : ${data.Date}</h6>
                            <hr>
                            <p class="card-text">Check In : ${data.CheckInTime}</p>
                            <p class="card-text">Check Out : ${data.CheckOutTime}</p>
                            <p class="card-text">Status : ${data.AttendType}</p>
                        </div>
                    </div>
                </div>
            `;
            // Append each card to the container
            $('#employee-attendance').append(card);
        });
    }

    var HandleCheckBoxOnOff = function (isCheckIn, isCheckOut) {
        $('#attendance #checkIn').prop('checked', isCheckIn);

        $('#attendance #checkOut').prop('checked', isCheckOut);

    }
}

var attendanceView = new AttendanceView();