############  HTML part ###########

<label for="ctl00_DropDownChoice">Choose a item:</label>

<select name="items" id="ctl00_DropDownChoice">
  <option value="_select">_select</option>  
</select>

############  HTML part ###########


// Cache Data
var items = items || {};

$(function () {
    if(item){
        items.forEach(item => {
            console.log(item);
            $("#ctl00_DropDownChoice").append(`<option value='${item["ID"]}'>${item["Title"]}</option>`);
        });
    }else{
      Employees();
    }
    
});

// only get 'ID,Title,num'  fields. 
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
    console.log(status);

    items = data.d.results;   // Store the result in cache
    items.forEach(item => {
        console.log(item);
        $("#ctl00_DropDownChoice").append(`<option value='${item["ID"]}'>${item["Title"]}</option>`);
    });
  
    var selItem = sessionStorage.getItem("SelItem");
    $('#CT100_DropdownChoice').val(selItem);    

    $('#ctl00_DropDownChoice').on('change', function () {

        let selected_item_id = $(this).val();
      
        sessionStorage.setItem("SelItem", selected_item_id);  

        alert(selected_item_id);  // shows the correct selected employees.
        // How do I get the other properties of the selected employees here?? such as email, designation, office in the REST query

        console.log(`title is: ${items.find(x => x.ID == selected_item_id)["Title"]}`);
        console.log(`num is: ${items.find(x => x.ID == selected_item_id)["num"]}`);
    });
}

function _error(xhr, status, error) {
    alert("Error occured. Please try again." + status + error);
}
