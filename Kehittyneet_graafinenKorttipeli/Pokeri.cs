using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kehittyneet_graafinenKorttipeli
{
    class Pokeri
    {
        private Korttipakka korttipakka;     
        private int vuorojaJaljella;        
        
        public Pokeri()
        {
            korttipakka = new Korttipakka();
            vuorojaJaljella = 3;
        }

        //peli päälle, jaa käsi täyteen kortteja (5 kpl)
        public void kaynnistaPeli(Kasi pelaaja1, Kasi pelaaja2)
        {
            while(!pelaaja1.kasiTaynna())            
                pelaaja1.lisaaKortti(korttipakka.annaKortti());

            while (!pelaaja2.kasiTaynna())
                pelaaja2.lisaaKortti(korttipakka.annaKortti());

            pelaaja1.jarjestaKortit();
            pelaaja2.jarjestaKortit();
        }

        //jos UIssa kääntyy kortti --> kääntyy kortti olion attribuutti
        public void kaannaKortti(Kasi pelaaja, int picBoxIndex)
        {
            pelaaja.getKortti(picBoxIndex).kaannaKortti();
        }

        //vaihda käännetyt kortit kun painetaan vaihda
        public void vaihdaKortit(Kasi pelaaja)
        {
            //otetaan kortti pois niin indeksointi menee sekaisin
            //katsotaan vaihdettavat indeksit ja vaihdetaan kaikki kerralla
            List<int> vaihdettavatIndeksit = new List<int>();

            for (int i = 0; i < 5; i++)
            {
                if (pelaaja.getKortti(i).korttiOikeinPain() == false)
                { 
                    vaihdettavatIndeksit.Add(i);                   
                }
            }

            for (int i = vaihdettavatIndeksit.Count()-1; i > -1; i--)
            {
                int temp = vaihdettavatIndeksit.ElementAt(i);
                pelaaja.otaKortti(temp);
            }

            for (int i = 0; i < vaihdettavatIndeksit.Count(); i++)
            {
                pelaaja.lisaaKortti(korttipakka.annaKortti());
            } 

            //järjestä kortit suuruusjärjestykseen
            pelaaja.jarjestaKortit();
        }

        public void vahennaJaljellaolevariaVuoroja()
        {
            vuorojaJaljella--;
        }
        
        public int GetVuorojaJaljella()
        {
            return vuorojaJaljella;
        }

        public string getKadenArvo(Kasi pelaaja)
        {
            return pelaaja.getKadenArvo();
        }

        public string getKortinTiedostonimi(Kasi pelaaja, int kortinIndex)
        {
            return pelaaja.getKortti(kortinIndex).getTiedostoNimi();
        }

        public bool getKorttiOikeinPain(Kasi pelaaja, int kortinIndex)
        {
            return pelaaja.getKortti(kortinIndex).korttiOikeinPain();
        }
    }
}
