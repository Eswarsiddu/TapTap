public static class Constants
{
    private static System.Random random = new System.Random();
    public static string PLAYERDATA => "ScriptableObjects/PlayerData";
    public static string LEVELSDATA => "ScriptableObjects/LevelsData";
    public static string ASSETSSTORE => "ScriptableObjects/AssetsStore";
    public static string STOREUITILE => "prefabs/Store UI Tile";
    public static string TARGETPREFAB => "Prefabs/Target";


    public static int RandomInt(int min, int max) => random.Next(min, max + 1);
    public static int RandomInt(int max) => random.Next(0, max + 1);
    public static bool RandomBool() => random.Next() % 2 == 0;


    public static string getTimeText(float time) => getTimeText((int)time);
    public static string getTimeText(int time)
    {
        string timeText = "";
        if (time >= 360)
        {
            timeText += doubleDigit(time / 360) + ":";
            time %= 360;
        }

        timeText += doubleDigit(time / 60) + ":";
        time %= 60;

        timeText += doubleDigit(time);

        return timeText;
    }
    private static string doubleDigit(int num)
    {
        string time = "";
        if (num < 10)
        {
            time = "0";
        }
        return time + num.ToString(); ;
    }

}