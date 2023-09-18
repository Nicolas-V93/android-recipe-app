using System.ComponentModel;

namespace Imi.Project.Mobile.Models
{
    public class Instruction : INotifyPropertyChanged
    {
        private int _stepNumber;
        private string _description;

        public int StepNumber
        {
            get { return _stepNumber; }
            set
            {
                _stepNumber = value;
                OnPropertyChanged(nameof(StepNumber));
            }
        }
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
