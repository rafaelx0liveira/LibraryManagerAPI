using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibraryManagerAPI.Domain.ValueObjects.Output
{
    public class BookResultVO
    {
        public BookResultVO(int id, string title, string author, string iSBN, int year, string language, int quantity)
        {
            Id = id;
            Title = title;
            Author = author;
            ISBN = iSBN;
            Year = year;
            Language = language;
            Quantity = quantity;
        }

        public int Id { get; }

        public string Title { get; }

        public string Author { get; }

        public string ISBN { get; }

        public int Year { get; }

        public string Language { get; }

        public int Quantity { get; set; }
    }
}
