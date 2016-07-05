# Storify Backend
This is backend for [Storify AngularJS] (../../../Storify.Angular.js) project written in C#, Asp.Net Web API. It uses EF Database First approach.

### Requirements:
1. Visual Studio 2012 or above
2. SQL Server Express 2012 or above 

### Local setup:
1. Clone repository 
```
$ git clone https://github.com/YOUR_USERNAME/Storify.git

YOUR_USERNAME -> Change to your GitHub username
```
2. Build Application to install dependencies:  `Ctrl+Shift+B`
3. Deploy Database: 
```
 - Open Storify.Database project from the list
 - Double click Storify.Database.publish.xml file. If you are using SQL Express change datasource to .\SQLExpress, otherwise leave it as localhost
 - Click Publish. It will start creating database with testing data.
```

4. Run Application `F5`. It will publish project in IIS

Now you can run [Storify AngularJS] (../../../Storify.Angular.js) application and add, edit, delete data into Database.

Thanks for testing my simple  project. In case you make have any question, please write me.

