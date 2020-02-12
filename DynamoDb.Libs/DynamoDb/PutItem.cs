using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DynamoDb.Libs.DynamoDb
{
    public class PutItem : IPutItem
    {
        private readonly IAmazonDynamoDB _dynamoDbClient;
        private static readonly string tableName = "Books";

        public PutItem(IAmazonDynamoDB dynamoDbClient)
        {
            _dynamoDbClient = dynamoDbClient;
        }

        public async Task AddNewEntry(string isbn, string title, string description)
        {
            var queryRequest = RequestBuilder(isbn, title, description);

            await PutItemAsync(queryRequest);
        }

        private PutItemRequest RequestBuilder(string isbn, string title, string description)
        {
            var item = new Dictionary<string, AttributeValue>
            {
                {"ISBN", new AttributeValue {S = isbn}},
                {"Title", new AttributeValue {S = title}},
                {"Description", new AttributeValue {S = description}}
            };

            return new PutItemRequest
            {
                TableName = tableName,
                Item = item
            };
        }

        private async Task PutItemAsync(PutItemRequest request)
        {
            await _dynamoDbClient.PutItemAsync(request);
        }
    }
}
