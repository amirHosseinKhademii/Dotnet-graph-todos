namespace hot_demo.services
{
    public partial class Service : IBookService
    {
        public async Task<List<Book>> GetBooksAsync() =>
            await _booksCollection.Find(_ => true).ToListAsync();

        public async Task<Book?> GetBookByIdAsync(string id) =>
            await _booksCollection.Find(item => item.Id == id).FirstOrDefaultAsync();

        public async Task<Book> CreateBookAsync(string title)
        {
            var book = new Book()
            {
                Title = title
            };
            await _booksCollection.InsertOneAsync(book);
            return book;
        }

        public async Task<string> RemoveBookAsync(string id)
        {
            await _booksCollection.DeleteOneAsync(x => x.Id == id);
            return id;
        }

        public async Task<Book> UpdateBookAsync(Book book)
        {
            await _booksCollection.ReplaceOneAsync(x => x.Id == book.Id, book);
            return book;
        }

        public async Task<string> UpdateBookAuthorAsync(string id, string name)
        {
            var author = new Author() { Name = name };
            var update = Builders<Book>.Update.Set(book => book.Author, author);
            var filter = Builders<Book>.Filter.Where(item => item.Id == id);
            await _booksCollection.UpdateOneAsync(filter, update);
            return id;
        }
    }
}
