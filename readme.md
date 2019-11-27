Simple test cosmosdb .NET console app for testing/ troubleshooting purposes only. This app puts an item into a CosmosDB collection.

Nuget-install the following packages:
Newtonsoft.Json;
Microsoft.Azure.Cosmos

Replace the YOUR_COSMOS_ACCOUNT_NAME , YOUR_COSMOS_KEY== , YOUR_COSMOS_DATABASE_NAME , and YOUR_COSMOS_CONTAINER_NAME in the app.config.

Modify the someEntity class in Program.cs according to the type of entity.

Build using C# 7.1 or later.

Run, and when prompted enter the values (id and someProperty (which map to the document id and partition key respectively, but you can change to something else in the someEntity class)).

