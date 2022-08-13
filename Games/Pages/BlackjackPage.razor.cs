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
        public bool playerHasBet = false;
        public string winner { get; set; } = "No One";

        public string playerMoney => $"${string.Format("{0:0.00}", _blackjackGame.PlayerMoney)}";

        public string playerBet => $"${string.Format("{0:0.00}", _blackjackGame.PlayerBet)}";
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

        public async Task StartGame()
        {
            gameStart = true;
            _blackjackGame.StartGame();
            _blackjackGame.FlipCards("Player");
            _blackjackGame.FlipOneDealerCard();
        }

        public async Task PlayerHit()
        {
            await _blackjackGame.PlayerHit();
            _blackjackGame.FlipCards("Player");
        }

        public async Task PlayerStay()
        {
            playerStay = true;
            await _blackjackGame.PlayerStay();
            _blackjackGame.FlipCards("Dealer");
            StateHasChanged();
        }

        public async Task PlayerDoubleDown()
        {
            playerStay = true;
            await _blackjackGame.PlayerDoubleDown();
            _blackjackGame.FlipCards("Player");
        }

        public void MakePlayerBet()
        {
            playerHasBet = true;
            _blackjackGame.MakePlayerBet();
        }

        public void Reset()
        {
            gameOver = false;
            playerStay = false;
            playerHasBet = false;
            winner = "No One";
            _blackjackGame.Reset();
        }

        public void NewGame()
        {
            gameOver = false;
            playerStay = false;
            playerHasBet = false;
            winner = "No One";
            _blackjackGame.NewGame();
            _blackjackGame.FlipCards("Player");
            _blackjackGame.FlipOneDealerCard();
        }

        public void Dispose()
        {
            _blackjackGame.UpdateUIEvent -= UpdateUIEventHandler;
            _blackjackGame.GameOverEvent -= GameOverEventHandler;
        }
    }
}
