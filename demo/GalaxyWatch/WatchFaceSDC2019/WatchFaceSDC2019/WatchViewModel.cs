using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace WatchFaceSDC2019
{
    public class WatchViewModel : INotifyPropertyChanged
    {
        public static readonly string s_defaultHero1ImageSource = "ghost1.png";
        public static readonly string s_altHero1ImageSource = "monster1.png";
        public static readonly string s_defaultHero3ImageSource = "ghost3_noeyeballs.png";
        public static readonly string s_altHero3ImageSource = "monster3.png";

        string _date;
        string _time;
        string _hero1ImageSource = s_defaultHero1ImageSource;
        string _hero3ImageSource = s_defaultHero3ImageSource;
        bool _isHero3EyeVisible = true;
        Color _textColor = Color.White;

        public string Date
        {
            get => _date;
            set
            {
                if (_date == value) return;
                _date = value;
                OnPropertyChanged();
            }
        }

        public string Time
        {
            get => _time;
            set
            {
                if (_time == value) return;
                _time = value;
                OnPropertyChanged();
            }
        }

        public string Hero1ImageSource
        {
            get => _hero1ImageSource;
            set
            {
                if (_hero1ImageSource == value) return;
                _hero1ImageSource = value;
                OnPropertyChanged();
            }
        }

        public string Hero3ImageSource
        {
            get => _hero3ImageSource;
            set
            {
                if (_hero3ImageSource == value) return;
                IsHero3EyeVisible = value.Equals(s_defaultHero3ImageSource) ? true : false;
                _hero3ImageSource = value;
                OnPropertyChanged();
            }
        }

        public bool IsHero3EyeVisible
        {
            get => _isHero3EyeVisible;
            set
            {
                if (_isHero3EyeVisible == value) return;
                _isHero3EyeVisible = value;
                OnPropertyChanged();
            }
        }

        public Color TextColor
        {
            get => _textColor;
            set
            {
                if (_textColor == value) return;
                _textColor = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
