var items = items || {};

$(function () {
    console.log("Jquery Ready. Time: " + ((Date.now()) - performance.timing.navigationStart));

    if ($.isEmptyObject(items)) {
        Employees();
    } else {
        items.forEach(item => {
            console.log(item);
            $("select[id$='_$DropDownChoice'][title='mychoice']").append(`<option data-id='${item["ID"]}' value='${item["Title"]}'>${item["Title"]}</option>`);
        });

        var selItem = sessionStorage.getItem("SelItem");
        $("select[id$='_$DropDownChoice'][title='mychoice']").val(selItem);
    }

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
        $("select[id$='_$DropDownChoice'][title='mychoice']").append(`<option data-id='${item["ID"]}' value='${item["Title"]}'>${item["Title"]}</option>`);
    });

    var selItem = sessionStorage.getItem("SelItem");
    $("select[id$='_$DropDownChoice'][title='mychoice']").val(selItem);

    $("select[id$='_$DropDownChoice'][title='mychoice']").on('change', function () {

        var optionSelected = $("option:selected", this);
        let selected_item_id = optionSelected.attr('data-id');

        sessionStorage.setItem("SelItem", optionSelected.val());

        console.log(optionSelected.val());
        console.log(optionSelected.text());

        alert(selected_item_id);  // shows the correct selected employees.
        // How do I get the other properties of the selected employees here?? such as email, designation, office in the REST query

        console.log(`title is: ${items.find(x => x.ID == selected_item_id)["Title"]}`);
        console.log(`num is: ${items.find(x => x.ID == selected_item_id)["num"]}`);

        let s = `title is: ${items.find(x => x.ID == selected_item_id)["Title"]}
num is: ${items.find(x => x.ID == selected_item_id)["num"]}`;

        alert(s);
    });
}

function _error(xhr, status, error) {
    alert("Error occured. Please try again." + status + error);
}
