using Games.Classes.GameClasses;
using Games.Classes;

namespace Games.Pages
{
    public partial class BlackjackPage
    {
        public Blackjack _blackjackGame = new Blackjack();

        public bool gameOver = false;
        public bool gameStart = false;
        public bool playerStay = false;
        public string winner { get; set; } = "No One";

        protected override void OnInitialized()
        {
            _blackjackGame.UpdateUIEvent += UpdateUIEventHandler;
            _blackjackGame.GameOverEvent += GameOverEventHandler;
        }

        public void UpdateUIEventHandler(object sender, GameEventArgs e)
        {
            InvokeAsync(() => StateHasChanged());
        }

        public void GameOverEventHandler(object sender, GameEventArgs e)
        {
            gameOver = true;
            winner = e.Text;
            InvokeAsync(() => StateHasChanged());
        }

        public void StartGame()
        {
            gameStart = true;
            _blackjackGame.StartGame();
        }

        public async Task PlayerHit()
        {
            await _blackjackGame.PlayerHit();
        }

        public async Task PlayerStay()
        {
            playerStay = true;
            InvokeAsync(() => StateHasChanged());
            await _blackjackGame.PlayerStay();
            await InvokeAsync(() => StateHasChanged());
        }

        public void Reset()
        {
            gameStart = false;
            gameOver = false;
            playerStay = false;
            winner = "No One";
            _blackjackGame = new Blackjack();
        }

        public void Dispose()
        {
            _blackjackGame.UpdateUIEvent -= UpdateUIEventHandler;
            _blackjackGame.GameOverEvent -= GameOverEventHandler;
        }
    }
}
