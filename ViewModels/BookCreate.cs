namespace FPTBook.ViewModels.Book;

public class BookCreate
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string Image { get; set; }
    public string Summary { get; set; }
    public int Price { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
}