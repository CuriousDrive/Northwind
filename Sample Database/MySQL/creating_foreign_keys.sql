-- first let's list down the foreign keys that we need to make and then write scripts to do that

-- categories
-- customers 
-- employees
-- orderdetails

ALTER TABLE orderdetails ADD FOREIGN KEY (OrderId) REFERENCES Orders(OrderId);
ALTER TABLE orderdetails ADD FOREIGN KEY (ProductId) REFERENCES products(ProductId);

ALTER TABLE orders 
ADD CONSTRAINT FK_OrdersCustomers
FOREIGN KEY (CustomerId) REFERENCES customers(CustomerId);

ALTER TABLE orders 
ADD CONSTRAINT FK_OrdersEmployees
FOREIGN KEY (EmployeeId) REFERENCES employees(EmployeeId);

desc orders

