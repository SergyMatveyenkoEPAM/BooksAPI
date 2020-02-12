using Amazon.DynamoDBv2.Model;
using System.Threading.Tasks;

namespace DynamoDb.Libs.DynamoDb
{
    public interface IDeleteItem
    {
        Task<DeleteItemResponse> ExecuteItemDelete(string isbn);
    }
}
