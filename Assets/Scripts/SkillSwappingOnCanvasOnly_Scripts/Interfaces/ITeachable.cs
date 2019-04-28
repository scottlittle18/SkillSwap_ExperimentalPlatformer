/// <summary>
/// Used for implementing the required functions that allow the character
/// that the skill was dropped onto to learn and apply the skill that wass attached
/// </summary>
public interface ITeachable
{
    void DetermineNewlyAcquiredSkill();
    void ForgetOldSkill();
    
}
