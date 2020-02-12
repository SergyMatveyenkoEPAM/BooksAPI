using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using DynamoDb.Libs.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamoDb.Libs.DynamoDb
{
    public class UpdateItem : IUpdateItem
    {
        private readonly IGetBook _getBook;
        private static readonly string tableName = "Books";
        private readonly IAmazonDynamoDB _dynamoDbClient;

        public UpdateItem(IGetBook getBook, IAmazonDynamoDB dynamoDbClient)
        {
            _getBook = getBook;
            _dynamoDbClient = dynamoDbClient;
        }

        public async Task<Book> Update(string isbn, string title, string description)
        {
            var response = await _getBook.GetBooks(isbn);

            var currentTitle = response.Select(p => p.Title).FirstOrDefault();

            var replyDescription = response.Select(p => p.Description).FirstOrDefault();

            var request = RequestBuilder(isbn, title, description);

            var result = await UpdateItemAsync(request);

            return new Book
            {
                Isbn = result.Attributes["ISBN"].S,
                Title = result.Attributes["Title"].S,
                Description = result.Attributes["Description"].S
            };
        }

        private UpdateItemRequest RequestBuilder(string isbn, string title, string description)
        {
            var request = new UpdateItemRequest
            {
                Key = new Dictionary<string, AttributeValue>
                {
                    { "ISBN", new AttributeValue
                    {
                        S=isbn
                    }}
//                    {"ReplyDateTime",new AttributeValue
//                    {
//                        N=replyDateTime
//                    } }
                },
                //ExpressionAttributeNames = new Dictionary<string, string>
                //{
                //    {"#P","Price" }
                //},
                //ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                //{
                //    { ":newtitle", new AttributeValue
                //    {
                //        N=title
                //    }},
                //    {":currtitle", new AttributeValue
                //    {
                //        N=currentTitle
                //    }},
                //},

                //UpdateExpression = "SET #P = :newtitle",
                //ConditionExpression = "#P = :currtitle",

                AttributeUpdates = new Dictionary<string, AttributeValueUpdate>
                {
                    { "Title",new AttributeValueUpdate
                    {
                        Action = AttributeAction.PUT,
                        Value = new AttributeValue{S=title}
                    }},
                    {"Description", new AttributeValueUpdate
                    {
                        Action = AttributeAction.PUT,
                        Value = new AttributeValue{S=description}
                    } }
                },

                TableName = tableName,
                ReturnValues = "ALL_NEW"
            };

            return request;
        }

        private async Task<UpdateItemResponse> UpdateItemAsync(UpdateItemRequest request)
        {
            var response = await _dynamoDbClient.UpdateItemAsync(request);

            return response;
        }
    }
}
