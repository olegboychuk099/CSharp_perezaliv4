using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Csharp_laba4.Tools.Exceptions;

namespace Csharp_laba4.Model
{
    [Serializable]
    internal class Person : INotifyPropertyChanged
    {
        internal const string filename = "UsersList.dat";

        private DateTime _birthDay;
        private string _name, _surname;
        private string _email;
        private int _age;
        private bool _isAdult;
        private bool _isBirthday;
        private string _chineseSign;
        private string _sunSign;
        private readonly string[] _chineseSigns = { "Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Goat", "Monkey", "Rooster", "Dog", "Pig" };

        public string BirthDateString
        {
            get { return _birthDay.ToShortDateString(); }
            set
            {
                string[] dmy = value.Split('/');
                BirthDate = new DateTime(Int32.Parse(dmy[2]), Int32.Parse(dmy[1]), Int32.Parse(dmy[0]));
            }
        }

        public DateTime BirthDate
        {
            get { return _birthDay; }
            set
            {
                int d = (DateTime.Today - value).Days;
                if (d < 0)
                    throw new FutureBirthdayException($"Date isn`t correct (not yet come): {value.ToShortDateString()}");
                int y = d / 365;
                if (y > 135)
                    throw new PastBirthdayException($"Date isn`t correct (long time ago): {value.ToShortDateString()}");
                _birthDay = value;
                Age = CalculateAge();
                ChineseSign = CalculateChineseSign();
                SunSign = CalculateSunSign();
                IsAdult = (Age = CalculateAge()) > 18;
                IsBirthday = BirthDate.DayOfYear == DateTime.Today.DayOfYear;
                ChineseSign = CalculateChineseSign();
                SunSign = CalculateSunSign();
                OnPropertyChanged();
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Surname
        {
            get
            {
                return _surname;
            }
            set
            {
                _surname = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public bool IsAdult
        {
            get { return _isAdult; }
            private set
            {
                _isAdult = value;
                OnPropertyChanged();
            }
        }
        public bool IsBirthday
        {
            get { return _isBirthday; }
            private set
            {
                _isBirthday = value;
                OnPropertyChanged();
            }
        }
        public int Age
        {
            get { return _age; }
            private set
            {
                _age = value;
                OnPropertyChanged();
            }
        }
        public string ChineseSign
        {
            get { return _chineseSign; }
            private set
            {
                _chineseSign = value;
                OnPropertyChanged();
            }
        }
        public string SunSign
        {
            get { return _sunSign; }
            private set
            {
                _sunSign = value;
                OnPropertyChanged();
            }
        }

        internal Person(string name, string surname, string email, DateTime birthDay)
        {
            ValidName(name, surname);
            ValidBirthday(birthDay);
            ValidEmail(email);

            _name = name;
            _surname = surname;
            _email = email;
            BirthDate = birthDay;
        }
        internal Person(string name, string surname, string email, int age) : this(name, surname, email, DateTime.Today)
        {
        }
        internal Person(string name, string surname, DateTime birthDay, int age) : this(name, surname, "null", birthDay)
        {
        }

        private int CalculateAge()
        {
            DateTime today = DateTime.Today;
            return today.Year - BirthDate.Year - (today.DayOfYear < BirthDate.DayOfYear ? 1 : 0);
        }

        private string CalculateChineseSign()
        {
            var date = _birthDay.Year;
            return _chineseSigns[(date - 4) % 12];
        }

        private string CalculateSunSign()
        {
            int moth = BirthDate.Month;
            int day = BirthDate.Day;
            switch (moth)
            {
                case 1:
                    return day <= 20 ? "Capricorn" : "Aquarius";
                case 2:
                    return day <= 19 ? "Aquarius" : "Pisces";
                case 3:
                    return day <= 20 ? "Pisces" : "Aries";
                case 4:
                    return day <= 20 ? "Aries" : "Taurus";
                case 5:
                    return day <= 21 ? "Taurus" : "Gemini";
                case 6:
                    return day <= 22 ? "Gemini" : "Cancer";
                case 7:
                    return day <= 22 ? "Cancer" : "Leo";
                case 8:
                    return day <= 23 ? "Leo" : "Virgo";
                case 9:
                    return day <= 23 ? "Virgo" : "Libra";
                case 10:
                    return day <= 23 ? "Libra" : "Scorpio";
                case 11:
                    return day <= 22 ? "Scorpio" : "Sagittarius";
                case 12:
                    return day <= 21 ? "Sagittarius" : "Capricorn";
                default:
                    throw new ArgumentException("Inappropriate format of month !");
            }
        }

        private void ValidName(string name, string surname)
        {
            if (surname.Length < 3 || name.Length < 2 || !Regex.IsMatch(name, @"^[a-zA-Z]+$") || !Regex.IsMatch(surname, @"^[a-zA-Z]+$"))
                throw new NameException($"This name isn`t correct {name} {surname}");
        }

        private void ValidBirthday(DateTime birthday)
        {
            if (DateTime.Today.Year - birthday.Year >= 135)
                throw new PastBirthdayException($"Date isn`t correct (long time ago): {birthday.ToShortDateString()}");
            if (birthday > DateTime.Today)
                throw new FutureBirthdayException($"Date isn`t correct (not yet come): {birthday.ToShortDateString()}");
        }

        private void ValidEmail(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (!match.Success)
                throw new EmailException($"Email isn`t correct: {email}");
        }

        #region Implementation

        [field: NonSerialized()]
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}