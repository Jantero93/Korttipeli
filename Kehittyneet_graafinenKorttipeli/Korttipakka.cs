using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kehittyneet_graafinenKorttipeli
{
    class Korttipakka
    {
        /* Kortit listaan kun siinä on kivoja sorttaus metodeja valmiina, pelin toteutuksessa stäkki. vois olla vaan metodeilla temp_lista
         koko luokan attribuutin sijaan... legacy koodia ennen kun stakki tuli käyttöön... */
       private List<Kortti> korttiLista = new List<Kortti>();
       private Stack<Kortti> korttiPakka = new Stack<Kortti>();

        //tekee konstruktorissa valmiiksi sekoitetun stäkin
        public Korttipakka()
        {            
            for (int i = 2; i < 15; i++)
            {
                //Enum.GetValues = hakee enumin varsinaisen int arvon. risti = 0, ruutu = 1, hertta = 2, pata = 3 
                //eli pakkaan tehdään kaikki kortit ristit 2 - A, ruudut 2 - A jne
                foreach (MAA maa in Enum.GetValues(typeof(MAA)))
                    korttiPakka.Push(new Kortti(i, maa)); 
            }

            this.sekoitaKorttiPakka();            
        }   

        public void sekoitaKorttiPakka()
        {   /* 
            esim tälläinen toteutus niin saisi sekoitaKorttilista() ja jarjestaKorttilista() metodit pois
             */
            List<Kortti> tempKorttilista = new List<Kortti>();

            while (korttiPakka.Count > 0)
                tempKorttilista.Add(korttiPakka.Pop());

            tempKorttilista = tempKorttilista.OrderBy(a => Guid.NewGuid()).ToList();

            foreach (Kortti kortti in tempKorttilista)
                korttiPakka.Push(kortti);
        }

        public override string ToString()
        {   // legacya
            string tuloste = "";
            foreach (Kortti kortti in korttiPakka)
            {
               tuloste += kortti + "\n";
            }
            return tuloste;
        }

        public Kortti annaKortti()
        {
            if (korttiPakka.Count() <= 0)
                throw new IndexOutOfRangeException("Korttipakasta loppui kortit");
            else
                return korttiPakka.Pop();
        }

        public int getStackKoko()
        {
            return korttiPakka.Count();
        }

    }
}
