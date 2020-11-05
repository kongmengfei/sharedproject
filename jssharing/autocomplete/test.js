$(document).ready(function () {
    Employees();
});

function Employees() {
    var listName = "EmployeeList";
    var url = _spPageContextInfo.siteAbsoluteUrl;

    getListItems(listName, url, function (data) {
        var items = data.d.results;
        for (var i = 0; i < items.length; i++) {
            var itemId = items[i].Employee_Name,
                itemVal = items[i].Employee_Name;
            $("#ctl00_DropDownChoice").append('<option value="' + itemId + '"selected>' + itemVal + '</option>');
        }

        $("#ctl00_DropDownChoice").each(function () {
            $('option', this).each(function () {
                if ($(this).text() == 'Select') {
                    $(this).attr('selected', 'selected')
                };
            });
        });

        $('#ctl00_DropDownChoice').on('change', function () {
            alert($(this).val());  // shows the correct selected employees.
            // How do I get the other properties of the selected employees here?? such as email, designation, office in the REST query

        });

    }, function (data) {
        alert("Error occured. Please try again.");
    });
}

function getListItems(listName, siteurl, success, failure) {
    $.ajax({
        url: siteurl + "/_api/web/lists/getbytitle('" + listName + "')/items?$select=Employee_Number,Employee_Name,Email_Address,Manager_Name,Department_Code,Office,Designation&$filter=((Department_Code eq 100) or (Department_Code eq 200) or (Department_Code eq 500) or (Department_Code eq 600) or (Department_Code eq 700) or (Department_Code eq 800) or (Department_Code eq 900))&$orderby=Employee_Name asc",
        method: "GET",
        headers: { "Accept": "application/json; odata=verbose" },
        success: function (data) {
            success(data);
        },
        error: function (data) {
            failure(data);
        }
    });
}

