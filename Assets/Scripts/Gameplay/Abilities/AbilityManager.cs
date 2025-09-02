using System.Collections.Generic;
using System.Linq;

namespace Gameplay.Abilities
{
    public class AbilityManager
    {
        public List<Ability> abilities;
        public List<Ability> activeAbilities => abilities.Where(a => a.IsActive).ToList();

        public AbilityManager(IEnumerable<Ability> abilities)
        {
            this.abilities = abilities.ToList();
        }
        
        public void SetAbilities(IEnumerable<Ability> abilities)
        {
            this.abilities = abilities.ToList();
        }

        public void InitAbilities()
        {
            foreach (var ability in abilities) 
                ability.Init();
        }

        public void HookAbilities()
        {
            foreach (var ability in abilities)
                ability.Hook();
        }

        public void UnhookAbilities()
        {
            foreach (var ability in abilities)
                ability.Unhook();
        }

        public void UpdateAbilities()
        {
            foreach (var ability in abilities)
                ability.AbilityUpdate();
        }

        public void GizmosAbilities()
        {
            foreach (var ability in abilities)
                ability.AbilityGizmos();
        }
    }
}