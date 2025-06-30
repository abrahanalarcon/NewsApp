using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsApp.Models;

namespace NewsApp.Services
{
    public class NewsService : INewsService
    {
        private static readonly List<NewsItem> _favoriteNews = new();

        public async Task<List<NewsItem>> GetLatestNewsAsync()
        {
            // Simular llamada a API
            await Task.Delay(1000);

            var news = new List<NewsItem>
            {
                new NewsItem
                {
                    Title = "Avances en Inteligencia Artificial",
                    Description = "Nuevos desarrollos en IA están transformando múltiples industrias...",
                    ImageUrl = "https://via.placeholder.com/300x200?text=AI+News",
                    PublishedAt = DateTime.Now.AddHours(-2),
                    Source = "Tech News",
                    Url = "https://example.com/ai-news"
                },
                new NewsItem
                {
                    Title = "Cambio Climático: Últimas Investigaciones",
                    Description = "Científicos publican nuevos datos sobre el impacto del cambio climático...",
                    ImageUrl = "https://via.placeholder.com/300x200?text=Climate+News",
                    PublishedAt = DateTime.Now.AddHours(-4),
                    Source = "Science Daily",
                    Url = "https://example.com/climate-news"
                },
                new NewsItem
                {
                    Title = "Tecnología Espacial en 2025",
                    Description = "Las misiones espaciales planificadas para este año prometen grandes descubrimientos...",
                    ImageUrl = "https://via.placeholder.com/300x200?text=Space+News",
                    PublishedAt = DateTime.Now.AddHours(-6),
                    Source = "Space News",
                    Url = "https://example.com/space-news"
                },
                new NewsItem
                {
                    Title = "Energías Renovables Crecen 40%",
                    Description = "El sector de energías renovables muestra un crecimiento sin precedentes...",
                    ImageUrl = "https://via.placeholder.com/300x200?text=Energy+News",
                    PublishedAt = DateTime.Now.AddHours(-8),
                    Source = "Energy Today",
                    Url = "https://example.com/energy-news"
                }
            };

            // Marcar favoritos
            foreach (var item in news)
            {
                item.IsFavorite = await IsFavoriteAsync(item.Id);
            }

            return news;
        }

        public async Task<List<NewsItem>> GetFavoriteNewsAsync()
        {
            await Task.CompletedTask;
            return _favoriteNews.ToList();
        }

        public async Task AddToFavoritesAsync(NewsItem newsItem)
        {
            await Task.CompletedTask;

            if (!_favoriteNews.Any(f => f.Id == newsItem.Id))
            {
                newsItem.IsFavorite = true;
                _favoriteNews.Add(newsItem);
            }
        }

        public async Task RemoveFromFavoritesAsync(string newsId)
        {
            await Task.CompletedTask;

            var favorite = _favoriteNews.FirstOrDefault(f => f.Id == newsId);
            if (favorite != null)
            {
                favorite.IsFavorite = false;
                _favoriteNews.Remove(favorite);
            }
        }

        public async Task<bool> IsFavoriteAsync(string newsId)
        {
            await Task.CompletedTask;
            return _favoriteNews.Any(f => f.Id == newsId);
        }
    }
}
