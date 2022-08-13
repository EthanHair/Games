using Games.Classes;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Games.Pages
{
    public partial  class DeckPage
    {
        [Inject]
        private ISnackbar Snackbar { get; set; }

        public Deck _deck = new Deck();

        public Hand DealtCardsHand = new Hand("Dealt Cards");

        public void Shuffle()
        {
            _deck.Shuffle();
            StateHasChanged();
        }
        public void Draw()
        {
            _deck.DrawCard(ref DealtCardsHand);
            StateHasChanged();
        }

        public void RecollectCards()
        {
            _deck.RecollectCards();
            DealtCardsHand.Cards.Clear();
            StateHasChanged();
        }

        public async Task FlipDrawnCards()
        {
            foreach (Card card in _deck.DrawnCards)
            {
                if (!card.IsFlipped)
                {
                    card.FlipCard();
                    StateHasChanged();
                    await Task.Delay(500);
                }
            }
        }


        // Second Deck
        public Deck _deck2 = new Deck();

        public Hand leftHand = new Hand("LeftHand");

        public Hand rightHand = new Hand("RightHand");

        public void Shuffle2()
        {
            _deck2.Shuffle();
            StateHasChanged();
        }
        public async Task DrawRight()
        {
            _deck2.DrawCard(ref rightHand);
            StateHasChanged();
            await FlipRightCards();
        }

        public async Task DrawLeft()
        {
            _deck2.DrawCard(ref leftHand);
            StateHasChanged();
            await FlipLeftCards();
        }

        public async Task FlipLeftCards()
        {
            foreach (Card card in leftHand.Cards)
            {
                if (!card.IsFlipped)
                {
                    card.FlipCard();
                    StateHasChanged();
                    await Task.Delay(1500);
                }
            }
        }

        public async Task FlipRightCards()
        {
            foreach (Card card in rightHand.Cards)
            {
                if (!card.IsFlipped)
                {
                    card.FlipCard();
                    StateHasChanged();
                    await Task.Delay(1500);
                }
            }
        }

        public void RecollectCards2()
        {
            _deck2.RecollectCards();
            leftHand.Cards.Clear();
            rightHand.Cards.Clear();
            StateHasChanged();
        }

        public void CauseInvalidDrawException1()
        {
            var testDeck = new Deck(false);
            testDeck.Cards.Clear();
            try
            {
                testDeck.DrawCard();
            }
            catch (InvalidDrawException ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
        }

        public void CauseInvalidDrawException2()
        {
            var testDeck = new Deck();
            testDeck.Cards.Clear();
            testDeck.DiscardPile.Clear();
            try
            {
                testDeck.DrawCard();
            }
            catch (InvalidDrawException ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
        }
    }
}
