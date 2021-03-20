using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace Tales_of_Neko
{
    public class CooldownManager:MonoBehaviour
    {
        public static CooldownManager Instance;
        public List<Spell> onCooldown=new List<Spell>();

        public void Awake()
        {
            if (Instance != null)
            {
                Instance = this;
            }

            if (Instance != this)
            {
                Destroy(this);
            }
            DontDestroyOnLoad(this);
        }

        public void Update()
        {
            for (int i = 0; i < onCooldown.Count; i++)
            {
                onCooldown[i].CurrentCooldown -= Time.deltaTime;
                if (onCooldown[i].CurrentCooldown <= 0)
                {
                    onCooldown[i].CurrentCooldown = 0;
                    onCooldown.Remove(onCooldown[i]);
                }
            }
        }

        public void StartCoolDown(Spell spell)
        {
            if (!onCooldown.Contains(spell))
            {
                spell.CurrentCooldown = spell.MaxCooldown;
                onCooldown.Add(spell);
            }
        }
    }
}