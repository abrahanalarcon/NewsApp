using NewsApp.Models;

namespace NewsApp.Views;

[QueryProperty(nameof(NewsId), "id")]
public partial class NewsDetailPage : ContentPage
{
    private string _newsId = string.Empty;

    public string NewsId
    {
        get => _newsId;
        set
        {
            _newsId = value;
            LoadNewsDetail();
        }
    }
    public NewsDetailPage()
	{
		InitializeComponent();
	}

    private void LoadNewsDetail()
    {
        // Here loading details based ID
        
        var newsItem = new NewsItem
        {
            Id = NewsId,
            Title = "Noticia de Ejemplo",
            Description = "Esta es una descripción detallada de la noticia seleccionada. Aquí podrías mostrar el contenid.",
            ImageUrl = "https://via.placeholder.com/400x200?text=News+Detail",
            Source = "Fuente de Noticias",
            PublishedAt = DateTime.Now,
            Url = "https://example.com/news"
        };

        BindingContext = newsItem;
    }
}