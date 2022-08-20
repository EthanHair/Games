using Games.Classes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using MudBlazor;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace Games.Pages
{
    public partial class DicePage
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        public HubConnection hubConnection;

        public Dictionary<int, int> result = new Dictionary<int, int>();

        // Chart
        public List<ChartSeries> chartResult = new List<ChartSeries>();

        public string[] axisLabels = new string[] { };

        public double[] chartData = new double[] { };

        public Dictionary<int, double> statsDict = new Dictionary<int, double>();

        public ChartOptions chartOptions = new ChartOptions() { DisableLegend = true, LineStrokeWidth = 4 };

        public bool AskForInput = true;

        public int NumberOfSides = 0;
        public int NumberOfDie = 0;
        public int NumberOfRolls = 0;
        public int MaxValueForProgressBars = 100;
        public int RollNumber = 0;
        public string ElapsedTime = "...";
        public bool ShowChart;

        protected override async Task OnInitializedAsync()
        {
            hubConnection = new HubConnectionBuilder()
                                .WithUrl("https://localhost:7053/dicerollhub")
                                .Build();

            hubConnection.On<List<int>>("SendRolls", (rolls) =>
            {
                foreach (int roll in rolls)
                {
                    if (result.ContainsKey(roll))
                    {
                        result[roll]++;
                        MaxValueForProgressBars = Math.Max(MaxValueForProgressBars, (int)Math.Ceiling(result[roll] * 1.1));
                    }
                    else
                    {
                        Snackbar.Add("A roll was recieved but the result dictionary did not contain it", Severity.Warning);
                    }
                }
                StateHasChanged();
            });

            hubConnection.On<string>("RollStarted", (message) =>
            {
                Snackbar.Add(message, Severity.Info);
                StateHasChanged();
            });

            hubConnection.On<string>("RollFinished", (elapsedTime) =>
            {
                ElapsedTime = elapsedTime;
                RollNumber++;
                CreateChart();
                Snackbar.Add("The roll completed", Severity.Success);
                StateHasChanged();
            });

            await hubConnection.StartAsync();
        }

        public void CreateDiceListAndDict()
        {
            for (int i = NumberOfDie; i <= NumberOfSides * NumberOfDie; i++)
            {
                result.Add(i, 0);
                statsDict.Add(i, 0);
            }
        }

        public async Task RollDice()
        {
            CreateDiceListAndDict();

            Snackbar.Add("A roll was requested", Severity.Info);

            AskForInput = false;

            MaxValueForProgressBars = (int)Math.Ceiling((double)(NumberOfRolls / NumberOfSides));

            await JSRuntime.InvokeVoidAsync("blazorInterop.rollDice", NumberOfDie, NumberOfSides, NumberOfRolls);
        }

        public async Task RollAgain()
        {
            Snackbar.Add("A roll was requested", Severity.Info);

            ElapsedTime = "...";

            foreach (var key in result.Keys)
            {
                result[key] = 0;
            }

            await JSRuntime.InvokeVoidAsync("blazorInterop.rollDice", NumberOfDie, NumberOfSides, NumberOfRolls);
        }

        public string GetPercent(int value)
        {
            var percent = ((double)value / (double)NumberOfRolls) * 100;
            return String.Format("{0:0.00}%", percent);
        }

        public void CreateChart()
        {
            var data = new List<double>();
            var labels = new List<string>();
            if (result.Keys != null)
            {
                foreach (int key in result.Keys)
                {
                    labels.Add(key.ToString());
                    data.Add(result[key]);
                    statsDict[key] += result[key];
                }
            }
            axisLabels = labels.ToArray();
            chartData = data.ToArray();
            chartResult.Add(new ChartSeries() { Name = String.Format("Roll {0}", RollNumber), Data = chartData });
            ShowChart = true;
        }

        public void Reset()
        {
            AskForInput = true;
            MaxValueForProgressBars = 5;
            RollNumber = 0;
            result = new Dictionary<int, int>();
            statsDict = new Dictionary<int, double>();
            axisLabels = new string[] { };
            chartData = new double[] { };
            chartResult.Clear();
        }
    }
}
