var ajaxRequestCount = 0;
var oldXHR = window.XMLHttpRequest;
var progressbarStopped = false;
var messageType = {
    success: 1,
    error: 2,
    warning: 3,
    information: 4,
    broadcast: 5
};

var ColumnPreferenceModuleName = {

    CustomerServiceTaskList: 1,
    CustomerServiceActionList: 2,
    ProductsList: 3,
    ProductAvailabilityList: 4,
    ToBeScheduledList: 5,
    SalesOrdersList: 6,
    ToBeDeliveredItemsList: 7,
    PurchaseOrdersList: 8,
    PurchaseOrdersRequestList: 9,
    GoodsInList: 10,
    StockTransferList: 11,
    SupplierList: 12,
    TicketConfigurationList: 13,
    DeliveryFeedbackList:14
};

function AjaxRequestStart() {
    console.log(progressbarStopped)
    if (!progressbarStopped) {
        ajaxRequestCount++;
     $("#ProgressBar").show();
      $("#overlay").show();
    }
}

function AjaxRequestFinish() {
    if (ajaxRequestCount > 0)
        ajaxRequestCount--;
    if (ajaxRequestCount == 0) {
       $("#ProgressBar").hide();
       $("#overlay").hide();
    }
}


$(document).ajaxSend(function (event, xhr, settings) {   
    if (settings.url.indexOf('/Chat') != -1) {
        progressbarStopped = true;
    }
    else {
        progressbarStopped = false;
    }
});


$(document).ajaxError(function (event, xhr, settings) {
    if (xhr.status == "401") {
        POSJS.OpenErrorInfo("You dont have permission to view this page.", 'Access Denied')
    } else {
        POSJS.OpenErrorInfo("Something went wrong !! please contact administrator.", 'Error')
    }
});

function newXHR() {
    var realXHR = new oldXHR();
    realXHR.addEventListener("readystatechange", function () {

        if (realXHR.readyState == 1) {
            AjaxRequestStart();
        }
        //if (realXHR.readyState == 2) {

        //}
        //if (realXHR.readyState == 3) {

        //}
        if (realXHR.readyState == 4) {
            AjaxRequestFinish();
        }
    }, false);
    return realXHR;
}
window.XMLHttpRequest = newXHR;

// Function to take action on error in response
function RedirectToLoginOnSessionTimeout(xhr) {

    // If error is 401 or 501 then redirect user to login page
    if (xhr.status == "401" || xhr.status == "501") {
        if ($(this).parent.length > 0) {
            window.parent.location.reload();
        }
        else
            window.location.reload();
    }
}


function DisplayMessage(divID, messageType, messages, isConcateWithPreviousMessage, isShowClose) {

    if (isConcateWithPreviousMessage == undefined) {
        isConcateWithPreviousMessage = false;
    }
    if (isShowClose == undefined) {
        isShowClose = true;
    }

    if (!isConcateWithPreviousMessage) {
        // first remove all existing messages
        $("#" + divID + " .success").remove();
        $("#" + divID + " .error").remove()
        $("#" + divID + " .warning").remove()
        $("#" + divID + " .information").remove();
        $("#" + divID + " .broadcast").remove();
    }

    var messageclass = '';
    switch (messageType) {
        case 1:
            messageclass = 'success';
            break;
        case 2:
            messageclass = 'error';
            break;
        case 3:
            messageclass = 'warning';
            break;
        case 4:
            messageclass = 'information';
            break;
        case 5:
            messageclass = 'broadcast';
            break;
        default:
            break;
    }

    // append the newly created message to specified div
    var messageHTML = "";
    if (isShowClose)
        messageHTML = "<div class='" + messageclass + "'><a href='javascript:void(0)' onclick='HideMessages(this);' class='ic-close'></a>" + messages.join("<br/>") + "</div>";
    else
        messageHTML = "<div class='" + messageclass + "'><a href='javascript:void(0)' onclick='HideMessages(this);'></a>" + messages.join("<br/>") + "</div>";
    $("#" + divID).prepend(messageHTML);
}

function HideMessages(elem) {
    $(elem).parent().hide();
}

function IsTabletOrMobile() {
    var isMobile = (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent));
    var iswebKeyboardEnabled = $("#hdnWebKeyboardEnabled").val();
    return (isMobile && (iswebKeyboardEnabled === "true"));
}

function IsTabletOrMobileDevice() {
    var isMobile = (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent));

    return (isMobile);
}


function GetCurrentDateString(dateFormat) {
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!

    var yyyy = today.getFullYear();
    if (dd < 10) {
        dd = '0' + dd;
    }
    if (mm < 10) {
        mm = '0' + mm;
    }

    if (dateFormat == 'mm-dd-yy') {
        var today = mm + '-' + dd + '-' + yyyy;
        return today;
    }
    else {
        var today = dd + '-' + mm + '-' + yyyy;
        return today;
    }
}

