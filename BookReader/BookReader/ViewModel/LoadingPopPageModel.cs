using Xamvvm;

namespace BookReader.ViewModel
{
    public class LoadingPopPageModel : BasePageModel
    {
        public void Init(string loadingStr)
        {
            LoadingStr = loadingStr;
        }

        public string LoadingStr
        {
            get => GetField<string>();
            set => SetField(value);
        }
    }
}