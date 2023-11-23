using UnityEngine;

namespace I2.Loc
{
	public static class ScriptLocalization
	{

		public static class HUD
		{
			public static string HEALTH_DISPLAY 		{ get{ return LocalizationManager.GetTranslation ("HUD/HEALTH_DISPLAY"); } }
			public static string RECOVERING_STATE 		{ get{ return LocalizationManager.GetTranslation ("HUD/RECOVERING_STATE"); } }
			public static string STAGE 		{ get{ return LocalizationManager.GetTranslation ("HUD/STAGE"); } }
		}

		public static class PopupGameRating
		{
			public static string EMAIL_SUBJECT 		{ get{ return LocalizationManager.GetTranslation ("PopupGameRating/EMAIL_SUBJECT"); } }
		}

		public static class PopupMoreCoins
		{
			public static string MESSAGE 		{ get{ return LocalizationManager.GetTranslation ("PopupMoreCoins/MESSAGE"); } }
			public static string TITLE 		{ get{ return LocalizationManager.GetTranslation ("PopupMoreCoins/TITLE"); } }
		}

		public static class PopupStageCleared
		{
			public static string STAGE 		{ get{ return LocalizationManager.GetTranslation ("PopupStageCleared/STAGE"); } }
		}

		public static class Ranking
		{
			public static string ME 		{ get{ return LocalizationManager.GetTranslation ("Ranking/ME"); } }
		}

		public static class ScreenStageSelection
		{
			public static string STAGE 		{ get{ return LocalizationManager.GetTranslation ("ScreenStageSelection/STAGE"); } }
		}

		public static class Toast
		{
			public static string CONNECTION_FAILED 		{ get{ return LocalizationManager.GetTranslation ("Toast/CONNECTION_FAILED"); } }
			public static string APP_UPDATE_FAILED 		{ get{ return LocalizationManager.GetTranslation ("Toast/APP_UPDATE_FAILED"); } }
			public static string APP_UPDATE_CANCELLED 		{ get{ return LocalizationManager.GetTranslation ("Toast/APP_UPDATE_CANCELLED"); } }
		}

		public static class UpgradePanel
		{
			public static string LEVEL 		{ get{ return LocalizationManager.GetTranslation ("UpgradePanel/LEVEL"); } }
			public static string MAX 		{ get{ return LocalizationManager.GetTranslation ("UpgradePanel/MAX"); } }
		}
	}

    public static class ScriptTerms
	{

		public static class HUD
		{
		    public const string HEALTH_DISPLAY = "HUD/HEALTH_DISPLAY";
		    public const string RECOVERING_STATE = "HUD/RECOVERING_STATE";
		    public const string STAGE = "HUD/STAGE";
		}

		public static class PopupGameRating
		{
		    public const string EMAIL_SUBJECT = "PopupGameRating/EMAIL_SUBJECT";
		}

		public static class PopupMoreCoins
		{
		    public const string MESSAGE = "PopupMoreCoins/MESSAGE";
		    public const string TITLE = "PopupMoreCoins/TITLE";
		}

		public static class PopupStageCleared
		{
		    public const string STAGE = "PopupStageCleared/STAGE";
		}

		public static class Ranking
		{
		    public const string ME = "Ranking/ME";
		}

		public static class ScreenStageSelection
		{
		    public const string STAGE = "ScreenStageSelection/STAGE";
		}

		public static class Toast
		{
		    public const string CONNECTION_FAILED = "Toast/CONNECTION_FAILED";
		    public const string APP_UPDATE_FAILED = "Toast/APP_UPDATE_FAILED";
		    public const string APP_UPDATE_CANCELLED = "Toast/APP_UPDATE_CANCELLED";
		}

		public static class UpgradePanel
		{
		    public const string LEVEL = "UpgradePanel/LEVEL";
		    public const string MAX = "UpgradePanel/MAX";
		}
	}
}