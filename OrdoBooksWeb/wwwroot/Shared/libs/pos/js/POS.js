var posService = (function () {
    var instance;
    var dialogCustomerInstance;
    var baseUrl = window.location.protocol + "//" + window.location.hostname + (window.location.port ? ':' + window.location.port : '');

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
    ///General function to open DialogueBox
  function LoadDialogueBox(partialViewUrl, dialogtitle, paramData) {          
          $.ajax({
            url: partialViewUrl,
            cache: false,
            async: false,
            data: paramData,
            success: function (result) {
              $('#modalContentStandard').html(result);
              $('#standard-modal').modal('show');
              $('#standardLabel').text(dialogtitle);
            },
            complete: function () {

            },
            error: function (request, status, errorThrown) {
              //alert("Something Went Wrong !")
            }
           });
     }

  function LoadDialogueBoxSmall(partialViewUrl, dialogtitle, paramData) {
          $.ajax({
            url: partialViewUrl,
            cache: false,
            async: false,
            data: paramData,
            success: function (result) {
              $('#modalContentSmall').html(result);
              $('#right-modal').modal('show');
              $('#smallLabel').text(dialogtitle);
            },
            complete: function () {

            },
            error: function (request, status, errorThrown) {
              //alert("Something Went Wrong !")
            }
          });
    }

  function LoadDialogueBoxLarge(partialViewUrl, dialogtitle, paramData) {
          $.ajax({
            url: partialViewUrl,
            cache: false,
            async: false,
            data: paramData,
            success: function (result) {
              $('#modalContentLarge').html(result);
              $('#large-modal').modal('show');
              $('#largeLabel').text(dialogtitle);
            },
            complete: function () {

            },
            error: function (request, status, errorThrown) {
              //alert("Something Went Wrong !")
            }
          });
    }

  function LoadDialogueBoxFull(partialViewUrl, dialogtitle, paramData) {
            $.ajax({
              url: partialViewUrl,
              cache: false,
              async: false,
              data: paramData,
              success: function (result) {
                $('#modalContentfull').html(result);
                  $('#full-width-modal').modal('show');
                  $('#fullWidthLabel').text(dialogtitle);
              },
              complete: function () {

              },
              error: function (request, status, errorThrown) {
                //alert("Something Went Wrong !")
              }
            });
    }

  function LoadDialogueBoxScrollable(partialViewUrl, dialogtitle, paramData) {
            $.ajax({
              url: partialViewUrl,
              cache: false,
              async: false,
              data: paramData,
              success: function (result) {
                $('#modalContentScrollable').html(result);
                 $('#scrollable-modal').modal('show');
                $('#scrollableLabel').text(dialogtitle);
              },
              complete: function () {

              },
              error: function (request, status, errorThrown) {
                //alert("Something Went Wrong !")
              }
             });
    }
    function CloseAllDialog() {
      $('.modal').not($(this)).each(function () {
        $(this).modal('hide');
      });
    }
    function CloseDialogByName(dialogName) {
      $('.modal').not($(this)).each(function () {
        if ($(this).modal().attr("id") == dialogName) {
          $('#' + dialogName).modal('hide');
        }        
      });
   }

  function OpenErrorInfo(message, title) {
      $('#errorToastTitle').text(title)
      $('#toastErrorBody').html(message);
      new bootstrap.Toast($('#basicErrorToast')).show()
  }
  function OpenSuccessInfo(message, title) {
    $('#successToastTitle').text(title)
    $('#toastSuccessBody').html(message);
    new bootstrap.Toast($('#basicSuccessToast')).show()
  }

  function OpenInfo(message, title) {
    $('#infoToastTitle').text(title)
    $('#toastInfoBody').html(message);
    new bootstrap.Toast($('#basicInfoToast')).show()
  }

  function OpenWarning(message, title) {
    $('#warningToastTitle').text(title)
    $('#toastWarningBody').html(message);
    new bootstrap.Toast($('#basicWarningToast')).show()
  }

 function OpenRightModel(partialViewUrl, paramData)
    {

      $.ajax({
          url: partialViewUrl,
          type: 'POST', // or 'POST' if required
          data: paramData, 
          success: function (data) {
              // Set the content of the modal body with the loaded data
              $("#rightModelbody").html(data);
          },
          error: function () {
              // Handle errors if necessary
          }
      });

      // Show the modal
        $("#rightBarModel").modal('show');

  }

  function ShowRightPanelModelInfo(partialViewUrl, dialogtitle, paramData,widthInPercent) {   
    $.ajax({
      url: partialViewUrl,
      cache: false,
      data: paramData,
      success: function (result) {
        $('#offcanvasExampleTitle').html(dialogtitle)
        $('#offCanvasBody').html(result);
        $('#btnOffCanvas').click();
        if (!widthInPercent)
        {
          widthInPercent = 50;
        }
        $('#ContentIframe').height((window.innerHeight * widthInPercent) / 100);
        $('#offcanvasPopup').width(widthInPercent + "%");
       
      },
      complete: function () {
      
      },
      error: function (request, status, errorThrown) {
        // alert("Something Went Wrong !")       
      }
    });
  }  

  function ShowLeftPanelModelInfo(partialViewUrl, dialogtitle, paramData,widthInPercent) {   
    $.ajax({
      url: partialViewUrl,
      cache: false,
      data: paramData,
      success: function (result) {
        $('#offcanvasExampleTitleLeft').text(dialogtitle)
        $('#offCanvasBodyLeft').html(result);
        $('#btnOffCanvasLeft').click();
        if (!widthInPercent)
        {
          widthInPercent = 50;
        }
        $('#ContentIframe').height((window.innerHeight * widthInPercent) / 100);
        $('#offcanvasPopupLeft').width(widthInPercent + "%");
       
      },
      complete: function () {
      
      },
      error: function (request, status, errorThrown) {
        //alert("Something Went Wrong !")       
      }
    });
  }

    // Initialize object of the messageBoxService
    function init() {
        // Public methods and variables
        return {
          LoadDialogueBox: function (partialViewUrl, dialogtitle, paramData) {
            return LoadDialogueBox(partialViewUrl, dialogtitle, paramData);
            },
          LoadDialogueBoxSmall: function (partialViewUrl, dialogtitle, paramData) {
            return LoadDialogueBoxSmall(partialViewUrl, dialogtitle, paramData);
            },
          LoadDialogueBoxLarge: function (partialViewUrl, dialogtitle, paramData) {
            return LoadDialogueBoxLarge(partialViewUrl, dialogtitle, paramData);
          },
          LoadDialogueBoxFull: function (partialViewUrl, dialogtitle, paramData) {
            return LoadDialogueBoxFull(partialViewUrl, dialogtitle, paramData);
          },
          LoadDialogueBoxScrollable: function (partialViewUrl, dialogtitle, paramData) {
            return LoadDialogueBoxScrollable(partialViewUrl, dialogtitle, paramData);
          },
          CloseAllDialog: function () {
            return CloseAllDialog();
          },
          CloseDialogByName: function (dialogName) {
            return CloseDialogByName(dialogName);
          },
          OpenErrorInfo: function (message, title) {
            return OpenErrorInfo(message, title);
          },
          OpenInfo: function (message, title) {
            return OpenInfo(message, title);
          },
          OpenSuccessInfo: function (message, title) {
            return OpenSuccessInfo(message, title);
          },
          OpenWarning: function (message, title) {
            return OpenWarning(message, title);
            },
          OpenRightModel: function (partialViewUrl, paramData) {
                OpenRightModel(partialViewUrl, paramData)
          },
          ShowRightPanelModelInfo: function (partialViewUrl, dialogtitle, paramData, widthInPercent) {
            return ShowRightPanelModelInfo(partialViewUrl, dialogtitle, paramData, widthInPercent);
          },
          ShowLeftPanelModelInfo: function (partialViewUrl, dialogtitle, paramData, widthInPercent) {
            return ShowLeftPanelModelInfo(partialViewUrl, dialogtitle, paramData, widthInPercent);
          }
        }
    };
    return {
        // Get the Singleton instance if one exists or create one if it doesn't
        getInstance: function () {
            if (!instance) {
                instance = init();
            }
            return instance;
        }
    };

})();

var POSJS = posService.getInstance();
