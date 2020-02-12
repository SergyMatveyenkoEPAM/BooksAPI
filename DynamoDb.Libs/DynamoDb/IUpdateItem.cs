using DynamoDb.Libs.Models;
using System.Threading.Tasks;

namespace DynamoDb.Libs.DynamoDb
{
    public interface IUpdateItem
    {
        Task<Book> Update(string isbn, string title, string description);
    }
}
