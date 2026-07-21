namespace Game.Gameplay
{
    public static class EntityRelationUtils
    {
        public static EntityRelation GetRelation(IEntity first, IEntity second)
        {
            if (ReferenceEquals(first, second))
                return EntityRelation.Self;
            return GetRelationByFaction(first.Faction, second.Faction);
        }

        public static EntityRelation GetRelationByFaction(EntityFaction first, EntityFaction second)
        {
            if (first == second)
            {
                return EntityRelation.Ally;
            }

            if (first == EntityFaction.Hero && second == EntityFaction.Monster)
            {
                return EntityRelation.Enemy;
            }

            if (first == EntityFaction.Monster && second == EntityFaction.Hero)
            {
                return EntityRelation.Enemy;
            }

            return EntityRelation.Neutral;

        }

        public static bool CanAffect(EntityFaction from, EntityFaction to, EntityRelation targetMask)
        {
            var relation = GetRelationByFaction(from, to);
            return (targetMask & relation) != 0;
        }
    }
}