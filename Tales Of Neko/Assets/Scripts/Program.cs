using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Tales_of_Neko
{
    class Program
    {
        static void Main(string[] args)
        {
            Stats MyStats=new Stats(20,0,20,20,40);
            Stats MonsterStats =new Stats(30,0,0,0,0);
            Player me =new  Player("Alin",CharacterClass.Mage,100.0f,100.0f,MyStats);
            Mob siren=new Mob("Siren1",60,MonsterStats);
            
            Console.WriteLine(me.Stats);
            while ( Combat.CanFight(me,siren))
            {
                Console.Write("Enter action: \"attack\" or \"run\" : ");
                
                string action="aaa";
                action = Console.ReadLine();
                Console.WriteLine(action);
                if (action.Equals("attack"))
                   {
                       Console.Write("Enter attack type: \"mana attack\" or \"basic attack\" : ");
                       string type=Console.ReadLine();
                       if (type.Equals("mana attack")) {
                           if (Combat.IsFirst(me, siren)) {
                               Combat.ManaAttack(ref me,ref siren);
                               if (!Combat.CanFight(me,siren))
                               {
                                   break;
                               }
                               Combat.Attack(ref siren,ref me);
                           }
                           else {
                               Combat.Attack(ref siren,ref me);
                               if (!Combat.CanFight(me,siren))
                               {
                                   break;
                               }
                               Combat.ManaAttack(ref me,ref siren);
                           }
                       }
                       else {
                           if (Combat.IsFirst(me, siren)) {
                               Combat.BasicAttack(ref me,ref siren);
                               if (!Combat.CanFight(me,siren))
                               {
                                   break;
                               }
                               Combat.Attack(ref siren,ref me);
                           }
                           else {
                               Combat.Attack(ref siren,ref me);
                               if (!Combat.CanFight(me,siren))
                               {
                                   break;
                               }
                               Combat.BasicAttack(ref me,ref siren);
                           }
                       }
                   }
                   else if (action.Equals("run")) {
                        me.GetRawStats();
                        if (me.CanEscape(siren)) {
                           Console.WriteLine("You escaped!");
                           break;
                        }
                        Combat.Attack(ref siren,ref me);

                }
                   else
                   {
                       Console.WriteLine("Not a recognised action.");
                   }
                   Console.WriteLine(me.ToString()+"\n"+siren.ToString());
               
            }

            if (!siren.IsAlive()) {
                Console.WriteLine(me.Name+" won!");
            }
            else if(!me.IsAlive()) {
                Console.WriteLine(siren.Name+" won!");
            }
        }
    }
}