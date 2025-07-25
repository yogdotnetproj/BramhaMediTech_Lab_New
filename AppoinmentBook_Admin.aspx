 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="AppoinmentBook_Admin.aspx.cs" Inherits="AppoinmentBook_Admin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Appoinment Book Admin</title>  
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/fullcalendar.css" rel="stylesheet" />
    <link href="Content/jquery-ui.css" rel="stylesheet" />
    <link rel="stylesheet" href="Content/simple-calendar.css" />
    <style type="text/css">
        /*!
 * Timepicker Component for Twitter Bootstrap
 *
 * Copyright 2013 Joris de Wit
 *
 * Contributors https://github.com/jdewit/bootstrap-timepicker/graphs/contributors
 *
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 */
        .bootstrap-timepicker {
            position: relative;
        }

            .bootstrap-timepicker.pull-right .bootstrap-timepicker-widget.dropdown-menu {
                left: auto;
                right: 0;
            }

                .bootstrap-timepicker.pull-right .bootstrap-timepicker-widget.dropdown-menu:before {
                    left: auto;
                    right: 12px;
                }

                .bootstrap-timepicker.pull-right .bootstrap-timepicker-widget.dropdown-menu:after {
                    left: auto;
                    right: 13px;
                }

            .bootstrap-timepicker .input-group-addon {
                cursor: pointer;
            }

                .bootstrap-timepicker .input-group-addon i {
                    display: inline-block;
                    width: 16px;
                    height: 16px;
                }

        .bootstrap-timepicker-widget.dropdown-menu {
            padding: 4px;
        }

            .bootstrap-timepicker-widget.dropdown-menu.open {
                display: inline-block;
            }

            .bootstrap-timepicker-widget.dropdown-menu:before {
                border-bottom: 7px solid rgba(0, 0, 0, 0.2);
                border-left: 7px solid transparent;
                border-right: 7px solid transparent;
                content: "";
                display: inline-block;
                position: absolute;
            }

            .bootstrap-timepicker-widget.dropdown-menu:after {
                border-bottom: 6px solid #FFFFFF;
                border-left: 6px solid transparent;
                border-right: 6px solid transparent;
                content: "";
                display: inline-block;
                position: absolute;
            }

        .bootstrap-timepicker-widget.timepicker-orient-left:before {
            left: 6px;
        }

        .bootstrap-timepicker-widget.timepicker-orient-left:after {
            left: 7px;
        }

        .bootstrap-timepicker-widget.timepicker-orient-right:before {
            right: 6px;
        }

        .bootstrap-timepicker-widget.timepicker-orient-right:after {
            right: 7px;
        }

        .bootstrap-timepicker-widget.timepicker-orient-top:before {
            top: -7px;
        }

        .bootstrap-timepicker-widget.timepicker-orient-top:after {
            top: -6px;
        }

        .bootstrap-timepicker-widget.timepicker-orient-bottom:before {
            bottom: -7px;
            border-bottom: 0;
            border-top: 7px solid #999;
        }

        .bootstrap-timepicker-widget.timepicker-orient-bottom:after {
            bottom: -6px;
            border-bottom: 0;
            border-top: 6px solid #ffffff;
        }

        .bootstrap-timepicker-widget table {
            width: 100%;
            margin: 0;
        }

            .bootstrap-timepicker-widget table td {
                text-align: center;
                height: 30px;
                margin: 0;
                padding: 2px;
            }

                .bootstrap-timepicker-widget table td:not(.separator) {
                    min-width: 30px;
                }

                .bootstrap-timepicker-widget table td span {
                    width: 100%;
                }

                .bootstrap-timepicker-widget table td a {
                    border: 1px transparent solid;
                    width: 100%;
                    display: inline-block;
                    margin: 0;
                    padding: 8px 0;
                    outline: 0;
                    color: #333;
                }

                    .bootstrap-timepicker-widget table td a:hover {
                        text-decoration: none;
                        background-color: #eee;
                        -webkit-border-radius: 4px;
                        -moz-border-radius: 4px;
                        border-radius: 4px;
                        border-color: #ddd;
                    }

                    .bootstrap-timepicker-widget table td a i {
                        margin-top: 2px;
                        font-size: 18px;
                    }

                .bootstrap-timepicker-widget table td input {
                    width: 25px;
                    margin: 0;
                    text-align: center;
                    border: none;
                }

        .bootstrap-timepicker-widget .modal-content {
            padding: 4px;
        }

        @media (min-width: 767px) {
            .bootstrap-timepicker-widget.modal {
                width: 200px;
                margin-left: -100px;
            }
        }

        @media (max-width: 767px) {
            .bootstrap-timepicker {
                width: 100%;
            }

                .bootstrap-timepicker .dropdown-menu {
                    width: 100%;
                }
        }
    </style>

    <style type="text/css">
        .fc-event .fc-content {
            position: relative;
            z-index: 2;
            color: whitesmoke;
        }

        #loading {
            /*width: 600px;
            height: 550px;*/
            position: absolute;
            background-color: gray;
            color: white;
            text-align: center;
            vertical-align: middle;
            display: table-cell;
        }
    </style>

    <style>
        .eve {
            margin-right: 12px;
            margin-bottom: 8px;
        }
    </style>

    <style>
        #loading {
            background: #ffffff;
            color: #666666;
            position: fixed;
            height: 100%;
            width: 100%;
            z-index: 5000;
            top: 0;
            left: 0;
            float: left;
            text-align: center;
            padding-top: 25%;
            opacity: .80;
        }

        .spinner {
            margin: 0 auto;
            height: 64px;
            width: 64px;
            animation: rotate 0.8s infinite linear;
            border: 5px solid firebrick;
            border-right-color: transparent;
            border-radius: 50%;
        }

        @keyframes rotate {
            0% {
                transform: rotate(0deg);
            }

            100% {
                transform: rotate(360deg);
            }
        }
    </style>
