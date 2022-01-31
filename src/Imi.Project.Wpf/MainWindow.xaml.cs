using Imi.Project.Wpf.Core.Entities;
using Imi.Project.Wpf.Core.Interfaces;
using Imi.Project.Wpf.Core.Services;
using Imi.Project.Wpf.Infrastructure.Services;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Imi.Project.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(IAuthService authService,
                          IGamesService gamesService,
                          ILocationsService locationsService,
                          IRacketsService racketsService,
                          IShuttleCocksService shuttleCocksService)
        {
            InitializeComponent();
            _authService = authService;
            _gamesService = gamesService;
            _locationsService = locationsService;
            _racketsService = racketsService;
            _shuttleCocksService = shuttleCocksService;
        }

        #region Services
        private readonly IAuthService _authService;
        private readonly IGamesService _gamesService;
        private readonly ILocationsService _locationsService;
        private readonly IRacketsService _racketsService;
        private readonly IShuttleCocksService _shuttleCocksService;
        #endregion
        #region Properties & Variables
        private bool isEditing;
        public ObservableCollection<GameModel> Games { get; set; }
        public ObservableCollection<LocationModel> Locations { get; set; }
        public ObservableCollection<RacketModel> Rackets { get; set; }
        public ObservableCollection<ShuttleCockModel> ShuttleCocks { get; set; }



        #endregion
        #region Methods
        private async Task ShowLoggedInView()
        {
            EndLoggingInAnim();
            StartLoadingDataAnim();
            await RetrieveData();
            EndLoadingDataAnim();
            ShowGamesView();
        }

        private void ShowLoggingInView()
        {
            grpLogin.Visibility = Visibility.Hidden;
            StartLoggingInAnim();
        }
        private void ShowInitialView()
        {
            EndLoggingInAnim();
            EndLoadingDataAnim();

            grpLogin.Visibility= Visibility.Visible;
        }
        private void ShowGamesView()
        {
            lblLoadingData.Visibility = Visibility.Hidden;
            grpGames.Visibility = Visibility.Visible;
            grpDetails.Visibility = Visibility.Visible;
        }

        // Animations
        private void StartLoggingInAnim()
        {
            lblLoggingIn.Visibility = Visibility.Visible;
            sbLoggingIn.Begin();
        }
        private void EndLoggingInAnim()
        {
            lblLoggingIn.Visibility = Visibility.Hidden;
            sbLoggingIn.Stop();
        }

        private void StartLoadingDataAnim()
        {
            sbLoadingData.Begin();
            lblLoadingData.Visibility = Visibility.Visible;
        }

        private void EndLoadingDataAnim()
        {
            sbLoadingData.Stop();
            lblLoadingData.Visibility = Visibility.Hidden;
        }

        private async Task RetrieveData()
        {
            var retrievedGames = await _gamesService.GetAllGamesAsync();
            var retrievedLocations = await _locationsService.GetAllLocationsAsync();
            var retrievedRackets = await _racketsService.GetAllRacketsAsync();
            var retrievedShuttles = await _shuttleCocksService.GetAllShuttleCocksAsync();

            Games = retrievedGames is null ? null : new ObservableCollection<GameModel>(retrievedGames.Results);
            Locations = retrievedLocations is null ? null : new ObservableCollection<LocationModel>(retrievedLocations.Results);
            Rackets = retrievedRackets is null ? null : new ObservableCollection<RacketModel>(retrievedRackets.Results);
            ShuttleCocks = retrievedShuttles is null ? null : new ObservableCollection<ShuttleCockModel>(retrievedShuttles.Results);

            cmbLocations.ItemsSource = Locations;
            cmbRackets.ItemsSource = Rackets;
            cmbShuttles.ItemsSource = ShuttleCocks;
            lbGames.ItemsSource = Games;
        }

        private void FillDetails()
        {
            if (!(lbGames.SelectedItem is GameModel selectedModel)) return;

            txtOpponentName.Text = selectedModel.Opponent;
            txtYourScore.Text = selectedModel.Score.ToString();
            txtOpponentScore.Text = selectedModel.OpponentScore.ToString();
            lblStatus.Content = selectedModel.Status;
            lblWinner.Content = selectedModel.Winner;
            cmbLocations.SelectedItem = Locations.FirstOrDefault(l => l.Id == selectedModel.LocationId);
            cmbRackets.SelectedItem = Rackets.FirstOrDefault(r => r.Id == selectedModel.RacketId);
            cmbShuttles.SelectedItem = ShuttleCocks.FirstOrDefault(sc => sc.Id == selectedModel.ShuttleCockId);
        }

        private async Task UpdateModelAsync()
        {
            if (!(lbGames.SelectedItem is GameModel selectedModel)) return;
            if(!int.TryParse(txtYourScore.Text, out int yourScore))
            {
                MessageBox.Show("Value in textbox 'Your score' is not of a number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }            
            if(!int.TryParse(txtOpponentScore.Text, out int opponentScore))
            {
                MessageBox.Show("Value in textbox 'Opponent' score' is not of a number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var index = Games.IndexOf(selectedModel);

            selectedModel.Opponent = txtOpponentName.Text;
            selectedModel.Score = yourScore;
            selectedModel.OpponentScore = opponentScore;
            selectedModel.RacketId = ((RacketModel)cmbRackets.SelectedItem)?.Id;
            selectedModel.ShuttleCockId = ((ShuttleCockModel)cmbShuttles.SelectedItem)?.Id;
            selectedModel.LocationId = ((LocationModel)cmbLocations.SelectedItem)?.Id;

            var updatedModel = await _gamesService.UpdateGameAsync(selectedModel);
            if (updatedModel is null)
            {
                MessageBox.Show("Update failed, please try again later.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Games.RemoveAt(index);
            Games.Insert(index, updatedModel);
            lbGames.SelectedIndex = index;
        }

        private async Task DeleteModelAsync()
        {
            if (!(lbGames.SelectedItem is GameModel selectedModel)) return;

            var deleted = await _gamesService.DeleteGameAsync(selectedModel.Id);
            if (!deleted)
            {
                MessageBox.Show("Deletion failed, please try again later.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Games.Remove(selectedModel);
            ClearDetails();
        }

        private async Task CreateNewModelAsync()
        {
            if (!int.TryParse(txtYourScore.Text, out int yourScore))
            {
                MessageBox.Show("Value in textbox 'Your score' is not of a number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!int.TryParse(txtOpponentScore.Text, out int opponentScore))
            {
                MessageBox.Show("Value in textbox 'Opponent' score' is not of a number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var newGameModel = new GameModel
            {
                Opponent = txtOpponentName.Text,
                Score = yourScore,
                OpponentScore = opponentScore,
                RacketId = ((RacketModel)cmbRackets.SelectedItem)?.Id,
                ShuttleCockId = ((ShuttleCockModel)cmbShuttles.SelectedItem)?.Id,
                LocationId = ((LocationModel)cmbLocations.SelectedItem)?.Id
            };

            var addedModel = await _gamesService.AddGameAsync(newGameModel);

            if (addedModel is null)
            {
                MessageBox.Show("Creation failed, please try again later.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Games.Add(addedModel);
        }

        private async Task LoginAsync()
        {
            if (string.IsNullOrEmpty(txtPassword.Password))
            {
                MessageBox.Show("Username cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (string.IsNullOrEmpty(txtUserName.Text))
            {
                MessageBox.Show("Username cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            LoginModel login = new LoginModel
            {
                UserNameOrEmail = txtUserName.Text,
                Password = txtPassword.Password,
            };
            try
            {
                ShowLoggingInView();
                var result = await _authService.LoginAsync(login);
                if (!result.Succeeded)
                {
                    MessageBox.Show("Login failed, please try again later.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var token = result.Results.First().Token;
                TokenService.SaveToken(token);
                await ShowLoggedInView();
            }
            catch
            {
                ShowInitialView();
                MessageBox.Show("Server is down, please try again later.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearDetails()
        {
            txtOpponentName.Text = string.Empty;
            txtYourScore.Text = string.Empty;
            txtOpponentScore.Text = string.Empty;
            lblStatus.Content = string.Empty;
            lblWinner.Content = string.Empty;
            cmbLocations.SelectedIndex = -1;
            cmbRackets.SelectedIndex = -1;
            cmbShuttles.SelectedIndex = -1;
        }

        private void NewMode()
        {
            isEditing = false;
            grpDetails.Header = "New Game";
            lbGames.SelectedIndex = -1;
        }
        private void EditMode()
        {
            isEditing = true;
            grpDetails.Header = "Details";
        }
        #endregion
        #region EventHandlers
        private async void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            await LoginAsync();
        }

        private void LbGames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lbGames.SelectedIndex >= 0)
            {
                EditMode();
                FillDetails();
            }
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            NewMode();
            ClearDetails();
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (isEditing)
            {
                await UpdateModelAsync();
                FillDetails();
                return;
            }

            await CreateNewModelAsync();
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (!isEditing)
            {
                MessageBox.Show("You can't delete a not existing game.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            await DeleteModelAsync();
        }
        #endregion
    }
}
