using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BookShopProject.Domain.Entities.Author;
using BookShopProject.Domain.Entities.Book;
using BookShopProject.Domain.Entities.Genre;
using BookShopProject.Domain.Entities.Publisher;

namespace BookShopProject.Models
{
    public class HomePage : UserMinimal
    {
        public HomePage(List<BookDbTable> bookDb, List<GenreDbTable> genreDb, List<AuthorDbTable> authorDb,
            List<PublisherDbTable> publisherDb, Domain.Entities.User.UserMinimal user): base(user)
        {
            authors = new List<Author>();
            genres = new List<string>();
            books = new List<Book>();
            publishers = new List<Publisher>();

            var authorMapper = new MapperConfiguration(cfg => cfg.CreateMap<AuthorDbTable, Author>()).CreateMapper();
            var bookMapper = new MapperConfiguration(cfg => cfg.CreateMap<BookDbTable, Book>()).CreateMapper();
            var publisherMapper = new MapperConfiguration(cfg => cfg.CreateMap<PublisherDbTable, Publisher>())
                .CreateMapper();

            foreach (var author in authorDb.Take(8))
            {
                authors.Add(authorMapper.Map<Author>(author));
            }

            foreach (var genre in genreDb.Take(8))
            {
                genres.Add(genre.Name);
            }

            foreach (var book in bookDb.Take(8))
            {
                books.Add(bookMapper.Map<Book>(book));
            }

            foreach (var publisher in publisherDb.Take(8))
            {
                publishers.Add(publisherMapper.Map<Publisher>(publisher));
            }
        }

        public List<Author> authors;

        public List<string> genres;

        public List<Book> books;

        public List<Publisher> publishers;
    }
}