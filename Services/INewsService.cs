using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsApp.Models;

namespace NewsApp.Services
{
    public interface INewsService
    {
        Task<List<NewsItem>> GetLatestNewsAsync();
        Task<List<NewsItem>> GetFavoriteNewsAsync();
        Task AddToFavoritesAsync(NewsItem newsItem);
        Task RemoveFromFavoritesAsync(string newsId);
        Task<bool> IsFavoriteAsync(string newsId);
    }
}
