
var urlPath = '';
var data = '';
var selected_Table, dataTable_SearchValue, temp_dtSelectedRow_Id;


function getUrl() {
    urlPath = '';
    var url = window.location.href;
    var urlArr = url.split('/');
    for (i = 0; i < urlArr.length - 2; i++) {
        urlPath += urlArr[i] + "/";
        console.log('urlPath' + urlPath);
    }
    return urlPath;
}

function deleteDialog(controller, action, dataField, rowid, module, contentflag, getAction) {

    var dataObj = '{';
    for (i = 0; i < dataField.length; i++) {
        dataObj += '"' + dataField[i] + '": "' + rowid[i] + '", ';
    }
    //var connectionType = (action == 'DeleteConfirmPresentation') ? 'GET' : 'POST';  
    dataObj = dataObj.replace(/,(?=[^,]*$)/, '') + '}'; //remove last comma
    var text, title, confirmButtonText;
    text = (module.toUpperCase() == "MASTER") ? "" : (module.toUpperCase() == "LOGOUT") ? "" : "All associated entities will be deleted!";
    title = (module.toUpperCase() == "LOGOUT") ? "Are you sure you want to logout?" : "Are you sure ?";
    confirmButtonText = (module.toUpperCase() == "MASTER") ? "Yes, logout" : (module.toUpperCase() == "LOGOUT") ? "Yes, logout" : "Yes, delete it!"
    swal({
        title: title,
        text: text,
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: confirmButtonText
    }).then(
        function () {
            if (module.toUpperCase() == "LOGOUT" || module.toUpperCase() == "MASTER") {
                if (module.toUpperCase() == "LOGOUT") {
                    //clean localstorage
                    localStorage.removeItem("OrgId");
                    localStorage.removeItem("GroupIds");
                    localStorage.removeItem("DeptIds");
                    localStorage.removeItem("OrgIsSimple");
                }
                $('#' + dataField).submit();
            }
            else {
                $.ajax({
                    url: getUrl()  + controller + "/" + action,
                    data: JSON.parse(dataObj),
                    type: 'POST',
                    cache: false,
                    beforeSend: function () {
                    },
                    complete: function () {
                    },
                    success: function (data) {

                        if (data.resultCode == 0) {

                            $('#row' + rowid[0]).remove();
                            showDialog("Record has been deleted.", "Deleted!", "success", "", "", 0, false, true);


                            if (module.toLowerCase() != 'master' && getAction != '_EditPresentation') {
                                RefreshList(controller, getAction, contentflag, '', 0, false, true);
                            }                           
                        }
                        else if (data.resultCode == 138) { // delete confirm for presenation delete
                            action = (action == 'DeletePresentations') ? 'DeleteConfirmPresentation' : action;
                            deleteDialogConfirm(controller, action, dataField, rowid, module, contentflag, getAction, data.resultDescription);
                        }
                        else {
                            showDialog(data.resultDescription, 'Fail To Delete', "error", "", "", 0, false, false);
                        }
                    },
                    error: function (e) {
                        showDialog(module + "Cannot delete.It is related with other entities!", "Error!", "error", "", "", 0, false, true);
                    }
                });
            }
        },
        function (dismiss) {
            if (dismiss === 'cancel') {
            }
        }
    );
}

function deleteDialogStatus(controller, action, dataField, rowid, module, contentflag, getAction) {

    var dataObj = '{';
    for (i = 0; i < dataField.length; i++) {
        dataObj += '"' + dataField[i] + '": "' + rowid[i] + '", ';
    }
    //var connectionType = (action == 'DeleteConfirmPresentation') ? 'GET' : 'POST';  
    dataObj = dataObj.replace(/,(?=[^,]*$)/, '') + '}'; //remove last comma
    var text, title, confirmButtonText;
    text = (module.toUpperCase() == "MASTER") ? "" : (module.toUpperCase() == "LOGOUT") ? "" : "All associated entities will be deleted!";
    title = (module.toUpperCase() == "LOGOUT") ? "Are you sure you want to logout?" : "Are you sure ?";
    confirmButtonText = (module.toUpperCase() == "MASTER") ? "Yes, logout" : (module.toUpperCase() == "LOGOUT") ? "Yes, logout" : "Yes, delete it!"
    swal({
        title: title,
        text: text,
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: confirmButtonText
    }).then(
        function () {
            if (module.toUpperCase() == "LOGOUT" || module.toUpperCase() == "MASTER") {
                if (module.toUpperCase() == "LOGOUT") {
                    //clean localstorage
                    localStorage.removeItem("OrgId");
                    localStorage.removeItem("GroupIds");
                    localStorage.removeItem("DeptIds");
                    localStorage.removeItem("OrgIsSimple");
                }
                $('#' + dataField).submit();
            }
            else {
                $.ajax({
                    url: urlPath + controller + "/" + action,
                    data: JSON.parse(dataObj),
                    type: 'POST',
                    cache: false,
                    beforeSend: function () {
                    },
                    complete: function () {
                    },
                    success: function (data, textStatus, jqXHR) {
                        if (data.resultCode == 0) {

                            $('#row' + rowid[0]).remove();
                            showDialog("Record has been deleted.", "Deleted!", "success", "", "", 0, false, true);
                            RefreshList(controller, getAction, contentflag, '', 0, false, true);
                           
                        }
                        else if (data.resultCode == 138) { // delete confirm for presenation delete
                            action = (action == 'DeletePresentations') ? 'DeleteConfirmPresentation' : action;
                            deleteDialogConfirm(controller, action, dataField, rowid, module, contentflag, getAction, jqXHR.statusText);
                        }
                        else {
                            showDialog(jqXHR.statusText, 'Fail To Delete', "error", "", "", 0, false, false);
                        }
                    },
                    error: function (e) {
                        showDialog(module + "Cannot delete.It is related with other entities!", "Error!", "error", "", "", 0, false, true);
                    }
                });
            }
        },
        function (dismiss) {
            if (dismiss === 'cancel') {
            }
        }
    );
}

