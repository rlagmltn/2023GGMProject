using Assets.HeroEditor4D.Common.Scripts.Common;
using Assets.HeroEditor4D.Common.Scripts.Enums;
using UnityEngine;

namespace Assets.HeroEditor4D.Common.Scripts.CharacterScripts
{
    /// <summary>
    /// You can extend 'Character' class here.
    /// </summary>
    public static class CharacterExtensions
    {
        public static Color RandomColor => new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f), 1f);

        public static void Randomize(this Character4D character)
        {
            character.ResetEquipment();
            character.SetBody(character.Character.SpriteCollection.Eyes.Random(), BodyPart.Eyes);

            if (character.Character.SpriteCollection.Hair.Count > 0) character.SetBody(character.Character.SpriteCollection.Hair.Random(), BodyPart.Hair, RandomColor);
            if (character.Character.SpriteCollection.Eyebrows.Count > 0) character.SetBody(character.Character.SpriteCollection.Eyebrows.Random(), BodyPart.Eyebrows);
            if (character.Character.SpriteCollection.Eyes.Count > 0) character.SetBody(character.Character.SpriteCollection.Eyes.Random(), BodyPart.Eyes, RandomColor);
            if (character.Character.SpriteCollection.Ears.Count > 0) character.SetBody(character.Character.SpriteCollection.Ears.Random(), BodyPart.Ears);
            if (character.Character.SpriteCollection.Mouth.Count > 0) character.SetBody(character.Character.SpriteCollection.Mouth.Random(), BodyPart.Mouth);

            character.Equip(character.Character.SpriteCollection.Armor.Random(), EquipmentPart.Helmet);
            character.Equip(character.Character.SpriteCollection.Armor.Random(), EquipmentPart.Armor);

            switch (Random.Range(0, 5))
            {
                case 0:
                    character.Equip(character.Character.SpriteCollection.MeleeWeapon1H.Random(), EquipmentPart.MeleeWeapon1H);
                    character.UnEquip(EquipmentPart.Shield);
                    break;
                case 1:
                    character.Equip(character.Character.SpriteCollection.MeleeWeapon1H.Random(), EquipmentPart.MeleeWeapon1H);
                    character.Equip(character.Character.SpriteCollection.Shield.Random(), EquipmentPart.Shield);
                    break;
                case 2:
                    character.Equip(character.Character.SpriteCollection.MeleeWeapon2H.Random(), EquipmentPart.MeleeWeapon2H);
                    break;
                case 3:
                    character.Equip(character.Character.SpriteCollection.Bow.Random(), EquipmentPart.Bow);
                    break;
                case 4:
                    character.Equip(character.Character.SpriteCollection.Firearm1H.Random(), EquipmentPart.SecondaryFirearm1H);
                    break;
            }
        }
    }
}