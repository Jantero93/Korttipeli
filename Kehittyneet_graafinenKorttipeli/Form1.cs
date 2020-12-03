using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Kehittyneet_graafinenKorttipeli
{
    //globaali enum 
    public enum MAA
    {
        RISTI, RUUTU, HERTTA, PATA
    }



    public partial class Form1 : Form
    {
        //muuttujien alustus

            /*
             Dokumentaatio:
            
            Kuvia ei pysty fiksusti vertaileen?
            Huomaa että pictureBoxit ja Kadessa olevien kortit samalla indeksillä

            atm tulostus tehty sorttaamalla aina kortit ja tulostettu kaikki kortit uudelleen, 
            kasi olion indeksointi muuttuu kun poistaa kortin kädestä, mikä ei ole viimeisessä indeksissä

            */

      

        //globaaleja
        Kasi kasi = new Kasi();
        Korttipakka pakka = new Korttipakka();
        int vuorojaJaljella = 0;
        // ei käytössä
        bool peliKaynnissa = false;



        List<String> korttienTeemat = new List<String>();
        int valittuTeemaIndex = 0;
        //korttien takapuolet
        string korttiVaarinpain = "cards_59_backs.bmp";
        // const string korttiVaarinpain = "cards_53_backs.bmp"; 
        // const string korttiVaarinpain = "cards_54_backs.bmp";
        //  const string korttiVaarinpain = "cards_57_backs.bmp";
        //  const string korttiVaarinpain = "cards_60_backs.bmp";
        //  const string korttiVaarinpain = "cards_62_backs.bmp";
        //  const string korttiVaarinpain = "cards_65_backs.bmp";
        // const string korttiVaarinpain = "cards_63_backs.bmp";

        //pictureBoxit listassa
        List<PictureBox> pictureBoxit = new List<PictureBox>();

        //Lista jossa pidetään vaihdettavien korttien indeksi, ei tarvisi ehkä olla globaali
        List<int> vaihdettavatKortit = new List<int>();
        //"animaation" nopeus (ms)
        const int odotusaika = 100;



        public Form1()
        {
            InitializeComponent();
            button2.Enabled = false;
            LABEL2.Text = "";
            kadenArvoLabel.Text = "";

            //pictureBoxit yhteen listaan
            pictureBoxit.Add(pictureBox0);
            pictureBoxit.Add(pictureBox1);
            pictureBoxit.Add(pictureBox2);
            pictureBoxit.Add(pictureBox3);
            pictureBoxit.Add(pictureBox4);
            // korttien eri teemat listaan
            korttienTeemat.Add("cards_59_backs.bmp");
            korttienTeemat.Add("cards_53_backs.bmp");
            korttienTeemat.Add("cards_57_backs.bmp");
            korttienTeemat.Add("cards_65_backs.bmp");
        }

        private void peliPaalle()
        {
            //alustaa aina käden ja korttipakan kun aloittaa uuden pelin
            // pakka = null;
            pakka = new Korttipakka();
            kasi = new Kasi();
            pakka.sekoitaKorttiPakka();

            //DEBUGGAUSTA
            Kasi testikasi = new Kasi();
            Kortti kort2 = new Kortti(8, MAA.PATA);
            Kortti kort3 = new Kortti(7, MAA.HERTTA);
            Kortti kort4 = new Kortti(14, MAA.RISTI);
            Kortti kort5 = new Kortti(14, MAA.RISTI);
            Kortti kort6 = new Kortti(14, MAA.PATA);
            testikasi.lisaaKortti(kort2);
            testikasi.lisaaKortti(kort3);
            testikasi.lisaaKortti(kort4);
            testikasi.lisaaKortti(kort5);
            testikasi.lisaaKortti(kort6);
            testikasi.jarjestaKortit();
            string testi = testikasi.getKadenArvo();
            //DEBUGGAUSTA

            //peli käyntiin
            peliKaynnissa = true;
            //aseta vuorojen määrä
            vuorojaJaljella = 9;

            //disabloi uusi peli nappi
            button1.Enabled = false;

            //aktivoi korttien vaihtonappi. vois ehkä myös hidettää kokonaan sen sijaan kuin enable/disable
            button2.Enabled = true;

            //tyhjennä käden arvo label
            kadenArvoLabel.Text = "";

            //täytä käsi (5 korttia)
            while (!kasi.kasiTaynna())
            {
                kasi.lisaaKortti(pakka.annaKortti());
            }
            //järjestä kortit numerojärjestykseen                      
            kasi.jarjestaKortit();

            //tulosta aloitustilanne  
            tulostaKokoKasiAnimaatioAsync(); 
        }

        //tiputusvalikko credits
        private void creditsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Versio 1.1\n\nTekijat:\n\nJani Sillanpaa\nMatti Moisander\nEemil Peltonen", "Moro!");
        }

        //koko korttirivin animointi
        private async void tulostaKokoKasiAnimaatioAsync()
        {
            LABEL2.Text = "Vaihtoja jäljellä:\n" + vuorojaJaljella.ToString();

            // ota pictureBoxeista kuvat pois, tarviikohan?
            for (int i = 0; i < pictureBoxit.Count(); i++)
                pictureBoxit.ElementAt(i).InitialImage = null;

            //käännä kaikki kortit väärinpäin
            for (int i = 0; i < 5; i++)
            {
                pictureBoxit.ElementAt(i).Image = Image.FromFile(korttiVaarinpain);
                await Task.Delay(odotusaika);
            }

           
            

            //käännä kaikki kortit oikeinpäin
            for (int i = 0; i < pictureBoxit.Count(); i++)
            {
                pictureBoxit.ElementAt(i).Image = Image.FromFile(kasi.getKortti(i).getTiedostoNimi());
                await Task.Delay(odotusaika);
            }
            //käden arvo
            kadenArvoLabel.Text = "Käden arvo:\n" + kasi.getKadenArvo();

        }

        //menupalkin quit
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //uusi peli
        private void button1_Click(object sender, EventArgs e)
        {
            peliPaalle();
        }

        // :D
        private void salaisuusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ei saa olla salaisuuksia");
        }

        // jos kortti väärinpäin, kortti vaihdetaan. Eli vaihda kortit painike
        private void button2_Click(object sender, EventArgs e)
        {
            /* paskaa koodia kun koitin ettiä bugia */
            //listaan kortit jotka väärinpäin ja vaihdetaan ne
            List<int> vaihdettavatIndeksit = new List<int>();

            //vähennä jäljellä olevia vuoroja
            vuorojaJaljella--;

            

            // katotaan vaihdettavat kortit suurimmasta pienimpää, muuten indeksointi menee rikki (((menee joka tapauksessa, ei väliä)))
            for (int i = 4; i >= 0; i--)
                if (kasi.getKortti(i).korttiOikeinPain() == false)
                    vaihdettavatIndeksit.Add(i);

            int count = 0;
            int temp = -1;


            //ota vaihdettavat kortit pois
            for (int i = 0; i < vaihdettavatIndeksit.Count(); i++)
            {
                temp = vaihdettavatIndeksit.ElementAt(i);
                kasi.otaKortti(temp);
                count++;
            }

            //lisätään saman verran kortteja
            for (int i = 0; i < count; i++)
            {
                kasi.lisaaKortti(pakka.annaKortti());
            }
                                          
            //järjestetään käden kortit ja printataan se
            kasi.jarjestaKortit();

            tulostaKokoKasiAnimaatioAsync();

            //jos vuoroja jäljellä 0 --> esitä käden vahvuus, enabloi uusi peli nappi ja disabloi vaihtonappi. 
            //pitäisi varmaan tulostaa myös mahdollinen käden vahvuus
            if (vuorojaJaljella == 0)
            { 
                peliKaynnissa = false;
                button1.Enabled = true;
                button2.Enabled = false;
            }
        }

        /*
         kaikille pictureBoxeille clikkaus funktio josta viedään yhteen funktioon ettei tarvis kirjotella niin paljoa
         oisko parempaa toteutusta? 
         */
        private void pictureBox0_Click(object sender, EventArgs e)
        {
            int index = 0;
            korttienVaihto(pictureBoxit.ElementAt(index), index);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            int index = 1;
            korttienVaihto(pictureBoxit.ElementAt(index), index);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            int index = 2;
            korttienVaihto(pictureBoxit.ElementAt(index), index);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            int index = 3;
            korttienVaihto(pictureBoxit.ElementAt(index), index);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            int index = 4;
            korttienVaihto(pictureBoxit.ElementAt(index), index);
        }

        //klikki funktioille yhteinen toteutus, sama järjestys picboxien indeksit ja käden korttien indeksit ettei mee sekaisin
        private void korttienVaihto(PictureBox p_box, int index)
        {   // jos kortti oikeinpäin
            if (kasi.getKortti(index).korttiOikeinPain())
            {
                //picBox tyhjäksi ja kortti väärinpäin kuva, aseta kortti olioon booleani
                p_box.InitialImage = null;
                p_box.Image = Image.FromFile(korttiVaarinpain);
                kasi.getKortti(index).kaannaKortti();
            }
            else
            {
                p_box.InitialImage = null;
                p_box.Image = Image.FromFile(kasi.getKortti(index).getTiedostoNimi());
                kasi.getKortti(index).kaannaKortti();
            }
        }

        // en oo uskaltanu poistaa ku visual studio herjaa
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void LABEL2_Click(object sender, EventArgs e)
        {

        }

        private void teemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            valittuTeemaIndex++;
            if (valittuTeemaIndex == korttienTeemat.Count()) 
                valittuTeemaIndex = 0;

            korttiVaarinpain = korttienTeemat.ElementAt(valittuTeemaIndex);
            tulostaKokoKasiAnimaatioAsync();            
        }
    }




}

