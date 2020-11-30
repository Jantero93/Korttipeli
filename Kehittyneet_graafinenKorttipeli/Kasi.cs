using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Kehittyneet_graafinenKorttipeli
{

    //pitäskö tehdä metodi joka antaa stringinä minkä arvoinen käsi, esim pari, kaks paria, suora, väri jne
    class Kasi
    {   //monta korttia kadessa kerralla maksimissaan
        private const int maxKorttiaKadessa = 5;
        private List<Kortti> kasi = new List<Kortti>();

        public void lisaaKortti(Kortti lisattavaKortti)
        {
            kasi.Add(lisattavaKortti);
        }

         /*
         INDEKSOINTI MENEE SEKAISIN KUN OTTAA JONKUN MUUN KUIN VIIMEISEN
         */
        public void otaKortti(int index)
        {
            kasi.RemoveAt(index);
        }

        public Kortti getKortti(int index)
        {
            return kasi.ElementAt(index);
        }

        public void jarjestaKortit()
        {            
            kasi = kasi.OrderBy(p => p.arvo).ToList();
        }

        // legacya, turha
        public override string ToString()
        {
            string tulostus = "";
            for (int i = 0; i < kasi.Count(); i++)
            {
                tulostus += kasi.ElementAt(i) + "    idx: " + i + "\n";
            }

            return tulostus;
        }

        public bool kasiTaynna() //kädessä 5 korttia max
        {
            if (kasi.Count() == maxKorttiaKadessa) return true;
            else return false;
        }
    }
}
