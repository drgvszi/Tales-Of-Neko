using System;

namespace Tales_of_Neko
{
    [Serializable] 
    public class Dialogue
    {
        public string Who;
        public string What;

        public Dialogue(string who, string what)
        {
            Who = who;
            What = what;
        }


    }
}