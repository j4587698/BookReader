using System.Threading.Tasks;
using System.Windows.Input;
using Xamvvm;

namespace BookReader.ViewModel
{
    public class ReadingPageModel : BasePageModel
    {
        public ReadingPageModel()
        {
            ReadingClick = new BaseCommand((arg) => { IsFullSrceen = !IsFullSrceen; });
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
    }
}