USE ShoppingApplicationUAE

select orders.OrderID,orders.FirstName ,
orders.LastName , orders.DateTimeIssued , 
orders.CurrencyCode , orders.Address , orders.OrderPhone , orders.OriginalPhone ,
orders.Total , orders.TotalShippingFees 
from orders  inner join OrderDetails on orders.OrderID = OrderDetails.OrderID
inner join AspNetUsers as currentUser on currentUser.Id = orders.UserID 
inner join Products as prod on prod.ProductID = OrderDetails.ProductID