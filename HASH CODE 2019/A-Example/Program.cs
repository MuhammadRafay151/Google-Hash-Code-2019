using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            SlideShow s1 = new SlideShow();
            
            string[][] images;
            images = s1.Load_Images(@"C:\Users\HP\Downloads\b_lovely_landscapes.txt");
            //for (int i = 0; i < images.Length; i++)
            //{
            //    Console.Write(i+">>");
            //    foreach (string j in images[i])
            //    {
            //        Console.Write(j + " ");
            //    }
            //    Console.WriteLine(); 
            //}
            //Console.WriteLine(">>>>>>>>>>>>>>>");
          
            Image[] slides = s1.VerticalMerge(images);
            Console.WriteLine(slides.Length);
             slides = s1.CreateShow(slides);
            //Console.WriteLine(22);
            //for(int i=0;i<slides.Length;i++)
            //{
            //    foreach(int j in slides[i].GetImage())
            //    {
            //        Console.Write(j-1+" ");
            //    }
            //    Console.WriteLine();
            //}
             s1.GenerateOutput(slides);

        }
    }
}
