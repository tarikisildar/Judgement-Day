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
    
    //PlayerPrefs
    public const string MapKey = "Map";
    public const string PlayerKey = "Player";
    
    //Tags
    public const string SpawnPositionTag = "SpawnPosition";
    public const string ProjectileTag = "Projectile";
    
    //Animation Keys
    public const string CharacterRunAnimBool = "Run";
    public const string CharacterRunDirectionXFloat = "DirectionX";
    public const string CharacterRunDirectionYFloat = "DirectionY";
    
    
    //Event Time Delays
    public const float FirstBatch = 0.2f;
    public const float SecondBatch = 0.3f;
    public const float ThirdBatch = 0.4f;
    
    
    //Colors
    public static Color Gold = new Color(0.84f,0.70f,0.40f);
}