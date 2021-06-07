using Enums;
using UnityEngine;

public static class Constants
{
    public const string VibrationsStatusKey = "VibrationsStatus";
    public const string SoundsStatusKey = "SoundsStatus";

    public const int PlayerCount = 6;
    
    //paths
    public const string EnvironmentsPath = "Environment/Maps/";
    public const string CharactersPath = "Environment/Characters/";
    public const string DamagePopupPath = "Environment/DamagePopup";
    public const string SkillDataPath = "Skills/";
    public const string SkillPath = "Projectiles/";
    public const string UniversePath = "Universe/";

    //PlayerPrefs
    public const string MapKey = "Map";
    public const string PlayerKey = "Player";
    public const string UserNameKey = "UserName";
    public const string SoundKey = "Sound";
    public const string HapticKey = "Haptic";
        
    //Tags
    public const string SpawnPositionTag = "SpawnPosition";
    public const string ProjectileTag = "Projectile";
    
    //Animation Keys
    public const string CharacterRunAnimBool = "Run";
    public const string CharacterRunDirectionXFloat = "DirectionX";
    public const string CharacterRunDirectionYFloat = "DirectionY";
    public const string CharacterDieTrigger = "Die";
    public const string CharacterDanceBool = "Dance";
    
    
    //Event Time Delays
    public const float FirstBatch = 0.2f;
    public const float SecondBatch = 0.3f;
    public const float ThirdBatch = 0.4f;
    
    
    //Colors
    public static Color Gold = new Color(0.84f,0.70f,0.40f);
    
    //Round Values
    public static int RoundCount = 3;
    public static float[] RoundLengths = {10,30,10};
    public static int SkillSelectionTime = 10;
    public static int RoundEndTime =7;
    public static int GameEndTime = 8;
    private static readonly SkillType[] FirstRoundTypes = {SkillType.Base, SkillType.Base, SkillType.Base};
    private static readonly SkillType[] SecondRoundTypes = {SkillType.Defence, SkillType.Attack, SkillType.Special};
    private static readonly SkillType[] ThirdRoundTypes = {SkillType.Defence, SkillType.Attack, SkillType.Special};
    public static SkillType[][] RoundSkillTypes = {FirstRoundTypes, SecondRoundTypes, ThirdRoundTypes};
    public static int RespawnTime = 3;
    
    
    //Greetings
    public static string StrangerGreeting = "Hello Stranger";
    public static string NewUserGreeting = "Nice To Meet You ";
    public static string WelcomeBackGreeting = "Welcome Back ";


    //Networking
    public static string GameVersion = "0.1";
    public static int MaxPlayersPerRoom = 1;

}