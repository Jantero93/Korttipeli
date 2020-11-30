using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kehittyneet_graafinenKorttipeli
{
    class Korttipakka
    {
        /* Kortit listaan kun siinä on kivoja sorttaus metodeja valmiina, pelin toteutuksessa stäkki, vois olla vaan metodeilla temp_lista
         koko luokan attribuutin sijaan... legacy koodia ennen kun stakki tuli käyttöön... */
       private List<Kortti> korttiLista = new List<Kortti>();
       private Stack<Kortti> korttiPakka = new Stack<Kortti>();

        //tekee konstruktorissa valmiiksi sekoitetun stäkin
        public Korttipakka()
        {            
            for (int i = 2; i < 15; i++)
            {
                foreach (MAA maa in Enum.GetValues(typeof(MAA)))
                    korttiLista.Add(new Kortti(i, maa));
            }

            this.sekoitaKorttilista();

            foreach (Kortti kortti in korttiLista)
            {
                korttiPakka.Push(kortti);
            }
        }
        /* 
        public void tulostaKokoLista()
        {           
            for (int i = 0; i < korttiLista.Count(); i++)
            {
                Console.Write(korttiLista.ElementAt(i) + "\n");
            }   
            
        }        
        */
        private void jarjestaKorttilista()
        {
            korttiLista = korttiLista.OrderBy(p => p.arvo).ToList();
        }

        private void sekoitaKorttilista()
        {
            korttiLista = korttiLista.OrderBy(a => Guid.NewGuid()).ToList();
        }

        public void sekoitaKorttiPakka()
        {
            List<Kortti> temp_kortit = new List<Kortti>();

            while (korttiPakka.Count > 0)
                temp_kortit.Add(korttiPakka.Pop());

            temp_kortit = temp_kortit.OrderBy(a => Guid.NewGuid()).ToList();

            foreach (Kortti kortti in temp_kortit)
                korttiPakka.Push(kortti);

        }

        public override string ToString()
        {
            string tuloste = "";
            foreach (Kortti kortti in korttiPakka)
            {
               tuloste += kortti + "\n";
            }
            return tuloste;
        }

        public Kortti annaKortti()
        {
            return korttiPakka.Pop();
        }


        public int getStackKoko()
        {
            return korttiPakka.Count();
        }

    }
}
