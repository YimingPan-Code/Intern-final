# Intern-final
#I have a summer Intern in Amazon. In this internship, I first established an identity authentication system based on Microsoft Identity and Cognito. In this system, users can register, log in, log out, reset passwords and manage user information. Among them, customers can manage personal addresses, set up main addresses, and convert other addresses into main addresses. The entire authentication system uses Razor pages as the framework. Secondly, the database structure diagram was established, and the connection between tables was established. Select Microsoft SQL Server as the engine to build the database in RDS. Set up an S3 bucket to store images, CSS and related documents. Then set up an exhibit display page, read all the books directly from the database, and list all the products without the user searching. The shopping cart and wish list must be available to anonymous users, so when opening the webpage, a GUID will be stored in a cookie as a unique IP, and a shopping cart will be created based on this GUID. When a user registers a new account or logs in, the webpage will transfer the items in the anonymous user's shopping cart to the logged-in user's shopping cart by default. Finally, the user can check out in the shopping cart, select the delivery address and create a new delivery address during the checkout.  In the homepage, customer can also find the hottest book which is based on sales.
