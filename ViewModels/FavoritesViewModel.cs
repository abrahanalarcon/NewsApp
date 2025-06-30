using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NewsApp.Models;
using NewsApp.Services;

namespace NewsApp.ViewModels
{
    public class FavoritesViewModel : BaseViewModel
    {
        private readonly INewsService _newsService;

        public ObservableCollection<NewsItem> FavoriteNews { get; } = new();

        public ICommand RefreshCommand { get; }
        public ICommand ItemTappedCommand { get; }
        public ICommand RemoveFavoriteCommand { get; }

        public FavoritesViewModel(INewsService newsService)
        {
            _newsService = newsService;
            Title = "Noticias Favoritas";

            RefreshCommand = new Command(async () => await RefreshFavoritesAsync());
            ItemTappedCommand = new Command<NewsItem>(async (item) => await OnItemTappedAsync(item));
            RemoveFavoriteCommand = new Command<NewsItem>(async (item) => await RemoveFavoriteAsync(item));
        }

        public async Task LoadFavoritesAsync()
        {
            if (IsBusy) return;

            IsBusy = true;

            try
            {
                var favorites = await _newsService.GetFavoriteNewsAsync();

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    FavoriteNews.Clear();
                    foreach (var item in favorites)
                    {
                        FavoriteNews.Add(item);
                    }
                });
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"No se pudieron cargar los favoritos: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task RefreshFavoritesAsync()
        {
            await LoadFavoritesAsync();
        }

        private async Task OnItemTappedAsync(NewsItem newsItem)
        {
            if (newsItem == null) return;

            await Shell.Current.GoToAsync($"newsdetail?id={newsItem.Id}");
        }

        private async Task RemoveFavoriteAsync(NewsItem newsItem)
        {
            if (newsItem == null) return;

            try
            {
                await _newsService.RemoveFromFavoritesAsync(newsItem.Id);
                FavoriteNews.Remove(newsItem);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Error al eliminar favorito: {ex.Message}", "OK");
            }
        }
    }
}
