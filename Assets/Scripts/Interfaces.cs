public interface IDamageable 
{
    void Damage(int amount, Player player);
}

public interface IInteractable
{
    void Interact();
}

public interface IHealth
{
    int curHealth { get; }
}

public interface ICharacterClass
{
    int baseMaxHealth { get; }
    int baseMaxStamina  { get; }
    void Attack(int id);
}
