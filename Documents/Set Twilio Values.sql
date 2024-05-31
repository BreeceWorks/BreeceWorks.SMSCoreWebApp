USE [SMSCommunication]
GO

UPDATE [dbo].[Configurations]
   SET [Value] ='Twilio:AuthToken' /****** put the Twilio AuthToken here ******/
 WHERE Name = 'Twilio:AuthToken'
GO

UPDATE [dbo].[Configurations]
   SET [Value] ='Twilio:Client:AccountSid' /****** put the Twilio AccountSid here ******/
 WHERE Name = 'Twilio:Client:AccountSid'
GO

UPDATE [dbo].[Configurations]
   SET [Value] ='Twilio:StatusCallbackUrl' /****** put the Twilio StatusCallbackUrl here ******/
 WHERE Name = 'Twilio:StatusCallbackUrl'
GO


INSERT INTO [dbo].[CompanyPhoneNumbers]
           ([PhoneNumber]
           ,[SMSProcessor]
           ,[IsActive])
     VALUES
           ('TWILIO PHONE NUMBER' /****** put the Twilio phone number here ******/
           ,'TwilioSMS'
           ,1)
GO


