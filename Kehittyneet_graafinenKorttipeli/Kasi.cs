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
            kasi = kasi.OrderBy(kortti => kortti.arvo).ToList();
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
            if (kasi.Count() == maxKorttiaKadessa)
                return true;
            else
                return false;
        }

        public string getKadenArvo()
        {
            //kädet on aina pienimmästä suurimpaan
        //    string kadenArvo = "Ei mitään";
           

            //värisuora
            bool onkoTama = true;
            for (int i = 0; i < kasi.Count() - 1; i++)
            {                
                if (kasi.ElementAt(i).getMAA() != kasi.ElementAt(i+1).getMAA() || kasi.ElementAt(i).getArvo() != kasi.ElementAt(i+1).getArvo()-1)
                      onkoTama = false;                         
            }

            if (onkoTama)
                return "Värisuora";

            //neljä samaa
            
            int samojaKortteja = 0;
            for (int i = 0; i < kasi.Count() -1; i++)
            {
                if (kasi.ElementAt(i).getArvo() == kasi.ElementAt(i + 1).getArvo())
                {
                    samojaKortteja++;

                    if (samojaKortteja == 3) //ensimmäinen verrattava kortti ei laskussa mukana
                        return "Neloset";
                }

                else
                    samojaKortteja = 0;
            }



            //mökki          
            // vaihtoehdot XXX YY tai XX YYY
            if (kasi.ElementAt(0).getArvo() == kasi.ElementAt(1).getArvo() && kasi.ElementAt(0).getArvo() == kasi.ElementAt(2).getArvo())
                if (kasi.ElementAt(3).getArvo() == kasi.ElementAt(4).getArvo())
                    return "Mökki";

            if (kasi.ElementAt(0).getArvo() == kasi.ElementAt(1).getArvo())
                if (kasi.ElementAt(2).getArvo() == kasi.ElementAt(3).getArvo() && kasi.ElementAt(2).getArvo() == kasi.ElementAt(4).getArvo())
                    return "Mökki";

            //väri
            onkoTama = true;
            for (int i = 1; i < kasi.Count(); i++)
            {
                if (kasi.ElementAt(0).getMAA() != kasi.ElementAt(i).getMAA())
                {
                    onkoTama = false;
                    break;
                }
            }

            if (onkoTama)
                return "Väri";

            //suora
            onkoTama = true;
            for (int i = 0; i < kasi.Count()-1; i++)
            {
                if (kasi.ElementAt(i).getArvo() != kasi.ElementAt(i+1).getArvo()-1)
                {
                    onkoTama = false;
                    break;
                }
            }

            if (onkoTama)
                return "Suora";

            //kolmoset 
            samojaKortteja = 0;
            for (int i = 0; i < kasi.Count()-1; i++)
            {
                if (kasi.ElementAt(i).getArvo() == kasi.ElementAt(i + 1).getArvo())
                {
                    samojaKortteja++;
                    if (samojaKortteja == 2) //ensimmäinen verrattava kortti ei mukana
                        return "Kolmoset";

                }
                else
                {
                    samojaKortteja = 0;
                }
            }

            //kaksi paria
            samojaKortteja = 0;
            onkoTama = false; //jos tulee yksi pari -> true ja toinen pari katotaan vertailulla
            for (int i = 0; i < kasi.Count() - 1; i++)
            {
                if (kasi.ElementAt(i).getArvo() == kasi.ElementAt(i + 1).getArvo())
                {
                    if (onkoTama)
                        return "Kaksi paria";
                    else
                        onkoTama = true;
                }
            }

            //pari
            for (int i = 0; i < kasi.Count()-1; i++)
                if (kasi.ElementAt(i).getArvo() == kasi.ElementAt(i + 1).getArvo())
                    return "Pari";

            //jos ei mitään niin palauta hai
            
            switch (kasi.ElementAt(4).getArvo())
            {
                case 11:
                    return "Jätkä hai";
                    
                case 12:
                    return "Rouva hai";

                case 13:
                    return "Kunkku hai";

                case 14:
                    return "Ässä hai";

                default:
                    return kasi.ElementAt(4).getArvo().ToString() + " hai";
            }
        }
    }
}
