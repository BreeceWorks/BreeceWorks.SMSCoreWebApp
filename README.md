# BreeceWorks.SignalRTwilioChat
A demo of communication betwee a SignalR chat app and a mobile phone via Twilio sms.  

It is a simple proof of concept of how an agent could use a chat app to send messages to a clients mobile phone via Twilio's sms methods.  The client could then respond by sending an sms message, which Twilio would then deliver to a webhook, which is part of the code.  The webhook then relays the response to the agent's chat app via Signal R.  In the demo, the webhook sends to directly to the Signal R hub.  This would better be done by another service.  For example, the webhook could insert the customer's sms message info into a RabbitMQ.  A separate service would consume the message and perform the business logic to determine where to send it.

The Blazor app and the web api both authenticate using the demo IDP provided by Duende Software.  You can login as either Bob or Alice.  The SignalR hub shows how it is possible to specify which user receives the message.  In this case, you will only see messages sent by the customer if you login as Bob.  

The webhook on the SMSController in the web api project has a filter to only allow calls that have the properly hashed message in the header that Twilio provides in all the calls it makes as part of its authentication.

In order to receive the message relayed from the customer by Twilio, the web api has to reachable by Twilio.  This means deploying it to a publicly accessible server.  During development and testing, you can also use something like Ngrok to temporarily expose the api running on your local machine.

Twilio's documentation goes into detail on how to set up the accounts and phone numbers, specify webhooks, etc.  You will need to supply the info from your Twilio account into the appsettings in order to run the solution.
