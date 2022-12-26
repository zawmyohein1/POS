
var appFilter = {
    /*** Get Application URL **/
    getApplicaitonURL: function () {
     
        var url = window.location.href;
        var urlArr = url.split('/');
        var urlPath = '';
        var data = '';
        var toCount = urlArr.length > 5 ? 5 : urlArr.length;
        for (i = 0; i < toCount - 2; i++) {
            urlPath += urlArr[i] + "/";
        }
        return urlPath;
    },

    /* Left Side Menu Button Click Event */
    leftSideMenuClickEvent: function () {
        $('#ulLeftSideMenu').on('click', 'a', function () {

            var btnDataURL = $(this).attr('data-url');
            if (btnDataURL != undefined)//check for main menu and menu
            {
                $('#ulLeftSideMenu').find('li a').removeClass('active');
                $(this).addClass('active');
                var path = appFilter.getApplicaitonURL();
                var relativeURI = path + $(this).attr('data-url');
                var controllerName = btnDataURL.split('/')[1];

                //set default datatable page length, size, selected row and search value
                if (selected_Table) {
                    selected_Table.clear().draw();
                    selected_Table.page.len(10).draw();
                    dataTable_SearchValue = '';
                    temp_SearchValue = '';
                }
                appFilter.renderPartialViewMasterUI(relativeURI, controllerName, 0, false, false);
            }
        });
    },

    /** Render to UI Controller **/
    renderPartialViewMasterUI: function (relatetiveURI, controllerName, rowid, isupdate, isDeleted) {
        /* Once loading the page, show loading bar */
        if (!isDeleted) {
            $('.page-loader-wrapper').fadeIn();
        }
        var loadTime = Date.now();

        $.ajax({
            url: relatetiveURI,
            type: "get",
            datatype: 'json',
            /*  data: dataPass,*/
            traditional: true,
            success: function (data) {
            
                $("#PartialView").html("");
                $("#PartialView").html(data);

                if (isupdate) {
                    /* To highlight updated row */
                    $('#row' + rowid).css({ 'background': '#FFFFF0' });
                    setTimeout(function () {
                        $('#row' + rowid).removeAttr('style');
                    }, 1500);
                    /* End of highLight updated row */
                }
                admin.loadTimeLog(loadTime, relatetiveURI + ' page request success');
            },
            complete: function () {
                if (!isDeleted) {

                    //keep datatable as previous state
                    if ($('.listingTbl').length !== 0) {
                        var new_Table = $('.listingTbl').DataTable();

                        if (selected_Table && new_Table) {
                            var tableID = $('.listingTbl').attr('id');
                            //set search value
                            $('#' + tableID + '_wrapper input').val(dataTable_SearchValue);
                            //make search at datatable
                            new_Table.search($('#' + tableID + '_wrapper input').val()).draw();

                            if (selected_Table.page.len()) {
                                //set pagelength
                                new_Table.page.len(selected_Table.page.len()).draw();
                            }

                            if (selected_Table.page.info()) {
                                //set pagesize
                                new_Table.page(selected_Table.page.info().page).draw(false);
                            }

                            if (selected_Table.rows('.selected').data()[0]) {
                                //click on selected row
                                $('#' + selected_Table.rows('.selected').data()[0].DT_RowId).click();
                            }
                        }
                    }
                    //end of keep datatable as previous state
                }
                else {
                    if ($('.listingTbl').length !== 0) {
                        var new_Table = $('.listingTbl').DataTable();
                    }
                }
            }
        });
    },

    routeLandingPage: function (landingPage, landingURL) { 
        
        $('#ulLeftSideMenu P:contains("' + landingPage + '")').parent().addClass('active');
        var controllerName = landingURL.split('/')[1];
        var path = appFilter.getApplicaitonURL();
        var relativeURL = path + landingURL;
        appFilter.renderPartialViewMasterUI(relativeURL, controllerName, 0, false);
    }
}