$(".numericbox").keydown(function (e) {
    if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
        // Allow: Ctrl+A, Command+A
        (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
        // Allow: home, end, left, right, down, up
        (e.keyCode >= 35 && e.keyCode <= 40)) {
        // let it happen, don't do anything
        return;
    }
    // Ensure that it is a number and stop the keypress
    if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
        e.preventDefault();
    }
});
function GetStatusHTML(deliverystatus) {
    
    if (deliverystatus !== "") {

        var multicolortext = "";
        var ItemValueArray = deliverystatus.split(",");

        for (var dataValue in deliverystatus.split(",")) {
            var StatusData = ItemValueArray[dataValue];
            var lableClass = "";
            if (StatusData.includes("NO ITEMS"))
                lableClass = "orderstatuslabel-lightorange";
            else if (StatusData.includes("IN TRANSFER"))
                lableClass = "orderstatuslabel-lightorange";
            else if (StatusData.includes("CANCELLED"))
                lableClass = "orderstatuslabel-cancel";
            else if (StatusData.includes("HOLD"))
                lableClass = "orderstatuslabel-hold";
            else if (StatusData.includes("TO BE ORDERED"))
                lableClass = "orderstatuslabel-tobeordered";
            else if (StatusData.includes("ORDERED"))
                lableClass = "orderstatuslabel-ordered";
            else if (StatusData.includes("READY FOR DELIVERY"))
                lableClass = "orderstatuslabel-readyfordelivery";
            else if (StatusData.includes("ON DELIVERY"))
                lableClass = "orderstatuslabel-ondelivery";
            else if (StatusData.includes("REFUND"))
                lableClass = "orderstatuslabel-refund";
            else if (StatusData.includes("EXCHANGE"))
                lableClass = "orderstatuslabel-refund";
            else if (StatusData.includes("RETURN QUEUE"))
                lableClass = "orderstatuslabel-lightorange";
            else if (StatusData.includes("Stock, Immediate"))
                lableClass = "orderstatuslabel-readyfordelivery";
            else if (StatusData.includes("Off Hold"))
                lableClass = "orderstatuslabel-hold";
            else if (StatusData.includes("New Order"))
                lableClass = "orderstatuslabel-tobeordered";
            else if (StatusData.includes("Inbound Order"))
                lableClass = "orderstatuslabel-ordered";
            else
                lableClass = "orderstatuslabel-fulldelivered";

            multicolortext = multicolortext + "<span class='orderstatuslabel " + lableClass + "'>" + StatusData + "</span>";
        }
        return multicolortext;
    }
    else {
        return "<div></div>";
    }
}
function GetFormattedDate(dateObject, format) {
    //"dd/MM/yyyy HH:mm:ss"
    var dateToFormat = new Date(dateObject);
    var Day = dateToFormat.getDate();
    var Month = dateToFormat.getMonth() + 1;
    var Year = dateToFormat.getFullYear();
    var Hour = dateToFormat.getHours();
    var Minute = dateToFormat.getMinutes();
    var Second = dateToFormat.getSeconds();
    /*var formatedDate = (Day > 9 ? Day : ("0" + Day)) + "/" + (Month > 9 ? Month : ("0" + Month)) + "/" + Year + " " + (Hour > 9 ? Hour : ("0" + Hour)) + ':' + (Minute > 9 ? Minute : ("0" + Minute)) + ':' + (Second > 9 ? Second : ("0" + Second));*/
    var formatedDate = (Day > 9 ? Day : ("0" + Day)) + "/" + (Month > 9 ? Month : ("0" + Month)) + "/" + Year;

    if (format) {
        formatedDate = Year + "-" + (Month > 9 ? Month : ("0" + Month)) + "-" + (Day > 9 ? Day : ("0" + Day));
    }

    return formatedDate;
}
function ConvertHoursToDaysHoursMinutes(hours) {
    // Calculate the number of days, hours, and minutes
    const days = Math.floor(hours / 24);
    const remainingHours = Math.floor(hours % 24);
    const minutes = Math.round((hours % 1) * 60);

    // Create the formatted string
    let formattedString = '';

    if (days > 0) {
        formattedString += days + ' Day ';
    }
    if (remainingHours > 0) {
        formattedString += remainingHours + ' Hour ';
    }
    if (minutes > 0) {
        formattedString += minutes + ' Minute';
    }

    return formattedString.trim();
}

function GetFlatPickerDateFormat(format) {
        if (format == 'MM-dd-yyyy')
        {
            return 'm-d-Y';
        }
        else
        {
                return 'd-m-Y';
        }      
    }
function productNameFormatter(value, row) {
    var url = '/Products/Products/ProductDetails?StockId=' + row.stockId;
    var productNameHtml = '<p class="m-0 d-inline-block align-middle font-16">';
    productNameHtml += '<a href="' + url + '" target="_blank" class="text-reset font-family-secondary">' + value + '</a><br>';
    productNameHtml += '<small class="me-2"><b>Description:-</b> ' + row.description + '</small>';
    productNameHtml += '</p>';
    return productNameHtml;
}
/* User Column Preferences */
function GetUserColumnPreferences(ColumnPreferenceModuleName) {
    var columnsJsonData = ""
    $.ajax({
        type: "GET",
        url: '/Common/Common/GetUserColumnPreferences',
        data: { moduleName: ColumnPreferenceModuleName },
        async: false,
        success: function (data) {
            columnsJsonData = data;
        },
        error: function (error) {
            columnsJsonData = "";
        }
    })

    return columnsJsonData;
}
function SetUserColumnPreferences(ColumnsJson, ColumnPreferenceModuleName) {

    $.ajax({
        type: "Post",
        url: '/Common/Common/SaveUserColumnPreferences',
        data: { columnsPreferences: JSON.stringify(ColumnsJson), moduleName: ColumnPreferenceModuleName },
        success: function (response) {
        },
        error: function (request, status, errorThrown) {
            POSJS.OpenErrorInfo('Something went wrong.')
        }
    })
}

function setTableColumnsVisibility(tableId, module) {
    $(tableId).on('column-switch.bs.table', function (e, name, checked) {
        var visibleColumns = $(tableId).bootstrapTable('getVisibleColumns');
        var visibleColumnNames = visibleColumns.map(column => column.field);

        SetUserColumnPreferences(visibleColumnNames, module);
    });

    var savedColumns = GetUserColumnPreferences(module);
    if (savedColumns) {
        savedColumns = $.parseJSON(savedColumns);
        $(tableId).bootstrapTable('hideAllColumns');

        savedColumns.forEach(function (column) {
            $(tableId).bootstrapTable('showColumn', column);
        });
    }
}