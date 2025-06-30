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
    public class LatestNewsViewModel : BaseViewModel
    {
        private readonly INewsService _newsService;

        public ObservableCollection<NewsItem> News { get; } = new();

        public ICommand RefreshCommand { get; }
        public ICommand ItemTappedCommand { get; }
        public ICommand ToggleFavoriteCommand { get; }

        public LatestNewsViewModel(INewsService newsService)
        {
            _newsService = newsService;
            Title = "Últimas Noticias";

            RefreshCommand = new Command(async () => await RefreshNewsAsync());
            ItemTappedCommand = new Command<NewsItem>(async (item) => await OnItemTappedAsync(item));
            ToggleFavoriteCommand = new Command<NewsItem>(async (item) => await ToggleFavoriteAsync(item));

            Task.Run(async () => await LoadNewsAsync());
        }

        private async Task LoadNewsAsync()
        {
            if (IsBusy) return;

            IsBusy = true;

            try
            {
                var news = await _newsService.GetLatestNewsAsync();

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    News.Clear();
                    foreach (var item in news)
                    {
                        News.Add(item);
                    }
                });
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"No se pudieron cargar las noticias: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task RefreshNewsAsync()
        {
            await LoadNewsAsync();
        }

        private async Task OnItemTappedAsync(NewsItem newsItem)
        {
            if (newsItem == null) return;

            await Shell.Current.GoToAsync($"newsdetail?id={newsItem.Id}");
        }

        private async Task ToggleFavoriteAsync(NewsItem newsItem)
        {
            if (newsItem == null) return;

            try
            {
                if (newsItem.IsFavorite)
                {
                    await _newsService.RemoveFromFavoritesAsync(newsItem.Id);
                    newsItem.IsFavorite = false;
                }
                else
                {
                    await _newsService.AddToFavoritesAsync(newsItem);
                    newsItem.IsFavorite = true;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Error al actualizar favoritos: {ex.Message}", "OK");
            }
        }
    }
}
