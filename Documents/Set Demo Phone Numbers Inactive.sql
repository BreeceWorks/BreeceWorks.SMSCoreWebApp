USE [SMSCommunication]
GO

UPDATE [dbo].[CompanyPhoneNumbers]
   SET [IsActive] = 0
 WHERE SMSProcessor = 'DemoSMS'
GO


