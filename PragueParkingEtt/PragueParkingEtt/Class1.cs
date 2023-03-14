using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PragParking
{
    internal class Ticket
    {
        private string fordonstyp;
        private string regnr;
        private DateTime ankomsttid;
        public string Fordonstyp
        {
            get { return fordonstyp; }
            set { fordonstyp = value; }
        }
        public string Regnr
        {
            get { return regnr; }
            set { regnr = value; }


        }
        public DateTime Ankomsttid
        {
            get { return ankomsttid; }
            set { ankomsttid = value; }

        }
        public Ticket(string Fordonstyp, string Regnr, DateTime Ankomsttid)
        {
            this.fordonstyp = Fordonstyp;
            this.regnr = Regnr;
            this.ankomsttid = Ankomsttid;
        }
    }
}
