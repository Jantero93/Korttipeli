using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Kehittyneet_graafinenKorttipeli
{
    class Kasi
    {
        private List<Kortti> kasi = new List<Kortti>();

        public void lisaaKortti(Kortti lisattavaKortti)
        {
            kasi.Add(lisattavaKortti);
        }

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
            /* vois toistaa pari kertaa jos tuntuu huonosti sekottavan */
            kasi = kasi.OrderBy(p => p.arvo).ToList();
        }

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
            if (kasi.Count() == 5) return true;
            else return false;
        }
    }
}
