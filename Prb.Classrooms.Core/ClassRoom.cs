using System;
using System.Collections.Generic;
using System.Text;

namespace Prb.Classrooms.Core
{
    public class ClassRoom
    {
        private string name;
        private sbyte floor;
        private char wing;
        private int capacity;
        private bool isComputerClassRoom;

        public string Name
        {
            get { return name; }
            set
            {
                if (value == "")
                    value = "nog toe te kennen naam";
                name = value;
            }
        }
        public sbyte Floor
        {
            get { return floor; }
            set
            {
                if (value < -1 || value > 2)
                    value = 0;
                floor = value;
            }
        }
        public char Wing
        {
            get { return wing; }
            set
            {
                // we wijzigen eerst de binnengekomen waarde naar hoofdletter
                string waarde = value.ToString();
                waarde = waarde.ToUpper();
                value = char.Parse(waarde);
                // controle waarden
                if (value != 'A' && value != 'B' && value != 'C' && value != 'D')
                    value = '?';
                // private variabele instellen
                wing = value;
            }
        }   
        public int Capacity
        {
            get { return capacity; }
            set
            {
                if (value < 1)
                    value = 1;
                if (value > 250)
                    value = 250;
                capacity = value;
            }
        }
        public bool IsComputerClassRoom
        {
            get { return isComputerClassRoom; }
            set { isComputerClassRoom = value; }
        }

        public ClassRoom(string name, sbyte floor, char wing, int capacity, bool isComputerClassRoom)
        {
            Name = name;
            Floor = floor;
            Wing = wing;
            Capacity = capacity;
            this.isComputerClassRoom = isComputerClassRoom;
        }

        public override string ToString()
        {
            return $"{name} : {wing}-vleugel ({floor})";
        }


    }
}
