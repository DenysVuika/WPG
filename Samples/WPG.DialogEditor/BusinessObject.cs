using System.Windows.Controls.WpfPropertyGrid;
using System.ComponentModel;

namespace DialogEditor
{
    public class BusinessObject : INotifyPropertyChanged
    {
        private string name;
        private string photo;
        private string description;
        [PropertyOrder(0)]
        public string Name
        {
            get { return name; }
            set
            {
                if (name == value) return;
                name = value;
                OnPropertyChanged("Name");
            }
        }
        [PropertyOrder(1)]
        public string Description
        {
            get { return description; }
            //set
            //{
            //    if (description == value) return;
            //    description = value;
            //    OnPropertyChanged("Description");
            //}
        }
        [PropertyOrder(2)]
        public string Descriptionx
        {
            get { return description; }
            set
            {
                if (description == value) return;
                description = value;
                OnPropertyChanged("Description");
            }
        }

        [PropertyEditor(typeof(FilePathPicker))]
        [PropertyOrder(3)]
        public string Photo
        {
            get { return photo; }
            set
            {
                if (photo == value) return;
                photo = value;
                OnPropertyChanged("Photo");
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
