/// <summary>
/// Used for the Player Characters in order to keep this
/// and the player movement controller scripts seperate
/// </summary>
public interface ISwappableCharacter
{
    void SetAsInactiveCharacter();
    void SetAsActiveCharacter();
}