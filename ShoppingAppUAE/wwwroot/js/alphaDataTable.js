function tablePlugin(selector, url, columns , search ="search", callBackFunctionAfterFinish =  null) { 
  
   var x = $(selector).DataTable({ //"#citytbl"
        lengthChange: false,
        info: true,
        searching: true,
        search: true,
        ordering: false,
        responsive: true,
        searchable: true,
        processing: true,
       serverSide: true,
       "scrollX": true,
        language: {
            paginate: {
                next: "Next",
                previous: "Pervious"
            },
        
                "sSearch": search
            
        },
        ajax: {
            url: url,
            async: true,
            dataSrc: function (json) {
               
             
                return json.data;
            }

        },
       columns: columns,
       drawCallback: callBackFunctionAfterFinish

    });
    console.log(x);
    return x;

}


function tablePluginSelectBox(selector, url, columns, search = "search", callBackFunctionAfterFinish = null) {

    var x = $(selector).DataTable({ //"#citytbl"
      
        lengthChange: false,
        info: true,
        searching: true,
        search: true,
        ordering: false,
        responsive: true,
        searchable: true,
        processing: true,
        serverSide: true,
        "scrollX": true,
        language: {
            paginate: {
                next: "Next",
                previous: "Pervious"
            },
            "sSearch": search

        },
        columnDefs: [{
            orderable: false,
            className: 'select-checkbox',
            targets: 0
        }],
        ajax: {
            url: url,
            async: true,
            dataSrc: function (json) {


                return json.data;
            }

        },
        columns: columns,
        drawCallback: callBackFunctionAfterFinish

    });
    console.log(x);
    return x;

}

function tablePluginWithoutSearch(selector, url, columns) {

    var x = $(selector).DataTable({ //"#citytbl"
        lengthChange: false,
        info: true,
        searching: false,
        search: false,
        ordering: false,
        responsive: true,
        searchable: true,
        processing: true,
        serverSide: true,
        language: {
            paginate: {
                next: "Next",
                previous: "Pervious"
            }

        },
        ajax: {
            url: url,
            async: true,
            dataSrc: function (json) {

                console.log(json);
                return json.data;
            }

        },
        columns: columns

    });
    console.log(x);
    return x;

}

