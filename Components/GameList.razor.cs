using Games.Classes;
using Games.Classes.GameClasses;
using Microsoft.AspNetCore.Components;

namespace Games.Components
{
    public partial class GameList
    {
        List<IGame> games = new List<IGame>();

        [Inject]
        private NavigationManager _navigation { get; set; }

        public void NavigateTo(string uri)
        {
            _navigation.NavigateTo(uri);
        }

        protected override void OnInitialized()
        {
            games.Add(new Blackjack());
            games.Add(new Solitaire());
        }
    }
}
