using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DynamoDb.Libs.DynamoDb
{
    public class DeleteItem : IDeleteItem
    {
        private readonly IGetBook _getBook;
        private readonly IAmazonDynamoDB _dynamoDbClient;
        private static readonly string tableName = "Books";

        public DeleteItem(IAmazonDynamoDB dynamoDbClient, IGetBook getBook)
        {
            _dynamoDbClient = dynamoDbClient;
            _getBook = getBook;
        }

        public async Task<DeleteItemResponse> ExecuteItemDelete(string isbn)
        {
            //var item = _getBook.GetBooks(isbn).Result.FirstOrDefault();

            var request = new DeleteItemRequest
            {
                TableName = tableName,
                Key = new Dictionary<string, AttributeValue>
                {
                    {"ISBN", new AttributeValue {S = isbn}}
                }
            };

            var response = await _dynamoDbClient.DeleteItemAsync(request);

            return response;
        }
    }
}
