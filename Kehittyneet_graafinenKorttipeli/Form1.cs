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

    public enum MAA
    {
        RISTI, RUUTU, HERTTA, PATA
    }



    public partial class Form1 : Form
    {
        //muuttujien alustus



        bool peliKaynnissa = false;

        Kasi kasi = new Kasi();
        Korttipakka pakka = new Korttipakka();


        //spessu 
        //string korttiVaarinpain = "cards_53_backs.bmp"; 
        const string korttiVaarinpain = "cards_59_backs.bmp";

        //pictureBoxit listassa
        List<PictureBox> pictureBoxit = new List<PictureBox>();

        //Lista jossa pidetään vaihdettavien korttien indeksi
        List<int> vaihdettavatKortit = new List<int>();

        const int odotusaika = 100;



        public Form1()
        {
            InitializeComponent();
            button2.Enabled = false;
            LABEL2.Text = "";
        }

        private void peliPaalle()
        {
            //alustaa aina käden ja korttipakan kun aloittaa uuden pelin
            pakka = new Korttipakka();
            kasi = new Kasi();
            pakka.sekoitaKorttiPakka();
            LABEL2.Text = "Jäljellä olevat korit:\n" + pakka.getStackKoko().ToString();

            //tyhjennä käsi, jos alotetaan uusi peli kesken pelin
            // niin pakka resetoituu mutta käteen jää kortit

            //pictureBoxit yhteen listaan
            pictureBoxit.Add(pictureBox0);
            pictureBoxit.Add(pictureBox1);
            pictureBoxit.Add(pictureBox2);
            pictureBoxit.Add(pictureBox3);
            pictureBoxit.Add(pictureBox4);

            button2.Enabled = true;

            
            

            //täytä käsi (5 korttia)
            while (!kasi.kasiTaynna())
            {
                kasi.lisaaKortti(pakka.annaKortti());
            }
            //järjestä kortit numerojärjestykseen
           

            // näytä kortit väärinpäin ja sitten "animoidusti oikeinpäin"
            /*
               for (int i = 0; i < 5; i++)
               {
                   pictureBoxit.ElementAt(i).Image = Image.FromFile(korttiVaarinpain);
                   await Task.Delay(odotusaika);
               }

               for (int i = 0; i < 5; i++)
               {
                   pictureBoxit.ElementAt(i).Image = Image.FromFile(kasi.getKortti(i).getTiedostoNimi());
                   await Task.Delay(odotusaika);
               }
               */


            kasi.jarjestaKortit();


            tulostaKokoKasiAnimaatio();



            //  for (int i = 0; i < 5; i++)
            //   pictureBoxit.ElementAt(i).Image = Image.FromFile(kasi.getKortti(i).getTiedostoNimi());

            if (pakka.getStackKoko() < 5)
            {
                Application.Exit();
            }

            //pictureBox0.Image = Image.FromFile(kasi.getKortti(0).getTiedostoNimi());


        }

        private void creditsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Versio 1.0\n\nTekijat:\n\nJani Sillanpaa\nMatti Moisander\nEemil Peltonen", "Moro");
        }

        private async Task tulostaKokoKasiAnimaatio()
        {

            for (int i = 0; i < 5; i++)
                pictureBoxit.ElementAt(i).InitialImage = null;

            for (int i = 0; i < 5; i++)
            {

                pictureBoxit.ElementAt(i).Image = Image.FromFile(korttiVaarinpain);
                await Task.Delay(odotusaika);
            }

            for (int i = 0; i < 5; i++)
            {
                pictureBoxit.ElementAt(i).Image = Image.FromFile(kasi.getKortti(i).getTiedostoNimi());
                await Task.Delay(odotusaika);
            }
            LABEL2.Text = "Jäljellä olevat korit:\n" + pakka.getStackKoko().ToString();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            peliPaalle();
        }


        private void salaisuusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ei saa olla salaisuuksia");
        }

        // jos kortti väärinpäin, kortti vaihdetaan
        private void button2_Click(object sender, EventArgs e)
        {

            //listaan kortit jotka väärinpäin ja vaihdetaan ne
            List<int> vaihdettavatIndeksit = new List<int>();

            // katotaan vaihdettavat kortit suurimmasta pienimpää, muuten indeksointi menee rikki
            for (int i = 4; i >= 0; i--)
                if (kasi.getKortti(i).getStatus() == false)
                    vaihdettavatIndeksit.Add(i);

            int count = 0;
            int temp = -1;


            for (int i = 0; i < vaihdettavatIndeksit.Count(); i++)
            {
                temp = vaihdettavatIndeksit.ElementAt(i);
                kasi.otaKortti(temp);
                count++;
            }

            for (int i = 0; i < count; i++)
            {
                kasi.lisaaKortti(pakka.annaKortti());
            }
                                          
            //järjestetään käsi ja printataan se
            kasi.jarjestaKortit();

            tulostaKokoKasiAnimaatio();
        }

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

        private void korttienVaihto(PictureBox p_box, int index)
        {   // jos kortti oikeinpäin
            if (kasi.getKortti(index).getStatus())
            {
                //picBox tyhjäksi ja kortti väärinpäin kuva, aseta kortti olioon booleani
                p_box.InitialImage = null;
                p_box.Image = Image.FromFile(korttiVaarinpain);
                kasi.getKortti(index).setStatus();

            }
            else
            {
                p_box.InitialImage = null;
                p_box.Image = Image.FromFile(kasi.getKortti(index).getTiedostoNimi());
                kasi.getKortti(index).setStatus();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void LABEL2_Click(object sender, EventArgs e)
        {

        }
    }




}

