namespace Mechanics
{ 
    [System.Flags]
    public enum EGameEffects
    {
        NONE=0,
        QUARANTINE_START=1,
        QUARANTINE_END=2,
        HOLIDAY=4,
        FRIEND1_ZOOM=8,
        FRIEND2_ZOOM=16,
        STRANGER1_ZOOM=32,
        STRANGER2_ZOOM=64,
        STRANGER3_ZOOM=128,
    }
}