function showDialog(text, title, type, controller, action, rowid, isupdate, isDeleted, module, contentflag, contentType, isReturnSuccess) {
    isReturnSuccess = (typeof isReturnSuccess !== 'undefined') ? isReturnSuccess : true;
    var timerTime = 800;
    if (type == 'error') {
        timerTime = 3000;
    }
    swal({
        title: title,
        text: text,
        type: type,
        timer: timerTime,
        showCancelButton: false,
        showConfirmButton: false
    }).then(
        function () {
        },
        function (dismiss) {
            if (dismiss === 'timer') {
                if (type == "success" && isDeleted == false && isReturnSuccess == true) {
                    RefreshList(controller, action, contentflag, contentType, rowid, isupdate, isDeleted);
                }               
                if (type == "warning") {
                    $('.modal-backdrop').remove();
                }
            }
        }
    );
}

function RefreshList(controller, action, contentflag, contentType, rowid, isupdate, isDelete) {

    var rootUrl = getUrl();
    console.log(rootUrl);
    var relativeURI = rootUrl + controller + '/' + action;

    if (controller != 'Presentations' && action != 'ListPresentationByOrganizationId') {
        selected_Table = $('.listingTbl').DataTable();

        var tableID = $('.customTbl').attr('id');
        dataTable_SearchValue = $('#' + tableID + '_wrapper input').val();
    }

   appFilter.renderPartialViewMasterUI(relativeURI, controller, rowid, isupdate, isDelete);
}

// click cancel button in form wizard presentation
function redirectToPresentationList() {
    RefreshList("Presentations", "ListPresentationByOrganizationId", 0, '', 0, false, false);
}

//show alert dialog for message push
function pushMessageDialog(type, messageType) {
    var notMessageType, title, text, alertType;
    if (type == "cannotpush") {
        notMessageType = (messageType.toLowerCase() == "emerg") ? "Notification" : "Emergency";
        messageType = (messageType == "noti") ? "Notification" : "Emergency";
        alertType = "error";
        title = "Fail To Enabled";
        text = "The " + messageType + " cannot perform with " + notMessageType + " simultaneously.";
    }

    if (type == "enabled") {
        title = "Message Enabled Successfully";
        text = "";
        alertType = "success";
    }

    if (type == "disabled") {
        title = "Message Disabled Successfully";
        text = "";
        alertType = "success";
    }

    var timerValue = (alerType == 'error') ? 2000 : 800;

    swal({
        title: title,
        text: text,
        type: alertType,
        timer: timerValue,
        showCancelButton: false,
        showConfirmButton: false
    }).then(function () {
    },
        function (dismiss) {
        }
    );
}

function existPage(title, module) {
    swal({
        title: title,
        text: '',
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: 'Yes, leave'
    }).then(function (isConfirm) {
        if (typeof setFocuseStyle === "function") {
            setFocuseStyle();
        }
        if (typeof destoryDatePicker === "function") {
            console.log("destory");
            destoryDatePicker();
        }

        $('.sp-container').remove(); //remove spectrum color picker element from dom
        if (module == "presentation")
            redirectToPresentationList();
        //else if (module == "schedule")
        //    $('.dhx_cal_light_wide').css({ 'display': 'none' });
        else {
            $('#Custom-myModal').hide();
            $('.tblDisplay').find('tbody>tr').find('.tdProgressStatus').find('img').remove();
        }
    }, function (dimiss) {
        if (module != "presentation" && module != "template")
            $('#Custom-myModal').modal('show');
        if (module == "template")
            $('#modalModification').modal('show');
    });
}

function deleteDialogConfirm(controller, action, dataField, rowid, module, contentflag, getAction, dialogText) {


    var dataObj = '{';
    for (i = 0; i < dataField.length; i++) {
        dataObj += '"' + dataField[i] + '": "' + rowid[i] + '", ';
    }
    dataObj = dataObj.replace(/,(?=[^,]*$)/, '') + '}'; //remove last comma
    var text, title, confirmButtonText;
    text = (module.toUpperCase() == "MASTER") ? "" : (module.toUpperCase() == "LOGOUT") ? "" : dialogText;
    title = (module.toUpperCase() == "LOGOUT") ? "Are you sure you want to logout?" : "Are you sure ?";
    confirmButtonText = (module.toUpperCase() == "MASTER") ? "Yes, logout" : (module.toUpperCase() == "LOGOUT") ? "Yes, logout" : "Yes, delete it!"
    swal({
        title: title,
        text: text,
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: confirmButtonText
    }).then(
        function () {
            $.ajax({
                url: getUrl()  + controller + "/" + action,
                data: JSON.parse(dataObj),
                type: "POST",
                cache: false,
                beforeSend: function () {
                },
                complete: function () {
                },
                success: function (data) {

                    if (data.resultCode == 0) {
                        $('#row' + rowid[0]).remove();
                        showDialog("Record has been deleted.", "Deleted!", "success", "", "", 0, false, true);
                        if (module.toLowerCase() != 'master' && getAction != '_EditPresentation') {
                            RefreshList(controller, getAction, contentflag, '', 0, false, true);
                        }
                    }
                    else {
                        showDialog(data.resultDescription, 'Fail To Delete', "error", "", "", 0, false, false);
                    }
                },
                error: function (e) {
                    showDialog(module + "Cannot delete.It is related with other entities!", "Error!", "error", "", "", 0, false, true);
                }
            });
        },
        function (dismiss) {
            if (dismiss === 'cancel') {
            }
        }
    );
}