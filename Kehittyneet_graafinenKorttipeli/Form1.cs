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
        Pokeri pokeripeli = new Pokeri();
        Kasi pelaaja1 = new Kasi();
        Kasi pelaaja2 = new Kasi();

      

        //globaaleja
        Kasi kasi = new Kasi();
        Kasi kasi2 = new Kasi();
        Korttipakka pakka = new Korttipakka();
        int vuorojaJaljella = 0;
        // ei käytössä
        bool peliKaynnissa;




        List<String> korttienTeemat = new List<String>();
        int valittuTeemaIndex = 0;
        //korttien takapuolet
        string korttiVaarinpain = "cards_59_backs.bmp";

        //teemoja
        // const string korttiVaarinpain = "cards_53_backs.bmp"; 
        // const string korttiVaarinpain = "cards_54_backs.bmp";
        // const string korttiVaarinpain = "cards_57_backs.bmp";
        // const string korttiVaarinpain = "cards_60_backs.bmp";
        // const string korttiVaarinpain = "cards_62_backs.bmp";
        // const string korttiVaarinpain = "cards_65_backs.bmp";
        // const string korttiVaarinpain = "cards_63_backs.bmp";

        //pictureBoxit listassa
        List<PictureBox> PicBoxitPelaaja1 = new List<PictureBox>();
        List<PictureBox> PicBoxitPelaaja2 = new List<PictureBox>();

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
            kadenArvoLabel2.Text = "";

            //pictureBoxit yhteen listaan (pelaaja 1)
            PicBoxitPelaaja1.Add(pictureBox0);
            PicBoxitPelaaja1.Add(pictureBox1);
            PicBoxitPelaaja1.Add(pictureBox2);
            PicBoxitPelaaja1.Add(pictureBox3);
            PicBoxitPelaaja1.Add(pictureBox4);

            //pictureboxit listaan pelaaja 2
            PicBoxitPelaaja2.Add(pictureBox5);
            PicBoxitPelaaja2.Add(pictureBox6);
            PicBoxitPelaaja2.Add(pictureBox7);
            PicBoxitPelaaja2.Add(pictureBox8);
            PicBoxitPelaaja2.Add(pictureBox9);

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
            pokeripeli = new Pokeri();
            pelaaja1 = new Kasi();
            pelaaja2 = new Kasi();

            //lisätään pelaajille kortit
            pokeripeli.kaynnistaPeli(pelaaja1, pelaaja2);            

            //disabloi uusi peli nappi
            button1.Enabled = false;

            //aktivoi korttien vaihtonappi. vois ehkä myös hidettää kokonaan sen sijaan kuin enable/disable
            button2.Enabled = true;

            //tyhjennä käden arvo label
            kadenArvoLabel.Text = "";
            kadenArvoLabel2.Text = "";                     

            //tulosta aloitustilanne  
            tulostaKokoKasiAnimaatioAsync(); 
        }

        //tiputusvalikko credits
        private void creditsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Versio 2.0\n\nTekijat:\n\nJani Sillanpaa", "Moro!");
        }

        //koko korttirivin animointi
        private async void tulostaKokoKasiAnimaatioAsync()
        {            
            LABEL2.Text = "Vaihtoja jäljellä:" + pokeripeli.GetVuorojaJaljella().ToString(); 

            // ota pictureBoxeista kuvat pois, tarviikohan?
            for (int i = 0; i < PicBoxitPelaaja1.Count(); i++)
            { 
                PicBoxitPelaaja1.ElementAt(i).InitialImage = null;
                PicBoxitPelaaja2.ElementAt(i).InitialImage = null;
            }

            //käännä kaikki kortit väärinpäin
            for (int i = 0; i < 5; i++)
            {
                PicBoxitPelaaja1.ElementAt(i).Image = Image.FromFile(korttiVaarinpain);
                PicBoxitPelaaja2.ElementAt(i).Image = Image.FromFile(korttiVaarinpain);
                await Task.Delay(odotusaika);
            }
            
            //käännä kaikki kortit oikeinpäin
            for (int i = 0; i < PicBoxitPelaaja1.Count(); i++)
            {
                PicBoxitPelaaja1.ElementAt(i).Image = Image.FromFile(pokeripeli.getKortinTiedostonimi(pelaaja1,i));

                PicBoxitPelaaja2.ElementAt(i).Image = Image.FromFile(pokeripeli.getKortinTiedostonimi(pelaaja2,i));
                await Task.Delay(odotusaika);
            }
            //käden arvo
            kadenArvoLabel.Text = "Pelaaja 1:\n" + pelaaja1.getKadenArvo();
            kadenArvoLabel2.Text = "Pelaaja 2:\n" + pelaaja2.getKadenArvo();
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
            //vähennä jäljellä olevia vuoroja
            pokeripeli.vahennaJaljellaolevariaVuoroja();

            // katotaan vaihdettavat kortit suurimmasta pienimpää, muuten indeksointi menee rikki (((menee joka tapauksessa, ei väliä)))
            pokeripeli.vaihdaKortit(pelaaja1);
            pokeripeli.vaihdaKortit(pelaaja2);

            //tulosta animaatio kaikille korteille
            tulostaKokoKasiAnimaatioAsync();

            //jos vuoroja jäljellä 0 -->  enabloi uusi peli nappi ja disabloi vaihtonappi. 
            //pitäisi varmaan tulostaa myös mahdollinen käden vahvuus
            if (pokeripeli.GetVuorojaJaljella() == 0)
            {              
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
            korttienVaihto(PicBoxitPelaaja1.ElementAt(index), index, pelaaja1);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            int index = 1;
            korttienVaihto(PicBoxitPelaaja1.ElementAt(index), index,pelaaja1);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            int index = 2;
            korttienVaihto(PicBoxitPelaaja1.ElementAt(index), index, pelaaja1);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            int index = 3;
            korttienVaihto(PicBoxitPelaaja1.ElementAt(index), index, pelaaja1);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            int index = 4;
            korttienVaihto(PicBoxitPelaaja1.ElementAt(index), index, pelaaja1);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            int index = 0;
            korttienVaihto(PicBoxitPelaaja2.ElementAt(index), index, pelaaja2);
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            int index = 1;
            korttienVaihto(PicBoxitPelaaja2.ElementAt(index), index, pelaaja2);
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            int index = 2;
            korttienVaihto(PicBoxitPelaaja2.ElementAt(index), index, pelaaja2);
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            int index = 3;
            korttienVaihto(PicBoxitPelaaja2.ElementAt(index), index, pelaaja2);
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            int index = 4;
            korttienVaihto(PicBoxitPelaaja2.ElementAt(index), index, pelaaja2);
        }

        //klikki funktioille yhteinen toteutus, sama järjestys picboxien indeksit ja käden korttien indeksit ettei mee sekaisin
        private void korttienVaihto(PictureBox p_box, int index, Kasi player)
        {
            //jos ei vaihtoja jäljellä, ei voi vaihtaa enää
            if (pokeripeli.GetVuorojaJaljella() == 0)
                return;
            
            
            // jos kortti oikeinpäin
            if (pokeripeli.getKorttiOikeinPain(player, index))
            {
                //picBox tyhjäksi ja kortti väärinpäin kuva, aseta kortti olioon booleani
                p_box.InitialImage = null;
                p_box.Image = Image.FromFile(korttiVaarinpain);
                pokeripeli.kaannaKortti(player, index);
            }
            else
            {
                //kortti oikein päin, aseta booleani
                p_box.InitialImage = null;
                p_box.Image = Image.FromFile(pokeripeli.getKortinTiedostonimi(player,index));
                pokeripeli.kaannaKortti(player, index);
            }            
        }


        private void korttienVaihto2(PictureBox p_box, int index)
        {
            //jos ei vaihtoja jäljellä, ei voi vaihtaa enää
            if (vuorojaJaljella == 0)
            {
                return;
            }

            // jos kortti oikeinpäin
            if (kasi2.getKortti(index).korttiOikeinPain())
            {
                //picBox tyhjäksi ja kortti väärinpäin kuva, aseta kortti olioon booleani
                p_box.InitialImage = null;
                p_box.Image = Image.FromFile(korttiVaarinpain);
                kasi2.getKortti(index).kaannaKortti();
            }
            else
            {
                p_box.InitialImage = null;
                p_box.Image = Image.FromFile(kasi2.getKortti(index).getTiedostoNimi());
                kasi2.getKortti(index).kaannaKortti();
            }
        }


        
        private void teemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            valittuTeemaIndex++;
            if (valittuTeemaIndex == korttienTeemat.Count()) 
                valittuTeemaIndex = 0;

            korttiVaarinpain = korttienTeemat.ElementAt(valittuTeemaIndex);
            tulostaKokoKasiAnimaatioAsync();            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }

}

