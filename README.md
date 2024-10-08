# 1. Introduction
The MiaTicket_Server is a backend system that handles functionalities/requests to serve the MiaTicket - Ticketing Web Platform For Events using ASP.NET.

# 2. System Requirements
- Visual Studio 2022
- SQL Server Management Studio 20
- SQL Server 2022
- Docker 

# 3. Enviroment
- Create new instance in the SQL Server with any name you like.
- Add enviroment variables.

```
`MiaTickConnectionString` = "Your Server Connection String"  
`MiaTickIssuer` = "Your Issuer"  
`MiaTickAudience` = "Your Audience"  
`SecretKey` = "Your Secret Key"  
`CloudinaryUrl` = "Cloudinary URL" (Register at [Cloudinary](https://cloudinary.com/) to get this)  
`SmtpServer` = "Your SMTP Server"  
`SmtpPort` = "Your SMTP Port"  
`SmtpEmail` = "Your SMTP Email"  
`SmtpAppPassword` = "Your SMTP App Password"  
`RedisConnectionString` = "Your Redis Connection String"  
`VNPay_TMNCODE` = "Your VNPay TMNCODE"  (Register at [VNPAY SANBOX](https://sandbox.vnpayment.vn/apis/) to get this)  
`VNPay_HashSecret` = "Your VNPay Hash Secret" (Register at [VNPAY SANBOX](https://sandbox.vnpayment.vn/apis/) to get this)  
`ZaloPay_Key1` = "Your ZaloPay Key1" (Register at [ZaloPay SANBOX](https://docs.zalopay.vn/v1/start/) to get this)  
`ZaloPay_Key2` = "Your ZaloPay Key2" (Register at [ZaloPay SANBOX](https://docs.zalopay.vn/v1/start/) to get this)  
`RabbitMQConnectionString` = "Your RabbitMQ Connection String"  
`RabbitMQUserName` = "Your RabbitMQ Username"  
`RabbitMQPassword` = "Your RabbitMQ Password"  
```

# 4. How to use
- Step 1: Start docker and open a terminal in the project root directory.
- Step 2: Run the command "docker compose up -d" to start the service applications.
- Step 3: Access https://localhost:8081/swagger/index.html to test the API on localhost

# 5. Other related information
- Web : [website](https://github.com/ducdevday/MiaTicket_Website)

# 6. Contact Information
Email: ducdevday@gmail.com