</head>
<body data-spy="scroll" data-target="#myScrollspy" data-offset="20">
    <div class="container">
        <div class="row">
            <div class="col-md-4">
                <div class="row">
                    <div class="col-md-12">
                        <div class="card">
                            <h5 class="card-header text-white" style="background-color: #00AA9E;">Doctors</h5>
                            <div class="card-body">
                                <select id="doctorNames" class="form-control  selectpicker" data-show-subtext="true" data-live-search="true">
                                    <option value="0">--Select Doctor--</option>
                                </select>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="card">
                            <h5 class="card-header text-white" style="background-color: #00AA9E;">Availability</h5>
                            <div class="card-body">
                                <div id="event-cal-container" class="calendar-container" hidden="hidden"></div>
                                <div id="calendarMain"></div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-8">
                <div class="card">
                    <h5 class="card-header text-white" style="background-color: #00AA9E;">Appointment Scheduler</h5>
                    <div class="card-body">
                        <div id="loading" style="display: none;">
                            <div class="spinner"></div>
                            <br />
                            Loading...
                        </div>
                        <div id="calendarSub" style="display: none;"></div>
                        <div id="timeSlots" style="display: none;"></div>
                        <div id="myScrollspy">
                            <table class="table table-bordered" id="patientList">
                                <thead>
                                    <tr>
                                        <th>#</th>
                                        <th>Date</th>
                                        <th>Time</th>
                                        <th>Name</th>
                                        <th>Address</th>
                                        <th>Note</th>
                                        <th>User</th>
                                        <th>Actions</th>
                                    </tr>
                                </thead>
                                <tbody></tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Create new appointment</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-6">
                            <label>First Name</label>
                            <div class="input-group">
                                <input type="text" name="patient_name" id="patient_nameF" class="form-control input-small" />
                                <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <label>Last Name</label>
                            <div class="input-group">
                                <input type="text" name="patient_name" id="patient_nameL" class="form-control input-small" />
                                <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-4">
                            <label>Date</label>
                            <div class="input-group">
                                <input type="text" name="start_date" id="start_date" class="form-control input-small" disabled="disabled" />
                                <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                            </div>
                        </div>

                        <div class="col-md-4">
                            <label>Start time</label>
                            <div class="input-group bootstrap-timepicker">
                                <input type="text" class="form-control input-small" id="starts-at" disabled="disabled" />
                                <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <label>End time</label>
                            <div class="input-group bootstrap-timepicker">
                                <input type="text" class="form-control input-small" id="ends-at" disabled="disabled" />
                                <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-6">
                            <label>Address</label>
                            <div class="input-group">
                                <textarea name="address" id="txtAddress" class="form-control" ></textarea>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <label>Note</label>
                            <div class="input-group">
                                <textarea name="address" id="txtNote" class="form-control" ></textarea>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="modal-footer">
                        <label id="lblMsg"></label>
                        <button type="button" class="btn btn-primary" data-dismiss="modal">Close</button>
                        <button type="button" class="btn btn-success" id="save-event">Book Appointment</button>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
        <!-- /.modal -->
    </div>

    <script src="Scripts/jquery-3.4.1.js"></script>
    <script src="Scripts/jquery-ui.min.js"></script>
    <script src="Scripts/moment.js"></script>
    <script src="Scripts/fullcalendar/fullcalendar.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <script src="Scripts/bootstrap-timepicker.js"></script>
    <script src="Scripts/bootstrap-datetimepicker.min.js"></script>
    <script src="Scripts/simple-calendar.js"></script>

    <script type="text/javascript">
        var timeDiff;//= 10;

        $(document).ready(function () {
            $("#calendarMain").hide();

            var docId = 0;

            $('#doctorNames').on('change', function () {
                $("#timeSlots").empty();
                $("#timeSlots").hide();
                $("#calendarMain").hide();

                docId = this.value;

                if (docId > 0) {
                    debugger;
                    $("#calendarMain").show();
                    timeDiff = $('option:selected', this).attr('myTag');
                }
                BindPatientLists(docId, null);
            });

            docId = $('#doctorNames').val();

            BindPatientLists(docId, null);

            $.ajax({
                type: "POST",
                url: "calendars.asmx/GetDocList",
                dataType: "json",
                success: function (res) {
                    for (var x = 0; x < res.length; x++) {
                        var selectList = "<option value='" + res[x].docId + "' myTag='" + res[x].slotInterval + "'>" + res[x].docName + "</option>";
                        $("#doctorNames").append(selectList);
                    }
                },
                error: function (jqXHR, exception) {
                    errorFun();
                }
            });


            $("#starts-at, #ends-at").datetimepicker(
            {
                format: "hh:mm A"
            });

            $("#calendarMain").fullCalendar({
                header: {
                    left: 'prev',
                    center: 'title',
                    right: 'next'
                },
                //defaultDate: '2019-05-16',
                navLinks: false,
                selectable: true,
                unselectAuto: false,
                defaultView: 'month',
                aspectRatio: 0.75,
                dayClick: function (date, allDay, jsEvent, view) {
                    debugger;
                    slotDetails(docId, moment(date).format("YYYY-MM-DD"));
                    BindPatientLists(docId, moment(date).format("YYYY-MM-DD"));
                }
            });

            $('#save-event').on('click', function () {
                debugger;

                docId = $('#doctorNames').val();
                var title = $('#patient_name').val();
                var datess = $('#start_date').val();
                var startDate = $('#start_date').val().split("-").reverse().join("-");
                var startTime = moment($('#starts-at').val(), ["h:mm A"]).format("HH:mm");
                var endTime = moment($('#ends-at').val(), ["h:mm A"]).format("HH:mm");
                var patientNameF = $('#patient_nameF').val();
                var patientNameL = $('#patient_nameL').val();

                var address = $('#txtAddress').val();
                var note = $('#txtNote').val();

                if (patientNameF) {
                    var s = startDate + "T" + startTime;
                    var e = startDate + "T" + endTime;

                    if (startDate && startTime && endTime) {
                        $.ajax({
                            type: "POST",
                            contentType: 'application/x-www-form-urlencoded',
                            data: {
                                'doctorId': docId, 'appDate': startDate, 'startTime': startTime, 'endTime': endTime,
                                'patientNameF': patientNameF, 'patientNameL': patientNameL, 'address': address, 'note': note,
                            },
                            url: "calendars.asmx/SaveAppointmentEvent",
                            dataType: "json",
                            success: function (data) {
                                clearModal();
                                alert(data);
                                slotDetails(docId, moment(startDate).format("YYYY-MM-DD"));
                                BindPatientLists(docId, moment(startDate).format("YYYY-MM-DD"));
                            },
                            error: function (jqXHR, exception) {
                                errorFun();
                            }
                        });
                    }
                    else {
                        $('#lblMsg').text("enter all details");
                    }
                }
                else {
                    $('#lblMsg').text("enter names");
                }
            });

        });//ready end

        slotDetails = function (docId, dates) {           
            if (docId > 0) {
                $.ajax({
                    type: "POST",
                    contentType: 'application/x-www-form-urlencoded',
                    data: { 'doctorId': docId, 'appDate': dates },
                    url: "calendars.asmx/GetDocScheduleByDate",
                    dataType: "json",
                    success: function (data) {
                        var res = data;
                        $("#timeSlots").empty();
                        if (data.length > 0) {
                            var markup = "";
                            $.each(data, function (k, v) {
                                debugger;
                                var values = $.trim(v.name);
                                var classNames = v.className;
                                markup += "<input type='button' value='" +
                                  values +
                                   ((classNames == 'Available') ? "' class='eve btn btn-success'" : "' class='eve btn btn-light' disabled ") +
                                  "onclick=myEvent('" + values + "','" + dates + "')" +
                               " ></button>";
                            });
                            $("#timeSlots").append(markup);

                            $('#loading').fadeIn().delay(2000).fadeOut();
                            $("#timeSlots").fadeOut().delay(2000).fadeIn();
                        }
                        else {
                            $('#loading').fadeIn().delay(2000).fadeOut();
                            $("#timeSlots").fadeOut().delay(2000).fadeIn();
                            $("#timeSlots").append("<h2>No records</h2>");
                        }
                    },
                    error: function (jqXHR, exception) {
                        errorFun();
                    }
                });
            }
        }

        myEvent = function (value, startDate) {
            debugger;
            clearModal();

            var newTime1 = moment(value.replace("-", ' '), ["h:mm A"]).format("hh:mm A");
            var newTime2 = moment(value.replace("-", ' '), ["h:mm A"]).add(timeDiff, 'minutes').format("hh:mm A");

            $('.modal').find('#start_date').val(moment(startDate).format("DD-MM-YYYY"));
            $('.modal').find('#starts-at').val(newTime1);
            $('.modal').find('#ends-at').val(newTime2);
            $('.modal').modal('show');
        }

        function BindPatientLists(doctorId, clickedDate) {
            debugger;
            //var startDate = clickedDate.split("-").reverse().join("-");
            $.ajax({
                type: "POST",
                contentType: 'application/x-www-form-urlencoded',
                url: "calendars.asmx/Get_AppoinmentPatientDetails",
                data: { "doctorId": doctorId, "clickedDate": clickedDate },
                dataType: "json",
                success: function (json) {
                    $('#patientList tbody').empty();
                    //alert('json ' + JSON.stringify(json));
                    var tr;
                    var count = 1;
                    for (var i = 0; i < json.length; i++) {
                        tr = $('<tr/>');
                        tr.append("<td>" + count + "</td>");
                        tr.append("<td>" + moment(json[i].AppDate).format("DD-MM-YYYY") + "</td>");
                        tr.append("<td>" + moment(json[i].AppTime, ["h:mm A"]).format("HH:mm A") + "</td>");
                        tr.append("<td>" + json[i].PatientFirstName + ' ' + json[i].PatientLastName + "</td>");
                        tr.append("<td>" + json[i].Address + "</td>");
                        tr.append("<td>" + json[i].Note + "</td>");
                        tr.append("<td>" + json[i].CreatedBy + "</td>");

                        tr.append("<td><button type='button' id='RedirectBtn' " +
                            "onclick=RedirectToReg('" + json[i].PatientFirstName + "','"
                            + json[i].PatientLastName + "','" + json[i].AppId + "')" +
                            ((json[i].Status == '2') ? " class='btn btn-success' " : " class='eve btn btn-light' disabled ") +
                            "> Register" + "</button></td>");

                        $('#patientList tbody').append(tr);
                        count = count + 1;
                    }
                },
                error: function (jqXHR, exception) {
                    errorFun();
                }
            });
        }

        function RedirectToReg(firstName, lastName, AppId) {
            debugger;
            if (firstName && lastName && AppId) {
                window.location = '/register.aspx?firstName=' + firstName + '&lastName=' + lastName + '&appId = ' + AppId;
            }
        }

        //clear the modal inputs
        function clearModal() {
            //clear modal inputs------------------------------------

            $('#calendarSub').fullCalendar('unselect');

            // Clear modal inputs
            $('.modal').find('input').val('');

            //hide modal
            $('.modal').modal('hide');

            //------------------------------------------------------

            //// Clear modal inputs
            //$('.modal').find('input').val('');
        }

        function errorFun() {
            //debugger;
            var msg = '';
            if (jqXHR.status === 0) {
                msg = 'Not connect.\n Verify Network.';
            } else if (jqXHR.status == 404) {
                msg = 'Requested page not found. [404]';
            } else if (jqXHR.status == 500) {
                msg = 'Internal Server Error [500].';
            } else if (exception === 'parsererror') {
                msg = 'Requested JSON parse failed.';
            } else if (exception === 'timeout') {
                msg = 'Time out error.';
            } else if (exception === 'abort') {
                msg = 'Ajax request aborted.';
            } else {
                msg = 'Uncaught Error.\n' + jqXHR.responseText;
            }
            alert(msg);
        }

    </script>
</body>
</html>
