
function showDownloadNotificationModal(FileID, FileName, UserIDOfPersonThatDownloadedTheFile) {
    $("#FileID").val(FileID);
    $("#UserIDOfPersonThatDownloadedTheFile").val(UserIDOfPersonThatDownloadedTheFile);
    $("#DownloadSelectedFileModal").modal("show");
}
function CloseFileDetails(fileID) {
    var tdObj = $("#FileDetails_" + fileID);
    tdObj.addClass("hidden");
}
function showFileDetails(fileID) {
    //debugger;
    $(".FileDetailsClass").addClass("hidden");
    var tdObj = $("#FileDetails_" + fileID);
    tdObj.removeClass("hidden");
    //show loading
    $("#FileHistory_Loading_" + fileID).show();
    $('#FileHistory_Content_' + fileID).hide();
    loadFileHistory(fileID);
}
function loadFileHistory(FileID) {

    //var ajaxCallURL = '@Url.Action("GetFileHistory", "Files")';
    var ajaxCallURL = window.rootUrl + 'Files/GetFileHistory';
    //debugger;
    var DataObj = JSON.parse('{}');

    DataObj.FileID = FileID;

    $.ajax({
        type: "POST",
        url: ajaxCallURL,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(DataObj),
        datatype: "json",
        success: function (data) {

            $("#FileHistory_Loading_" + FileID).fadeOut("slow", function () {
                $('#FileHistory_Content_' + FileID).html(data).fadeIn("slow");
            });
        },
        error: function (data) {

            $("#FileHistory_Loading_" + FileID).fadeOut("slow", function () {
                $('#FileHistory_Content_' + FileID).html(data).fadeIn("slow");
            });

        }
    });

}
$(function () {
    $("#closeFileHistory").click(function () {
        //debugger;
        $('#ShowFileHistoryModal').modal('hide');
    });
    $("#DownloadSelectedFileModal").on('hidden.bs.modal', function () {
        $("#btnRefreshPage")[0].click();
    });

    $("#UploadUserFileModal").on('hidden.bs.modal', function () {
        $("#btnRefreshPage")[0].click();
    });
    $("#UploadUpdatedFileModal").on('hidden.bs.modal', function () {
        $("#btnRefreshPage")[0].click();
    });

    $("#ProcessFileRequestNotificationModal").on('hidden.bs.modal', function () {
        $("#btnRefreshPage")[0].click();
    });

    

    $('[data-toggle="tooltip"]').tooltip();

});
function DownloadFile() {
    $("#btnDownload")[0].click();
}
function sendRequestNotification(FileID, IDOFPersonLoggedOn, IDOfTheFileOwner) {
    //Create Json Object
    var DataObj = JSON.parse('{}');
    var ajaxCallURL = window.rootUrl + 'Notifications/ProcessRequestNotification';
    //populate the json Object with the fields that need to be passed to the method
    DataObj.FileID = FileID;
    DataObj.IDOFPersonLoggedOn = IDOFPersonLoggedOn;
    DataObj.IDOfTheFileOwner = IDOfTheFileOwner;

    $.ajax({
        type: "POST",
        url: ajaxCallURL,//'@Url.Action("ProcessRequestNotification", "Notifications")',
        data: JSON.stringify(DataObj),//'{ FileID: "' + FileID + '", NotificationTypeID: "' + NotificationTypeID + '", IDOFPersonLoggedOn: "' + IDOFPersonLoggedOn + '",IDOfTheFileOwner: "' + IDOfTheFileOwner + '" }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            $('#SuccessConfiramtionMessage').html(data.Message);

            $('#SuccessConfirmationModal').modal('show');
            var f = function () {
                setTimeout(function () {
                    $('#SuccessConfirmationModal').modal('hide');
                }, 2500);
            };
            f();
        },
        error: function (xhr, textStatus, error) {

            $('#FailureConfiramtionMessage').html(error);

            $('#FailureConfirmationModal').modal('show');


        }
    });
}
function UploadSelectedFile() {

    var input = $('#Crtl_UploadFile');
    var files = input.prop("files");
    //debugger;
    if (files.length > 0) {
        if (window.FormData !== undefined) {
            var data = new FormData();
            for (var x = 0; x < files.length; x++) {
                data.append("file" + x, files[x]);
            }

            //data.append("FileShareStatusID", $('input[name=radFileShareStatus]:checked').val());

           // var test = $('input[name=radFileShareStatus]:checked').val();

            //var ajaxCallURL = '@Url.Action("SaveNewUserFile", "Files")' + '?FileShareStatusID=' + $('input[name=radFileShareStatus]:checked').val();
            var ajaxCallURL = window.rootUrl +'Files/SaveNewUserFile?FileShareStatusID=' + $('input[name=radFileShareStatus]:checked').val();
            
            $.ajax({
                type: "POST",
                url: ajaxCallURL,//'/ContentManagement/UserFileUpload?_FileShareStatusID=' + $('input[name=radFileShareStatus]:checked').val(),
                contentType: false,
                processData: false,
                data: data,
                success: function (result) {

                    var jsonResult = $.parseJSON(result);

                    if (jsonResult[0].WasSuccessfull) {
                        $('#SuccessConfiramtionMessage').html('<b>File</b> : ' + jsonResult[0].FileName + ' : ' + jsonResult[0].Message);

                        $('#SuccessConfirmationModal').modal('show');
                        var f = function () {
                            setTimeout(function () {
                                $('#SuccessConfirmationModal').modal('hide');
                            }, 2500);
                        };
                        f();
                    } else {
                        $('#FailureConfiramtionMessage').html(jsonResult[0].Message);

                        $('#FailureConfirmationModal').modal('show');
                    }

                    ///
                    //$("#UpLoadMessage").hide();
                    //$('#UpLoadMessage').html(result);

                    //$("#UpLoadMessage").fadeIn("slow", function () {
                    //    // Animation complete.

                    //    $(this).fadeOut("slow", function () {
                    //        $('#UpLoadMessage').html('');
                    //    });
                    //});

                },
                error: function (xhr, status, p3, p4) {

                    var err = "Error " + " " + status + " " + p3 + " " + p4;
                    //if (xhr.responseText && xhr.responseText[0] == "{")
                    //    err = JSON.parse(xhr.responseText).Message;
                    //alert(err);
                    //$("#UpLoadMessage").hide();
                    //$('#UpLoadMessage').html(err);

                    //$("#UpLoadMessage").fadeIn("slow", function () {
                    //    // Animation complete.
                    //    $(this).fadeOut("slow", function () {
                    //        $('#UpLoadMessage').html('');
                    //    });
                    //});

                    /////
                    $('#FailureConfiramtionMessage').html(err);

                    $('#FailureConfirmationModal').modal('show');
                }
            });
        } else {
            alert("This browser doesn't support HTML5 file uploads!");
        }

        $('#Crtl_UploadFile').val(null);
    }
}
var _FileUploadModel;
function showUploadNewUpdateModal(FileID, FileName, UserIDOfPersonThatUploadingTheFile) {
    _FileUploadModel = JSON.parse('{}');

    _FileUploadModel.FileID = FileID;
    _FileUploadModel.UserIDOfPersonThatUploadingTheFile = UserIDOfPersonThatUploadingTheFile;
    _FileUploadModel.FileName = FileName;
    $("#UploadUpdatedFileModal").modal('show');
}

