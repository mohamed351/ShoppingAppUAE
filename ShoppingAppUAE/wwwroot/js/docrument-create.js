

var taxItems = [];
var unitTypes = [];
currentTaxDetailsRow = null;
getTaxs();
getUnitTypes();

$("#DateTimeIssued").datepicker({
    format: 'dd/mm/yyyy',
    todayHighlight: 'TRUE',
    autoclose: true,
});
$("#currencyID").select2();
$("#currencyID").on("change", function () {
    $currentCurrency = $(this);
    $.ajax({
        url: "/Document/GetExchangeRate?currencyCode=" + $currentCurrency.val(),
        method: "GET",
        success: function (result) {
            if (result != 0) {
                $("#ExchangeRate").val(result);
                changedValueInText();
               
            }
            else {
                $currentCurrency.val("");
                changedValueInText();

              
             
            }
        }
    });
});


$("#AddNewRow").click(function () {
    $("#invoice-line-details").append(buildRow($(".invoice-row").length,"InvoiceLinesViews"));
    $(".Invoice-Description").select2(
        {
            ajax: {
                url: "/Document/GetProducts",
                dataType: 'json',
                type: "GET",
                quietMillis: 50,
                data: function (term) {
                    return {
                        term: term.term,

                    };
                },
                processResults: function (data) {



                    return {

                        results: $.map(data, function (item) {
                            return {
                                text: item.name,
                                id: item.id,
                                price: item.price,
                                codeType: item.codeType

                            }
                        })
                    };
                }
            }

        });

    $(".unitType").select2({
        data: getUnitTypes()
    });

});
$(document).on("change", ".Invoice-Description", function () {
    let $currentSelector = $(this);
    $.ajax({
        url: "/Document/GetProductInfo",
        data: {
            id: $currentSelector.val()
        },
        method: "GET",
        success: function (result) {
            $currentSelector.parents("tr").find(".itemType").val(result.type);
            $currentSelector.parents("tr").find(".itemCode").val(result.code);
            $currentSelector.parents("tr").find(".price").val(result.price);
            calculateAmountInEGP();
            calculateTotalSales();
            calculateDiscountRate();


           
            calculateNetTotal();
          
           
        }
    })
});
function buildRow(index, listName) {
    return ` <tr class="invoice-row" id="invoice-line[${index}]">

                                   <td > <select required="required" name="${listName}[${index}].Description"  class="form-control Invoice-Description"> </select>  </td>
                                     <td > <input required="required" name="${listName}[${index}].ItemType" type="text" readonly class="form-control  itemType"  />   </td>
                                     <td > <input required="required" name="${listName}[${index}].ItemCode" type="text" readonly  class="form-control itemCode"  />   </td>
                                        <td> <input required="required" name="${listName}[${index}].Quantity"  type="number" step="any" class="form-control  quantity" /> </td>
                                    <td > <select required="required" name="${listName}[${index}].UnitTypeID" class="unitType form-control"> <option disabled selected value=""> -- select an option -- </option> </select>  </td>

                                    <td> <input required="required" name="${listName}[${index}].AmountSold" type="number"  step="any" class="form-control  price" />  </td>
                                      <td> <input required="required" name="${listName}[${index}].AmountEGP" type="number" step="any" class="form-control AmountInEGP" readonly   AmountInEGP" />  </td>
                                        <td> <input  required="required" name="${listName}[${index}].DiscountRate"type="number" class="form-control  DiscountRate" />  </td>
                                             <td> <input required="required" type="number" step="any" readonly class="form-control  DiscountAmount" /> </td>
                                                <td> <input  required="required" type="number" step="any" readonly class="form-control  netTotal" /> </td>
                                            <td> <input required="required" type="number" step="any"  readonly class=" form-control  SalesTotal" />  </td>
                                            <td> <input required="required" type="number" step="any"  readonly class=" form-control  Total" />  </td>
                                            <td>  <button  type="button"  class="btn btn-primary taxModel" > + </button> <input name="${listName}[${index}].JsonTax"   class='invoice-tax-details' type="hidden" /> </td>
                                            <td> <input required="required" type="number" step="any"  readonly class=" form-control  totalTaxFees" />  </td>

                                          <td>  <button type="button" class="btn btn-danger btn-delete-line"> - </button> </td>
                                    </tr>
                              `;

}

function calculateAmountInEGP() {
    $(".invoice-row").each(function () {
        $(this).find(".AmountInEGP").val(+$(this).find(".price").val() * +$("#ExchangeRate").val());
    });
}
$("#invoice-line-details").on("change", "input", function () {


    changedValueInText();
});
//$(".taxModel").click(function () {
//    $("#myModel").modal("show");
//});

