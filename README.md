# .NET 5 and MVC 6 - Twilio Queues for Mac
A repository that shows you how to integrate RabitMQ with .NET 5 and .NET MVC 6 on a Mac.

* Libs
  * Contains helper libraries to help fetch data from the queue
* QueueReader
  * Reads off a predefined Queue and has a dependency on Libs for the Message contract
* QueueWritter
  * Is a .NET MVC 6 Project that is to be configured on your Twilio Telephone number. Just change the `SMS Request URL` to have the http://project-name.ngrok.io/Home/SayHello endpoint
