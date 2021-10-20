﻿using System;
using CompAndDel.Pipes;
using CompAndDel.Filters;

namespace CompAndDel
{
    class Program
    {
        static void Main(string[] args)
        {
            PictureProvider provider = new PictureProvider();
            IPicture picture = provider.GetPicture(@"beer.jpg");

            FilterNegative filternegative = new FilterNegative();
            FilterGreyscale filtergreyscale = new FilterGreyscale();

            PipeNull pipenull = new PipeNull();
            PipeSerial pipeserial2 = new PipeSerial(filternegative, pipenull);
            PipeSerial pipeserial1 = new PipeSerial(filtergreyscale, pipeserial2);

            IPicture image1 = pipeserial1.Send(picture);
            IPicture image2 = pipeserial2.Send(image1);
            IPicture image3 = pipenull.Send(image2);

            provider.SavePicture(image3, @"bothfilters.jpg");
        }
    }
}
