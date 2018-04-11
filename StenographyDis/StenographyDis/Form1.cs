using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace StenographyDis
{
    public partial class Form1 : Form
    {
        int thecount = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void loadImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open Image";
            //theDialog.Filter = "TXT files|*.txt";
            theDialog.InitialDirectory = @"C:\";

            if (theDialog.ShowDialog() == DialogResult.OK)
            {

                pictureBox1.Image = Bitmap.FromFile(theDialog.FileName);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        { 
            string text = txtEncrypt.Text;
            Bitmap myBitmap = new Bitmap(pictureBox1.Image);
            if (checkBoxRandom.Checked == true)
            {
                RandomClass r1 = new RandomClass(text, myBitmap);
                
            }
            else
            {
                //HERE CARRY ON BUT DO IN SEPERATE CLASS
                int startBit = 0;
                startBit = Convert.ToInt32(txtStartBit.Text);
                Console.WriteLine(startBit);
                List<string> ls = new List<string>();
                //Gets the ascii value of each character from the string
                //Could changet this from an array to make easier?
                //Gets 
                byte[] asciiBytes = Encoding.ASCII.GetBytes(text);

                foreach (byte b in asciiBytes)
                {
                    ls.Add(Convert.ToString(b, 2).PadLeft(8, '0'));

                }

                //List of of the message bits
                List<Char> lsChar = ls.SelectMany(s => s.ToCharArray()).ToList();


                // int intergers = int.Parse(n);

                byte[] rgbBytes = new byte[0];
                int R = 0, G = 0, B = 0;
                int count = 0;
                int stringLength = lsChar.Count;
                int isevenBitsPixel = 0, isevenBitsPixel1 = 0;
                string sevenBitsPixel = "Null", sevenBitsPixel1 = "Null";
                int countHeight = 0;

                for (int i = 0; i < myBitmap.Width; i++)
                {
                    for (int j = 0; j < myBitmap.Height; j++)
                    {
                        countHeight = countHeight + 1;
                    }
                }


                Console.WriteLine(countHeight);


                for (int i = 0; i < myBitmap.Width; i++)
                {
                    for (int j = 0; j < myBitmap.Height; j++)
                    {
                        if (count < stringLength)
                        {
                            //CHANGE THESE BACK TO I AND J
                            Color pixel = myBitmap.GetPixel(i, j);
                            Console.WriteLine("Blue Pixel Before  " + Convert.ToString(pixel.B, 2).PadLeft(8, '0'));
                            string bitsPixel = Convert.ToString(pixel.B, 2).PadLeft(8, '0');

                            sevenBitsPixel = bitsPixel.Substring(0, 7);

                            // now, clear the least significant bit (LSB) from each pixel element
                            //Therefore Three bits in each pixel spare
                            //EVERY PIXEL IN THE IMAGE HAS LSB REMOVED COULD IMPROVE THIS ALGORITHM SO IT DEPENDS ON SIZE OF TEXT
                            //BITMANIPULTION
                            Console.WriteLine("The bit that needs hiding in the pixel =" + lsChar[count].ToString());

                            if (lsChar[count].ToString() == "1")
                            {
                                sevenBitsPixel = sevenBitsPixel + "1";

                                isevenBitsPixel = Convert.ToInt32(sevenBitsPixel, 2);

                                Console.WriteLine("Blue Pixel After" + sevenBitsPixel);

                            }
                            else if (lsChar[count].ToString() == "0")
                            {
                                sevenBitsPixel = sevenBitsPixel + "0";

                                isevenBitsPixel = Convert.ToInt32(sevenBitsPixel, 2);
                                Console.WriteLine("Blue Pixel After" + sevenBitsPixel);
                            }
                            else
                            {
                                Console.WriteLine("ERRORR");
                            }

                            Console.WriteLine("--------------------");

                            myBitmap.SetPixel(i, j, Color.FromArgb(pixel.R, pixel.G, isevenBitsPixel));



                        }

                        if (count == stringLength)
                        {

                            {
                                Color pixel = myBitmap.GetPixel(i, j);
                                Console.WriteLine("Blue Pixel Before 0 d " + Convert.ToString(pixel.B, 2).PadLeft(8, '0'));
                                string bitsPixel1 = Convert.ToString(pixel.B, 2).PadLeft(8, '0');
                                sevenBitsPixel1 = bitsPixel1.Substring(0, 8);

                                sevenBitsPixel1 = "00000000";

                                isevenBitsPixel1 = Convert.ToInt32(sevenBitsPixel1, 2);
                                Console.WriteLine("Blue Pixel After" + sevenBitsPixel1);

                                myBitmap.SetPixel(i, j, Color.FromArgb(pixel.R, pixel.G, isevenBitsPixel1));
                            }
                        }
                        count++;
                    }
                }
                //Adding changed image into picture box 2 

                pictureBox2.Image = myBitmap;
            }
            
        }

        private void btnDetect_Click(object sender, EventArgs e)
        {
            
            Bitmap myBitmap = new Bitmap(pictureBox1.Image);
            for (int i = 0; i < myBitmap.Width; i++)
            {
                for (int j = 0; j < myBitmap.Height; j++)
                {
                    thecount = thecount + 1;

                }

            }

            lblCount.Text = thecount.ToString();
            
        }


        private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap saveBitmap = new Bitmap(pictureBox2.Image);
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";

            if (save.ShowDialog() == DialogResult.OK)
            {
                saveBitmap.Save(save.FileName);
            }
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            List<int> LSBList = new List<int>();
            Bitmap myBitmap = new Bitmap(pictureBox2.Image);
            
            for (int i = 0; i < myBitmap.Width; i++)
            {
                for (int j = 0; j < myBitmap.Height; j++)
                {
                    Color pixel = myBitmap.GetPixel(i, j);
                    string bitsPixel = Convert.ToString(pixel.B, 2).PadLeft(8, '0');
                    if (bitsPixel == "00000000")
                    {
                        goto End;
                    }
                    else
                   {
                        string stringLSBvalue = Convert.ToString(bitsPixel.Substring(7, 1));

                        int LSBvalue = Convert.ToInt32(stringLSBvalue);
                        
                        LSBList.Add(LSBvalue);
                    }

                }
                





            }
            End:;
            
            List<string> LSBstring = new List<string>();
            for (int i = 0; i < LSBList.Count / 8; i++)
            {
                LSBstring.Add(string.Concat(LSBList.Skip(i * 8).Take(8)));
            }

            List<string> OutputList = new List<string>();
            foreach (string s in LSBstring)
            {
                int output = Convert.ToInt32(s.ToString(), 2);
                string stringoutput = Char.ConvertFromUtf32(output);

                OutputList.Add(stringoutput);
            }

            string decodedmessage = "";
            foreach(string s in OutputList)
            {
                decodedmessage = decodedmessage + s;
            }

            lblEncryptMessage.Text = "Decoded Message: " + decodedmessage;
        }

       
    }
}
 

