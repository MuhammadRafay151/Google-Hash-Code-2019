using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace A_Example
{
    class SlideShow
    {
        string text;
        string[][] image_data;

        public string[][] Load_Images(string Path)
        {
            using (StreamReader s1 = new StreamReader(Path))
            {
                text = s1.ReadToEnd();
            }
            string[] text2 = text.Split('\n');
            image_data = new string[text2.Length - 1][];
            for (int i = 0; i < text2.Length - 1; i++)
            {
                image_data[i] = text2[i].Split(' ');
            }
            return image_data;

        }
        public Image[] VerticalMerge(string[][] images)
        {
            bool union = false;
            LinkedList<Image> l1 = new LinkedList<Image>();
            List<int> IsVisited = new List<int>();
            for (int i = 1; i < images.Length; i++)
            {
                union = false;
                if (images[i][0] == "H")
                {
                    Image i1 = new Image();

                    i1.AddImage(i);
                    i1.AddTags(images[i]);
                    IsVisited.Add(i);
                    l1.AddLast(i1);
                }
                else
                {
                    if (!IsVisited.Contains(i))
                    {
                        IsVisited.Add(i);
                        for (int j = i + 1; j < images.Length; j++)
                        {
                            if (!IsVisited.Contains(j))
                            {
                                if (images[j][0] != "H")
                                {



                                    for (int x = 2; x < images[j].Length; x++)
                                    {
                                        for (int y = 2; y < images[i].Length; y++)
                                        {
                                            if (images[i][y] == images[j][x])
                                            {
                                                union = true;

                                                IsVisited.Add(j);
                                                Image i1 = new Image();

                                                i1.AddImage(i);
                                                i1.AddImage(j);
                                                i1.AddTags(images[i], images[j]);
                                                l1.AddLast(i1);

                                                break;
                                            }


                                        }
                                        if (union)
                                        {
                                            break;
                                        }

                                    }
                                    if (union)
                                    {
                                        break;
                                    }

                                }
                            }
                            if (union)
                            {
                                break;
                            }


                        }
                    }

                }

            }
            return l1.ToArray();
        }
        public Image[] CreateShow(Image[] data)
        {
            List<int> Isvisited = new List<int>();
            LinkedList<Image> l1 = new LinkedList<Image>();
            LinkedList<Image> FinalIMage = new LinkedList<Image>();
          //  bool check = false;
          
            for (int i = 0; i < data.Length; i++)
            {
                
                if (Isvisited.Contains(i))
                    continue;
                string[] temp1 = data[i].GetTags();
                for (int j = i + 1; j < data.Length; j++)
                {
                    string[] temp2 = data[j].GetTags();
                    if (!Isvisited.Contains(j))
                    {
                        for (int k = 0; k < temp1.Length; k++)
                        {

                            for (int x = 0; x < temp2.Length; x++)
                            {
                                if (temp1[k] == temp2[x])
                                {
                                    data[j].match++;
                                  
                                    //check = true;
                                    break;
                                }
                            }
                           
                        }
                    }
                    if(!Isvisited.Contains(j)&& data[j].match>0)
                    {
                      //  Console.WriteLine("j>>>>"+j);
                        l1.AddLast(data[j]);
                        Isvisited.Add(j);
                    }


                }
                //sorting similar pics in descending order
                if (!Isvisited.Contains(i))
                {
                    Isvisited.Add(i);
                    Image[] sortedimages = l1.ToArray();
                    Console.WriteLine(l1.Count+">>>"+i);
                    QuickSort_Image(sortedimages, 0, l1.Count - 1);
                    FinalIMage.AddLast(data[i]);
                    foreach (Image x in sortedimages)
                    {
                        FinalIMage.AddLast(x);
                    }
                    l1 = new LinkedList<Image>();
                }
               
            }
            return FinalIMage.ToArray();
        }
        public void GenerateOutput(Image[] img)
        {
            using (StreamWriter s1 = new StreamWriter(Directory.GetCurrentDirectory() + "\\output.txt"))
            {
                s1.Write(img.Length + "\n");

                for (int i = 0; i < img.Length; i++)
                {
                    int[] img1 = img[i].GetImage();
                    for (int j = 0; j < img1.Length; j++)

                    {
                        s1.Write(img1[j] - 1 );
                        if(j!=img1.Length-1)
                        {
                            s1.Write(" ");
                        }


                    }
                    s1.Write("\n");

                }

            }
        }
        public void QuickSort_Image(Image[] img,int start,int end)
        {
            if(start<end)
            {
                int pinindex = PinIndex(img, start, end);
                QuickSort_Image(img, start, pinindex - 1);
                QuickSort_Image(img, pinindex + 1, end);
            }
        }
        private int PinIndex(Image[]img ,int start,int end)
        {
            int pivot = end;
            int pinindex = start;
            for(int i=start;i<end;i++)
            {
                if(img[i].match<=img[pivot].match)
                {
                    Image Temp = img[i];
                    img[i] = img[pinindex];
                    img[pinindex] = Temp;
                    pinindex++;
                }
            }
            Image Temp2 = img[pinindex];
            img[pinindex] = img[pivot];
            img[pivot] = Temp2;
            return pinindex;


        }

    }
    class Image
    {
        List<int> l1 = new List<int>();
        List<string> Tags = new List<string>();
        public int match;
        public void AddImage(int data)
        {
            l1.Add(data);

        }
        public void AddTags(string[] img1, string[] img2)
        {


            for (int j = 2; j < img1.Length; j++)
            {
                Tags.Add(img1[j]);
            }
            for (int k = 2; k < img2.Length; k++)
            {
                if (!Tags.Contains(img2[k]))
                    Tags.Add(img2[k]);
            }



        }
        public void AddTags(string[] img1)
        {

            for (int j = 2; j < img1.Length; j++)
            {
                Tags.Add(img1[j]);
            }




        }
        public string[] GetTags()
        {
            return Tags.ToArray();
        }
        public int[] GetImage()
        {
            return l1.ToArray();
        }
    }
}
