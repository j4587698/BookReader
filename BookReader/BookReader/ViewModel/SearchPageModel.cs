using System.Windows.Input;
using Xamvvm;

namespace BookReader.ViewModel
{
    public class SearchPageModel : BasePageModel
    {
        public SearchPageModel()
        {
            BackCommand = new BaseCommand(async (args) => { await this.PopPageAsync(); });
        }

        public ICommand BackCommand
        {
            get => GetField<ICommand>();
            set => SetField(value);
        }

        public ICommand SearchCommand
        {
            get => GetField<ICommand>();
            set => SetField(value);
        }

        public ICommand SearchTextChangedCommand
        {
            get => GetField<ICommand>();
            set => SetField(value);
        }
    }
}