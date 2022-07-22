using Games.Classes.GameClasses;
using Games.Classes;

namespace Games.Pages
{
    public partial class BlackjackPage
    {
        public Blackjack _blackjackGame = new Blackjack();

        public bool gameOver = false;
        public bool gameStart = false;
        public bool _processing = false;
        public string winner { get; set; } = "No One";

        protected override void OnInitialized()
        {
            _blackjackGame.UpdateUIEvent += UpdateUIEventHandler;
            _blackjackGame.GameOverEvent += GameOverEventHandler;
        }

        public void UpdateUIEventHandler(object sender, GameEventArgs e)
        {
            StateHasChanged();
        }

        public void GameOverEventHandler(object sender, GameEventArgs e)
        {
            gameOver = true;
            winner = e.Text;
            StateHasChanged();
        }

        public void StartGame()
        {
            gameStart = true;
            _blackjackGame.StartGame();
        }

        public void PlayerHit()
        {
            _blackjackGame.PlayerHit();
        }

        public async Task PlayerStay()
        {
            _processing = true;
            StateHasChanged();
            await _blackjackGame.PlayerStay();
        }

        public void Reset()
        {
            gameStart = false;
            gameOver = false;
            winner = "No One";
            _processing = false;
            _blackjackGame = new Blackjack();
        }

        public void Dispose()
        {
            _blackjackGame.UpdateUIEvent -= UpdateUIEventHandler;
            _blackjackGame.GameOverEvent -= GameOverEventHandler;
        }
    }
}
