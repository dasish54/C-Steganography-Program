using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StenographyDis
{
    class RandomClass
    {
        public string hiddenText;
        public Bitmap myBitmap;
        public Bitmap finalBitmap;
        
        //Instantiate Class 
        public RandomClass(string text, Bitmap bitmap)
        {
            hiddenText = text;
            myBitmap = bitmap;
            RandomNumber();

        }

        public void RandomNumber()
        {
            //Count the number of pixel
            int count = 0;
            //Variable for first pixel
            int firstPixel = 0;
            //Counts the number of pixels
            for (int i = 0; i < myBitmap.Width; i++)
            {
                for (int j = 0; j < myBitmap.Height; j++)
                {
                    count = count + 1;
                }
            }
            //Number of pixels needed to store the string of text
            int pixelsNeeded = hiddenText.Length * 8;
            int latestPixel = count - pixelsNeeded;
            Random rnd = new Random();
            int startbit = rnd.Next(firstPixel,latestPixel);
            Console.WriteLine("Random Number;" + startbit);
            Console.WriteLine("Length;" + count);
            Console.WriteLine("Length less" + latestPixel);
            HideData(startbit);

        }
        public void HideData(int bit)
        {
            List<string> ls = new List<string>();
            //Gets the ascii value of each character from the string
            //Could changet this from an array to make easier?
            //Gets 
            byte[] asciiBytes = Encoding.ASCII.GetBytes(hiddenText);

            foreach (byte b in asciiBytes)
            {
                ls.Add(Convert.ToString(b, 2).PadLeft(8, '0'));

            }

            //List of of the message bits
            List<Char> lsChar = ls.SelectMany(s => s.ToCharArray()).ToList();
            string sevenBitsPixel = "Null", sevenBitsPixel1 = "Null";
            int isevenBitsPixel = 0, isevenBitsPixel1 = 0;
            int startbit = bit;
            int stringLength = lsChar.Count;
            int count = 0, count2 =0;
            for (int i = 0; i < myBitmap.Width; i++)
            {
                for (int j = 0; j < myBitmap.Height; j++)
                {
                    count = count + 1;
                    if(count >= startbit && count <= startbit + stringLength)
                    {
                        count2 = count2 + 1;
                        //CHANGE THESE BACK TO I AND J
                        Color pixel = myBitmap.GetPixel(i, j);
                        Console.WriteLine("Blue Pixel Before  " + Convert.ToString(pixel.B, 2).PadLeft(8, '0'));
                        string bitsPixel = Convert.ToString(pixel.B, 2).PadLeft(8, '0');

                        sevenBitsPixel = bitsPixel.Substring(0, 7);

                                    // now, clear the least significant bit (LSB) from each pixel element
                                    //Therefore Three bits in each pixel spare
                                    //EVERY PIXEL IN THE IMAGE HAS LSB REMOVED COULD IMPROVE THIS ALGORITHM SO IT DEPENDS ON SIZE OF TEXT
                                    //BITMANIPULTION
                        Console.WriteLine("The bit that needs hiding in the pixel =" + lsChar[count2].ToString());

                                    if (lsChar[count2].ToString() == "1")
                                    {
                                        sevenBitsPixel = sevenBitsPixel + "1";

                                        isevenBitsPixel = Convert.ToInt32(sevenBitsPixel, 2);

                                        Console.WriteLine("Blue Pixel After" + sevenBitsPixel);

                                    }
                                    else if (lsChar[count2].ToString() == "0")
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

                                if (count ==  startbit + stringLength)
                                {

                                    {
                                        Color pixel = myBitmap.GetPixel(i, j);
                                        Console.WriteLine("Blue Pixel Before 0 d !!!!" + Convert.ToString(pixel.B, 2).PadLeft(8, '0'));
                                        string bitsPixel1 = Convert.ToString(pixel.B, 2).PadLeft(8, '0');
                                        sevenBitsPixel1 = bitsPixel1.Substring(0, 8);

                                        sevenBitsPixel1 = "00000000";

                                        isevenBitsPixel1 = Convert.ToInt32(sevenBitsPixel1, 2);
                                        Console.WriteLine("Blue Pixel After!!!!!" + sevenBitsPixel1);

                                        myBitmap.SetPixel(i, j, Color.FromArgb(pixel.R, pixel.G, isevenBitsPixel1));
                                    }
                                }
                                count++;
                            }

                        }


                        }
                }
            }

            