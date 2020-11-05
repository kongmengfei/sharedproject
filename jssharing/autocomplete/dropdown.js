/* Created from Chrome SP Editor */

var items = items || {};

$(function () {
    Employees();
});

function Employees() {
    var listName = "kkkk";

    $.ajax({
        url: `${_spPageContextInfo.siteAbsoluteUrl}/_api/web/lists/getbytitle('${listName}')/items?$select=ID,Title,num`,
        method: "GET",
        headers: { "Accept": "application/json; odata=verbose" },
        success: _success,
        error: _error
    });
}

function _success(data, status) {

    console.log(data);
    console.log(status);

    items = data.d.results;
    items.forEach(item => {
        console.log(item);
        $("#ctl00_DropDownChoice").append(`<option value='${item["ID"]}'>${item["Title"]}</option>`);
    });

    $('#ctl00_DropDownChoice').on('change', function () {

        let selected_item_id = $(this).val();

        alert(selected_item_id);  // shows the correct selected employees.
        // How do I get the other properties of the selected employees here?? such as email, designation, office in the REST query

        console.log(`title is: ${items.find(x => x.ID == selected_item_id)["Title"]}`);
        console.log(`num is: ${items.find(x => x.ID == selected_item_id)["num"]}`);
    });
}

function _error(xhr, status, error) {
    alert("Error occured. Please try again." + status + error);
}

