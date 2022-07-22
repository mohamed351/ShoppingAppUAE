$(document).ready(function () {


   
    function getWishListServer(callBack) {
        let wishList = localStorage.getItem("wishList");
        if (!wishList) {
            localStorage.setItem("wishList", JSON.stringify([]));
          wishList=  localStorage.getItem("wishList")
        }

        $.ajax({
            url: "/Home/GetWishList",
            method: "GET",
            success: function (result) {
                setWishListLocal(result);
                if (callBack) {
                    callBack(result);
                }
            }
        });
    }
    getWishListServer();

    function setWishListLocal(arrayOfProductIds) {
        localStorage.setItem("wishList", JSON.stringify(arrayOfProductIds));
    }
    

   


 
});

function getWishListLocal() {
    return localStorage.getItem("wishList");
}

function IsProductIn(productId) {
    let value = JSON.parse(getWishListLocal()).find(a => a === productId);
    if (value !== undefined) {
        return true;
    }
    else if (value === null) {
        return false;
    }
    else {
        return false;
    }

}

function DeleteToList(productId) {
    let list = JSON.parse(localStorage.getItem("wishList"));
    list = list.filter(a => a !== (+productId));
    localStorage.setItem("wishList", JSON.stringify(list));
   
}

function AddToList(productId) {
    let list = JSON.parse(localStorage.getItem("wishList"));
    list.push(+productId);


    localStorage.setItem("wishList", JSON.stringify(list));
}

