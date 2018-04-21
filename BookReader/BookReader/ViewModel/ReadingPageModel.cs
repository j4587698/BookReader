using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using BookReader.CustomeView;
using BookReader.Picture;
using Xamarin.Forms;
using Xamvvm;

namespace BookReader.ViewModel
{
    public class ReadingPageModel : BasePageModel
    {
        public ReadingPageModel()
        {
            ReadingClick = new BaseCommand((arg) => { IsFullSrceen = !IsFullSrceen; });
            ImageTapped = new BaseCommand(args =>
            {
                var point = (Point)args;
                Console.WriteLine($"x:{point.X} y:{point.Y}");

            });
            ImagePan = new BaseCommand((arg) =>
            {
                if (!( arg is Gesture.PanEventArgs panEvent))
                {
                    return;
                }
                Console.WriteLine($"start:{panEvent.StartPosition}, current:{panEvent.CurrentPosition}, Total:{panEvent.TotalMove}, status:{panEvent.StatusType}");
            });
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
            var page = this.GetCurrentPage() as FullSrceenPage;
            Image = ImageSource.FromStream(() => CreatePicture.Create(page.ScreenWidth, page.ScreenHeight));
        }

        

        public ImageSource Image
        {
            get => GetField<ImageSource>();
            set => SetField(value);
        }

        public bool IsFullSrceen
        {
            get => GetField<bool>();
            set => SetField(value);
        }

        public ICommand ReadingClick
        {
            get => GetField<ICommand>();
            set => SetField(value);
        }

        public ICommand ImageTapped
        {
            get => GetField<ICommand>();
            set => SetField(value);
        }

//        public Command<Point> ImageTapped => new Command<Point>(point =>
//        {
//            Console.WriteLine(point);
//        });

        public ICommand ImagePan
        {
            get => GetField<ICommand>();
            set => SetField(value);
        }
    }
}