Simple test cosmosdb .NET console app for testing/ troubleshooting purposes only. This app either puts an item into a CosmosDB collection, or reads all items from the collection, depending upno which option you choose.

Restore or Nuget-install the following packages:
Newtonsoft.Json;
Microsoft.Azure.Cosmos

Replace the YOUR_COSMOS_ACCOUNT_NAME , YOUR_COSMOS_KEY== , YOUR_COSMOS_DATABASE_NAME , and YOUR_COSMOS_CONTAINER_NAME in the app.config.

Modify the someEntity class in Program.cs according to the type of entity.

Build using C# 7.1 or later.

Run, and when prompted either press 1 to read all items from the collection or press 2 to add an item to the collection.
If you choose to add an item to the collection, enter the values (id and someProperty (which map to the document id and partition key respectively, but you can change to something else in the someEntity class)).

