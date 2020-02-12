using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using DynamoDb.Libs.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamoDb.Libs.DynamoDb
{
    public class GetBook : IGetBook
    {
        private readonly IAmazonDynamoDB _dynamoDbClient;
        private static readonly string tableName = "Books";

        public GetBook(IAmazonDynamoDB dynamoDbClient)
        {
            _dynamoDbClient = dynamoDbClient;
        }

        public async Task<IEnumerable<Book>> GetBooks(string Isbn)
        {
            var queryRequest = RequestBuilder(Isbn);

            var result = await ScanAsync(queryRequest);

            return result.Items.Select(Map).ToList();
        }

        private Book Map(Dictionary<string, AttributeValue> result)
        {
            return new Book
            {
                Isbn = result["ISBN"].S,
                Title = result["Title"].S,
                Description = result["Description"].S
            };
        }

        private async Task<ScanResponse> ScanAsync(ScanRequest request)
        {
            var response = await _dynamoDbClient.ScanAsync(request);

            return response;
        }

        private ScanRequest RequestBuilder(string Isbn)
        {
            if (string.IsNullOrEmpty(Isbn))
            {
                return new ScanRequest
                {
                    TableName = tableName
                };
            }
            return new ScanRequest
            {
                TableName = tableName,
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {
                    {
                        ":v_Isbn", new AttributeValue{ S = Isbn}
                    }
                },
                FilterExpression = "ISBN = :v_Isbn",
                ProjectionExpression = "ISBN, Title, Description"
            };
        }
    }
}
