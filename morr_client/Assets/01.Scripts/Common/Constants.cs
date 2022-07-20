public static class Constants
{
    public static class EXCEL
    {
        public static string SHEET_ID = $"1YWv8YHPRpw8tRQrj4bPvcus9Q5m3sQpOpvgiI-mNYGU";
        //
        public static string SKILL = $"Skill";
        public static string LANGUAGE = $"Language";
        public static string UNIT = $"Unit";
        public static string ITEM = $"Item";
        public static string GROUND_PROPERTY = $"GroundProperty";
        public static string SCENARIO =$"Scenario";
        public static string QACONFIG = $"QAConfig";
        
        //
        public static string API_KEY = $"AIzaSyB9LOl4OWIhiDfXmYSk0beoJ59N4t20wwY";
    }

    public static class GameState
    {
        public readonly static string GAME_READY = "GAME_READY"; //대기
        public readonly static string GAME_FIRE_READY = "GAME_FIRE_READY"; //공 발사대기
    }
}