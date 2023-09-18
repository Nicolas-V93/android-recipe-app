using System;
using System.ComponentModel;

namespace Imi.Project.Mobile.Models
{
    public class Recipe : INotifyPropertyChanged
    {
        private bool _isBookmarked;
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PrepTime { get; set; }
        public int CookTime { get; set; }
        public int Servings { get; set; }
        public string ImgURL { get; set; }
        public string Category { get; set; }
        public string Diet { get; set; }
        public User User { get; set; }
        public bool IsBookmarked
        {
            get { return _isBookmarked; }
            set
            {
                _isBookmarked = value;
                OnPropertyChanged(nameof(IsBookmarked));
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