$(document).on("click", ".taxModel", function () {

    currentTaxDetailsRow = $(this).parents("td").find(".invoice-tax-details");
    if (currentTaxDetailsRow.val() != "") {
        $("#taxBodyContainer").html("");
        buildInitalModal(currentTaxDetailsRow);
    }
    else { 
        //old row old
        $("#taxBodyContainer").html("");
    }
    $("#myModel").modal("show");

    $(".tax-select").select2({
        data: taxItems,
        width: '100%',
        dropdownParent: "#myModel"
    });

});

$(document).on("change", ".tax-select", function () {
    var tax = $(this).val()
    var rateSelector = $(this).parents("tr").find(".tax-rate");
    $.ajax({
        url: "/Document/GetTaxValue?code=" + tax,
        method: "GET",
        success: function (result) {
            rateSelector.val(result);
            changedValueInText();

        }
    });
});

$("#add-new-line-tax").on("click", function () {
    var newLine = $(newTaxBuilder())
    $(newLine.find(".tax-select")).select2({
        data: getTaxs(),
        width: '100%',
        dropdownParent: "#myModel"
    });

    $("#taxBodyContainer").append(newLine);
    changedValueInText();
});


function getTaxs() {


    if (taxItems.length == 0) {
        $.ajax({
            url: "/Document/GetTaxAndSub",
            method: "GET",
            success: function (result) {

                taxItems = result.map(function (a) {
                    return {
                        id: a.code,
                        text: a.description,
                        children: a.subTypes.map(function (c) {
                            return {
                                text: c.arabicDescription,
                                id: c.code
                            }
                        })
                    }
                })
                return taxItems;
            }
        });

    }
    else {
        return taxItems;
    }

}

function newTaxBuilder() {
    return `
        <tr>
               <td style="width:50%">
             <select style="width:100%" class="tax-select form-control">
                    <option disabled selected value> -- select an option -- </option>
                </select>
               </td>

                     <td>
                             <input readonly type="text" class="form-control tax-rate" />
                      </td>

                        <td>
                              <button class="btn btn-danger btn-delete-row"> X</button>
                            </td>

           </tr>
   `
}

$(document).on("click", ".btn-delete-row", function () {
    $(this).parents("tr").remove();
});

$("#btn-save-changes").on("click", function () {
    currentTaxDetailsRow.val(convertDetailsTaxToJson());
    $("#myModel").modal("hide");
    changedValueInText();
});
function convertDetailsTaxToJson() {
    let arrayOfTax = [];
    $("#taxBodyContainer tr").each(function () {
        
        arrayOfTax.push({
            taxType: getTaxTypeFromSubTax($(this).find(".tax-select").val()),
            subTax: $(this).find(".tax-select").val(),
            value: $(this).find(".tax-rate").val(),
            amount: $(this).find(".SalesTotal").val()
        });
    });
    console.log(arrayOfTax);
    return JSON.stringify(arrayOfTax);
}

function getTaxTypeFromSubTax(subTaxCode) {
    for (var tax of getTaxs()) {

        for (var subTaxIterator of tax.children) {
            if (subTaxIterator.id == subTaxCode) {
                return tax.id;
            }
        }
    }
}

function getUnitTypes(callBackfunction = null) {
    
    if (unitTypes.length == 0) {
        $.ajax({
            url: "/Document/GetUnitTypes",
            method: "GET",
            success: function (result) {
                unitTypes = result;
                if (callBackfunction != null) {
                    callBackfunction(unitTypes);
                }
            }
        });
        return unitTypes;
    }
    else {
        if (callBackfunction != null) {
            callBackfunction(unitTypes);
        }
        return unitTypes;
    }
}


function calculateTotalSales() {
    $(".invoice-row").each(function () {
        $(this).find(".SalesTotal").val(+$(this).find(".AmountInEGP").val() * +$(this).find(".quantity").val());
    });

}
$(document).on("change", ".quantity", function () {
  
    $("#TotalSalesAmount").val(calculateTotal(".SalesTotal"));
    changedValueInText();
    
});

function calculateDiscountRate() {
    $(".invoice-row").each(function () {
        $(this).find(".DiscountAmount").val((+$(this).find(".DiscountRate").val() / 100) * +$(this).find(".SalesTotal").val())
    });
}
$(document).on("change", ".DiscountRate", function () {
    changedValueInText();
});

function calculateNetTotal() {
    $(".invoice-row").each(function () {
        $(this).find(".netTotal").val(+$(this).find(".SalesTotal").val() - (+$(this).find(".DiscountAmount").val()))
    });
}

function deserlizeSubTax(selector) {
    if ($(selector).val() != "") {
        return JSON.parse($(selector).val())
    }
    else {
        return [];
    }
}
function buildInitalModal(selector) {
    if (deserlizeSubTax(selector).length != 0) {
        $("#taxBodyContainer tr").html("");
        var oldArrayOfTax = deserlizeSubTax(selector);
        for (var item of oldArrayOfTax) {

        var oldValue =    $(`
                        <tr>
                           <td style="width:50%">
                               <select style="width:100%" class="tax-select form-control">
                                   <option disabled="" selected="" value=""> -- select an option -- </option>
                               </select>
                           </td>

                           <td>
                               <input readonly="" type="text" value=${item.value} class="form-control tax-rate">
                           </td>


                            <td>
                              <button class="btn btn-danger btn-delete-row"> X</button>
                            </td>
                       </tr>

            `);

            
            oldValue.find(".tax-select").select2({
                data: taxItems
            });
            oldValue.find(".tax-select").val(item.subTax)
            oldValue.find(".tax-select").trigger("change");
            $("#taxBodyContainer").append(oldValue);
        }
    }
}

