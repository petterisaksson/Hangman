using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gemensamHangman
{
    class Player
    {
        string name;
        int life;
        
        public Player() //konstruktor (värden tilldelas alla nya instanser som skapas)
        {
             
            this.life = 10;
        }
        public string Name  //properties 
        {
            get { return name; }
            set
            {
                if(value.Length >=3)
                {
                    name = value;
                }
                else
                {
                    name = "Player1";
                }
            }
        }
        public int Life
        {
            get { return life; }
            private set { life = value; }
        }
        public void Damage()
        {
            life--;
        }
        
    }

   
}
