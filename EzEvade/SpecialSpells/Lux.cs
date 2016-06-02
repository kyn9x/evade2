﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

namespace ezEvade.SpecialSpells
{
    class Lux : ChampionPlugin
    {
        static Lux()
        {

        }

        public void LoadSpecialSpell(SpellData spellData)
        {
            if (spellData.spellName == "LuxMaliceCannon")
            {
                var hero = EntityManager.Heroes.Enemies.FirstOrDefault(h => h.ChampionName == "Lux");
                if (hero != null)
                {
                    GameObject.OnCreate += (obj, args) => OnCreateObj_LuxMaliceCannon(obj, args, hero, spellData);
                }
            }
        }

        private static void OnCreateObj_LuxMaliceCannon(GameObject obj, EventArgs args, AIHeroClient hero, SpellData spellData)
        {
            if (obj.IsEnemy && !hero.IsVisible &&
                obj.Name.Contains("Lux") && obj.Name.Contains("R_mis_beam_middle"))
            {
                var objList = ObjectTracker.objTracker.Values.Where(o => o.Name == "hiu");
                if (objList.Count() > 3)
                {
                    var dir = ObjectTracker.GetLastHiuOrientation();
                    var pos1 = obj.Position.To2D() - dir * 1750;
                    var pos2 = obj.Position.To2D() + dir * 1750;

                    SpellDetector.CreateSpellData(hero, pos1.To3D(), pos2.To3D(), spellData);

                    foreach (ObjectTrackerInfo gameObj in objList)
                    {
                        DelayAction.Add(1, () => ObjectTracker.objTracker.Remove(gameObj.obj.NetworkId));
                    }
                }
            }
        }
    }
}