function calculateTotal(selector) {
    let total = 0;
    $("#invoice-line-details tr").each(function () {
        total+=  +$(this).find(selector).val();
    });
    return total;
}

function changedValueInText() {
    calculateAmountInEGP();
    calculateTotalSales();
    calculateDiscountRate();
    calculateNetTotal();
   
    $("#TotalSalesAmount").val(calculateTotal(".SalesTotal"));
    $("#TotalDiscountAmount").val(calculateTotal(".DiscountAmount"));
    $("#NetAmount").val(calculateTotal(".netTotal"));
  
    calculateTaxFees();
    buildTaxTotalTable();
    calculateTotalAfterTax();
    $("#totalAmount").val(calculateTotalAmount());
    
 
}

function calculateTaxFromSalesTotal() {
   
}

function calculateTaxFees() {
    let totalFees = 0;
    $("#invoice-line-details tr").each(function () {
   
        if ($(this).find(".invoice-tax-details").val() != "") {
            let salesTotal = +$(this).find(".netTotal").val();
            let listOfTax = JSON.parse($(this).find(".invoice-tax-details").val()).map(a => {
                console.log(salesTotal * (+a.value / 100));
                return {
                    ...a,
                    amount: (salesTotal * (+a.value / 100)).toFixed(2),

                }
            });
            console.log(listOfTax);
            $(this).find(".invoice-tax-details").val(JSON.stringify(listOfTax));

            $(this).find(".totalTaxFees").val(sumTotalTax(listOfTax));
        }
        else {
            $(this).find(".totalTaxFees").val(0);
        }

    });
    
}
function sumTotalTax(list) {
    let total = 0;
    for (var item of list) {
        total += +item.amount;
    }
    return total;
}
function calculateTypeTax() {
    let taxArray = [];
    $("#invoice-line-details tr").each(function () {
        let taxdetails = $(this).find(".invoice-tax-details").val()
        if (taxdetails != "") {
            filterTaxType(JSON.parse(taxdetails), taxArray);
        }
    });
    console.log(taxArray);
    return taxArray;
}
function filterTaxType(list,array) {
      
    for (var item of list) {
        console.log(item);
        if (!array.find(a => a.taxType == item.taxType)) {
            array.push({ taxType: item.taxType, amount: (+item.amount) })
        }
        else {
           
            var x = array.find(a => a.taxType == item.taxType);
            console.log(x);
            console.log(item.amount);
            x.amount += (+item.amount);
          

        }
       
    }
}

function buildTaxTotalTable() {
    $("#tax-totls").html("");
    var taxTotalArray = calculateTypeTax()
  
    for (var item of taxTotalArray) {
        console.log("draw", item);
        $("#tax-totls").append(`
          <tr>
            <td>
            ${item.taxType}
           </td>
            <td>
                ${item.amount.toFixed(2)}
            </td>
        </tr>
        `);
    }
}
function calculateTotalAfterTax() {
    $("#invoice-line-details tr").each(function () {
        console.log("total",+$(this).find(".netTotal").val() + +$(this).find(".totalTaxFees").val());
        $(this).find(".Total").val(+$(this).find(".netTotal").val() + +$(this).find(".totalTaxFees").val())
    });
}

//function deleteInvoiceLine() {
//    $(".btn-delete-line").
//}
$(document).on("click", ".btn-delete-line", function () {
    $(this).parents("tr").remove();
    reIndexing();
    changedValueInText();
    
});
function reIndexing() {
  

    $("#invoice-line-details tr").each(function (index) {
        $(this).find("input,select").each(function () {

           $(this).attr("name",newIndex($(this), index));
        });
        
    })
}
function newIndex(selector,index) {
    var regex = /\d+/g;
    console.log($(selector).attr("name"));
    var string = $(selector).attr("name");
    if (string) {
        var matches = string.match(regex);  // creates array from matches

        return string.replace(matches, index);
    }
    else {
        if ($(selector).attr("name")) {
            return $(selector).attr("name");
        }
        else {
            return "";
        }

    }
}
function calculateTotalAmount() {
   let  totalTax = 0
    $("#invoice-line-details .totalTaxFees").each(function () {
        totalTax+=   +$(this).val();
    });
    return +$("#NetAmount").val() + totalTax;
}
$("#ActivityCodeID").select2();
$("#IssuerID").select2();
$("#ReceiverID").select2();