function UploadUpdatedFile() {
    var input = $('#Crtl_UploadUpdatedFile');
    var files = input.prop("files");
   // debugger;
    if (files.length > 0) {
        if (window.FormData !== undefined) {
            var data = new FormData();
            for (var x = 0; x < files.length; x++) {
                data.append("file" + x, files[x]);
            }
            //var ajaxCallURL = 'Url.Action("SaveNewUserFile", "Files")' + '?FileShareStatusID=' + $('input[name=radFileShareStatus]:checked').val();
           // var ajaxCallURL = '@Url.Action("UpdateCurrentFile", "Files")' + '?FileIDToUpdate=' + _FileUploadModel.FileID + '&UserIDOfPersonThatUploadingTheFile=' + _FileUploadModel.UserIDOfPersonThatUploadingTheFile;
            var ajaxCallURL = window.rootUrl + 'Files/UpdateCurrentFile?FileIDToUpdate=' + _FileUploadModel.FileID + '&UserIDOfPersonThatUploadingTheFile=' + _FileUploadModel.UserIDOfPersonThatUploadingTheFile;
            $.ajax({
                type: "POST",
                url: ajaxCallURL,
                contentType: false,
                processData: false,
                data: data,
                success: function (result) {

                    var jsonResult = $.parseJSON(result);

                    if (jsonResult[0].WasSuccessfull) {
                        $('#SuccessConfiramtionMessage').html('<b>File</b> : ' + jsonResult[0].FileName + ' : ' + jsonResult[0].Message);

                        $('#SuccessConfirmationModal').modal('show');
                        var f = function () {
                            setTimeout(function () {
                                $('#SuccessConfirmationModal').modal('hide');
                            }, 3000);
                        };
                        f();
                    } else {
                        $('#FailureConfiramtionMessage').html(jsonResult[0].Message);

                        $('#FailureConfirmationModal').modal('show');
                    }
                },
                error: function (xhr, status, p3, p4) {

                    var err = "Error " + " " + status + " " + p3 + " " + p4;
                    //if (xhr.responseText && xhr.responseText[0] == "{")
                    //    err = JSON.parse(xhr.responseText).Message;

                    $('#FailureConfiramtionMessage').html(err);

                    $('#FailureConfirmationModal').modal('show');
                }
            });
        } else {
            // alert("This browser doesn't support HTML5 file uploads!");
            $('#FailureConfiramtionMessage').html("This browser doesn't support HTML5 file uploads!");

            $('#FailureConfirmationModal').modal('show');
        }

        input.val(null);
    }

}

