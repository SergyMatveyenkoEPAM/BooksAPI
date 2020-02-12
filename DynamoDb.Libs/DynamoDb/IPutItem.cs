using System.Threading.Tasks;

namespace DynamoDb.Libs.DynamoDb
{
    public interface IPutItem
    {
        Task AddNewEntry(string isbn, string title, string description);
    }
}
