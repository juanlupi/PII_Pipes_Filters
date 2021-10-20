using System;
using CompAndDel.Pipes;
using CompAndDel.Filters;
using TwitterUCU;

namespace CompAndDel
{
    class Program
    {
        static void Main(string[] args)
        {
            var twitter = new TwitterImage();

            PictureProvider provider = new PictureProvider();
            IPicture picture = provider.GetPicture(@"luke.jpg");

            FilterNegative filternegative = new FilterNegative();
            FilterGreyscale filtergreyscale = new FilterGreyscale();

            PipeNull pipenull = new PipeNull();
            PipeSerial pipeserial2 = new PipeSerial(filternegative, pipenull);
            PipeSerial pipeserial1 = new PipeSerial(filtergreyscale, pipeserial2);

            IPicture image1 = pipeserial1.Send(picture);
            IPicture image2 = pipeserial2.Send(image1);
            IPicture image3 = pipenull.Send(image2);

            provider.SavePicture(image1, @"greyandnegativefilters.jpg");
            provider.SavePicture(image3, @"greyfilter.jpg");

            Console.WriteLine(twitter.PublishToTwitter("Grey Luke", @"greyfilter.jpg"));
            Console.WriteLine(twitter.PublishToTwitter("Grey and negative Luke", @"greyandnegativefilters.jpg"));

        }
    }
}
