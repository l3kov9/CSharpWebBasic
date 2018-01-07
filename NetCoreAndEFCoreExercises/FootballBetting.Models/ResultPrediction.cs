using FootballBetting.Models.Enum;
using System.Collections.Generic;

namespace FootballBetting.Models
{
    public class ResultPrediction
    {
        public int Id { get; set; }

        public PredictionType Prediction { get; set; }

        public List<BetGame> BetGames { get; set; } = new List<BetGame>();
    }
}
