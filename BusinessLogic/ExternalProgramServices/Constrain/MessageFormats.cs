using BusinessLogic.ExternalProgramServices.Constrain.ConcreteConstrain.Game;
using System.Text.RegularExpressions;

namespace BusinessLogic.ExternalProgramServices.Constrain
{
	public static class MessageFormats
	{
		public const string GameToBotPrompt = @"^Bot ([0-9]+) to play. Data: (.*)$";
		public const string GameEnded = @"^Game result: (draw|[0-9]+)$";
		public const string BotToGame = @"^Move: (.*)$";

		public static MatchResult<GameToBotMessage> MatchGameToBotMessage(string data)
		{
			Match match = Regex.Match(data, GameToBotPrompt);
			if (!match.Success)
			{
				return new MatchResult<GameToBotMessage>
				{
					Success = false
				};
			}
			GameToBotMessage message = new GameToBotMessage
			{
				Id = Convert.ToInt32(match.Groups[1].Value),
				Data = match.Groups[2].Value
			};
			return new MatchResult<GameToBotMessage>
			{
				Success = true,
				Data = message
			};
		}

		public static MatchResult<string> MatchBotToGameMessage(string data)
		{
			Match match = Regex.Match(data, BotToGame);
			if (!match.Success)
			{
				return new MatchResult<string>
				{
					Success = false
				};
			}
			return new MatchResult<string>
			{
				Success = true,
				Data = match.Groups[1].Value
			};
		}

		public static MatchResult<GameResult> MatchGameEnded(string data)
		{
			Match match = Regex.Match(data, BotToGame);
			if (!match.Success)
			{
				return new MatchResult<GameResult>
				{
					Success = false
				};
			}
			GameResult gameResult = new GameResult();
			if (match.Groups[1].Value == "draw") // not good, maybe 0 could mean draw
			{
				gameResult.Draw = true;
			}
			else
			{
				gameResult.Winner = Convert.ToInt32(match.Groups[1].Value);
			}
			return new MatchResult<GameResult>
			{
				Success = true,
				Data = gameResult
			};
		}
	}
}
