/* Created from Chrome SP Editor */

$(function () {
    $("#Emp_NameTextBox").autocomplete({
        minLength: 1,
        source: function (request, response) {
            var restUrl = _spPageContextInfo.siteAbsoluteUrl;
            restUrl += "/_api/web/Lists/GetByTitle('kkkk')/Items";
            restUrl += "?$select=num,Title";
            $.ajax({
                contentType: "application/json;odata=verbose",
                headers: { "accept": "application/json;odata=verbose" },
                url: restUrl,
                dataType: 'json',
                success: function (data) {
                    response($.map(data.d.results, function (value, key) {
                        return {
                            label: value.Title,
                            object: value
                        };
                    }));
                }
            });
        }
    });
});