function SetTabIndex(iTabIndex) {
    $("#TabIndex").val(iTabIndex);
}


var _AcceptFileRequestNotifictionViewModel;

function showProcessRequestNotificationModal(FileID, NotificationID, FileName, PersonNameWhichIsRequestingFile) {

    //Create Json Object
    _AcceptFileRequestNotifictionViewModel = JSON.parse('{}');
    _AcceptFileRequestNotifictionViewModel.TabIndex = 0;
    _AcceptFileRequestNotifictionViewModel.NotificationID = NotificationID;

    //debugger;
    $("#requestNotificationSenderName").html(PersonNameWhichIsRequestingFile);
    $("#requestedFileNameToShareBaseOnTheREquestNotificationRecieved").html(FileName);
    $("#requestedNotificationFileID").html(FileID);


    $("#ProcessFileRequestNotificationModal").modal("show");

}

function ProcessAcceptedFileShareRequest() {

    var ajaxCallURL = window.rootUrl + 'Notifications/AcceptFileRequestNotification';
    //debugger;
    $.ajax({
        type: "POST",
        url: ajaxCallURL,//'@Url.Action("AcceptFileRequestNotification", "Notifications")',
        data: JSON.stringify(_AcceptFileRequestNotifictionViewModel),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            $("#ProcessFileRequestNotificationModal").modal("hide");

            $('#SuccessConfiramtionMessage').html(data.Message);

            $('#SuccessConfirmationModal').modal('show');

            var f = function () {
                setTimeout(function () {
                    $('#SuccessConfirmationModal').modal('hide');
                }, 3000);
            };
            f();
        },
        error: function (xhr, textStatus, error) {

            $('#FailureConfiramtionMessage').html(error);
            //$('#ShowFileRequerstNotificationConfirmationModal').modal(options);
            $('#FailureConfirmationModal').modal('show');

        }
    });

    
}
