﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Kehittyneet_graafinenKorttipeli
{
    class Kortti
    {
        /* kuvanTiedosto on tiedoston nimi kansiossa. Helppo tulostaa formissa */
        public int arvo;
        public MAA kortin_maa;
        string kuvanTiedosto;
        //pidetään tietoa kumminpäin kortti on grafiikoissa
        bool oikeinPain = true;
        
        public Kortti (int arvo_parametri, MAA kortin_maa_parametri)
        {
            if (arvo_parametri < 2 || arvo_parametri > 14)
            {
                throw new ArgumentException("Kortin arvo pitaa olla 2 - 14 (14 == ässä)");
            }

            switch (kortin_maa_parametri)
            {
                case MAA.RISTI:                    
                    break;

                case MAA.RUUTU:                    
                    break;

                case MAA.HERTTA:                    
                    break;

                case MAA.PATA:                   
                    break;

                default:
                    throw new ArgumentException("kortin maassa on jotain pahasti haikkaa");                   
            }

            arvo = arvo_parametri;
            kortin_maa = kortin_maa_parametri;
            // tiedosto nimet muotoa cards_ + arvo + maa 
            // ristit 2-14, ruudut (kortin arvo + 13)  15-27, (kortin arvo + 26) hertat 28-40, (kortin arvo + 39) padat 41-53
            string cardT = "cards_";          

            switch (kortin_maa)
            {
                case MAA.RISTI:
                    kuvanTiedosto += cardT;

                    if (arvo < 10)
                        kuvanTiedosto += "0" + arvo.ToString();
                    else
                        kuvanTiedosto += arvo.ToString();

                    kuvanTiedosto +=  "_" + "clubs.bmp";
                    break;

                case MAA.RUUTU:
                    kuvanTiedosto += cardT + (arvo + 13).ToString() + "_diamonds.bmp";
                    break;

                case MAA.HERTTA:
                    kuvanTiedosto += cardT + (arvo + 26).ToString() + "_hearts.bmp";
                    break;

                case MAA.PATA:
                    kuvanTiedosto += cardT + (arvo + 39).ToString() + "_spades.bmp";
                    break;
            }

        }

        public override string ToString()
        {
            // vanha turha toteutus
            /*
            string tulosta_arvo;
            string tulosta_maa;

            if (arvo == 14)
                tulosta_arvo = "A";
            else
                tulosta_arvo = arvo.ToString();

            switch (kortin_maa)
            {
                case MAA.RISTI:
                    tulosta_maa = "RISTI";
                    break;

                case MAA.RUUTU:
                    tulosta_maa = "RUUTU";
                    break;

                case MAA.HERTTA:
                    tulosta_maa = "HERTTA";
                    break;

                case MAA.PATA:
                    tulosta_maa = "PATA";
                    break;

                default:
                    tulosta_maa = "niin birusti erroria";
                    break;
            }

            //lisää tulosta_maahan välejä jotta se on aina 6 merkkiä pitkä
            if (tulosta_maa.Length < 6)
                while (tulosta_maa.Length < 6)
                    tulosta_maa += " ";

            //lisää väli jotta kaikki arvot samanpituisia
            if (tulosta_arvo.Length == 1)
                tulosta_arvo += " ";

            return tulosta_maa + " " + tulosta_arvo;          
            */
            return kuvanTiedosto;
        }

        public MAA getMAA()
        {
            return kortin_maa;
        }

        public int getArvo()
        {
            return arvo;
        }

        public string getTiedostoNimi()
        {
            return kuvanTiedosto;
        }

        public bool korttiOikeinPain()
        {
            return oikeinPain;
        }

        public void kaannaKortti()
        {
            oikeinPain = !oikeinPain;
        }
    }
}
