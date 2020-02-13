using DynamoDb.Libs.DynamoDb;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookAPI.Controllers
{
    [Route("api/DynamoDB")]
    [ApiController]
    public class DynamoDbController : ControllerBase
    {
        private readonly IPutItem _putItem;
        private readonly IGetBook _getBook;
        private readonly IUpdateItem _updateItem;
        private readonly IDeleteItem _deleteItem;

        public DynamoDbController(IPutItem putItem, IGetBook getBook, IUpdateItem updateItem, IDeleteItem deleteItem)
        {
            _putItem = putItem;
            _getBook = getBook;
            _updateItem = updateItem;
            _deleteItem = deleteItem;
        }

        [HttpPost]
        [Route("addbook")]
        public IActionResult AddBook([FromQuery] string isbn, string title, string description)
        {
            _putItem.AddNewEntry(isbn, title, description);

            return Ok();
        }

        [Route("getbooks")]
        public async Task<IActionResult> GetBooks([FromQuery] string Isbn)
        {
            var response = await _getBook.GetBooks(Isbn);

            return Ok(response);
        }

        [HttpPut]
        [Route("updatebook")]
        public async Task<IActionResult> UpdateBook([FromQuery] string isbn, string title, string description)
        {
            var response = await _updateItem.Update(isbn, title, description);

            return Ok(response);
        }

        [HttpDelete]
        [Route("deletebook")]
        public async Task<IActionResult> DeleteBook([FromQuery] string isbn)
        {
            var response = await _deleteItem.ExecuteItemDelete(isbn);

            return Ok(response);
        }
    }
}