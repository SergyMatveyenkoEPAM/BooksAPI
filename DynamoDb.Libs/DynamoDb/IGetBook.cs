using DynamoDb.Libs.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DynamoDb.Libs.DynamoDb
{
    public interface IGetBook
    {
        Task<IEnumerable<Book>> GetBooks(string Isbn);
    }
}
