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
            
          
            Image[] slides = s1.VerticalMerge(images);
            Console.WriteLine(slides.Length);
             slides = s1.CreateShow(slides);
           
             s1.GenerateOutput(slides);

        }
    }
}
